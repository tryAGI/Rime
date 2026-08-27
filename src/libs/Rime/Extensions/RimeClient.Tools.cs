using Microsoft.Extensions.AI;

namespace Rime;

/// <summary>
/// Extensions for using <see cref="RimeClient"/> as MEAI tools with any IChatClient.
/// </summary>
public static class RimeToolExtensions
{
    /// <summary>
    /// Creates an <see cref="AIFunction"/> that synthesizes speech with Rime AI,
    /// suitable for use as a tool with any IChatClient.
    /// Returns a summary of the synthesized audio (format, byte count, voice, model).
    /// </summary>
    /// <param name="client">The Rime client to use.</param>
    /// <param name="defaultSpeaker">
    /// Default voice name (e.g. "astra", "cove"). Can be overridden by the LLM's function call.
    /// </param>
    /// <param name="defaultModel">
    /// Default TTS model ("coda", "mistv3", or "mistv2"). Defaults to "coda" for voice quality.
    /// </param>
    /// <returns>An AIFunction suitable for ChatOptions.Tools.</returns>
    [CLSCompliant(false)]
    public static AIFunction AsTextToSpeechTool(
        this RimeClient client,
        string defaultSpeaker = "astra",
        string defaultModel = "coda")
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (string text, string? speaker, string? model, string? lang, CancellationToken cancellationToken) =>
            {
                var requestedModel = model ?? defaultModel;
                var modelId = requestedModel.ToUpperInvariant() switch
                {
                    "CODA" => TtsRequestModelId.Coda,
                    "MISTV3" => TtsRequestModelId.Mistv3,
                    "MISTV2" => TtsRequestModelId.Mistv2,
                    "ARCANA" => TtsRequestModelId.Arcana,
                    _ => throw new ArgumentException(
                        $"Unsupported Rime TTS model '{requestedModel}'. Use 'coda', 'mistv3', or 'mistv2'.",
                        nameof(model)),
                };

                var audio = await client.TextToSpeech.CreateTtsAsync(
                    speaker: speaker ?? defaultSpeaker,
                    text: text,
                    modelId: modelId,
                    lang: lang,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return $"Synthesized {audio.Length} bytes of audio using speaker '{speaker ?? defaultSpeaker}' on model '{modelId}'.";
            },
            name: "RimeTextToSpeech",
            description: "Synthesizes natural-sounding speech from text using Rime AI. " +
                         "Supports flagship voice quality via Coda, ultra-low-latency streaming via Mist v3, " +
                         "and hundreds of voices. Returns a summary of the synthesized audio bytes.");
    }

    /// <summary>
    /// Creates an <see cref="AIFunction"/> that lists Rime AI voice names grouped by model and language.
    /// </summary>
    /// <param name="client">The Rime client to use.</param>
    /// <returns>An AIFunction suitable for ChatOptions.Tools.</returns>
    [CLSCompliant(false)]
    public static AIFunction AsListVoicesTool(
        this RimeClient client)
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (string? model, string? lang, CancellationToken cancellationToken) =>
            {
                var voices = await client.Voices.ListVoicesAsync(
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return FormatVoiceCatalog(voices, model, lang);
            },
            name: "RimeListVoices",
            description: "Lists available Rime AI voice names grouped by model (coda / mistv3 / mistv2) " +
                         "and ISO 639-2 language code. Optionally filter by model and/or language.");
    }

    /// <summary>
    /// Creates an <see cref="AIFunction"/> that lists Rime AI voice details (speaker, gender, age,
    /// country, dialect, demographic, genre, styles, description, language, model).
    /// </summary>
    /// <param name="client">The Rime client to use.</param>
    /// <returns>An AIFunction suitable for ChatOptions.Tools.</returns>
    [CLSCompliant(false)]
    public static AIFunction AsListVoiceDetailsTool(
        this RimeClient client)
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (string? gender, string? model, string? language, CancellationToken cancellationToken) =>
            {
                var details = await client.Voices.ListVoiceDetailsAsync(
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return FormatVoiceDetails(details, gender, model, language);
            },
            name: "RimeListVoiceDetails",
            description: "Lists detailed Rime AI voice metadata (speaker name, gender, age, country, " +
                         "dialect, demographic, genre, styles, description, language, model, flagship). " +
                         "Optionally filter by gender, " +
                         "model, or language.");
    }

    private static string FormatVoiceCatalog(
        IDictionary<string, Dictionary<string, IList<string>>> voices,
        string? modelFilter,
        string? langFilter)
    {
        var parts = new List<string>();

        foreach (var (modelId, langs) in voices)
        {
            if (!string.IsNullOrWhiteSpace(modelFilter) &&
                !modelId.Equals(modelFilter, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            foreach (var (lang, names) in langs)
            {
                if (!string.IsNullOrWhiteSpace(langFilter) &&
                    !lang.Equals(langFilter, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                parts.Add($"{modelId}/{lang} ({names.Count}): {string.Join(", ", names)}");
            }
        }

        return parts.Count == 0
            ? "No voices matched the filter."
            : string.Join("\n", parts);
    }

    private static string FormatVoiceDetails(
        IList<VoiceDetail> details,
        string? genderFilter,
        string? modelFilter,
        string? languageFilter)
    {
        var filtered = details.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(genderFilter))
        {
            filtered = filtered.Where(v =>
                v.Gender?.Equals(genderFilter, StringComparison.OrdinalIgnoreCase) == true);
        }

        if (!string.IsNullOrWhiteSpace(modelFilter))
        {
            filtered = filtered.Where(v =>
                v.ModelId?.Equals(modelFilter, StringComparison.OrdinalIgnoreCase) == true);
        }

        if (!string.IsNullOrWhiteSpace(languageFilter))
        {
            filtered = filtered.Where(v =>
                v.Language?.Equals(languageFilter, StringComparison.OrdinalIgnoreCase) == true ||
                v.Lang?.Equals(languageFilter, StringComparison.OrdinalIgnoreCase) == true);
        }

        var list = filtered.ToList();
        var parts = new List<string> { $"Voice details ({list.Count}):" };

        foreach (var v in list)
        {
            var entry = $"- {v.Speaker}";
            if (!string.IsNullOrEmpty(v.ModelId)) entry += $" [{v.ModelId}]";
            if (!string.IsNullOrEmpty(v.Gender)) entry += $" {v.Gender}";
            if (!string.IsNullOrEmpty(v.Age)) entry += $", {v.Age}";
            if (!string.IsNullOrEmpty(v.Country)) entry += $", {v.Country}";
            if (!string.IsNullOrEmpty(v.Dialect)) entry += $" ({v.Dialect})";
            if (!string.IsNullOrEmpty(v.Language)) entry += $" — {v.Language}";
            if (v.Genre is { Count: > 0 }) entry += $" — genres: {string.Join(", ", v.Genre)}";
            if (v.Styles is { Count: > 0 }) entry += $" — styles: {string.Join(", ", v.Styles)}";
            if (v.Flagship == true) entry += " — flagship";
            if (!string.IsNullOrEmpty(v.Description)) entry += $" — {v.Description}";
            parts.Add(entry);
        }

        return string.Join("\n", parts);
    }
}

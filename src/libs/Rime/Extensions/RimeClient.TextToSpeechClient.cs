#nullable enable
#pragma warning disable MEAI001

using System.Runtime.CompilerServices;
using Microsoft.Extensions.AI;

namespace Rime;

public sealed partial class RimeClient : Microsoft.Extensions.AI.ITextToSpeechClient
{
    private const string DefaultTextToSpeechModel = "mistv3";
    private const string DefaultSpeaker = "cove";
    private TextToSpeechClientMetadata? _textToSpeechMetadata;

    object? Microsoft.Extensions.AI.ITextToSpeechClient.GetService(Type serviceType, object? serviceKey)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        return serviceKey is not null ? null :
            serviceType == typeof(TextToSpeechClientMetadata) ? (_textToSpeechMetadata ??= new("rime", new Uri(DefaultBaseUrl), DefaultTextToSpeechModel)) :
            serviceType.IsInstanceOfType(this) ? this :
            null;
    }

    async Task<TextToSpeechResponse> Microsoft.Extensions.AI.ITextToSpeechClient.GetAudioAsync(
        string text,
        TextToSpeechOptions? options,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        var request = CreateTtsRequest(text, options);
        var resolved = ResolveRequestOptions(options);
        using var acceptScope = TextToSpeechClient.UseAcceptHeader(resolved.Accept);
        var response = await TextToSpeech.CreateTtsAsResponseAsync(
            request,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        var modelId = request.ModelId?.ToValueString() ?? DefaultTextToSpeechModel;
        return new TextToSpeechResponse([
            new DataContent(response.Body, resolved.MediaType),
        ])
        {
            ModelId = modelId,
            RawRepresentation = request,
            AdditionalProperties = CreateResponseProperties(request, resolved),
        };
    }

    async IAsyncEnumerable<TextToSpeechResponseUpdate> Microsoft.Extensions.AI.ITextToSpeechClient.GetStreamingAudioAsync(
        string text,
        TextToSpeechOptions? options,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        var request = CreateTtsRequest(text, options);
        var resolved = ResolveRequestOptions(options);
        var modelId = request.ModelId?.ToValueString() ?? DefaultTextToSpeechModel;
        var responseId = Guid.NewGuid().ToString("N");

        yield return new TextToSpeechResponseUpdate
        {
            Kind = TextToSpeechResponseUpdateKind.SessionOpen,
            ResponseId = responseId,
            ModelId = modelId,
            RawRepresentation = request,
            AdditionalProperties = CreateResponseProperties(request, resolved),
        };

        using var acceptScope = TextToSpeechClient.UseAcceptHeader(resolved.Accept);
        using var stream = await TextToSpeech.CreateTtsAsStreamAsync(
            request,
            cancellationToken: cancellationToken).ConfigureAwait(false);
        var buffer = new byte[16 * 1024];
        while (true)
        {
            var bytesRead = await stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
            if (bytesRead <= 0)
            {
                break;
            }

            yield return new TextToSpeechResponseUpdate([
                new DataContent(buffer.AsSpan(0, bytesRead).ToArray(), resolved.MediaType),
            ])
            {
                Kind = TextToSpeechResponseUpdateKind.AudioUpdating,
                ResponseId = responseId,
                ModelId = modelId,
                AdditionalProperties = CreateResponseProperties(request, resolved),
            };
        }

        yield return new TextToSpeechResponseUpdate
        {
            Kind = TextToSpeechResponseUpdateKind.AudioUpdated,
            ResponseId = responseId,
            ModelId = modelId,
            AdditionalProperties = CreateResponseProperties(request, resolved),
        };

        yield return new TextToSpeechResponseUpdate
        {
            Kind = TextToSpeechResponseUpdateKind.SessionClose,
            ResponseId = responseId,
            ModelId = modelId,
        };
    }

    private TtsRequest CreateTtsRequest(string text, TextToSpeechOptions? options)
    {
        var request = options?.RawRepresentationFactory?.Invoke(this) as TtsRequest
            ?? new TtsRequest
            {
                Speaker = options?.VoiceId is { Length: > 0 } voiceId ? voiceId : DefaultSpeaker,
                Text = text,
            };

        if (string.IsNullOrWhiteSpace(request.Text))
        {
            request.Text = text;
        }

        if (string.IsNullOrWhiteSpace(request.Speaker))
        {
            request.Speaker = options?.VoiceId is { Length: > 0 } fallbackVoiceId ? fallbackVoiceId : DefaultSpeaker;
        }

        request.ModelId ??= ResolveModel(options?.ModelId);
        request.Lang ??= NormalizeLanguage(options?.Language);
        request.SamplingRate ??= options.GetInt(RimeTextToSpeechPropertyNames.SamplingRate);
        request.SpeedAlpha ??= options.GetFloat(RimeTextToSpeechPropertyNames.SpeedAlpha);
        request.TimeScaleFactor ??= options.GetFloat(RimeTextToSpeechPropertyNames.TimeScaleFactor);

        if (options?.Speed is { } speed && speed > 0)
        {
            if (request.ModelId == TtsRequestModelId.Mistv2)
            {
                request.SpeedAlpha ??= 1f / speed;
            }
            else
            {
                request.TimeScaleFactor ??= speed;
            }
        }

        request.PauseBetweenBrackets ??= options.GetBool(RimeTextToSpeechPropertyNames.PauseBetweenBrackets);
        request.PhonemizeBetweenBrackets ??= options.GetBool(RimeTextToSpeechPropertyNames.PhonemizeBetweenBrackets);
        request.InlineSpeedAlpha ??= options.GetString(RimeTextToSpeechPropertyNames.InlineSpeedAlpha);
        request.NoTextNormalization ??= options.GetBool(RimeTextToSpeechPropertyNames.NoTextNormalization);
        request.SaveOovs ??= options.GetBool(RimeTextToSpeechPropertyNames.SaveOovs);

        return request;
    }

    private static TtsRequestModelId ResolveModel(string? modelId)
    {
        if (modelId is not { Length: > 0 })
        {
            return TtsRequestModelId.Mistv3;
        }

        return TtsRequestModelIdExtensions.ToEnum(modelId)
            ?? throw new NotSupportedException($"Unsupported Rime TTS model '{modelId}'. Use 'mistv3', 'mistv2', or 'arcana'.");
    }

    private static ResolvedRimeTextToSpeechOptions ResolveRequestOptions(TextToSpeechOptions? options)
    {
        var (accept, mediaType) = ResolveAudioFormat(
            options.GetString(RimeTextToSpeechPropertyNames.Accept) ?? options?.AudioFormat);

        return new ResolvedRimeTextToSpeechOptions(accept, mediaType);
    }

    private static (string Accept, string MediaType) ResolveAudioFormat(string? format)
    {
        if (format is not { Length: > 0 })
        {
            return ("audio/mp3", "audio/mpeg");
        }

        if (string.Equals(format, "audio/mpeg", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "audio/mp3", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "mp3", StringComparison.OrdinalIgnoreCase))
        {
            return ("audio/mp3", "audio/mpeg");
        }

        if (string.Equals(format, "audio/wav", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "audio/wave", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "wav", StringComparison.OrdinalIgnoreCase))
        {
            return ("audio/wav", "audio/wav");
        }

        if (string.Equals(format, "audio/pcm", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "pcm", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "pcm_s16le", StringComparison.OrdinalIgnoreCase))
        {
            return ("audio/pcm", "audio/pcm;codec=s16le");
        }

        if (string.Equals(format, "audio/x-mulaw", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "audio/basic", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "mulaw", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "mu-law", StringComparison.OrdinalIgnoreCase))
        {
            return ("audio/x-mulaw", "audio/basic");
        }

        if (string.Equals(format, "audio/ogg", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "audio/ogg;codecs=opus", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "ogg", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "opus", StringComparison.OrdinalIgnoreCase))
        {
            return ("audio/ogg;codecs=opus", "audio/ogg;codecs=opus");
        }

        if (string.Equals(format, "audio/webm", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "audio/webm;codecs=opus", StringComparison.OrdinalIgnoreCase)
            || string.Equals(format, "webm", StringComparison.OrdinalIgnoreCase))
        {
            return ("audio/webm;codecs=opus", "audio/webm;codecs=opus");
        }

        return (format, format);
    }

    private static string NormalizeLanguage(string? language)
    {
        if (language is not { Length: > 0 })
        {
            return "eng";
        }

        var normalized = language.Split('-', '_')[0];
        return
            string.Equals(normalized, "en", StringComparison.OrdinalIgnoreCase) ? "eng" :
            string.Equals(normalized, "es", StringComparison.OrdinalIgnoreCase) ? "spa" :
            string.Equals(normalized, "fr", StringComparison.OrdinalIgnoreCase) ? "fra" :
            string.Equals(normalized, "de", StringComparison.OrdinalIgnoreCase) ? "deu" :
            string.Equals(normalized, "it", StringComparison.OrdinalIgnoreCase) ? "ita" :
            string.Equals(normalized, "pt", StringComparison.OrdinalIgnoreCase) ? "por" :
            normalized;
    }

    private static AdditionalPropertiesDictionary CreateResponseProperties(
        TtsRequest request,
        ResolvedRimeTextToSpeechOptions resolved)
    {
        AdditionalPropertiesDictionary properties = new()
        {
            ["speaker"] = request.Speaker,
            ["model_id"] = request.ModelId?.ToValueString() ?? DefaultTextToSpeechModel,
            [RimeTextToSpeechPropertyNames.Accept] = resolved.Accept,
            ["media_type"] = resolved.MediaType,
        };

        if (request.Lang is { Length: > 0 })
        {
            properties["language"] = request.Lang;
        }

        return properties;
    }

    private sealed record ResolvedRimeTextToSpeechOptions(
        string Accept,
        string MediaType);
}

public sealed partial class TextToSpeechClient
{
    private static readonly AsyncLocal<string?> s_acceptHeader = new();

    internal static IDisposable UseAcceptHeader(string acceptHeader)
    {
        var previous = s_acceptHeader.Value;
        s_acceptHeader.Value = acceptHeader;
        return new AcceptHeaderScope(previous);
    }

    partial void PrepareCreateTtsRequest(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        TtsRequest request)
    {
        _ = ReadResponseAsString;

        if (s_acceptHeader.Value is { Length: > 0 } accept)
        {
            httpRequestMessage.Headers.Accept.Clear();
            httpRequestMessage.Headers.Accept.ParseAdd(accept);
        }
    }

    private sealed class AcceptHeaderScope(string? previous) : IDisposable
    {
        public void Dispose()
        {
            s_acceptHeader.Value = previous;
        }
    }
}

internal static class RimeTextToSpeechOptionsExtensions
{
    public static bool? GetBool(this TextToSpeechOptions? options, string key)
    {
        if (options?.AdditionalProperties is not { } properties || !properties.TryGetValue(key, out var value))
        {
            return null;
        }

        return value switch
        {
            bool boolValue => boolValue,
            string text when bool.TryParse(text, out var parsed) => parsed,
            _ => null,
        };
    }

    public static float? GetFloat(this TextToSpeechOptions? options, string key)
    {
        if (options?.AdditionalProperties is not { } properties || !properties.TryGetValue(key, out var value))
        {
            return null;
        }

        return value switch
        {
            float floatValue => floatValue,
            double doubleValue => (float)doubleValue,
            decimal decimalValue => (float)decimalValue,
            int intValue => intValue,
            long longValue => longValue,
            string text when float.TryParse(
                text,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out var parsed) => parsed,
            _ => null,
        };
    }

    public static int? GetInt(this TextToSpeechOptions? options, string key)
    {
        if (options?.AdditionalProperties is not { } properties || !properties.TryGetValue(key, out var value))
        {
            return null;
        }

        return value switch
        {
            int intValue => intValue,
            long longValue when longValue is >= int.MinValue and <= int.MaxValue => (int)longValue,
            string text when int.TryParse(
                text,
                System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture,
                out var parsed) => parsed,
            _ => null,
        };
    }

    public static string? GetString(this TextToSpeechOptions? options, string key)
    {
        return options?.AdditionalProperties is { } properties
            && properties.TryGetValue(key, out var value)
            && value is string text
            && text.Length > 0
                ? text
                : null;
    }
}

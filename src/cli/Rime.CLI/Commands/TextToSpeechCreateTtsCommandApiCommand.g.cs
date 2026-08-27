#nullable enable
#pragma warning disable CS0618

using System.CommandLine;

namespace Rime.CLI.Commands;

internal static partial class TextToSpeechCreateTtsCommandApiCommand
{
    private static Option<string> Speaker { get; } = new(
        name: @"--speaker")
    {
        Description = @"The voice used to synthesize the text. Must be one of the voices
returned by `/data/voices/all-v2.json` for the selected `modelId`.
",
        Required = true,
    };

    private static Option<string> Text { get; } = new(
        name: @"--text")
    {
        Description = @"The text to speak. The cloud API accepts up to 1,000 characters per request.
",
        Required = true,
    };

    private static Option<global::Rime.TtsRequestModelId?> ModelId { get; } = new(
        name: @"--model-id")
    {
        Description = @"The TTS model to use. `coda` is the flagship model for new applications,
`mistv3` prioritizes lowest time to first audio, and `mistv2` retains inline
pronunciation control. `arcana` is a retired compatibility alias now served
by Coda; use `coda` for new requests.
",
    };

    private static Option<string?> Lang { get; } = new(
        name: @"--lang")
    {
        Description = @"Language identifier for the selected speaker. Must match the speaker's
language. Both two-letter and three-letter ISO codes are accepted. Coda
supports English, Arabic, French, German, Hindi, Italian, Japanese,
Portuguese, and Spanish; Mist v2/v3 support English, French, German,
and Spanish.
",
    };

    private static Option<int?> SamplingRate { get; } = new(
        name: @"--sampling-rate")
    {
        Description = @"Output sample rate in Hz. Mist v2 accepts 4000-44100. Coda and Mist v3
default to 24000; the public API's common range is 8000-96000, with values
above 24000 produced by upsampling.
",
    };

    private static Option<float?> SpeedAlpha { get; } = new(
        name: @"--speed-alpha")
    {
        Description = @"Adjusts the speed of speech for Mist v2. Values below 1.0 speed up, values
above 1.0 slow down.
",
    };

    private static Option<float?> TimeScaleFactor { get; } = new(
        name: @"--time-scale-factor")
    {
        Description = @"Adjusts the speed of speech for Coda and Mist v3. Values above 1.0 slow
down the audio and values below 1.0 speed it up. Values outside 0.4-2.5
are clamped by the API.
",
    };

    private static Option<bool?> PauseBetweenBrackets { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--pause-between-brackets",
        description: @"Adds pauses between words enclosed in angle brackets, with the pause
duration specified in milliseconds (e.g. `Hello <500> world`).
");

    private static Option<bool?> PhonemizeBetweenBrackets { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--phonemize-between-brackets",
        description: @"Enables custom pronunciation via phonemes specified inside curly brackets.
Supported by Mist v2; Coda and Mist v3 accept and ignore this option.
");

    private static Option<string?> InlineSpeedAlpha { get; } = new(
        name: @"--inline-speed-alpha")
    {
        Description = @"Comma-separated per-word speed multipliers for words inside square brackets.
Values below 1.0 speed up speech and values above 1.0 slow it down.
",
    };

    private static Option<bool?> NoTextNormalization { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--no-text-normalization",
        description: @"Skip text normalization to reduce latency. Available only on Mist v2.
");

    private static Option<bool?> SaveOovs { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--save-oovs",
        description: @"Legacy Mist v2 option retained for backward compatibility. The Speech QA
dashboard it reported to has retired, and Rime no longer documents this
option for new integrations.
");
      private static Option<string?> Input { get; } = new(@"--input")
      {
          Description = "Load request JSON from a file path, '-' for stdin, or an inline JSON object/array string.",
      };

      private static Option<string?> RequestJson { get; } = new(@"--request-json")
      {
          Description = "Request body as JSON.",
          Hidden = true,
      };

      private static Option<string?> RequestFile { get; } = new(@"--request-file")
      {
          Description = "Path to a JSON request file, or '-' for stdin.",
          Hidden = true,
      };

    public static Command Create()
    {
        var command = new Command(@"create-tts", @"Generate speech (Coda / Mist v3 / Mist v2)
Synthesize speech from text using Rime's TTS models (`coda`, `mistv3`, or `mistv2`).
The retired `arcana` identifier remains accepted by Rime as a compatibility alias
that is served by Coda, but new applications should send `coda`.
Audio bytes are returned in the format indicated by the `Accept` header.

Supported `Accept` values: `audio/webm;codecs=opus`, `audio/ogg;codecs=opus`,
`audio/mpeg`, `audio/wav`, `audio/L16`, `audio/PCMU`. The aliases `audio/mp3`,
`audio/pcm`, and `audio/x-mulaw` are deprecated but remain accepted by Rime.
");
                        command.Options.Add(Speaker);
                        command.Options.Add(Text);
                        command.Options.Add(ModelId);
                        command.Options.Add(Lang);
                        command.Options.Add(SamplingRate);
                        command.Options.Add(SpeedAlpha);
                        command.Options.Add(TimeScaleFactor);
                        command.Options.Add(PauseBetweenBrackets);
                        command.Options.Add(PhonemizeBetweenBrackets);
                        command.Options.Add(InlineSpeedAlpha);
                        command.Options.Add(NoTextNormalization);
                        command.Options.Add(SaveOovs);
          command.Options.Add(Input);
          command.Options.Add(RequestJson);
          command.Options.Add(RequestFile);
          command.Validators.Add(result =>
          {
              var hasInput = result.GetResult(Input) is not null;
              var hasRequestJson = result.GetResult(RequestJson) is not null;
              var hasRequestFile = result.GetResult(RequestFile) is not null;
              var specifiedCount = (hasInput ? 1 : 0) + (hasRequestJson ? 1 : 0) + (hasRequestFile ? 1 : 0);
              if (specifiedCount > 1)
              {
                  result.AddError(@"Specify at most one of --input, --request-json, or --request-file.");
              }
          });

        command.SetAction(async (ParseResult parseResult, CancellationToken cancellationToken) =>
            await CliRuntime.RunAsync(async () =>
            {
                        var __requestBase = await CliRuntime.ReadRequestOrDefaultAsync<global::Rime.TtsRequest>(
                            parseResult,
                            Input,
                            RequestJson,
                            RequestFile,
                            global::Rime.SourceGenerationContext.Default,
                            cancellationToken).ConfigureAwait(false);
                        var speaker = parseResult.GetRequiredValue(Speaker);
                        var text = parseResult.GetRequiredValue(Text);
                        var modelId = CliRuntime.WasSpecified(parseResult, ModelId) ? parseResult.GetValue(ModelId) : (__requestBase is { } __ModelIdBaseValue ? __ModelIdBaseValue.ModelId : default);
                        var lang = CliRuntime.WasSpecified(parseResult, Lang) ? parseResult.GetValue(Lang) : (__requestBase is { } __LangBaseValue ? __LangBaseValue.Lang : default);
                        var samplingRate = CliRuntime.WasSpecified(parseResult, SamplingRate) ? parseResult.GetValue(SamplingRate) : (__requestBase is { } __SamplingRateBaseValue ? __SamplingRateBaseValue.SamplingRate : default);
                        var speedAlpha = CliRuntime.WasSpecified(parseResult, SpeedAlpha) ? parseResult.GetValue(SpeedAlpha) : (__requestBase is { } __SpeedAlphaBaseValue ? __SpeedAlphaBaseValue.SpeedAlpha : default);
                        var timeScaleFactor = CliRuntime.WasSpecified(parseResult, TimeScaleFactor) ? parseResult.GetValue(TimeScaleFactor) : (__requestBase is { } __TimeScaleFactorBaseValue ? __TimeScaleFactorBaseValue.TimeScaleFactor : default);
                        var pauseBetweenBrackets = CliRuntime.WasSpecified(parseResult, PauseBetweenBrackets) ? parseResult.GetValue(PauseBetweenBrackets) : (__requestBase is { } __PauseBetweenBracketsBaseValue ? __PauseBetweenBracketsBaseValue.PauseBetweenBrackets : default);
                        var phonemizeBetweenBrackets = CliRuntime.WasSpecified(parseResult, PhonemizeBetweenBrackets) ? parseResult.GetValue(PhonemizeBetweenBrackets) : (__requestBase is { } __PhonemizeBetweenBracketsBaseValue ? __PhonemizeBetweenBracketsBaseValue.PhonemizeBetweenBrackets : default);
                        var inlineSpeedAlpha = CliRuntime.WasSpecified(parseResult, InlineSpeedAlpha) ? parseResult.GetValue(InlineSpeedAlpha) : (__requestBase is { } __InlineSpeedAlphaBaseValue ? __InlineSpeedAlphaBaseValue.InlineSpeedAlpha : default);
                        var noTextNormalization = CliRuntime.WasSpecified(parseResult, NoTextNormalization) ? parseResult.GetValue(NoTextNormalization) : (__requestBase is { } __NoTextNormalizationBaseValue ? __NoTextNormalizationBaseValue.NoTextNormalization : default);
                        var saveOovs = CliRuntime.WasSpecified(parseResult, SaveOovs) ? parseResult.GetValue(SaveOovs) : (__requestBase is { } __SaveOovsBaseValue ? __SaveOovsBaseValue.SaveOovs : default);
                using var client = await CliRuntime.CreateClientAsync(parseResult, cancellationToken).ConfigureAwait(false);


                                var response = await client.TextToSpeech.CreateTtsAsync(
                                    speaker: speaker,
                                    text: text,
                                    modelId: modelId,
                                    lang: lang,
                                    samplingRate: samplingRate,
                                    speedAlpha: speedAlpha,
                                    timeScaleFactor: timeScaleFactor,
                                    pauseBetweenBrackets: pauseBetweenBrackets,
                                    phonemizeBetweenBrackets: phonemizeBetweenBrackets,
                                    inlineSpeedAlpha: inlineSpeedAlpha,
                                    noTextNormalization: noTextNormalization,
                                    saveOovs: saveOovs,
                                    cancellationToken: cancellationToken).ConfigureAwait(false);

                                await CliRuntime.WriteBinaryAsync(parseResult, response, cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false));
        return command;
    }
}
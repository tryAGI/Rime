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
        Description = @"The text to speak. Character limits depend on the model:
Mist v2/v3 allow 500 characters per request via the API, Arcana allows 3,000.
",
        Required = true,
    };

    private static Option<global::Rime.TtsRequestModelId?> ModelId { get; } = new(
        name: @"--model-id")
    {
        Description = @"The TTS model to use. `arcana` for flagship conversational voices,
`mistv3` for low-latency streaming, `mistv2` as a fallback.
",
    };

    private static Option<string?> Lang { get; } = new(
        name: @"--lang")
    {
        Description = @"Language identifier for the selected speaker. Must match the speaker's
language. Defaults to `eng`/`en` depending on model.
",
    };

    private static Option<int?> SamplingRate { get; } = new(
        name: @"--sampling-rate")
    {
        Description = @"Output sample rate in Hz. Allowed range is 4000-44100 for Mist v2;
Mist v3 and Arcana default to 24000 when omitted.
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
        Description = @"Adjusts the speed of speech for Mist v3 / Arcana. Values below 1 slow down
the audio; values above 1 speed it up.
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
");

    private static Option<string?> InlineSpeedAlpha { get; } = new(
        name: @"--inline-speed-alpha")
    {
        Description = @"Comma-separated per-word speed multipliers for words inside square brackets.
Values > 1.0 accelerate, values < 1.0 decelerate.
",
    };

    private static Option<bool?> NoTextNormalization { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--no-text-normalization",
        description: @"Skip text normalization to reduce latency. Available only on Mist v2.
");

    private static Option<bool?> SaveOovs { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--save-oovs",
        description: @"Save out-of-vocabulary words for later review. Available only on Mist v2.
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
        var command = new Command(@"create-tts", @"Generate speech (Mist v3 / Mist v2 / Arcana)
Synthesize speech from text using Rime's TTS models (`arcana`, `mistv2`, or `mistv3`).
Audio bytes are returned in the format indicated by the `Accept` header.

Supported `Accept` values: `audio/webm;codecs=opus`, `audio/ogg;codecs=opus`,
`audio/mp3`, `audio/wav`, `audio/pcm`, `audio/x-mulaw`.
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
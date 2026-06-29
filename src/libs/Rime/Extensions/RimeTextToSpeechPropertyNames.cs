#nullable enable

namespace Rime;

/// <summary>
/// Rime-specific <see cref="Microsoft.Extensions.AI.TextToSpeechOptions.AdditionalProperties" /> keys.
/// </summary>
public static class RimeTextToSpeechPropertyNames
{
    /// <summary>Exact HTTP <c>Accept</c> header for generated audio, such as <c>audio/mp3</c> or <c>audio/wav</c>.</summary>
    public const string Accept = "rime:accept";

    /// <summary>Output sample rate in Hz.</summary>
    public const string SamplingRate = "rime:sampling_rate";

    /// <summary>Mist v2 speed control. Values below 1 speed up, values above 1 slow down.</summary>
    public const string SpeedAlpha = "rime:speed_alpha";

    /// <summary>Mist v3 / Arcana speed control. Values below 1 slow down, values above 1 speed up.</summary>
    public const string TimeScaleFactor = "rime:time_scale_factor";

    /// <summary>Enable pauses between angle-bracket markers.</summary>
    public const string PauseBetweenBrackets = "rime:pause_between_brackets";

    /// <summary>Enable phoneme pronunciation between curly-bracket markers.</summary>
    public const string PhonemizeBetweenBrackets = "rime:phonemize_between_brackets";

    /// <summary>Comma-separated per-word speed multipliers for bracketed words.</summary>
    public const string InlineSpeedAlpha = "rime:inline_speed_alpha";

    /// <summary>Skip text normalization when supported by the selected model.</summary>
    public const string NoTextNormalization = "rime:no_text_normalization";

    /// <summary>Save out-of-vocabulary words when supported by the selected model.</summary>
    public const string SaveOovs = "rime:save_oovs";
}

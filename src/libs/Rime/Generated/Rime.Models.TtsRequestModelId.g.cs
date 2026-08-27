
#nullable enable

namespace Rime
{
    /// <summary>
    /// The TTS model to use. `coda` is the flagship model for new applications,<br/>
    /// `mistv3` prioritizes lowest time to first audio, and `mistv2` retains inline<br/>
    /// pronunciation control. `arcana` is a retired compatibility alias now served<br/>
    /// by Coda; use `coda` for new requests.<br/>
    /// Example: coda
    /// </summary>
    public enum TtsRequestModelId
    {
        /// <summary>
        ///
        /// </summary>
        Arcana,
        /// <summary>
        ///
        /// </summary>
        Coda,
        /// <summary>
        ///
        /// </summary>
        Mistv2,
        /// <summary>
        ///
        /// </summary>
        Mistv3,
    }

    /// <summary>
    /// Enum extensions to do fast conversions without the reflection.
    /// </summary>
    public static class TtsRequestModelIdExtensions
    {
        /// <summary>
        /// Converts an enum to a string.
        /// </summary>
        public static string ToValueString(this TtsRequestModelId value)
        {
            return value switch
            {
                TtsRequestModelId.Arcana => "arcana",
                TtsRequestModelId.Coda => "coda",
                TtsRequestModelId.Mistv2 => "mistv2",
                TtsRequestModelId.Mistv3 => "mistv3",
                _ => throw new global::System.ArgumentOutOfRangeException(nameof(value), value, null),
            };
        }
        /// <summary>
        /// Converts an string to a enum.
        /// </summary>
        public static TtsRequestModelId? ToEnum(string value)
        {
            return value switch
            {
                "arcana" => TtsRequestModelId.Arcana,
                "coda" => TtsRequestModelId.Coda,
                "mistv2" => TtsRequestModelId.Mistv2,
                "mistv3" => TtsRequestModelId.Mistv3,
                _ => null,
            };
        }
    }
}
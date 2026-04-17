
#nullable enable

namespace Rime
{
    /// <summary>
    /// The TTS model to use. `arcana` for flagship conversational voices,<br/>
    /// `mistv3` for low-latency streaming, `mistv2` as a fallback.<br/>
    /// Example: mistv3
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
                "mistv2" => TtsRequestModelId.Mistv2,
                "mistv3" => TtsRequestModelId.Mistv3,
                _ => null,
            };
        }
    }
}
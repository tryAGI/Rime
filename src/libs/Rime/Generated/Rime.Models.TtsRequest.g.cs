
#nullable enable

namespace Rime
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class TtsRequest
    {
        /// <summary>
        /// The voice used to synthesize the text. Must be one of the voices<br/>
        /// returned by `/data/voices/all-v2.json` for the selected `modelId`.<br/>
        /// Example: cove
        /// </summary>
        /// <example>cove</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("speaker")]
        [global::System.Text.Json.Serialization.JsonRequired]
        public required string Speaker { get; set; }

        /// <summary>
        /// The text to speak. The cloud API accepts up to 1,000 characters per request.<br/>
        /// Example: Hello from Rime AI.
        /// </summary>
        /// <example>Hello from Rime AI.</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("text")]
        [global::System.Text.Json.Serialization.JsonRequired]
        public required string Text { get; set; }

        /// <summary>
        /// The TTS model to use. `coda` is the flagship model for new applications,<br/>
        /// `mistv3` prioritizes lowest time to first audio, and `mistv2` retains inline<br/>
        /// pronunciation control. `arcana` is a retired compatibility alias now served<br/>
        /// by Coda; use `coda` for new requests.<br/>
        /// Example: coda
        /// </summary>
        /// <example>coda</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("modelId")]
        [global::System.Text.Json.Serialization.JsonConverter(typeof(global::Rime.JsonConverters.TtsRequestModelIdJsonConverter))]
        public global::Rime.TtsRequestModelId? ModelId { get; set; }

        /// <summary>
        /// Language identifier for the selected speaker. Must match the speaker's<br/>
        /// language. Both two-letter and three-letter ISO codes are accepted. Coda<br/>
        /// supports English, Arabic, French, German, Hindi, Italian, Japanese,<br/>
        /// Portuguese, and Spanish; Mist v2/v3 support English, French, German,<br/>
        /// and Spanish.<br/>
        /// Example: en
        /// </summary>
        /// <example>en</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("lang")]
        public string? Lang { get; set; }

        /// <summary>
        /// Output sample rate in Hz. Mist v2 accepts 4000-44100. Coda and Mist v3<br/>
        /// default to 24000; the public API's common range is 8000-96000, with values<br/>
        /// above 24000 produced by upsampling.<br/>
        /// Example: 24000
        /// </summary>
        /// <example>24000</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("samplingRate")]
        public int? SamplingRate { get; set; }

        /// <summary>
        /// Adjusts the speed of speech for Mist v2. Values below 1.0 speed up, values<br/>
        /// above 1.0 slow down.<br/>
        /// Example: 1.0
        /// </summary>
        /// <example>1.0</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("speedAlpha")]
        public float? SpeedAlpha { get; set; }

        /// <summary>
        /// Adjusts the speed of speech for Coda and Mist v3. Values above 1.0 slow<br/>
        /// down the audio and values below 1.0 speed it up. Values outside 0.4-2.5<br/>
        /// are clamped by the API.<br/>
        /// Example: 1.0
        /// </summary>
        /// <example>1.0</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("timeScaleFactor")]
        public float? TimeScaleFactor { get; set; }

        /// <summary>
        /// Adds pauses between words enclosed in angle brackets, with the pause<br/>
        /// duration specified in milliseconds (e.g. `Hello &lt;500&gt; world`).<br/>
        /// Default Value: false
        /// </summary>
        [global::System.Text.Json.Serialization.JsonPropertyName("pauseBetweenBrackets")]
        public bool? PauseBetweenBrackets { get; set; }

        /// <summary>
        /// Enables custom pronunciation via phonemes specified inside curly brackets.<br/>
        /// Supported by Mist v2; Coda and Mist v3 accept and ignore this option.<br/>
        /// Default Value: false
        /// </summary>
        [global::System.Text.Json.Serialization.JsonPropertyName("phonemizeBetweenBrackets")]
        public bool? PhonemizeBetweenBrackets { get; set; }

        /// <summary>
        /// Comma-separated per-word speed multipliers for words inside square brackets.<br/>
        /// Values below 1.0 speed up speech and values above 1.0 slow it down.<br/>
        /// Example: 1.2,0.8
        /// </summary>
        /// <example>1.2,0.8</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("inlineSpeedAlpha")]
        public string? InlineSpeedAlpha { get; set; }

        /// <summary>
        /// Skip text normalization to reduce latency. Available only on Mist v2.<br/>
        /// Default Value: false
        /// </summary>
        [global::System.Text.Json.Serialization.JsonPropertyName("noTextNormalization")]
        public bool? NoTextNormalization { get; set; }

        /// <summary>
        /// Legacy Mist v2 option retained for backward compatibility. The Speech QA<br/>
        /// dashboard it reported to has retired, and Rime no longer documents this<br/>
        /// option for new integrations.<br/>
        /// Default Value: false
        /// </summary>
        [global::System.Text.Json.Serialization.JsonPropertyName("saveOovs")]
        public bool? SaveOovs { get; set; }

        /// <summary>
        /// Additional properties that are not explicitly defined in the schema
        /// </summary>
        [global::System.Text.Json.Serialization.JsonExtensionData]
        public global::System.Collections.Generic.IDictionary<string, object> AdditionalProperties { get; set; } = new global::System.Collections.Generic.Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TtsRequest" /> class.
        /// </summary>
        /// <param name="speaker">
        /// The voice used to synthesize the text. Must be one of the voices<br/>
        /// returned by `/data/voices/all-v2.json` for the selected `modelId`.<br/>
        /// Example: cove
        /// </param>
        /// <param name="text">
        /// The text to speak. The cloud API accepts up to 1,000 characters per request.<br/>
        /// Example: Hello from Rime AI.
        /// </param>
        /// <param name="modelId">
        /// The TTS model to use. `coda` is the flagship model for new applications,<br/>
        /// `mistv3` prioritizes lowest time to first audio, and `mistv2` retains inline<br/>
        /// pronunciation control. `arcana` is a retired compatibility alias now served<br/>
        /// by Coda; use `coda` for new requests.<br/>
        /// Example: coda
        /// </param>
        /// <param name="lang">
        /// Language identifier for the selected speaker. Must match the speaker's<br/>
        /// language. Both two-letter and three-letter ISO codes are accepted. Coda<br/>
        /// supports English, Arabic, French, German, Hindi, Italian, Japanese,<br/>
        /// Portuguese, and Spanish; Mist v2/v3 support English, French, German,<br/>
        /// and Spanish.<br/>
        /// Example: en
        /// </param>
        /// <param name="samplingRate">
        /// Output sample rate in Hz. Mist v2 accepts 4000-44100. Coda and Mist v3<br/>
        /// default to 24000; the public API's common range is 8000-96000, with values<br/>
        /// above 24000 produced by upsampling.<br/>
        /// Example: 24000
        /// </param>
        /// <param name="speedAlpha">
        /// Adjusts the speed of speech for Mist v2. Values below 1.0 speed up, values<br/>
        /// above 1.0 slow down.<br/>
        /// Example: 1.0
        /// </param>
        /// <param name="timeScaleFactor">
        /// Adjusts the speed of speech for Coda and Mist v3. Values above 1.0 slow<br/>
        /// down the audio and values below 1.0 speed it up. Values outside 0.4-2.5<br/>
        /// are clamped by the API.<br/>
        /// Example: 1.0
        /// </param>
        /// <param name="pauseBetweenBrackets">
        /// Adds pauses between words enclosed in angle brackets, with the pause<br/>
        /// duration specified in milliseconds (e.g. `Hello &lt;500&gt; world`).<br/>
        /// Default Value: false
        /// </param>
        /// <param name="phonemizeBetweenBrackets">
        /// Enables custom pronunciation via phonemes specified inside curly brackets.<br/>
        /// Supported by Mist v2; Coda and Mist v3 accept and ignore this option.<br/>
        /// Default Value: false
        /// </param>
        /// <param name="inlineSpeedAlpha">
        /// Comma-separated per-word speed multipliers for words inside square brackets.<br/>
        /// Values below 1.0 speed up speech and values above 1.0 slow it down.<br/>
        /// Example: 1.2,0.8
        /// </param>
        /// <param name="noTextNormalization">
        /// Skip text normalization to reduce latency. Available only on Mist v2.<br/>
        /// Default Value: false
        /// </param>
        /// <param name="saveOovs">
        /// Legacy Mist v2 option retained for backward compatibility. The Speech QA<br/>
        /// dashboard it reported to has retired, and Rime no longer documents this<br/>
        /// option for new integrations.<br/>
        /// Default Value: false
        /// </param>
#if NET7_0_OR_GREATER
        [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
#endif
        public TtsRequest(
            string speaker,
            string text,
            global::Rime.TtsRequestModelId? modelId,
            string? lang,
            int? samplingRate,
            float? speedAlpha,
            float? timeScaleFactor,
            bool? pauseBetweenBrackets,
            bool? phonemizeBetweenBrackets,
            string? inlineSpeedAlpha,
            bool? noTextNormalization,
            bool? saveOovs)
        {
            this.Speaker = speaker ?? throw new global::System.ArgumentNullException(nameof(speaker));
            this.Text = text ?? throw new global::System.ArgumentNullException(nameof(text));
            this.ModelId = modelId;
            this.Lang = lang;
            this.SamplingRate = samplingRate;
            this.SpeedAlpha = speedAlpha;
            this.TimeScaleFactor = timeScaleFactor;
            this.PauseBetweenBrackets = pauseBetweenBrackets;
            this.PhonemizeBetweenBrackets = phonemizeBetweenBrackets;
            this.InlineSpeedAlpha = inlineSpeedAlpha;
            this.NoTextNormalization = noTextNormalization;
            this.SaveOovs = saveOovs;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TtsRequest" /> class.
        /// </summary>
        public TtsRequest()
        {
        }

    }
}
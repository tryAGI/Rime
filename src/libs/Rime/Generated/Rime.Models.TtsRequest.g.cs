
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
        /// The text to speak. Character limits depend on the model:<br/>
        /// Mist v2/v3 allow 500 characters per request via the API, Arcana allows 3,000.<br/>
        /// Example: Hello from Rime AI.
        /// </summary>
        /// <example>Hello from Rime AI.</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("text")]
        [global::System.Text.Json.Serialization.JsonRequired]
        public required string Text { get; set; }

        /// <summary>
        /// The TTS model to use. `arcana` for flagship conversational voices,<br/>
        /// `mistv3` for low-latency streaming, `mistv2` as a fallback.<br/>
        /// Example: mistv3
        /// </summary>
        /// <example>mistv3</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("modelId")]
        [global::System.Text.Json.Serialization.JsonConverter(typeof(global::Rime.JsonConverters.TtsRequestModelIdJsonConverter))]
        public global::Rime.TtsRequestModelId? ModelId { get; set; }

        /// <summary>
        /// Language identifier for the selected speaker. Must match the speaker's<br/>
        /// language. Defaults to `eng`/`en` depending on model.<br/>
        /// Example: eng
        /// </summary>
        /// <example>eng</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("lang")]
        public string? Lang { get; set; }

        /// <summary>
        /// Output sample rate in Hz. Allowed range is 4000-44100 for Mist v2;<br/>
        /// Mist v3 and Arcana default to 24000 when omitted.<br/>
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
        /// Adjusts the speed of speech for Mist v3 / Arcana. Values below 1 slow down<br/>
        /// the audio; values above 1 speed it up.<br/>
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
        /// Default Value: false
        /// </summary>
        [global::System.Text.Json.Serialization.JsonPropertyName("phonemizeBetweenBrackets")]
        public bool? PhonemizeBetweenBrackets { get; set; }

        /// <summary>
        /// Comma-separated per-word speed multipliers for words inside square brackets.<br/>
        /// Values &gt; 1.0 accelerate, values &lt; 1.0 decelerate.<br/>
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
        /// Save out-of-vocabulary words for later review. Available only on Mist v2.<br/>
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
        /// The text to speak. Character limits depend on the model:<br/>
        /// Mist v2/v3 allow 500 characters per request via the API, Arcana allows 3,000.<br/>
        /// Example: Hello from Rime AI.
        /// </param>
        /// <param name="modelId">
        /// The TTS model to use. `arcana` for flagship conversational voices,<br/>
        /// `mistv3` for low-latency streaming, `mistv2` as a fallback.<br/>
        /// Example: mistv3
        /// </param>
        /// <param name="lang">
        /// Language identifier for the selected speaker. Must match the speaker's<br/>
        /// language. Defaults to `eng`/`en` depending on model.<br/>
        /// Example: eng
        /// </param>
        /// <param name="samplingRate">
        /// Output sample rate in Hz. Allowed range is 4000-44100 for Mist v2;<br/>
        /// Mist v3 and Arcana default to 24000 when omitted.<br/>
        /// Example: 24000
        /// </param>
        /// <param name="speedAlpha">
        /// Adjusts the speed of speech for Mist v2. Values below 1.0 speed up, values<br/>
        /// above 1.0 slow down.<br/>
        /// Example: 1.0
        /// </param>
        /// <param name="timeScaleFactor">
        /// Adjusts the speed of speech for Mist v3 / Arcana. Values below 1 slow down<br/>
        /// the audio; values above 1 speed it up.<br/>
        /// Example: 1.0
        /// </param>
        /// <param name="pauseBetweenBrackets">
        /// Adds pauses between words enclosed in angle brackets, with the pause<br/>
        /// duration specified in milliseconds (e.g. `Hello &lt;500&gt; world`).<br/>
        /// Default Value: false
        /// </param>
        /// <param name="phonemizeBetweenBrackets">
        /// Enables custom pronunciation via phonemes specified inside curly brackets.<br/>
        /// Default Value: false
        /// </param>
        /// <param name="inlineSpeedAlpha">
        /// Comma-separated per-word speed multipliers for words inside square brackets.<br/>
        /// Values &gt; 1.0 accelerate, values &lt; 1.0 decelerate.<br/>
        /// Example: 1.2,0.8
        /// </param>
        /// <param name="noTextNormalization">
        /// Skip text normalization to reduce latency. Available only on Mist v2.<br/>
        /// Default Value: false
        /// </param>
        /// <param name="saveOovs">
        /// Save out-of-vocabulary words for later review. Available only on Mist v2.<br/>
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
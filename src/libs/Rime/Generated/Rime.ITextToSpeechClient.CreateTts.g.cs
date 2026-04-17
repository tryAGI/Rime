#nullable enable

namespace Rime
{
    public partial interface ITextToSpeechClient
    {
        /// <summary>
        /// Generate speech (Mist v3 / Mist v2 / Arcana)<br/>
        /// Synthesize speech from text using Rime's TTS models (`arcana`, `mistv2`, or `mistv3`).<br/>
        /// Audio bytes are returned in the format indicated by the `Accept` header.<br/>
        /// Supported `Accept` values: `audio/webm;codecs=opus`, `audio/ogg;codecs=opus`,<br/>
        /// `audio/mp3`, `audio/wav`, `audio/pcm`, `audio/x-mulaw`.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Rime.ApiException"></exception>
        global::System.Threading.Tasks.Task<byte[]> CreateTtsAsync(

            global::Rime.TtsRequest request,
            global::Rime.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Generate speech (Mist v3 / Mist v2 / Arcana)<br/>
        /// Synthesize speech from text using Rime's TTS models (`arcana`, `mistv2`, or `mistv3`).<br/>
        /// Audio bytes are returned in the format indicated by the `Accept` header.<br/>
        /// Supported `Accept` values: `audio/webm;codecs=opus`, `audio/ogg;codecs=opus`,<br/>
        /// `audio/mp3`, `audio/wav`, `audio/pcm`, `audio/x-mulaw`.
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
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<byte[]> CreateTtsAsync(
            string speaker,
            string text,
            global::Rime.TtsRequestModelId? modelId = default,
            string? lang = default,
            int? samplingRate = default,
            float? speedAlpha = default,
            float? timeScaleFactor = default,
            bool? pauseBetweenBrackets = default,
            bool? phonemizeBetweenBrackets = default,
            string? inlineSpeedAlpha = default,
            bool? noTextNormalization = default,
            bool? saveOovs = default,
            global::Rime.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}
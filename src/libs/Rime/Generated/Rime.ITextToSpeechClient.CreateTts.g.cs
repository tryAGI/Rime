#nullable enable

namespace Rime
{
    public partial interface ITextToSpeechClient
    {
        /// <summary>
        /// Generate speech (Coda / Mist v3 / Mist v2)<br/>
        /// Synthesize speech from text using Rime's TTS models (`coda`, `mistv3`, or `mistv2`).<br/>
        /// The retired `arcana` identifier remains accepted by Rime as a compatibility alias<br/>
        /// that is served by Coda, but new applications should send `coda`.<br/>
        /// Audio bytes are returned in the format indicated by the `Accept` header.<br/>
        /// Supported `Accept` values: `audio/webm;codecs=opus`, `audio/ogg;codecs=opus`,<br/>
        /// `audio/mpeg`, `audio/wav`, `audio/L16`, `audio/PCMU`. The aliases `audio/mp3`,<br/>
        /// `audio/pcm`, and `audio/x-mulaw` are deprecated but remain accepted by Rime.
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
        /// Generate speech (Coda / Mist v3 / Mist v2)<br/>
        /// Synthesize speech from text using Rime's TTS models (`coda`, `mistv3`, or `mistv2`).<br/>
        /// The retired `arcana` identifier remains accepted by Rime as a compatibility alias<br/>
        /// that is served by Coda, but new applications should send `coda`.<br/>
        /// Audio bytes are returned in the format indicated by the `Accept` header.<br/>
        /// Supported `Accept` values: `audio/webm;codecs=opus`, `audio/ogg;codecs=opus`,<br/>
        /// `audio/mpeg`, `audio/wav`, `audio/L16`, `audio/PCMU`. The aliases `audio/mp3`,<br/>
        /// `audio/pcm`, and `audio/x-mulaw` are deprecated but remain accepted by Rime.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Rime.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::System.IO.Stream> CreateTtsAsStreamAsync(

            global::Rime.TtsRequest request,
            global::Rime.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Generate speech (Coda / Mist v3 / Mist v2)<br/>
        /// Synthesize speech from text using Rime's TTS models (`coda`, `mistv3`, or `mistv2`).<br/>
        /// The retired `arcana` identifier remains accepted by Rime as a compatibility alias<br/>
        /// that is served by Coda, but new applications should send `coda`.<br/>
        /// Audio bytes are returned in the format indicated by the `Accept` header.<br/>
        /// Supported `Accept` values: `audio/webm;codecs=opus`, `audio/ogg;codecs=opus`,<br/>
        /// `audio/mpeg`, `audio/wav`, `audio/L16`, `audio/PCMU`. The aliases `audio/mp3`,<br/>
        /// `audio/pcm`, and `audio/x-mulaw` are deprecated but remain accepted by Rime.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Rime.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Rime.AutoSDKHttpResponse<byte[]>> CreateTtsAsResponseAsync(

            global::Rime.TtsRequest request,
            global::Rime.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Generate speech (Coda / Mist v3 / Mist v2)<br/>
        /// Synthesize speech from text using Rime's TTS models (`coda`, `mistv3`, or `mistv2`).<br/>
        /// The retired `arcana` identifier remains accepted by Rime as a compatibility alias<br/>
        /// that is served by Coda, but new applications should send `coda`.<br/>
        /// Audio bytes are returned in the format indicated by the `Accept` header.<br/>
        /// Supported `Accept` values: `audio/webm;codecs=opus`, `audio/ogg;codecs=opus`,<br/>
        /// `audio/mpeg`, `audio/wav`, `audio/L16`, `audio/PCMU`. The aliases `audio/mp3`,<br/>
        /// `audio/pcm`, and `audio/x-mulaw` are deprecated but remain accepted by Rime.
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
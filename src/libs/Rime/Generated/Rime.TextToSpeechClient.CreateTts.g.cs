
#nullable enable

namespace Rime
{
    public partial class TextToSpeechClient
    {


        private static readonly global::Rime.EndPointSecurityRequirement s_CreateTtsSecurityRequirement0 =
            new global::Rime.EndPointSecurityRequirement
            {
                Authorizations = new global::Rime.EndPointAuthorizationRequirement[]
                {                    new global::Rime.EndPointAuthorizationRequirement
                    {
                        Type = "Http",
                        SchemeId = "HttpBearer",
                        Location = "Header",
                        Name = "Bearer",
                        FriendlyName = "Bearer",
                    },
                },
            };
        private static readonly global::Rime.EndPointSecurityRequirement[] s_CreateTtsSecurityRequirements =
            new global::Rime.EndPointSecurityRequirement[]
            {                s_CreateTtsSecurityRequirement0,
            };
        partial void PrepareCreateTtsArguments(
            global::System.Net.Http.HttpClient httpClient,
            global::Rime.TtsRequest request);
        partial void PrepareCreateTtsRequest(
            global::System.Net.Http.HttpClient httpClient,
            global::System.Net.Http.HttpRequestMessage httpRequestMessage,
            global::Rime.TtsRequest request);
        partial void ProcessCreateTtsResponse(
            global::System.Net.Http.HttpClient httpClient,
            global::System.Net.Http.HttpResponseMessage httpResponseMessage);

        partial void ProcessCreateTtsResponseContent(
            global::System.Net.Http.HttpClient httpClient,
            global::System.Net.Http.HttpResponseMessage httpResponseMessage,
            ref byte[] content);

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
        public async global::System.Threading.Tasks.Task<byte[]> CreateTtsAsync(

            global::Rime.TtsRequest request,
            global::Rime.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            request = request ?? throw new global::System.ArgumentNullException(nameof(request));

            PrepareArguments(
                client: HttpClient);
            PrepareCreateTtsArguments(
                httpClient: HttpClient,
                request: request);


            var __authorizations = global::Rime.EndPointSecurityResolver.ResolveAuthorizations(
                availableAuthorizations: Authorizations,
                securityRequirements: s_CreateTtsSecurityRequirements,
                operationName: "CreateTtsAsync");

            using var __timeoutCancellationTokenSource = global::Rime.AutoSDKRequestOptionsSupport.CreateTimeoutCancellationTokenSource(
                clientOptions: Options,
                requestOptions: requestOptions,
                cancellationToken: cancellationToken);
            var __effectiveCancellationToken = __timeoutCancellationTokenSource?.Token ?? cancellationToken;
            var __effectiveReadResponseAsString = global::Rime.AutoSDKRequestOptionsSupport.GetReadResponseAsString(
                clientOptions: Options,
                requestOptions: requestOptions,
                fallbackValue: ReadResponseAsString);
            var __maxAttempts = global::Rime.AutoSDKRequestOptionsSupport.GetMaxAttempts(
                clientOptions: Options,
                requestOptions: requestOptions,
                supportsRetry: true);

            global::System.Net.Http.HttpRequestMessage __CreateHttpRequest()
            {
                            var __pathBuilder = new global::Rime.PathBuilder(
                                path: "/v1/rime-tts",
                                baseUri: HttpClient.BaseAddress);
                            var __path = __pathBuilder.ToString();
                __path = global::Rime.AutoSDKRequestOptionsSupport.AppendQueryParameters(
                    path: __path,
                    clientParameters: Options.QueryParameters,
                    requestParameters: requestOptions?.QueryParameters);
                var __httpRequest = new global::System.Net.Http.HttpRequestMessage(
                    method: global::System.Net.Http.HttpMethod.Post,
                    requestUri: new global::System.Uri(__path, global::System.UriKind.RelativeOrAbsolute));
#if NET6_0_OR_GREATER
                __httpRequest.Version = global::System.Net.HttpVersion.Version11;
                __httpRequest.VersionPolicy = global::System.Net.Http.HttpVersionPolicy.RequestVersionOrHigher;
#endif

            foreach (var __authorization in __authorizations)
            {
                if (__authorization.Type == "Http" ||
                    __authorization.Type == "OAuth2" ||
                    __authorization.Type == "OpenIdConnect")
                {
                    __httpRequest.Headers.Authorization = new global::System.Net.Http.Headers.AuthenticationHeaderValue(
                        scheme: __authorization.Name,
                        parameter: __authorization.Value);
                }
                else if (__authorization.Type == "ApiKey" &&
                         __authorization.Location == "Header")
                {
                    __httpRequest.Headers.Add(__authorization.Name, __authorization.Value);
                } 
            }
                            var __httpRequestContentBody = request.ToJson(JsonSerializerContext);
                            var __httpRequestContent = new global::System.Net.Http.StringContent(
                                content: __httpRequestContentBody,
                                encoding: global::System.Text.Encoding.UTF8,
                                mediaType: "application/json");
                            __httpRequest.Content = __httpRequestContent;
                global::Rime.AutoSDKRequestOptionsSupport.ApplyHeaders(
                    request: __httpRequest,
                    clientHeaders: Options.Headers,
                    requestHeaders: requestOptions?.Headers);

                PrepareRequest(
                    client: HttpClient,
                    request: __httpRequest);
                PrepareCreateTtsRequest(
                    httpClient: HttpClient,
                    httpRequestMessage: __httpRequest,
                    request: request);

                return __httpRequest;
            }

            global::System.Net.Http.HttpRequestMessage? __httpRequest = null;
            global::System.Net.Http.HttpResponseMessage? __response = null;
            var __attemptNumber = 0;
            try
            {
                for (var __attempt = 1; __attempt <= __maxAttempts; __attempt++)
                {
                    __attemptNumber = __attempt;
                    __httpRequest = __CreateHttpRequest();
                    await global::Rime.AutoSDKRequestOptionsSupport.OnBeforeRequestAsync(
                            clientOptions: Options,
                            context: global::Rime.AutoSDKRequestOptionsSupport.CreateHookContext(
                                operationId: "CreateTts",
                                methodName: "CreateTtsAsync",
                                pathTemplate: "\"/v1/rime-tts\"",
                                httpMethod: "POST",
                                baseUri: BaseUri,
                                request: __httpRequest!,
                                response: null,
                                exception: null,
                                clientOptions: Options,
                                requestOptions: requestOptions,
                                attempt: __attempt,
                                maxAttempts: __maxAttempts,
                                willRetry: false,
                                cancellationToken: __effectiveCancellationToken)).ConfigureAwait(false);
                    try
                    {
                        __response = await HttpClient.SendAsync(
                request: __httpRequest,
                completionOption: global::System.Net.Http.HttpCompletionOption.ResponseContentRead,
                cancellationToken: __effectiveCancellationToken).ConfigureAwait(false);
                    }
                    catch (global::System.Net.Http.HttpRequestException __exception)
                    {
                        var __willRetry = __attempt < __maxAttempts && !__effectiveCancellationToken.IsCancellationRequested;
                        await global::Rime.AutoSDKRequestOptionsSupport.OnAfterErrorAsync(
                            clientOptions: Options,
                            context: global::Rime.AutoSDKRequestOptionsSupport.CreateHookContext(
                                operationId: "CreateTts",
                                methodName: "CreateTtsAsync",
                                pathTemplate: "\"/v1/rime-tts\"",
                                httpMethod: "POST",
                                baseUri: BaseUri,
                                request: __httpRequest!,
                                response: null,
                                exception: __exception,
                                clientOptions: Options,
                                requestOptions: requestOptions,
                                attempt: __attempt,
                                maxAttempts: __maxAttempts,
                                willRetry: __willRetry,
                                cancellationToken: __effectiveCancellationToken)).ConfigureAwait(false);
                        if (!__willRetry)
                        {
                            throw;
                        }

                        __httpRequest.Dispose();
                        __httpRequest = null;
                        await global::Rime.AutoSDKRequestOptionsSupport.DelayBeforeRetryAsync(
                            clientOptions: Options,
                            requestOptions: requestOptions,
                            cancellationToken: __effectiveCancellationToken).ConfigureAwait(false);
                        continue;
                    }

                    if (__response != null &&
                        __attempt < __maxAttempts &&
                        global::Rime.AutoSDKRequestOptionsSupport.ShouldRetryStatusCode(__response.StatusCode))
                    {
                        await global::Rime.AutoSDKRequestOptionsSupport.OnAfterErrorAsync(
                            clientOptions: Options,
                            context: global::Rime.AutoSDKRequestOptionsSupport.CreateHookContext(
                                operationId: "CreateTts",
                                methodName: "CreateTtsAsync",
                                pathTemplate: "\"/v1/rime-tts\"",
                                httpMethod: "POST",
                                baseUri: BaseUri,
                                request: __httpRequest!,
                                response: __response,
                                exception: null,
                                clientOptions: Options,
                                requestOptions: requestOptions,
                                attempt: __attempt,
                                maxAttempts: __maxAttempts,
                                willRetry: true,
                                cancellationToken: __effectiveCancellationToken)).ConfigureAwait(false);
                        __response.Dispose();
                        __response = null;
                        __httpRequest.Dispose();
                        __httpRequest = null;
                        await global::Rime.AutoSDKRequestOptionsSupport.DelayBeforeRetryAsync(
                            clientOptions: Options,
                            requestOptions: requestOptions,
                            cancellationToken: __effectiveCancellationToken).ConfigureAwait(false);
                        continue;
                    }

                    break;
                }

                if (__response == null)
                {
                    throw new global::System.InvalidOperationException("No response received.");
                }

                using (__response)
                {

                ProcessResponse(
                    client: HttpClient,
                    response: __response);
                ProcessCreateTtsResponse(
                    httpClient: HttpClient,
                    httpResponseMessage: __response);
                if (__response.IsSuccessStatusCode)
                {
                    await global::Rime.AutoSDKRequestOptionsSupport.OnAfterSuccessAsync(
                            clientOptions: Options,
                            context: global::Rime.AutoSDKRequestOptionsSupport.CreateHookContext(
                                operationId: "CreateTts",
                                methodName: "CreateTtsAsync",
                                pathTemplate: "\"/v1/rime-tts\"",
                                httpMethod: "POST",
                                baseUri: BaseUri,
                                request: __httpRequest!,
                                response: __response,
                                exception: null,
                                clientOptions: Options,
                                requestOptions: requestOptions,
                                attempt: __attemptNumber,
                                maxAttempts: __maxAttempts,
                                willRetry: false,
                                cancellationToken: __effectiveCancellationToken)).ConfigureAwait(false);
                }
                else
                {
                    await global::Rime.AutoSDKRequestOptionsSupport.OnAfterErrorAsync(
                            clientOptions: Options,
                            context: global::Rime.AutoSDKRequestOptionsSupport.CreateHookContext(
                                operationId: "CreateTts",
                                methodName: "CreateTtsAsync",
                                pathTemplate: "\"/v1/rime-tts\"",
                                httpMethod: "POST",
                                baseUri: BaseUri,
                                request: __httpRequest!,
                                response: __response,
                                exception: null,
                                clientOptions: Options,
                                requestOptions: requestOptions,
                                attempt: __attemptNumber,
                                maxAttempts: __maxAttempts,
                                willRetry: false,
                                cancellationToken: __effectiveCancellationToken)).ConfigureAwait(false);
                }
                            // Invalid request.
                            if ((int)__response.StatusCode == 400)
                            {
                                string? __content_400 = null;
                                global::System.Exception? __exception_400 = null;
                                global::Rime.Error? __value_400 = null;
                                try
                                {
                                    if (__effectiveReadResponseAsString)
                                    {
                                        __content_400 = await __response.Content.ReadAsStringAsync(__effectiveCancellationToken).ConfigureAwait(false);
                                        __value_400 = global::Rime.Error.FromJson(__content_400, JsonSerializerContext);
                                    }
                                    else
                                    {
                                        __content_400 = await __response.Content.ReadAsStringAsync(__effectiveCancellationToken).ConfigureAwait(false);

                                        __value_400 = global::Rime.Error.FromJson(__content_400, JsonSerializerContext);
                                    }
                                }
                                catch (global::System.Exception __ex)
                                {
                                    __exception_400 = __ex;
                                }

                                throw new global::Rime.ApiException<global::Rime.Error>(
                                    message: __content_400 ?? __response.ReasonPhrase ?? string.Empty,
                                    innerException: __exception_400,
                                    statusCode: __response.StatusCode)
                                {
                                    ResponseBody = __content_400,
                                    ResponseObject = __value_400,
                                    ResponseHeaders = global::System.Linq.Enumerable.ToDictionary(
                                        __response.Headers,
                                        h => h.Key,
                                        h => h.Value),
                                };
                            }
                            // Invalid or missing API key.
                            if ((int)__response.StatusCode == 401)
                            {
                                string? __content_401 = null;
                                global::System.Exception? __exception_401 = null;
                                global::Rime.Error? __value_401 = null;
                                try
                                {
                                    if (__effectiveReadResponseAsString)
                                    {
                                        __content_401 = await __response.Content.ReadAsStringAsync(__effectiveCancellationToken).ConfigureAwait(false);
                                        __value_401 = global::Rime.Error.FromJson(__content_401, JsonSerializerContext);
                                    }
                                    else
                                    {
                                        __content_401 = await __response.Content.ReadAsStringAsync(__effectiveCancellationToken).ConfigureAwait(false);

                                        __value_401 = global::Rime.Error.FromJson(__content_401, JsonSerializerContext);
                                    }
                                }
                                catch (global::System.Exception __ex)
                                {
                                    __exception_401 = __ex;
                                }

                                throw new global::Rime.ApiException<global::Rime.Error>(
                                    message: __content_401 ?? __response.ReasonPhrase ?? string.Empty,
                                    innerException: __exception_401,
                                    statusCode: __response.StatusCode)
                                {
                                    ResponseBody = __content_401,
                                    ResponseObject = __value_401,
                                    ResponseHeaders = global::System.Linq.Enumerable.ToDictionary(
                                        __response.Headers,
                                        h => h.Key,
                                        h => h.Value),
                                };
                            }
                            // Rate limit exceeded.
                            if ((int)__response.StatusCode == 429)
                            {
                                string? __content_429 = null;
                                global::System.Exception? __exception_429 = null;
                                global::Rime.Error? __value_429 = null;
                                try
                                {
                                    if (__effectiveReadResponseAsString)
                                    {
                                        __content_429 = await __response.Content.ReadAsStringAsync(__effectiveCancellationToken).ConfigureAwait(false);
                                        __value_429 = global::Rime.Error.FromJson(__content_429, JsonSerializerContext);
                                    }
                                    else
                                    {
                                        __content_429 = await __response.Content.ReadAsStringAsync(__effectiveCancellationToken).ConfigureAwait(false);

                                        __value_429 = global::Rime.Error.FromJson(__content_429, JsonSerializerContext);
                                    }
                                }
                                catch (global::System.Exception __ex)
                                {
                                    __exception_429 = __ex;
                                }

                                throw new global::Rime.ApiException<global::Rime.Error>(
                                    message: __content_429 ?? __response.ReasonPhrase ?? string.Empty,
                                    innerException: __exception_429,
                                    statusCode: __response.StatusCode)
                                {
                                    ResponseBody = __content_429,
                                    ResponseObject = __value_429,
                                    ResponseHeaders = global::System.Linq.Enumerable.ToDictionary(
                                        __response.Headers,
                                        h => h.Key,
                                        h => h.Value),
                                };
                            }

                            if (__effectiveReadResponseAsString)
                            {
                                var __content = await __response.Content.ReadAsByteArrayAsync(
                #if NET5_0_OR_GREATER
                                    __effectiveCancellationToken
                #endif
                                ).ConfigureAwait(false);

                                ProcessCreateTtsResponseContent(
                                    httpClient: HttpClient,
                                    httpResponseMessage: __response,
                                    content: ref __content);

                                try
                                {
                                    __response.EnsureSuccessStatusCode();

                                    return __content;
                                }
                                catch (global::System.Exception __ex)
                                {
                                    throw new global::Rime.ApiException(
                                        message: __response.ReasonPhrase ?? string.Empty,
                                        innerException: __ex,
                                        statusCode: __response.StatusCode)
                                    {
                                        ResponseHeaders = global::System.Linq.Enumerable.ToDictionary(
                                            __response.Headers,
                                            h => h.Key,
                                            h => h.Value),
                                    };
                                }
                            }
                            else
                            {
                                try
                                {
                                    __response.EnsureSuccessStatusCode();
                                    var __content = await __response.Content.ReadAsByteArrayAsync(
                #if NET5_0_OR_GREATER
                                        __effectiveCancellationToken
                #endif
                                    ).ConfigureAwait(false);

                                    return __content;
                                }
                                catch (global::System.Exception __ex)
                                {
                                    string? __content = null;
                                    try
                                    {
                                        __content = await __response.Content.ReadAsStringAsync(
                #if NET5_0_OR_GREATER
                                            __effectiveCancellationToken
                #endif
                                        ).ConfigureAwait(false);
                                    }
                                    catch (global::System.Exception)
                                    {
                                    }

                                    throw new global::Rime.ApiException(
                                        message: __content ?? __response.ReasonPhrase ?? string.Empty,
                                        innerException: __ex,
                                        statusCode: __response.StatusCode)
                                    {
                                        ResponseBody = __content,
                                        ResponseHeaders = global::System.Linq.Enumerable.ToDictionary(
                                            __response.Headers,
                                            h => h.Key,
                                            h => h.Value),
                                    };
                                }
                            }

                }
            }
            finally
            {
                __httpRequest?.Dispose();
            }
        }
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
        public async global::System.Threading.Tasks.Task<byte[]> CreateTtsAsync(
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
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            var __request = new global::Rime.TtsRequest
            {
                Speaker = speaker,
                Text = text,
                ModelId = modelId,
                Lang = lang,
                SamplingRate = samplingRate,
                SpeedAlpha = speedAlpha,
                TimeScaleFactor = timeScaleFactor,
                PauseBetweenBrackets = pauseBetweenBrackets,
                PhonemizeBetweenBrackets = phonemizeBetweenBrackets,
                InlineSpeedAlpha = inlineSpeedAlpha,
                NoTextNormalization = noTextNormalization,
                SaveOovs = saveOovs,
            };

            return await CreateTtsAsync(
                request: __request,
                requestOptions: requestOptions,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
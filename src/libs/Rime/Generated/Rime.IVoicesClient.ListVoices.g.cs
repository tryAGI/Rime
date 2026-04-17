#nullable enable

namespace Rime
{
    public partial interface IVoicesClient
    {
        /// <summary>
        /// List all available voices<br/>
        /// Returns a mapping of `modelId` → language code → list of voice names for every<br/>
        /// voice supported by Rime. Language keys follow ISO 639-2 (e.g. `eng`, `spa`).
        /// </summary>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Rime.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.IList<string>>>> ListVoicesAsync(
            global::Rime.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}
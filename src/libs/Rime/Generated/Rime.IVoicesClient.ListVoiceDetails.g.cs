#nullable enable

namespace Rime
{
    public partial interface IVoicesClient
    {
        /// <summary>
        /// List voice details<br/>
        /// Returns an array of voice metadata objects including speaker name, gender, age,<br/>
        /// country, dialect, demographic, genre, language, and the model the voice is<br/>
        /// available under.
        /// </summary>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Rime.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Rime.VoiceDetail>> ListVoiceDetailsAsync(
            global::Rime.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}
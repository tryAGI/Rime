
#nullable enable

namespace Rime
{
    /// <summary>
    /// Object keyed by `modelId` (e.g. `coda`, `mistv3`, `mistv2`). Each value is<br/>
    /// an object keyed by ISO 639-2 language code whose value is an array of voice<br/>
    /// name strings. The public catalog may also contain retired or externally hosted<br/>
    /// models that are not available through Rime cloud.<br/>
    /// Example: {"coda":{"eng":["astra","albion"],"ara":["salma"]},"mistv3":{"eng":["abbie","allison"]},"mistv2":{"eng":["abbie"],"spa":["isa","mari"]}}
    /// </summary>
    public sealed partial class ListVoicesResponse
    {

        /// <summary>
        /// Additional properties that are not explicitly defined in the schema
        /// </summary>
        [global::System.Text.Json.Serialization.JsonExtensionData]
        public global::System.Collections.Generic.IDictionary<string, object> AdditionalProperties { get; set; } = new global::System.Collections.Generic.Dictionary<string, object>();

    }
}

#nullable enable

namespace Rime
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class VoiceDetail
    {
        /// <summary>
        /// The name of the voice.<br/>
        /// Example: Cove
        /// </summary>
        /// <example>Cove</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("speaker")]
        public string? Speaker { get; set; }

        /// <summary>
        /// Voice gender designation.<br/>
        /// Example: Male
        /// </summary>
        /// <example>Male</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("gender")]
        public string? Gender { get; set; }

        /// <summary>
        /// Age group of the voice.<br/>
        /// Example: Middle
        /// </summary>
        /// <example>Middle</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("age")]
        public string? Age { get; set; }

        /// <summary>
        /// Country associated with the voice, typically representing accent.<br/>
        /// Example: US
        /// </summary>
        /// <example>US</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <summary>
        /// Dialect - more specific than country.<br/>
        /// Example: Generic
        /// </summary>
        /// <example>Generic</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("dialect")]
        public string? Dialect { get; set; }

        /// <summary>
        /// Demographic group the voice represents.<br/>
        /// Example: General American
        /// </summary>
        /// <example>General American</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("demographic")]
        public string? Demographic { get; set; }

        /// <summary>
        /// Genres the voice is suitable for.<br/>
        /// Example: [Conversational]
        /// </summary>
        /// <example>[Conversational]</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("genre")]
        public global::System.Collections.Generic.IList<string>? Genre { get; set; }

        /// <summary>
        /// Spoken language the voice is usable for.<br/>
        /// Example: English
        /// </summary>
        /// <example>English</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("language")]
        public string? Language { get; set; }

        /// <summary>
        /// Value passed as `lang` when synthesizing with this speaker.<br/>
        /// Example: eng
        /// </summary>
        /// <example>eng</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("lang")]
        public string? Lang { get; set; }

        /// <summary>
        /// Model id that the speaker is available under.<br/>
        /// Example: mistv3
        /// </summary>
        /// <example>mistv3</example>
        [global::System.Text.Json.Serialization.JsonPropertyName("modelId")]
        public string? ModelId { get; set; }

        /// <summary>
        /// Additional properties that are not explicitly defined in the schema
        /// </summary>
        [global::System.Text.Json.Serialization.JsonExtensionData]
        public global::System.Collections.Generic.IDictionary<string, object> AdditionalProperties { get; set; } = new global::System.Collections.Generic.Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceDetail" /> class.
        /// </summary>
        /// <param name="speaker">
        /// The name of the voice.<br/>
        /// Example: Cove
        /// </param>
        /// <param name="gender">
        /// Voice gender designation.<br/>
        /// Example: Male
        /// </param>
        /// <param name="age">
        /// Age group of the voice.<br/>
        /// Example: Middle
        /// </param>
        /// <param name="country">
        /// Country associated with the voice, typically representing accent.<br/>
        /// Example: US
        /// </param>
        /// <param name="dialect">
        /// Dialect - more specific than country.<br/>
        /// Example: Generic
        /// </param>
        /// <param name="demographic">
        /// Demographic group the voice represents.<br/>
        /// Example: General American
        /// </param>
        /// <param name="genre">
        /// Genres the voice is suitable for.<br/>
        /// Example: [Conversational]
        /// </param>
        /// <param name="language">
        /// Spoken language the voice is usable for.<br/>
        /// Example: English
        /// </param>
        /// <param name="lang">
        /// Value passed as `lang` when synthesizing with this speaker.<br/>
        /// Example: eng
        /// </param>
        /// <param name="modelId">
        /// Model id that the speaker is available under.<br/>
        /// Example: mistv3
        /// </param>
#if NET7_0_OR_GREATER
        [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
#endif
        public VoiceDetail(
            string? speaker,
            string? gender,
            string? age,
            string? country,
            string? dialect,
            string? demographic,
            global::System.Collections.Generic.IList<string>? genre,
            string? language,
            string? lang,
            string? modelId)
        {
            this.Speaker = speaker;
            this.Gender = gender;
            this.Age = age;
            this.Country = country;
            this.Dialect = dialect;
            this.Demographic = demographic;
            this.Genre = genre;
            this.Language = language;
            this.Lang = lang;
            this.ModelId = modelId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceDetail" /> class.
        /// </summary>
        public VoiceDetail()
        {
        }
    }
}
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Sdk.Token.Requests;

public class DocumentStoreSettings
{
    /// <summary>
    /// Document Store Name
    /// </summary>
    [JsonPropertyName("name")]
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    /// <summary>
    /// Document Store Description
    /// </summary>
    [JsonPropertyName("description")]
    [StringLength(100)]
    public string Description { get; set; }

    /// <summary>
    /// Document Store Type
    /// </summary>
    /// <remarks>
    /// Currently only Azure Files is supported.
    /// </remarks>
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("DocumentStoreType")]
    [Required]
    public string DocumentStoreType { get; set; }

    /// <summary>
    /// The parameters required for the Document Store credentials.
    /// </summary>
    /// <example>
    /// Azure Files Parameters:
    /// - "account": "string",
    /// - "key": "string"
    /// </example>
    [JsonPropertyName("parameters")]
    [Required]
    public Dictionary<string, string> Parameters { get; set; }

    /// <summary>
    /// The maximum file size in MB that can be uploaded to the Document Store.
    /// </summary>
    [JsonPropertyName("maxFileSizeInMB")]
    public int MaxFileSizeInMB { get; set; }

    [JsonPropertyName("maxFileCount")]
    /// <summary>
    /// The maximum number of files that can be uploaded to the Document Store via the Nexus API.
    /// </summary>
    public int MaxFileCount { get; set; }
}
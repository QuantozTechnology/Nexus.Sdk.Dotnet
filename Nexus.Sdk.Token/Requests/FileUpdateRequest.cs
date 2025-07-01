using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public class FileUpdateRequest
{
    /// <summary>
    /// The path of the file in the Document Store
    /// </summary>
    /// <example>
    /// path/to/file.txt
    /// </example>
    [JsonPropertyName("filePath")]
    [Required]
    [StringLength(255)]
    public required string FilePath { get; set; }

    /// <summary>
    /// The DocumentTypeCode is used to identify the type of document associated with the file.
    /// </summary>
    [JsonPropertyName("documentTypeCode")]
    [StringLength(40)]
    public string DocumentTypeCode { get; set; }

    /// <summary>
    /// An optional alias for the file. 
    /// </summary>

    [StringLength(50)]
    public string Alias { get; set; }

    /// <summary>
    /// An optional description of the file.
    /// </summary>
    [JsonPropertyName("description")]
    [StringLength(100)]
    public string Description { get; set; }

    /// <summary>
    /// A Customer code is used to identify the customer associated with the file.
    /// </summary>
    [JsonPropertyName("customerCode")]
    [StringLength(40)]
    public string CustomerCode { get; set; }

    /// <summary>
    /// An optional external unique identifier for the file.
    /// </summary>
    [JsonPropertyName("itemReference")]
    [StringLength(40)]
    public string ItemReference { get; set; }
}
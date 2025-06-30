using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public class DocumentRequest
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
}
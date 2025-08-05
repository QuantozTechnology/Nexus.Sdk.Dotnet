using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public class DocumentRequest
{
    private string _filePath = string.Empty;

    /// <summary>
    /// The path of the file in the Document Store
    /// </summary>
    /// <example>
    /// path/to/file.txt
    /// </example>
    /// <remarks>
    /// The path is automatically URL-encoded to ensure it is safe for transmission
    /// </remarks>
    [JsonPropertyName("filePath")]
    [Required]
    [StringLength(255)]
    public required string FilePath
    {
        get => _filePath;
        set => _filePath = Uri.EscapeDataString(value);
    }
}
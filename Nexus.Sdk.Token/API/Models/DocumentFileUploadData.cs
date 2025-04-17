using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Sdk.Token.API.Models;
public class DocumentFileUploadData
{
    /// <summary>
    /// The path of the file in the document store.
    /// </summary>
    /// <example>
    /// path/to/file.txt
    /// </example>
    [Required]
    [JsonPropertyName("filePath")]
    [StringLength(255)]
    public string FilePath { get; set; }

    /// <summary>
    /// An optional alias for the file. 
    /// </summary>
    [StringLength(50)]
    [JsonPropertyName("alias")]
    public string Alias { get; set; }

    /// <summary>
    /// An optional description of the file.
    /// </summary>
    [StringLength(100)]
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// An optional Customer code that can be used to identify the customer associated with the file.
    /// </summary>
    [StringLength(40)]
    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; set; }

    /// <summary>
    /// An optional external unique identifier for the file.
    /// </summary>
    [StringLength(40)]
    [JsonPropertyName("itemReference")]
    public string ItemReference { get; set; }

    /// <summary>
    /// The file to be added to the Document Store. 
    /// </summary>
    [Required]
    [JsonPropertyName("file")]
    public IFormFile File { get; set; }
}
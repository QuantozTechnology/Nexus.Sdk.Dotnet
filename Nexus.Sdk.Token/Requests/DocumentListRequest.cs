using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Requests;

public class DocumentListRequest
{
    /// <summary>
    /// List Document Store files associated with the specified Customer Code.
    /// </summary>
    [JsonPropertyName("customerCode")]
    [StringLength(40)]
    [Required]
    public required string CustomerCode { get; set; }

    /// <summary>
    /// Get specific page of Document Store items.
    /// </summary>
    /// <remarks>
    /// Page defaults to 1 if not specified.
    /// </remarks>
    [JsonPropertyName("page")]
    [Required]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Maximum number of Document Store items to return.
    /// </summary>
    /// <remarks>
    /// Limit defaults to 10 if not specified.
    /// </remarks>
    [JsonPropertyName("limit")]
    [Required]
    public int Limit { get; set; } = 10;

    /// <summary>
    /// Sort documents by this field.
    /// </summary>
    /// <remarks>
    /// SortBy defaults to CreatedDate if not specified.
    /// </remarks>
    [JsonPropertyName("sortBy")]
    [Required]
    public string SortBy { get; set; } = "CreatedDate";

    /// <summary>
    /// Sort documents in this direction.
    /// Possible values:
    /// - `Asc`
    /// - `Desc`
    /// </summary>
    /// <remarks>
    /// SortDirection defaults to Desc if not specified.
    /// </remarks>
    [JsonPropertyName("sortDirection")]
    [Required]
    public string SortDirection { get; set; } = "Desc";
}
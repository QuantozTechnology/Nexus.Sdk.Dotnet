using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses;

public class DocumentStoreItemResponse
{
    [JsonPropertyName("fileName")]
    public string? FileName { get; set; }

    [JsonPropertyName("filePath")]
    public string? FilePath { get; set; }

    [JsonPropertyName("documentTypeCode")]
    public string? DocumentTypeCode { get; set; }

    [JsonPropertyName("documentTypeName")]
    public string? DocumentTypeName { get; set; }

    [JsonPropertyName("documentTypeOtherDescription")]
    public string? DocumentTypeOtherDescription { get; set; }

    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("customerCode")]
    public string? CustomerCode { get; set; }

    [JsonPropertyName("itemReference")]
    public string? ItemReference { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("fileSizeInMB")]
    public string? FileSizeInMB { get; set; }

    [JsonPropertyName("createdDate")]
    public string? CreatedDate { get; set; }

    [JsonPropertyName("updatedDate")]
    public string? UpdatedDate { get; set; }
}
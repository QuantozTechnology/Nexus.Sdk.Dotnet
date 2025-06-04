namespace Nexus.Sdk.Token.Requests;

/// <summary>
/// Response object for Document Store settings that only exposes specific properties
/// </summary>
public class DocumentStoreSettingsResponse
{
    /// <summary>
    /// Name of the document store integration
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description of the document store integration
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The type of document store
    /// </summary>
    public string DocumentStoreType { get; set; }

    /// <summary>
    /// Maximum file size in megabytes
    /// </summary>
    public int MaxFileSizeInMB { get; set; }

    /// <summary>
    /// Maximum number of files allowed
    /// </summary>
    public int MaxFileCount { get; set; }
}
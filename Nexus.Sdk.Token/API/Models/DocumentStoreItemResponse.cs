namespace Nexus.Sdk.Token.API.Models;

public class DocumentStoreItemResponse
{
    /// <summary>    
    /// The name of the file in the document store.
    /// </summary>
    /// <example>
    /// file.txt
    /// </example>
    public string FileName { get; set; }

    /// <summary>
    /// The full path of the file in the document store.
    /// </summary>
    /// <example>
    /// /path/to/file.txt
    /// </example>
    public string FilePath { get; set; }

    /// <summary>
    /// An optional alias for the file. 
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// An optional description of the file.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// An optional Customer code that can be used to identify the customer associated with the file.
    /// </summary>    
    public string CustomerCode { get; set; }

    /// <summary>
    /// An optional external unique identifier for the file.
    /// </summary>
    public string ItemReference { get; set; }

    /// <summary>
    /// The document store item type
    /// </summary>
    /// <example>
    /// File | Folder
    /// </example>
    public string Type { get; set; }

    /// <summary>
    /// The state of the file in the document store.
    /// </summary>
    /// <example>
    /// Active | Deleted
    /// </example>
    public string State { get; set; }

    /// <summary>
    /// The size of the file in megabytes.
    /// </summary>
    public string FileSizeInMB { get; set; }
}
using System.Diagnostics.CodeAnalysis;

namespace Nexus.Sdk.Token.API;
public class NexusApiOptions
{
    /// <summary>
    /// If true, the NexusApiClient will throw an exception if no access token is found.
    /// </summary>
    public bool ThrowOnMissingAccessToken { get; set; } = true;

    /// <summary>
    /// The default base address for the Nexus API.
    /// Only used if there is no base address provided in the configured `NexusApiClient` HttpClient.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Uri)]
    public string DefaultBaseAddress { get; set; } = "https://api.quantoznexus.com";
}
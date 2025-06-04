using System.Net;

namespace Nexus.Sdk.Token.NexusAPI;

public class NexusApiException(string message) : Exception(message)
{
    public HttpStatusCode StatusCode { get; set; }
    public required string ResponseContent { get; set; }
}
using System.Net;

namespace Nexus.Sdk.Token.API.Models;

public class NexusApiException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string ResponseContent { get; set; }

    public NexusApiException(string message)
        : base(message)
    { }
}
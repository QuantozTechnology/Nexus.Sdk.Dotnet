namespace Nexus.SDK.Shared.ErrorHandling;

public class NexusApiException : ApiException
{
    public readonly string? ErrorCodes;

    public NexusApiException(int statusCode, string reasonPhrase, string[]? errorCodes) : base(statusCode, reasonPhrase)
    {
        StatusCode = statusCode;
        ErrorCodes = errorCodes?.Aggregate((a, b) => $"{a},{b}");
    }
}


namespace Nexus.SDK.Shared.ErrorHandling;

public class NexusApiException : Exception
{
    public readonly int StatusCode;
    public readonly string? ErrorCodes;

    public NexusApiException(int statusCode, string reasonPhrase, string[]? errorCodes) : base(reasonPhrase)
    {
        StatusCode = statusCode;
        ErrorCodes = errorCodes?.Aggregate((a, b) => $"{a},{b}");
    }
}


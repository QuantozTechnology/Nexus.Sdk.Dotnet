using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses;

public class NexusResponse<T> where T : class
{
    [JsonPropertyName("message")]
    public string Message { get; }

    [JsonPropertyName("errors")]
    public string[] Errors { get; }

    [JsonPropertyName("values")]
    public T Values { get; }

    [JsonConstructor]
    public NexusResponse(string message, string[] errors, T values)
    {
        Message = message;
        Errors = errors;
        Values = values;
    }
}

public class NexusResponse
{
    [JsonPropertyName("message")]
    public string Message { get; }

    [JsonPropertyName("errors")]
    public string[] Errors { get; }

    [JsonConstructor]
    public NexusResponse(string message, string[] errors)
    {
        Message = message;
        Errors = errors;
    }
}

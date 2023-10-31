using System.Text.Json;

namespace Nexus.Sdk.Shared.Http;

public static class JsonSingleton
{
    public static T? GetInstance<T>(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException($"'{nameof(content)}' cannot be null or empty.", nameof(content));
        }

        return JsonSerializer.Deserialize<T>(content);
    }
}

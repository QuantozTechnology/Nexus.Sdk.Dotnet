using Microsoft.Extensions.Options;

namespace Nexus.Sdk.Token.NexusAPI;
public class NexusApiClientFactory(
    IHttpClientFactory httpClientFactory,
    INexusApiGetAccessToken getAccessTokenFunc,
    IOptions<NexusApiOptions> options)
    : INexusApiClientFactory
{
    private readonly Func<Task<string?>> _getAccessTokenFunc = getAccessTokenFunc.GetAccessToken;

    public async Task<HttpClient> GetClient()
    {
        var nexusApiClient = httpClientFactory.CreateClient("NexusApiClient");
        var accessToken = await _getAccessTokenFunc();

        if (string.IsNullOrEmpty(accessToken)
            && options.Value.ThrowOnMissingAccessToken)
        {
            throw new Exception("No access token found");
        }

        // Set the default base address if it is not already set
        if (nexusApiClient.BaseAddress is null)
        {
            if (string.IsNullOrEmpty(options.Value.DefaultBaseAddress))
            {
                throw new Exception("No default base address found");
            }

            nexusApiClient.BaseAddress = new Uri(options.Value.DefaultBaseAddress);
        }

        nexusApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

        return nexusApiClient;
    }

    public async Task<HttpClient> GetClient(string apiVersion)
    {
        var client = await GetClient();

        client.DefaultRequestHeaders.Add("api_version", apiVersion);
        return client;
    }
}
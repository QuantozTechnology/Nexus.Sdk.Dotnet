namespace Nexus.Sdk.Token.NexusAPI;
public interface INexusApiClientFactory
{
    Task<HttpClient> GetClient();
    Task<HttpClient> GetClient(string apiVersion);
}
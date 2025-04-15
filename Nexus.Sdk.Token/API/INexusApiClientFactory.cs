namespace Nexus.Sdk.Token.API;
public interface INexusApiClientFactory
{
    Task<HttpClient> GetClient();
    Task<HttpClient> GetClient(string apiVersion);
}
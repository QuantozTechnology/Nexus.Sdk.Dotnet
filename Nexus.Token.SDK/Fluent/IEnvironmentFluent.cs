namespace Nexus.Token.SDK.Fluent;

public interface IEnvironmentFluent
{
    IBuilderFluent ConnectToProduction(string clientId, string clientSecret);
    IBuilderFluent ConnectToTest(string clientId, string clientSecret);
    IBuilderFluent ConnectToCustom(string apiUrl, string identityUrl, string clientId, string clientSecret);
}

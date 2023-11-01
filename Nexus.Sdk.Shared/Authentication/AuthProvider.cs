using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Nexus.Sdk.Shared.ErrorHandling;
using Nexus.Sdk.Shared.Options;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Nexus.Sdk.Shared.Authentication;

public class AuthProvider : IAuthProvider
{
    private readonly string _tokenEndpoint;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string? _scopes;

    private readonly HttpClient _client;
    private readonly ILogger? _logger;

    private DateTime? _expiresOn;
    private string? _accessToken;

    public AuthProvider(HttpClient client, NexusOptions options, ILogger? logger = null)
    {
        _tokenEndpoint = new Uri(options.AuthProviderOptions.IdentityUrl + "/connect/token").OriginalString;
        _clientId = options.AuthProviderOptions.ClientId;
        _clientSecret = options.AuthProviderOptions.ClientSecret;
        _scopes = options.AuthProviderOptions.Scopes;

        _logger = logger;
        _client = client;

        _expiresOn = null;
        _accessToken = null;
    }

    private static DateTime Now()
    {
        return DateTime.UtcNow;
    }

    private bool TokenIsValid()
    {
        if (string.IsNullOrWhiteSpace(_accessToken) || _expiresOn == null)
        {
            return false;
        }

        if (Now() >= _expiresOn)
        {
            return false;
        }

        return true;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        if (TokenIsValid())
        {
            return _accessToken!;
        }
        else
        {
            var response = await _client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _tokenEndpoint,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Scope = _scopes ?? "api1"
            });

            if (response.IsError)
            {
                _logger?.LogError("{statusCode} Auth Response: {error}", response.HttpStatusCode, response.Error);
                throw new AuthProviderException(response.Error);
            }

            _accessToken = response.AccessToken;
            _expiresOn = Now().AddSeconds(response.ExpiresIn);

            _logger?.LogDebug("{statusCode} Auth Response: Token expires on {_expiresOn}", response.HttpStatusCode, _expiresOn.Value.ToString("dd/MM/yyyy HH:mm:ss"));

            return _accessToken;
        }
    }
}

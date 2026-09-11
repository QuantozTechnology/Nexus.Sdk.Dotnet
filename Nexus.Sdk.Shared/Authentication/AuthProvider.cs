using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Nexus.Sdk.Shared.ErrorHandling;
using Nexus.Sdk.Shared.Options;

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

    private readonly bool _usePersonalAccessToken;

    public AuthProvider(HttpClient client, NexusOptions options, ILogger? logger = null)
    {
        var authOptions = options.AuthProviderOptions;

        if (!authOptions.HasAccessToken && !authOptions.HasClientCredentials)
        {
            throw new AuthProviderException(AuthProviderOptions.MissingCredentialsMessage);
        }

        _usePersonalAccessToken = authOptions.HasAccessToken;

        _tokenEndpoint = _usePersonalAccessToken
            ? string.Empty
            : new Uri(authOptions.IdentityUrl + "/connect/token").OriginalString;
        _clientId = authOptions.ClientId;
        _clientSecret = authOptions.ClientSecret;
        _scopes = authOptions.Scopes;

        _logger = logger;
        _client = client;

        _expiresOn = null;
        _accessToken = authOptions.AccessToken;
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
        if (_usePersonalAccessToken)
        {
            return _accessToken!;
        }

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
                throw new AuthProviderException(response.Error!);
            }

            _accessToken = response.AccessToken;
            _expiresOn = Now().AddSeconds(response.ExpiresIn);

            _logger?.LogDebug("{statusCode} Auth Response: Token expires on {_expiresOn}", response.HttpStatusCode, _expiresOn.Value.ToString("dd/MM/yyyy HH:mm:ss"));

            return _accessToken!;
        }
    }
}

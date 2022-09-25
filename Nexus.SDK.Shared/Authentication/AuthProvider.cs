using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace Nexus.SDK.Shared.Authentication;

public class AuthProvider : IAuthProvider
{
    private readonly string _tokenEndpoint;
    private readonly string _clientId;
    private readonly string _clientSecret;

    private ILogger<AuthProvider>? _logger;

    private DateTime? _expiresOn;
    private string? _accessToken;

    public AuthProvider(AuthProviderOptions options, ILogger<AuthProvider>? logger = null)
    {
        if (options.IdentityUrl is null)
        {
            throw new ArgumentNullException(nameof(options.IdentityUrl));
        }

        if (options.ClientId is null)
        {
            throw new ArgumentNullException(nameof(options.ClientId));
        }

        if (options.ClientSecret is null)
        {
            throw new ArgumentNullException(nameof(options.ClientSecret));
        }

        _tokenEndpoint = new Uri(options.IdentityUrl + "/connect/token").OriginalString;
        _clientId = options.ClientId;
        _clientSecret = options.ClientSecret;

        _logger = logger;

        _expiresOn = null;
        _accessToken = null;
    }

    public async Task AuthenticateAsync(HttpClient client)
    {
        if (TokenIsValid())
        {
            return;
        }
        else
        {
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _tokenEndpoint,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Scope = "api1"
            });

            if (response.IsError)
            {
                _logger?.LogError("{statusCode} Auth Response: {error}", response.HttpStatusCode, response.Error);
                throw new AuthProviderException(response.Error);
            }

            _accessToken = response.AccessToken;
            _expiresOn = Now().AddSeconds(response.ExpiresIn);

            _logger?.LogDebug("{statusCode} Auth Response: Token expires on {_expiresOn}", response.HttpStatusCode, _expiresOn.Value.ToString("dd/MM/yyyy HH:mm:ss"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }
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
}

using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Responses;
using System.Text.Json;

namespace Nexus.SDK.Shared.Requests;

public class RequestBuilder<T> where T : class
{
    private readonly List<string> _segments;
    private readonly IAuthProvider _authProvider;
    private readonly UriBuilder _uriBuilder;
    private readonly ResponseHandler _responseHandler;
    private readonly HttpClient _httpClient;

    private bool _segmentsAdded;
    protected ILogger<RequestBuilder<T>>? _logger;

    public RequestBuilder(Uri? serverUri, HttpClient httpClient, IAuthProvider authProvider,
                          ILogger<RequestBuilder<T>>? logger = null)
    {
        if (serverUri is null)
        {
            throw new ArgumentNullException(nameof(serverUri));
        }

        _uriBuilder = new UriBuilder(serverUri);
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        _segments = new List<string>();
        _segmentsAdded = false; //Allow overwriting segments
        _authProvider = authProvider ?? throw new ArgumentNullException(nameof(authProvider));
        _responseHandler = new ResponseHandler(logger);
        _logger = logger;
    }

    protected async Task<TResponse> ExecuteGet<TResponse>() where TResponse : class
    {
        await AuthenticateAsync();

        var uri = BuildUri();

        _logger?.LogDebug("GET {uri}", uri.ToString());

        var response = await _httpClient.GetAsync(uri);
        return await _responseHandler.HandleResponse<TResponse>(response);
    }

    protected async Task<TResponse> ExecutePost<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
    {
        await AuthenticateAsync();

        var json = JsonSerializer.Serialize(request);
        var uri = BuildUri();

        _logger?.LogDebug("POST to {uri}: {json}", uri.ToString(), json);

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, content);

        return await _responseHandler.HandleResponse<TResponse>(response);
    }

    protected async Task ExecutePost<TRequest>(TRequest request) where TRequest : class
    {
        await AuthenticateAsync();

        var json = JsonSerializer.Serialize(request);
        var uri = BuildUri();

        _logger?.LogDebug("POST to {uri}: {json}", uri.ToString(), json);

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, content);

        await _responseHandler.HandleResponse(response);
    }

    protected async Task<TResponse> ExecutePut<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
    {
        await AuthenticateAsync();

        var json = JsonSerializer.Serialize(request);
        var uri = BuildUri();

        _logger?.LogDebug("PUT to {uri}: {json}", uri.ToString(), json);

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(uri, content);

        return await _responseHandler.HandleResponse<TResponse>(response);
    }

    protected RequestBuilder<T> SetSegments(params string[] segments)
    {
        if (_segmentsAdded)
            throw new Exception("URL segments have already been added.");

        _segmentsAdded = true;

        //Remove default segments
        _segments.Clear();

        foreach (var segment in segments)
            _segments.Add(segment);

        return this;
    }

    private async Task AuthenticateAsync()
    {
        await _authProvider.AuthenticateAsync(_httpClient);
    }

    private Uri BuildUri()
    {
        if (_segments.Count > 0)
        {
            var path = "";

            foreach (var segment in _segments)
                path += "/" + segment;

            _uriBuilder.Path = path;

            try
            {
                // After building the url we reset the segments for the next request.
                _segments.Clear();
                _segmentsAdded = false;
                return _uriBuilder.Uri;
            }
            catch (UriFormatException)
            {
                throw;
            }
        }

        throw new NotSupportedException("No segments defined.");
    }
}

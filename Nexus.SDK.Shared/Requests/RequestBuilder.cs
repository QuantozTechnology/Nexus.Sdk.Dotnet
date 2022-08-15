using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Responses;
using System.Text.Json;
using System.Web;

namespace Nexus.SDK.Shared.Requests;

public class RequestBuilder<T> where T : class
{
    private readonly List<string> _segments;
    private readonly IAuthProvider _authProvider;
    private readonly UriBuilder _uriBuilder;
    private readonly ResponseHandler _responseHandler;
    private readonly HttpClient _httpClient;

    private bool _segmentsAdded;
    private bool _queryParametersAdded;
    private IDictionary<string, string>? _queryParameters;
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

        foreach (var segment in segments)
            _segments.Add(segment);

        return this;
    }

    protected RequestBuilder<T> SetQueryParameters(IDictionary<string, string> queryParameters)
    {
        if (_queryParametersAdded)
            throw new Exception("Query parameters have already been added.");

        _queryParametersAdded = true;
        _queryParameters = queryParameters;

        return this;
    }

    private async Task AuthenticateAsync()
    {
        try
        {
            await _authProvider.AuthenticateAsync(_httpClient);
        }
        catch (AuthProviderException ex)
        {
            ResetSegments();
            ResetQueryParameters();
            throw ex;
        }
    }

    private void ResetSegments()
    {
        if (_segmentsAdded)
        {
            _segments.Clear();
            _segmentsAdded = false;
        }
    }

    private void ResetQueryParameters()
    {
        if (_queryParametersAdded)
        {
            _queryParameters?.Clear();
            _queryParametersAdded = false;
        }
    }

    private Uri BuildUri()
    {
        if (_segments.Any())
        {
            var path = "";

            foreach (var segment in _segments)
                path += "/" + segment;

            _uriBuilder.Path = path;

            if (_queryParameters != null && _queryParameters.Any())
            {
                string getQueryString = BuildQuerystring(_queryParameters);
                _uriBuilder.Query = getQueryString;
            }

            try
            {
                // After building the url we reset the segments for the next request.
                ResetSegments();
                ResetQueryParameters();
                return _uriBuilder.Uri;
            }
            catch (UriFormatException)
            {
                throw;
            }
        }

        throw new NotSupportedException("No segments defined.");
    }

    private string BuildQuerystring(IDictionary<string, string> querystringParams)
    {
        List<string> paramList = new List<string>();

        foreach (var parameter in querystringParams)
        {
            paramList.Add(parameter.Key + "=" + parameter.Value);
        }

        return string.Join("&", paramList);
    }

    protected void AddDefaultRequestHeader(string key, string value)
    {
        _httpClient.DefaultRequestHeaders.Add(key, value);
    }
}

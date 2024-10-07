using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Nexus.Sdk.Shared.Http;

public class RequestBuilder
{
    private readonly HttpClient _httpClient;
    private readonly IResponseHandler _responseHandler;
    private readonly ILogger? _logger;

    private bool _segmentsAdded;
    private bool _queryParametersAdded;
    private bool _headersAdded;

    private readonly List<string> _segments;
    private IDictionary<string, string> _queryParameters;
    private IDictionary<string, string> _headers;

    public RequestBuilder(HttpClient httpClient, IResponseHandler responseHandler, ILogger? logger)
    {
        _httpClient = httpClient;
        _responseHandler = responseHandler;
        _logger = logger;

        _segments = new List<string>();
        _queryParameters = new Dictionary<string, string>();
        _headers = new Dictionary<string, string>();
        _segmentsAdded = false; //Allow overwriting segments
        _queryParametersAdded = false;
        _headersAdded = false;
    }

    public async Task<TResponse> ExecuteGet<TResponse>() where TResponse : class
    {
        var path = BuildPath();
        Uri requestUri = new Uri(_httpClient.BaseAddress!, path);

        _logger?.LogDebug("GET {uri}", path);

        var getRequest = HttpRequestBuilder.BuildGetRequest(requestUri, _headers);

        var response = await _httpClient.SendAsync(getRequest);

        ResetHeaders(); // reset headers

        return await _responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task ExecutePost<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : class
    {
        var json = JsonSerializer.Serialize(request);
        var path = BuildPath();
        Uri requestUri = new Uri(_httpClient.BaseAddress!, path);

        _logger?.LogDebug("POST to {path}: {json}", path, json);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postRequest = HttpRequestBuilder.BuildPostRequest(requestUri, content, _headers);
        var response = await _httpClient.SendAsync(postRequest, cancellationToken);

        ResetHeaders(); // reset headers

        await _responseHandler.HandleResponse(response, cancellationToken);
    }

    public async Task<TResponse> ExecutePost<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
    {
        var json = JsonSerializer.Serialize(request);
        var path = BuildPath();
        Uri requestUri = new Uri(_httpClient.BaseAddress!, path);

        _logger?.LogDebug("POST to {path}: {json}", path, json);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postRequest = HttpRequestBuilder.BuildPostRequest(requestUri, content, _headers);
        var response = await _httpClient.SendAsync(postRequest);

        ResetHeaders(); // reset headers

        return await _responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task<TResponse> ExecutePut<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
    {
        var json = JsonSerializer.Serialize(request);
        var path = BuildPath();
        Uri requestUri = new Uri(_httpClient.BaseAddress!, path);

        _logger?.LogDebug("PUT to {path}: {json}", path, json);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postRequest = HttpRequestBuilder.BuildPutRequest(requestUri, content, _headers);
        var response = await _httpClient.SendAsync(postRequest);

        ResetHeaders(); // reset headers

        return await _responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task<TResponse> ExecutePut<TResponse>() where TResponse : class
    {
        var path = BuildPath();
        Uri requestUri = new Uri(_httpClient.BaseAddress!, path);

        _logger?.LogDebug("PUT to {path}", path);

        var putRequest = HttpRequestBuilder.BuildPutRequest(requestUri, _headers);
        var response = await _httpClient.SendAsync(putRequest);

        ResetHeaders(); // reset headers

        return await _responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task<TResponse> ExecuteDelete<TResponse>() where TResponse : class
    {
        var path = BuildPath();
        Uri requestUri = new Uri(_httpClient.BaseAddress!, path);

        _logger?.LogDebug("DELETE {uri}", path);

        var deleteRequest = HttpRequestBuilder.BuildDeleteRequest(requestUri, _headers);
        var response = await _httpClient.SendAsync(deleteRequest);

        ResetHeaders(); // reset headers

        return await _responseHandler.HandleResponse<TResponse>(response);
    }

    public RequestBuilder SetSegments(params string[] segments)
    {
        if (_segmentsAdded)
            throw new Exception("URL segments have already been added.");

        _segmentsAdded = true;

        foreach (var segment in segments)
            _segments.Add(segment);

        return this;
    }

    public RequestBuilder SetQueryParameters(IDictionary<string, string> queryParameters)
    {
        if (_queryParametersAdded)
            throw new Exception("Query parameters have already been set.");

        _queryParametersAdded = true;
        _queryParameters = queryParameters;

        return this;
    }

    public RequestBuilder AddQueryParameter(string key, string value)
    {
        _queryParametersAdded = true;
        _queryParameters.Add(key, value);

        return this;
    }

    public RequestBuilder SetHeaders(IDictionary<string, string> headers)
    {
        if (_headersAdded)
            throw new Exception("Headers have already been set.");

        _headersAdded = true;
        _headers = headers;

        return this;
    }

    public RequestBuilder AddHeader(string key, string value)
    {
        _headersAdded = true;
        _headers.Add(key, value);

        return this;
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

    private void ResetHeaders()
    {
        if (_headersAdded)
        {
            _headers?.Clear();
            _headersAdded = false;
        }
    }

    private string BuildPath()
    {
        var builder = new StringBuilder();

        if (_segments.Any())
        {
            foreach (var segment in _segments)
            {
                builder.Append('/');
                builder.Append(segment);
            }

            if (_queryParameters != null && _queryParameters.Any())
            {
                string query = BuildQuerystring(_queryParameters);
                builder.Append('?');
                builder.Append(query);
            }

            // After building the url we reset the segments for the next request.
            ResetSegments();

            // After building the url we reset the query parameters for the next request.
            ResetQueryParameters();

            return builder.ToString();
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
}

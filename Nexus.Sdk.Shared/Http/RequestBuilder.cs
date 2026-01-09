using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Nexus.Sdk.Shared.Http;

public class RequestBuilder(HttpClient httpClient, IResponseHandler responseHandler, ILogger? logger, IDictionary<string, string>? initialHeaders = null)
{
    private bool _segmentsAdded = false;
    private bool _queryParametersAdded = false;
    private bool _headersAdded = false;

    private readonly List<string> _segments = [];
    private IDictionary<string, string> _queryParameters = new Dictionary<string, string>();
    private IDictionary<string, string> _headers = initialHeaders != null ? new Dictionary<string, string>(initialHeaders) : [];

    public async Task<TResponse> ExecuteGet<TResponse>() where TResponse : class
    {
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("GET {uri}", path);

        var getRequest = HttpRequestBuilder.BuildGetRequest(requestUri, _headers);

        var response = await httpClient.SendAsync(getRequest);

        ResetHeaders(); // reset headers

        return await responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task<Stream> ExecuteGetStream()
    {
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("GET {uri}", path);

        var getRequest = HttpRequestBuilder.BuildGetRequest(requestUri, _headers);

        var response = await httpClient.SendAsync(getRequest);

        ResetHeaders(); // reset headers

        return await responseHandler.HandleResponseStream(response);
    }

    public async Task ExecutePost<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : class
    {
        var json = JsonSerializer.Serialize(request);
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("POST to {path}: {json}", path, json);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postRequest = HttpRequestBuilder.BuildPostRequest(requestUri, content, _headers);
        var response = await httpClient.SendAsync(postRequest, cancellationToken);

        ResetHeaders(); // reset headers

        await responseHandler.HandleResponse(response, cancellationToken);
    }

    public async Task<TResponse> ExecutePost<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
    {
        var json = JsonSerializer.Serialize(request);
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("POST to {path}: {json}", path, json);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postRequest = HttpRequestBuilder.BuildPostRequest(requestUri, content, _headers);
        var response = await httpClient.SendAsync(postRequest);

        ResetHeaders(); // reset headers

        return await responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task<TResponse> ExecutePost<TResponse>(MultipartFormDataContent request) where TResponse : class
    {
        var json = JsonSerializer.Serialize(request);
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("POST to {path}: {json}", path, json);

        var postRequest = HttpRequestBuilder.BuildPostRequest(requestUri, request, _headers);
        var response = await httpClient.SendAsync(postRequest);

        ResetHeaders(); // reset headers

        return await responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task<TResponse> ExecutePut<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
    {
        var json = JsonSerializer.Serialize(request);
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("PUT to {path}: {json}", path, json);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postRequest = HttpRequestBuilder.BuildPutRequest(requestUri, content, _headers);
        var response = await httpClient.SendAsync(postRequest);

        ResetHeaders(); // reset headers

        return await responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task<TResponse> ExecutePut<TResponse>() where TResponse : class
    {
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("PUT to {path}", path);

        var putRequest = HttpRequestBuilder.BuildPutRequest(requestUri, _headers);
        var response = await httpClient.SendAsync(putRequest);

        ResetHeaders(); // reset headers

        return await responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task<TResponse> ExecuteDelete<TResponse>() where TResponse : class
    {
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("DELETE {uri}", path);

        var deleteRequest = HttpRequestBuilder.BuildDeleteRequest(requestUri, _headers);
        var response = await httpClient.SendAsync(deleteRequest);

        ResetHeaders(); // reset headers

        return await responseHandler.HandleResponse<TResponse>(response);
    }

    public async Task ExecuteDelete<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : class
    {
        var json = JsonSerializer.Serialize(request);
        var path = BuildPath();
        Uri requestUri = new(httpClient.BaseAddress!, path);

        logger?.LogDebug("DELETE to {path}: {json}", path, json);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var deleteRequest = HttpRequestBuilder.BuildDeleteRequest(requestUri, content, _headers);
        var response = await httpClient.SendAsync(deleteRequest, cancellationToken);

        ResetHeaders();

        await responseHandler.HandleResponse(response, cancellationToken);
    }

    public RequestBuilder SetSegments(params string[] segments)
    {
        if (_segmentsAdded)
        {
            throw new Exception("URL segments have already been added.");
        }

        _segmentsAdded = true;

        foreach (var segment in segments)
        {
            _segments.Add(segment);
        }

        return this;
    }

    public RequestBuilder SetQueryParameters(IDictionary<string, string> queryParameters)
    {
        if (_queryParametersAdded)
        {
            throw new Exception("Query parameters have already been set.");
        }

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
        {
            throw new Exception("Headers have already been set.");
        }

        _headersAdded = true;
        _headers = headers;

        return this;
    }

    /// <summary>
    /// Adds a header to the request if the value is not null or whitespace.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public RequestBuilder AddHeader(string key, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            _headersAdded = true;
            _headers.Add(key, value);
        }
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

        if (_segments.Count != 0)
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

    private static string BuildQuerystring(IDictionary<string, string> querystringParams)
    {
        List<string> paramList = [];

        foreach (var parameter in querystringParams)
        {
            paramList.Add(parameter.Key + "=" + parameter.Value);
        }

        return string.Join("&", paramList);
    }
}

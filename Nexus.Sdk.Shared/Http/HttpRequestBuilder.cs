public class HttpRequestBuilder
{
    private readonly Uri _path;
    private readonly HttpContent _content;
    private readonly IDictionary<string, string> _headers;
    private readonly HttpMethod _method;

    private HttpRequestBuilder(Uri path, HttpContent content, IDictionary<string, string> headers, HttpMethod method)
    {
        _path = path;
        _content = content;
        _headers = headers;
        _method = method;
    }

    public static HttpRequestMessage BuildPostRequest(Uri path, HttpContent content, IDictionary<string, string> headers)
    {
        return new HttpRequestBuilder(path, content, headers, HttpMethod.Post)
            .BuildRequest();
    }

    public static HttpRequestMessage BuildPutRequest(Uri path, HttpContent content, IDictionary<string, string> headers)
    {
        return new HttpRequestBuilder(path, content, headers, HttpMethod.Put)
            .BuildRequest();
    }

    public static HttpRequestMessage BuildPutRequest(Uri path, IDictionary<string, string> headers)
    {
        return new HttpRequestBuilder(path, null, headers, HttpMethod.Put)
            .BuildRequest();
    }

    public static HttpRequestMessage BuildGetRequest(Uri path, IDictionary<string, string> headers)
    {
        return new HttpRequestBuilder(path, null, headers, HttpMethod.Get)
            .BuildRequest();
    }

    public static HttpRequestMessage BuildDeleteRequest(Uri path, IDictionary<string, string> headers)
    {
        return new HttpRequestBuilder(path, null, headers, HttpMethod.Delete)
            .BuildRequest();
    }

    private HttpRequestMessage BuildRequest()
    {
        var request = new HttpRequestMessage
        {
            Method = _method,
            Content = _content,
            RequestUri = _path
        };

        foreach (KeyValuePair<string, string> header in _headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        return request;
    }
}
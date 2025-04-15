using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nexus.Sdk.Token.API.Models;
using Nexus.Sdk.Token.API.Models.Response;

namespace Nexus.Sdk.Token.API;

public class NexusAPIService(INexusApiClientFactory nexusApiClientFactory)
    : INexusApiService
{
    private readonly Dictionary<string, string> _headers = [];
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true
    };

    private async Task<HttpClient> GetApiClient(string apiVersion)
    {
        var client = await nexusApiClientFactory.GetClient(apiVersion);

        foreach (var header in _headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        return client;
    }

    private static async Task HandleErrorResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadFromJsonAsync<CustomResultHolder<T>>();

        var exception = content?.Errors is { Length: > 0 }
            ? new NexusApiException($"Request failed: {content.Errors.Aggregate((a, b) => a + ", " + b)}")
            : new NexusApiException($"Request failed: {response.ReasonPhrase} ({(int)response.StatusCode})");

        exception.StatusCode = response.StatusCode;
        exception.ResponseContent = await response.Content.ReadAsStringAsync();

        throw exception;
    }

    public NexusAPIService AddHeader(string key, string value)
    {
        _headers.Add(key, value);
        return this;
    }

    private async Task<TResponse> PostAsync<TInput, TResponse>(string endPoint, TInput? postObject, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.PostAsJsonAsync(endPoint, postObject);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<TResponse>(httpResponse);
        }

        return (await httpResponse.Content.ReadFromJsonAsync<TResponse>(options: _serializerOptions))!;
    }

    private async Task<T2> PostAsync<T2>(string endPoint, string apiVersion)
    {
        return await PostAsync<object, T2>(endPoint, null, apiVersion);
    }

    private async Task<T> GetAsync<T>(string endPoint, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.GetAsync(endPoint);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<T>(httpResponse);
        }

        return (await httpResponse.Content.ReadFromJsonAsync<T>(options: _serializerOptions))!;
    }

    /// <summary>
    /// Take Dictionary of query parameters and creates the query string to paste to the URI.
    /// Prepends the '?'. When the dictionary is empty, returns an empty string;
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    private static string CreateUriQuery(Dictionary<string, string> queryParams)
    {
        var query = string.Empty;

        foreach (var p in queryParams)
        {
            if (query == string.Empty)
            {
                query += "?";
            }
            else
            {
                query += "&";
            }

            query += $"{p.Key}={p.Value}";
        }

        return query;
    }

    public async Task<CustomResultHolder> CreateDocumentStore(Dictionary<string, string> queryParams)
    {
        return await PostAsync<CustomResultHolder>($"/integrations/documentstore{CreateUriQuery(queryParams)}", "1.2");
    }

    public async Task<CustomResultHolder<DocumentStoreSettingsResponse>> GetDocumentStore()
    {
        return await GetAsync<CustomResultHolder<DocumentStoreSettingsResponse>>("/integrations/documentstore", "1.2");
    }
}
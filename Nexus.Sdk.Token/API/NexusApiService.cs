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

        return (await httpResponse.Content.ReadFromJsonAsync<TResponse>(options: _serializerOptions))!;
    }
    private async Task<TResponse> PostContentAsync<TResponse>(string endPoint, HttpContent content, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.PostAsync(endPoint, content);

        return (await httpResponse.Content.ReadFromJsonAsync<TResponse>(options: _serializerOptions))!;
    }


    private async Task<T> PostAsync<T>(string endPoint, string apiVersion)
    {
        return await PostAsync<object, T>(endPoint, null, apiVersion);
    }

    private async Task<T> GetAsync<T>(string endPoint, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.GetAsync(endPoint);

        return (await httpResponse.Content.ReadFromJsonAsync<T>(options: _serializerOptions))!;
    }

    private async Task<T> DeleteAsync<T>(string endPoint, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.DeleteAsync(endPoint);

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

    public async Task<CustomResultHolder> CreateDocumentStore(DocumentStoreSettings documentStoreSettings)
    {
        return await PostAsync<DocumentStoreSettings, CustomResultHolder>($"/integrations/documentstore", documentStoreSettings, "1.2");
    }

    public async Task<CustomResultHolder<DocumentStoreSettingsResponse>> GetDocumentStore()
    {
        return await GetAsync<CustomResultHolder<DocumentStoreSettingsResponse>>("/integrations/documentstore", "1.2");
    }

    public async Task<CustomResultHolder> DeleteDocumentStore()
    {
        return await DeleteAsync<CustomResultHolder>("/integrations/documentstore", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<DocumentStoreItemResponse>>> GetDocumentStoreList(Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<DocumentStoreItemResponse>>>($"integrations/documentstore/list{CreateUriQuery(queryParams)}", "1.2");
    }

    public async Task<CustomResultHolder> AddDocumentToStore(DocumentFileUploadData fileData)
    {
        using var formContent = new MultipartFormDataContent();

        // Add file content
        using var fileStream = fileData.File.OpenReadStream();
        using var fileContent = new StreamContent(fileStream);
        formContent.Add(fileContent, "file", fileData.File.FileName);

        formContent.Add(new StringContent(fileData.FilePath), "filePath");

        if (!string.IsNullOrEmpty(fileData.Alias))
            formContent.Add(new StringContent(fileData.Alias), "alias");

        if (!string.IsNullOrEmpty(fileData.Description))
            formContent.Add(new StringContent(fileData.Description), "description");

        if (!string.IsNullOrEmpty(fileData.CustomerCode))
            formContent.Add(new StringContent(fileData.CustomerCode), "customerCode");

        if (!string.IsNullOrEmpty(fileData.ItemReference))
            formContent.Add(new StringContent(fileData.ItemReference), "itemReference");

        return await PostContentAsync<CustomResultHolder>($"/integrations/documentstore/file", formContent, "1.2");
    }
}
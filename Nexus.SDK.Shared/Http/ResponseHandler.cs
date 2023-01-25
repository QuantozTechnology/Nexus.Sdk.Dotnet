﻿using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.ErrorHandling;
using System.Net;

namespace Nexus.SDK.Shared.Http;

public class ResponseHandler : IResponseHandler
{
    private readonly ILogger? _logger;

    public ResponseHandler(ILogger? logger = null)
    {
        _logger = logger;
    }

    public async Task<T> HandleResponse<T>(HttpResponseMessage response) where T : class
    {
        var statusCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync();

        _logger?.LogDebug("{statusCode} Response: {content}", statusCode, content);

        if ((int)statusCode >= 300)
        {
            _logger?.LogError("{statusCode} Response: {content}", statusCode, content);

            if (statusCode == HttpStatusCode.Unauthorized)
            {
                _logger?.LogWarning("Did you configure your authentication provider using ConnectTo");
            }

            throw new ApiException((int)statusCode, "An error response was returned, please check the logs for details");
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ApiException((int)statusCode, "No content to parse");
        }

        var responseObj = JsonSingleton.GetInstance<T>(content);

        if (responseObj == null)
        {
            throw new ApiException((int)statusCode, "Unable to parse response to JSON");
        }

        return responseObj;
    }

    public async Task HandleResponse(HttpResponseMessage response)
    {
        var statusCode = response.StatusCode;

        if ((int)statusCode >= 300)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                _logger?.LogWarning("Did you configure your authentication provider using ConnectTo?");
            }

            var content = await response.Content.ReadAsStringAsync();
            _logger?.LogError("Response: {content}", content);

            throw new ApiException((int)statusCode, "An error response was returned, please check the logs for details");
        }

        _logger?.LogDebug("{statusCode} Response", statusCode);
    }
}

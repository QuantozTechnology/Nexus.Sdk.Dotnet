using Microsoft.Extensions.Logging;
using Nexus.Sdk.Shared.ErrorHandling;
using Nexus.Sdk.Shared.Responses;
using System.Net;

namespace Nexus.Sdk.Shared.Http;

public class NexusResponseHandler : IResponseHandler
{
    private readonly ILogger? _logger;

    public NexusResponseHandler(ILogger? logger = null)
    {
        _logger = logger;
    }

    public async Task<T> HandleResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken = default) where T : class
    {
        var statusCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        _logger?.LogDebug("{statusCode} Response: {content}", statusCode, content);

        var responseObj = JsonSingleton.GetInstance<NexusResponse<T>>(content);

        if (responseObj == null)
        {
            throw new NexusApiException((int)statusCode, "Unable to parse Nexus response to JSON", null);
        }

        if ((int)statusCode >= 300)
        {
            _logger?.LogError("{statusCode} Response: {content}", statusCode, content);

            if (statusCode == HttpStatusCode.Unauthorized)
            {
                _logger?.LogWarning("Did you configure your authentication provider using ConnectTo");
            }

            throw new CustomErrorsException((int)statusCode, content, responseObj.Errors);
        }

        return responseObj.Values;
    }

    public async Task HandleResponse(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        var statusCode = response.StatusCode;

        if ((int)statusCode >= 300)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                _logger?.LogWarning("Did you configure your authentication provider using ConnectTo?");
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger?.LogError("Response: {content}", content);

            var responseObj = JsonSingleton.GetInstance<NexusResponse>(content);

            if (responseObj == null)
            {
                throw new NexusApiException((int)statusCode, "Unable to parse Nexus response to JSON", null);
            }
;
            throw new NexusApiException((int)statusCode, responseObj.Message, responseObj.Errors);
        }

        _logger?.LogDebug("{statusCode} Response", statusCode);
    }
}

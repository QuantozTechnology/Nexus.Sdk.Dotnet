using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Nexus.Sdk.Shared.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace Nexus.Sdk.Shared.ErrorHandling;

public class CustomerHttpExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is CustomErrorsException customErrorsException)
        {
            await HandleCustomErrorExceptionsAsync(httpContext, customErrorsException);

            return true;
        }
        
        return false;
    }

    public async Task HandleCustomErrorExceptionsAsync(HttpContext context, CustomErrorsException exception)
    {
        int statusCode = 500;

        if (exception.CustomErrors.HasErrors())
        {
            statusCode = 400;

            if (exception.CustomErrors.Errors.Count == 1)
            {
                var error = exception.CustomErrors.Errors[0];

                if (error.Message.Contains("NotFound"))
                {
                    statusCode = 404;
                }
                else if (error.Message.Contains("CustomerExists"))
                {
                    statusCode = 409;
                }
            }
        }
        else
        {
            exception.CustomErrors.AddError(new CustomError("InternalServerError", "CustomerErrorException was thrown but no errors are present", "Errors"));
        }

        await WriteCustomErrors(context.Response, exception.CustomErrors, statusCode);
    }

    private static async Task WriteCustomErrors(HttpResponse httpResponse, CustomErrors customErrors, int statusCode)
    {
        httpResponse.StatusCode = statusCode;
        httpResponse.ContentType = "application/json";

        var response = CustomErrorsResponse.FromCustomErrors(customErrors);
        var json = JsonSerializer.Serialize(response);
        await httpResponse.WriteAsync(json);
    }
}
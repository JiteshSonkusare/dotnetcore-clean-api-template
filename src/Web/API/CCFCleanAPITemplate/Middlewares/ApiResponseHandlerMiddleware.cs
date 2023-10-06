using System.Net;
using System.Text.Json;
using Domain.ViewModels;
using Application.Common.Exceptions;
using System.Security.Authentication;

namespace CCFCleanAPITemplate.Middlewares;

/// <summary>
/// This is the sample hanlder middleware class, you can create your own to handle your application responses.
/// </summary>
public class ApiResponseHandlerMiddleware : AbstractResponseHandlerMiddleware
{
    public ApiResponseHandlerMiddleware(RequestDelegate next) : base(next) { }

    public override (HttpStatusCode code, string message) HandleResponse(Exception exception)
    {
        var code = exception switch
        {
            AuthenticationException
                => HttpStatusCode.Unauthorized,
            GeneralApplicationException
            or ArgumentNullException
            or NullReferenceException
            or TaskCanceledException
            or ApiException
                => HttpStatusCode.BadRequest,
            InvalidOperationException
                => HttpStatusCode.BadGateway,
            _
                => HttpStatusCode.InternalServerError,
        };

        var failureResponse = GetFailureResponse(exception.Message);
        return (code, JsonSerializer.Serialize(failureResponse));
    }

    private static FailureResponse GetFailureResponse(string exception)
    {
        if (string.IsNullOrWhiteSpace(exception))
            return default!;

        var exceptionData = exception
                .Split('|')
                .Select(segment => segment.Split(':'))
                .Where(parts => parts.Length == 2 && !(parts[0] == "extensions" && parts[1].StartsWith("System.Collections.Generic.Dictionary")))
                .ToDictionary(parts => parts[0], parts => parts[1]);

        var failureResponse = new FailureResponse
        {
            Type = exceptionData.TryGetValue("type", out string? type) ? type : string.Empty,
            Status = exceptionData.TryGetValue("status", out string? status) ? status : string.Empty,
            Error = exceptionData.TryGetValue("error", out string? error) ? error : string.Empty,
            ErrorDescription = exceptionData.TryGetValue("error_description", out string? errorDescription) ? errorDescription : string.Empty,
        };

        if (exceptionData.ContainsKey("extensions"))
            failureResponse.Extensions["details"] = exceptionData.TryGetValue("extensions", out string? extensions) ? extensions : string.Empty;

        return failureResponse;
    }
}
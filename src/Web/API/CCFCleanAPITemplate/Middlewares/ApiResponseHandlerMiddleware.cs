using System.Net;
using System.Text.Json;
using Domain.ViewModels;
using Application.Common.Exceptions;
using System.Security.Authentication;

namespace CCFCleanAPITemplate.Middlewares;

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

        var failureResponse = GetFailureResponse(code, exception.Message);
        return (code, JsonSerializer.Serialize(failureResponse));
    }

    private static FailureResponse GetFailureResponse(HttpStatusCode code, string exception)
    {
        if (string.IsNullOrWhiteSpace(exception))
            return default!;

        var exceptionData = exception
                .Split('|')
                .Select(segment => segment.Split(':'))
                .ToDictionary(parts => parts[0], parts => parts[1]);

        var failureResponse = new FailureResponse
        {
            Status = (int)code,
            Source = exceptionData.TryGetValue("source", out string? source) ? source : string.Empty,
            Error = exceptionData.TryGetValue("error", out string? error) ? error : string.Empty,
            ErrorDescription = exceptionData.TryGetValue("error_description", out string? errorDescription) ? errorDescription : string.Empty,
        };

        return failureResponse;
    }
}
using NLog;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;

namespace CCFCleanAPITemplate.ExceptionHandler;

public class GlobalExceptionHandler() : IExceptionHandler
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails? problemDetails;
        exception.AddErrorCode();

        if (exception is ValidationException validationException)
        {
            problemDetails = CreateValidationProblemDetails(httpContext, validationException);
        }
        else
        {
            problemDetails = CreateProblemDetails(httpContext, exception);
        }

        var json = MiddlewareExtensions.ToJson(problemDetails, _logger);
        httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;
        _logger.Error(json);
        await httpContext.Response.WriteAsync(json, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
    {
        var errorCode = exception.GetErrorCode();
        var statusCode = context.Response.StatusCode;
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode) ?? UnhandledExceptionMsg;

        var problemDetails = new ProblemDetails
        {
            Title = reasonPhrase,
            Status = statusCode,
            Instance = context.Request.Path,
            Extensions = {
                [nameof(errorCode)] = errorCode
            }
        };
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;
        problemDetails.Detail = exception.Message;
        problemDetails.Extensions["errorDescription"] = exception.Data;

        return problemDetails;
    }

    private static ProblemDetails CreateValidationProblemDetails(in HttpContext context, in ValidationException exception)
    {
        var errorCode = exception.GetErrorCode();

        var problemDetails = new ProblemDetails
        {
            Title = "Validation Failed",
            Status = StatusCodes.Status400BadRequest,
            Instance = context.Request.Path,
            Extensions = {
                [nameof(errorCode)] = errorCode
            }
        };
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;
        problemDetails.Detail = exception.Message.ToString();
        problemDetails.Extensions["errorDescription"] = exception.Errors;

        return problemDetails;
    }
}
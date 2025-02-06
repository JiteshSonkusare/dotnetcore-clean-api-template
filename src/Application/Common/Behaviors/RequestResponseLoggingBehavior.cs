using NLog;
using MediatR;
using Shared.Wrapper;
using System.Text.Json;
using Domain.Configs.MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Application.Common.Interfaces;
using Domain.Resources.GlobalHeaders;
using Microsoft.Extensions.Primitives;

namespace Application.Common.Behaviors;

public class RequestResponseLoggingBehavior<TRequest, TResponse>(
    IDateTimeService dateTimeService,
    IHttpContextAccessor httpContextAccessor,
    IOptions<BehaviourLoggingConfig> behaviourLoggingConfig)
    : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
      where TResponse : Result
{
    private readonly  IDateTimeService _dateTimeService = dateTimeService;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly BehaviourLoggingConfig _behaviourLoggingConfig = behaviourLoggingConfig.Value;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var (traceId, timestamp) = GetTraceIdAndTimestamp();

        if (_behaviourLoggingConfig.Enable)
        {
            // Request Logging
            var requstestUri = $"RequestUri: " +
                $"{httpContext?.Request.Method} " +
                $"{httpContext?.Request.Scheme}://{httpContext?.Request.Host}{httpContext?.Request.Path}   " +
                $"Time: {timestamp}";
            var requstestUriJson = JsonSerializer.Serialize(requstestUri);
            _logger.Info($"Executing request: traceId: {traceId}: requestUri: {requstestUriJson}");

            var requestJson = JsonSerializer.Serialize(request);
            _logger.Info($"Handling request traceId: {traceId}: request: {requestJson}");
        }

        var result = await next();

        //Error Logging
        if (result.IsFailure)
            _logger.Error($"Request failure " +
                $"{typeof(TRequest).Name}, " +
                $"traceId: {traceId}, " +
                $"timeStamp: {timestamp}, " +
                $"error: {result.Error}");

        if (_behaviourLoggingConfig.Enable)
        {
            // Response Logging
            var responseJson = JsonSerializer.Serialize(result);
            _logger.Info($"Response for traceId: {traceId}: response: {responseJson}");
        }

        return result;
    }


    private (string TraceId, string Timestamp) GetTraceIdAndTimestamp()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        string traceId = Guid.NewGuid().ToString();
        string timestamp = _dateTimeService.Now.ToString();

        if (httpContext?.Request?.Headers != null)
        {
            if (httpContext.Request.Headers.TryGetValue(nameof(HeaderName.TraceId), out StringValues headerTraceId) &&
                !string.IsNullOrWhiteSpace(headerTraceId))
            {
                traceId = headerTraceId.ToString();
            }

            if (httpContext.Request.Headers.TryGetValue(nameof(HeaderName.Timestamp), out StringValues headerTimestamp) &&
                !string.IsNullOrWhiteSpace(headerTimestamp))
            {
                timestamp = headerTimestamp.ToString();
            }
        }

        return (traceId, timestamp);
    }
}
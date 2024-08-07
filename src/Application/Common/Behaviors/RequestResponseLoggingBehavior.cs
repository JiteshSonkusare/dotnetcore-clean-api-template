﻿using NLog;
using MediatR;
using Shared.Wrapper;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Application.Common.Interfaces;

namespace Application.Common.Behaviors;

public class RequestResponseLoggingBehavior<TRequest, TResponse>(IDateTimeService dateTimeService, IHttpContextAccessor httpContextAccessor)
	: IPipelineBehavior<TRequest, TResponse>
	  where TRequest : IRequest<TResponse>
	  where TResponse : Result
{
	private readonly IDateTimeService _dateTimeService = dateTimeService;
	private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var correlationId = Guid.NewGuid();
		var httpContext = _httpContextAccessor.HttpContext;

		// Request Logging
		var requstestUri = $"RequestUri: " +
			$"{httpContext?.Request.Method} " +
			$"{httpContext?.Request.Scheme}://{httpContext?.Request.Host}{httpContext?.Request.Path}   " +
			$"Time: {_dateTimeService.Now}";
		var requstestUriJson = JsonSerializer.Serialize(requstestUri);
		_logger.Info($"Executing request: {correlationId}: {requstestUriJson}");

		var requestJson = JsonSerializer.Serialize(request);
		_logger.Info($"Handling request {correlationId}: {requestJson}");

		var result = await next();

		//Error Logging
		if (result.IsFailure)
			_logger.Error($"Request failure " +
				$"{typeof(TRequest).Name}, " +
				$"{result.Error}, " +
				$"{_dateTimeService.Now}");

		// Log response
		var responseJson = JsonSerializer.Serialize(result);
		_logger.Info($"Response for {correlationId}: {responseJson}");

		return result;
	}
}
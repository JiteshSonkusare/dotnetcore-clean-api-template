using Polly;
using Polly.Retry;
using Polly.Fallback;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.ResilliencePolicies;

public static class ResillienceExtension
{
	public static IServiceCollection ResilliencePipelineExtension(this IServiceCollection services)
	{
		services.AddResiliencePipeline("default", x =>
		{
			x.AddRetry(new RetryStrategyOptions
			{
				ShouldHandle = new PredicateBuilder().Handle<Exception>(),
				MaxRetryAttempts = 3,
				Delay = TimeSpan.FromSeconds(5),
				BackoffType = DelayBackoffType.Exponential,
				UseJitter = true
			})
			.AddTimeout(TimeSpan.FromSeconds(30));
		});

		return services;
	}

	public static void AddResilliencePipelineExtension(this IHttpClientBuilder builder, bool useRetry = false)
	{
		if (useRetry)
		{
			builder.AddResilienceHandler("ApiClient", pipelineBuilder =>
			{
				pipelineBuilder.AddRetry(new HttpRetryStrategyOptions
				{
					ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
										.Handle<HttpRequestException>()
										.HandleResult(r => !r.IsSuccessStatusCode),
					MaxRetryAttempts = 3,
					Delay = TimeSpan.FromSeconds(5),
					BackoffType = DelayBackoffType.Exponential,
					UseJitter = true
				})
				.AddTimeout(new HttpTimeoutStrategyOptions
				{
					Timeout = TimeSpan.FromSeconds(30)
				})
				.AddFallback(new FallbackStrategyOptions<HttpResponseMessage>
				{
					FallbackAction = _ => Outcome.FromResultAsValueTask(new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable))
				})
				.AddConcurrencyLimiter(100, 200)
				.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
				{
					SamplingDuration = TimeSpan.FromSeconds(30),
					MinimumThroughput = 10,
					BreakDuration = TimeSpan.FromSeconds(60),
				});
			});
		}
	}
}

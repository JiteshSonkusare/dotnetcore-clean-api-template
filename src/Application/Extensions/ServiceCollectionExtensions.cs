using FluentValidation;
using System.Reflection;
using Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ApplicationDependencies(this IServiceCollection services)
    {

		return services.AddAutoMapper(Assembly.GetExecutingAssembly())
						.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
						.AddMediatR(cfg =>
						{
							cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
							cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
							cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
						})
						.AddLazyCache();
    }
}
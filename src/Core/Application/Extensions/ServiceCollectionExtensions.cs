using MediatR;
using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ApplicationDependencies(this IServiceCollection services)
    {
        return services.AddAutoMapper(Assembly.GetExecutingAssembly())
                       .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                       .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                       .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                       .AddLazyCache();
    }
}
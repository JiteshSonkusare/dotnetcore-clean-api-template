using Microsoft.Extensions.DependencyInjection;

namespace Shared.ApiClientHanlder;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register this service to use api client hanlder.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection RegisterSharedDependencies(this IServiceCollection services)
    {
        return services.AddTransient<IApiClientWrapper, ApiClientWrapper>()
                       .AddHttpClient();
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace Shared.ApiClientHanlder;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterSharedDependencies(this IServiceCollection services)
    {
        return services.AddTransient<IApiClientWrapper, ApiClientWrapper>()
                       .AddHttpClient();
    }
}
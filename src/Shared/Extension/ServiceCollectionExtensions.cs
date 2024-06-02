using Microsoft.Extensions.DependencyInjection;
using Shared.ApiClientHanlder;

namespace Shared.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SharedDependencies(this IServiceCollection services)
    {
        return services.AddTransient<IApiClientWrapper, ApiClientWrapper>()
                       .AddHttpClient();
    }
}
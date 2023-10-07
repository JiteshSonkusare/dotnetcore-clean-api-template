using Infrastructure.Services;
using Infrastructure.Respositories;
using Application.Common.Interfaces;
using Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services)
    {
        return services
                .AddTransient<IDateTimeService, SystemDateTimeService>()
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
    }
}
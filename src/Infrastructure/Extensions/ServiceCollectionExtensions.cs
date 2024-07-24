using Infrastructure.Respositories;
using Application.Common.Interfaces;
using Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services.Common;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection InfrastructureDependencies(this IServiceCollection services)
    {
        return services
                .AddTransient<IDateTimeService, SystemDateTimeService>()
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
    }
}
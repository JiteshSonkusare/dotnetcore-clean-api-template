using NLog.Web;
using Infrastructure.Context;
using Application.Extensions;
using Shared.ApiClientHanlder;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using CCFCleanAPITemplate.Middlewares;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.EndpointDefinition.Models;

namespace CCFCleanAPITemplate.Extensions;

public class ServiceCollectionExtensionsEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(AppBuilderDefinition builderDefination)
    {
        builderDefination.App.UseMiddleware<ApiResponseHandlerMiddleware>()
                             .UseCors();

        if (builderDefination.App.Environment.IsDevelopment())
            builderDefination.App.Services.CreateScope()
                .ServiceProvider.GetRequiredService<ApplicationDBContext>()
                .Database.EnsureCreated();
    }

    public void DefineServices(WebApplicationBuilder builder)
    {
        builder.Host.UseNLog();
        builder.Logging.ClearProviders().SetMinimumLevel(LogLevel.Trace);
        builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")))
                        .AddCors(options =>
                        {
                            options.AddDefaultPolicy(policy =>
                            {
                                policy.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                            });
                        })
                        .RegisterInfrastructureDependencies()
                        .RegisterApplicationDependencies()
                        .RegisterSharedDependencies();
    }
}
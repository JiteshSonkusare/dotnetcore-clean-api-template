using NLog.Web;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using CCFCleanAPITemplate.ExceptionHandler;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
	#region Log

	internal static WebApplicationBuilder LogDependencies(this WebApplicationBuilder builder)
	{
		builder.Host.UseNLog();
		builder.Logging.ClearProviders().SetMinimumLevel(LogLevel.Trace);

		return builder;
	}

	#endregion

	#region Exception Handler

	internal static IServiceCollection RegisterExceptionHandler(this IServiceCollection services)
	{
		return services.AddExceptionHandler<GlobalExceptionHandler>()
					   .AddProblemDetails();
	}

	#endregion

	#region "Cors"

	internal static IServiceCollection AddCorsDependencies(this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				policy.WithOrigins()
					  .AllowAnyMethod()
					  .AllowAnyHeader();
			});
		});

		return services;
	}

	internal static IApplicationBuilder UseCorsDependencies(this IApplicationBuilder app)
	{
		return app.UseCors();
	}

	#endregion

	#region Database

	internal static IServiceCollection RegisterDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		return services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
	}

	internal static IApplicationBuilder MigrateDatabase(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
			app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDBContext>().Database.EnsureCreated();

		return app;
	}

	#endregion

	#region Environment Configuration

	public static IConfigurationBuilder SetEnvironmentConfiguration(this WebApplicationBuilder builder)
	{
		return
			builder.Configuration
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();
	}

	#endregion
}
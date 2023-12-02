using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CCFCleanAPITemplate.Extensions;

public static class ServiceCollectionExtensions
{
	internal static IServiceCollection RegisterCorsDependencies(this IServiceCollection services)
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
}
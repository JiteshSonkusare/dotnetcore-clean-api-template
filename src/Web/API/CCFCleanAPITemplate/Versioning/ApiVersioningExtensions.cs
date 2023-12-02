using Asp.Versioning;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;

namespace CCFCleanAPITemplate.Versioning;

public enum ApiVersioningReaderEnum
{
    UrlSegment,
    QueryString,
    Header
}

public record DefineApiVersion(int MajorVersion, int MinorVersion, bool IsVersionDeprecated = false);

public static class ApiVersioningExtensions
{
    public static ApiVersionSet ApiVersionSet(this WebApplication app, IEnumerable<DefineApiVersion> apiVersions)
    {
        return app.NewApiVersionSet()
                  .WithApiVersions(apiVersions)
                  .ReportApiVersions()
                  .Build();
    }

    public static ApiVersionSetBuilder WithApiVersions(this ApiVersionSetBuilder builder, IEnumerable<DefineApiVersion> apiVersions)
    {
		foreach (var version in apiVersions)
        {
            builder.HasApiVersion(version.MajorVersion, version.MinorVersion);
            if (version.IsVersionDeprecated)
                builder.HasDeprecatedApiVersion(version.MajorVersion);
        }

		return builder;
    }

    public static ApiVersioningOptions AddSunsetPolicy(this ApiVersioningOptions options)
    {
        options.Policies
            .Sunset(1.0)
            .Effective(2022, 4, 1)
            .Link("https://localhost:5000/swagger/index.html")
            .Title("Versioning Policy")
            .Type("text/html")
            .Language("en");
        
        return options;
    }
}
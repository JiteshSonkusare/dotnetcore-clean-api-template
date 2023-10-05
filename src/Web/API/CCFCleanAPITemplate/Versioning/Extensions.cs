using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;

namespace CCFCleanAPITemplate.Versioning;

public record DefineApiVersion(int MajorVersion, int MinorVersion, bool IsVersionDeprecated = false);

public static class Extensions
{
    public static ApiVersionSet ApiVersionSet(this WebApplication app, List<DefineApiVersion> apiVersions)
    {
        return app.NewApiVersionSet()
                  .WithApiVersions(apiVersions)
                  .ReportApiVersions()
                  .Build();
    }

    public static ApiVersionSetBuilder WithApiVersions(this ApiVersionSetBuilder builder, List<DefineApiVersion> apiVersions)
    {
        foreach (var version in apiVersions)
        {
            builder.HasApiVersion(version.MajorVersion, version.MinorVersion);
            if (version.IsVersionDeprecated)
                builder.HasDeprecatedApiVersion(version.MajorVersion);
        }

        return builder;
    }
}
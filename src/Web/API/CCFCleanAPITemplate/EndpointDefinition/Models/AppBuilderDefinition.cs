using Asp.Versioning.Builder;

namespace CCFCleanAPITemplate.EndpointDefinition.Models;

public class AppBuilderDefinition
{
    public WebApplication App { get; set; } = null!;
    public ApiVersionSet ApiVersionSet { get; set; } = null!;
}
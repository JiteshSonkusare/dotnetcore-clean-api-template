namespace CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

/// <summary>
/// Send require configuration values. SecurityVersionExt, OpenApiInfoExt, ServerPathFilters
/// </summary>
public record OpenApiConfig
{
    /// <summary>
    /// Sends authentication parameters to swagger document.
    /// </summary>
    public SecurityExt? SecurityExt { get; set; }
    /// <summary>
    /// Sends swagger document details.
    /// </summary>
    public OpenApiInfoExt? OpenApiInfoExt { get; set; }
    /// <summary>
    /// Sends server details to swagger document.
    /// </summary>
    public ServerPathFilters? ServerPathFilters { get; set; }
}
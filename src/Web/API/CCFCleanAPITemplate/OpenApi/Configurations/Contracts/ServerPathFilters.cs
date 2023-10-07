namespace CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

public record ServerPathFilters
{
    // <summary>
    /// Set it to true for a standard list of URLs with environment names, or set it to false to apply a custom URL environment filter.
    /// </summary>
    public bool IsDevBasePath { get; set; }

    /// <summary>
    /// If IsDevBasePath is set to true, it is mandatory to send the environment name and URL as a list.
    /// </summary>
    public List<DevBasePathFilter>? DevBasePathFilter { get; set; }

    /// <summary>
    /// If IsDevBasePath is set to false, it is mandatory too send the environment names: {stest, atest, prod}
    /// </summary>
    public List<string>? EnvironmentNames { get; set; }

    /// <summary>
    /// If IsDevBasePath is set to false, sends the URL, which must contain the {Environment} placeholder, eg: {https://{Environment}google.com}.
    /// </summary>
    public string? URL { get; set; }
}

public record DevBasePathFilter
{
    /// <summary>
    /// Environment name eg: stest, atest, prod.
    /// </summary>
    public string? Environment { get; set; }

    /// <summary>
    /// Url related to environments.
    /// </summary>
    public string? Url { get; set; }
}
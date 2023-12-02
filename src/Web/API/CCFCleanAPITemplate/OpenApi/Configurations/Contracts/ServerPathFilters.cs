namespace CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

public record ServerPathFilters
{
    // <summary>
    /// Set it to true for a standard list of URLs with environment names, or set it to false to apply a custom URL environment filter.
    /// </summary>
    public bool IsBasePathListFilter { get; set; }

    /// <summary>
    /// If IsBasePathListFilter is set to true, it is mandatory to send the environment name and URL as a list.
    /// </summary>
    public IList<BasePathListFilter>? BasePathListFilter { get; set; }

    /// <summary>
    /// If IsBasePathListFilter is set to false, then you can use this customize server path filter.
    /// </summary>
    public CustomeBasePathFilter? CustomeBasePathFilter { get; set; }
}

public record BasePathListFilter
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

public record CustomeBasePathFilter
{
    /// <summary>
    /// If IsBasePathListFilter is set to false, it is mandatory too send the environment names: {stest, atest, prod}
    /// </summary>
    public IList<string>? EnvironmentNames { get; set; }

    /// <summary>
    /// If IsBasePathListFilter is set to false, sends the URL, which must contain the {Environment} placeholder, eg: {https://{Environment}.contoso.com}
    /// </summary>
    public string? URL { get; set; }
}
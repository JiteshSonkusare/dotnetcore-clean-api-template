namespace CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

public record OpenApiInfoExt
{
    /// <summary>
    /// Swagger document Title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Swagger document description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Api owner details with name, email, url
    /// </summary>
    public OpenApiContactExt? OpenApiContactExt { get; set; }
}

public record OpenApiContactExt
{
    /// <summary>
    /// Api owner name 
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Api owner email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Api owner url
    /// </summary>
    public string? Url { get; set; }
}
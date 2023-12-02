namespace CCFCleanAPITemplate.EndpointDefinition.CustomAttributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class EndpointDeprecatedAttribute : Attribute
{
	public string? Message { get; }

	public EndpointDeprecatedAttribute(string? message = null)
	{
		Message = message;
	}
}
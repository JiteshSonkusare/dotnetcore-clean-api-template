namespace CCFCleanAPITemplate.EndpointDefinition.CustomAttributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class EndpointDeprecateAttribute : Attribute
{
	public string? Message { get; }

	public EndpointDeprecateAttribute(string? message = null)
	{
		Message = message;
	}
}
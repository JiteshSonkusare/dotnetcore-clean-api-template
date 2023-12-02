namespace CCFCleanAPITemplate.EndpointDefinition.CustomAttributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class EndpointDefinitionDeprecateAttribute : Attribute
{
	public string? Message { get; }

	public EndpointDefinitionDeprecateAttribute(string? message = null)
	{
		Message = message;
	}
}
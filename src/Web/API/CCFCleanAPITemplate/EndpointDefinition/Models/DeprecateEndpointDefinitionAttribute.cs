namespace CCFCleanAPITemplate.EndpointDefinition.Models;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class DeprecateEndpointDefinitionAttribute : Attribute
{
	public string? Message { get; }

	public DeprecateEndpointDefinitionAttribute(string? message = null)
	{
		Message = message;
	}
}
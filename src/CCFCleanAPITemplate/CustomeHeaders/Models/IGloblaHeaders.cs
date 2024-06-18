namespace CCFCleanAPITemplate.CustomeHeaders;

public interface IGlobalHeaders
{
	Guid? TraceId { get; }
	string UserId { get; }
	string UserType { get; }

	void AddCustomHeader(string name, string value);
}
namespace Shared.ApiClientHanlder;

public class HeaderData
{
    public string Name { get; private set; }
    public string[] Values { get; private set; }

    public HeaderData(string name, params string[] values) : this(false, name, values) { }

    internal HeaderData(bool isResponseHeader, string name, params string[] values)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Header name cannot be null or empty.", nameof(name));
        }

        if (values == null || values.Length == 0)
        {
            throw new ArgumentException($"At least 1 value should be present for header '{name}'.", nameof(values));
        }

        if (!isResponseHeader && values.Any(v => string.IsNullOrWhiteSpace(v)))
        {
            throw new ArgumentException($"Header values cannot be null or empty if it's not a response header.", nameof(values));
        }

        Name = name;
        Values = values;
    }
}
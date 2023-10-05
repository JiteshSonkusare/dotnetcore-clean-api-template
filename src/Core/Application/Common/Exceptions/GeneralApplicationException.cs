using System.Runtime.Serialization;

namespace Application.Common.Exceptions;

[Serializable]
public class GeneralApplicationException : ApplicationException
{
    public GeneralApplicationException() : base() { }

    public GeneralApplicationException(string message) : base(message) { }

    public GeneralApplicationException(string message, Exception innerException) : base(message, innerException) { }

    public GeneralApplicationException(SerializationInfo serialiizationInfo, StreamingContext context) : base(serialiizationInfo, context) { }
}
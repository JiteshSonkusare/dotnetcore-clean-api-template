namespace Application.Common.Exceptions;

public class GeneralApplicationException : ApplicationException
{
	public GeneralApplicationException() : base() { }

	public GeneralApplicationException(string message) : base(message) { }

	public GeneralApplicationException(string message, Exception innerException) : base(message, innerException) { }
}
using Application.Common.Exceptions;

namespace Application.Common.ExceptionHandlers;

public static class ExceptionExtensions
{
	public static GeneralApplicationException With(this Exception exception, params string?[] exceptionDetails)
	{
		var message = string.Join("\n", exceptionDetails) ?? string.Empty;
		return new(message, exception);
	}
}
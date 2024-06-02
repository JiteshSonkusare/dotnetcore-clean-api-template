using Application.Common.Exceptions;

namespace Application.Common.ExceptionHandlers;

public static class GeneralApplicationExceptionExtensions
{
	public static GeneralApplicationException ProjectId(this GeneralApplicationException exception, in int projectId)
	{
		exception.DetailData(nameof(projectId), projectId);
		return exception;
	}

	public static GeneralApplicationException Version(this GeneralApplicationException exception, in string version)
	{
		exception.DetailData(nameof(version), version);
		return exception;
	}

	public static GeneralApplicationException Id(this GeneralApplicationException exception, in int id)
	{
		exception.DetailData(nameof(id), id);
		return exception;
	}

	public static GeneralApplicationException Name(this GeneralApplicationException exception, in string name)
	{
		exception.DetailData(nameof(name), name);
		return exception;
	}

	public static GeneralApplicationException DetailData<T>(this GeneralApplicationException exception, in string key, in T value)
		where T : struct
	{
		exception.Data[key] = value;
		return exception;
	}

	public static GeneralApplicationException DetailData(this GeneralApplicationException exception, in string key, in string value)
	{
		exception.Data[key] = value;
		return exception;
	}

	public static GeneralApplicationException DetailData(this GeneralApplicationException exception, in string key, in object value)
	{
		exception.Data[key] = ExceptionDataEntry.FromValue(value);
		return exception;
	}
}
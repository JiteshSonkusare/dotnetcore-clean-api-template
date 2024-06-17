using System.Text.Json.Serialization;

namespace Shared.Wrapper;

public class Result
{
	protected internal Result(bool isSuccess, Error? error)
	{
		Suceeded = isSuccess;
		Error = error;
	}

	[JsonIgnore]
	public bool IsFailure => !Suceeded;

	public bool Suceeded { get; }

	public Error? Error { get; } = default!;

	public static Result Success() => new(true, null);

	public static Result<TValue> Success<TValue>(TValue value) => new(value, true, null);

	public static Result Failure(Error error) => new(false, error);

	public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

	public static Result Create(bool condition) => condition ? Success() : Failure(Error.ConditionNotMet);

	public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}
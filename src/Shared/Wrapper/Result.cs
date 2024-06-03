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

public class Result<TValue> : Result
{
	private readonly TValue? _data;
	public TValue Data => Suceeded ? _data! : default!;

	protected internal Result(TValue? value, bool isSuccess, Error? error) : base(isSuccess, error) =>
		_data = value;

	public static implicit operator Result<TValue>(TValue? value) => Create(value);
}

public record Error(string Code, string Message)
{
	public static readonly Error None = new(string.Empty, string.Empty);

	public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

	public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.");
}
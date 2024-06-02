using System.Text.Json.Serialization;

namespace Shared.Wrapper;

/// <summary>
/// Code - unique name for the error in the application
/// Description - contains developer-friendly details about the error
/// </summary>
/// <param name="Code"></param>
/// <param name="Description"></param>
public sealed record Error(string Code, string Description)
{
	public static readonly Error None = new(string.Empty, string.Empty);
}

/// <summary>
/// Use to implement functional result pattern.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{
	public bool Succeeded { get; set; }
	public string? Messages { get; set; }
	public T Data { get; set; } = default!;

	[JsonIgnore]
	public Error? Error { get; set; }

	public static Result<T> Failure(Error? error)
	{
		return new Result<T> { Succeeded = false, Messages = error?.Description, Error = error };
	}

	public static Task<Result<T>> FailureAsync(Error? error)
	{
		return Task.FromResult(Failure(error));
	}

	public static Result<T> Success(string? message = null)
	{
		return new Result<T> { Succeeded = true, Messages = message };
	}

	public static Result<T> Success(T data, string? message = null)
	{
		return new Result<T> { Succeeded = true, Data = data, Messages = message };
	}

	public static Task<Result<T>> SuccessAsync(string? message = null)
	{
		return Task.FromResult(Success(message));
	}

	public static Task<Result<T>> SuccessAsync(T data, string? message = null)
	{
		return Task.FromResult(Success(data, message));
	}
}
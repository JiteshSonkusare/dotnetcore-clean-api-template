//using System.Text.Json.Serialization;

//namespace Shared.Wrapper;

///// <summary>
///// Code - unique name for the error in the application
///// Description - contains developer-friendly details about the error
///// </summary>
///// <param name="Code"></param>
///// <param name="Description"></param>
//public sealed record ErrorTest(string Code, string Description)
//{
//	public static readonly ErrorTest None = new(string.Empty, string.Empty);
//}

///// <summary>
///// Use to implement functional result pattern.
///// </summary>
///// <typeparam name="T"></typeparam>
//public class ResultTest<T>
//{
//	public bool Succeeded { get; set; }
//	public string? Messages { get; set; }
//	public T Data { get; set; } = default!;

//	[JsonIgnore]
//	public ErrorTest? Error { get; set; }

//	public static ResultTest<T> Failure(ErrorTest? error)
//	{
//		return new ResultTest<T> { Succeeded = false, Messages = error?.Description, Error = error };
//	}

//	public static Task<ResultTest<T>> FailureAsync(ErrorTest? error)
//	{
//		return Task.FromResult(Failure(error));
//	}

//	public static ResultTest<T> Success(string? message = null)
//	{
//		return new ResultTest<T> { Succeeded = true, Messages = message };
//	}

//	public static ResultTest<T> Success(T data, string? message = null)
//	{
//		return new ResultTest<T> { Succeeded = true, Data = data, Messages = message };
//	}

//	public static Task<ResultTest<T>> SuccessAsync(string? message = null)
//	{
//		return Task.FromResult(Success(message));
//	}

//	public static Task<ResultTest<T>> SuccessAsync(T data, string? message = null)
//	{
//		return Task.FromResult(Success(data, message));
//	}
//}
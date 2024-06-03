namespace Shared.Wrapper;

public static class ResultExtensions
{
	public static TOutput Match<TResult, TOutput>(
		this Result<TResult> result,
		Func<Result<TResult>, TOutput> onSuccess,
		Func<Result<TResult>, TOutput> onFailure)
	{
		return result.IsFailure ? onFailure(result) : onSuccess(result);
	}
}
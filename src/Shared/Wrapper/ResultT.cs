namespace Shared.Wrapper;

public class Result<TValue> : Result
{
	private readonly TValue? _data;
	public TValue Data => Suceeded ? _data! : default!;

	protected internal Result(TValue? value, bool isSuccess, Error? error) : base(isSuccess, error) =>
		_data = value;

	public static implicit operator Result<TValue>(TValue? value) => Create(value);
}
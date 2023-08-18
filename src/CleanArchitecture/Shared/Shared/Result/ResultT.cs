namespace SharedKernel.Result;
public class Result<TValue> : Result
{
    private readonly TValue? value;

    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error) =>
        this.value = value;
    protected internal Result(bool isSuccess, Error error) : base(isSuccess, error) { }

    public TValue Value => IsSuccess && value is not null ? value : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(value, true, Error.None);
}

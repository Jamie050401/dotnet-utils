namespace Utils.CSharp.Result;

public class Result
{
    private Result()
    {
        IsSuccess = true;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result Success() => new();
    public static Result Failure(Error error) => new(error);

    public static implicit operator Result(Error error) => Failure(error);

    public bool TryGetError(out Error? error)
    {
        if (IsSuccess)
        {
            error = default;
            return false;
        }

        error = Error!;
        return true;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    private Error? Error { get; }
}

public class Result<ValueType>
{
    private Result(ValueType value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success<T>(T value) => new(value);
    public static Result<T> Failure<T>(Error error) => new(error);

    public static implicit operator Result<ValueType>(ValueType value) => Success(value);
    public static implicit operator Result<ValueType>(Error error) => Failure<ValueType>(error);

    public bool TryGetValue(out ValueType? value, out Error? error)
    {
        if (IsFailure)
        {
            value = default;
            error = Error!;
            return false;
        }

        value = Value!;
        error = default;
        return true;
    }

    public bool TryGetError(out Error? error)
    {
        if (IsSuccess)
        {
            error = default;
            return false;
        }

        error = Error!;
        return true;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    private ValueType? Value { get; }
    private Error? Error { get; }
}

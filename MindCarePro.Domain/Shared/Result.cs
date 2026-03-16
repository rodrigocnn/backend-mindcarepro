namespace MindCarePro.Domain.Shared;



public enum ResultErrorType
{
    None = 0,
    Unspecified = 1,
    Validation = 2,
    Unauthorized = 3,
    NotFound = 4,
    Conflict = 5
}

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<string> Errors { get; }
    public ResultErrorType ErrorType { get; }

    protected Result(bool isSuccess, ResultErrorType errorType = ResultErrorType.None, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Errors = errors ?? new();
        ErrorType = isSuccess ? ResultErrorType.None : errorType;
    }

    public static Result Success()
        => new Result(true);

    public static Result Failure(params string[] errors)
        => new Result(false, ResultErrorType.Unspecified, errors.ToList());

    public static Result Failure(ResultErrorType errorType, params string[] errors)
        => new Result(false, errorType, errors.ToList());
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, ResultErrorType errorType = ResultErrorType.None, List<string>? errors = null)
        : base(isSuccess, errorType, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
        => new Result<T>(true, value);

    public static new Result<T> Failure(params string[] errors)
        => new Result<T>(false, default, ResultErrorType.Unspecified, errors.ToList());

    public static Result<T> Failure(ResultErrorType errorType, params string[] errors)
        => new Result<T>(false, default, errorType, errors.ToList());
}

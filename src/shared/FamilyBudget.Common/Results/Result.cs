using FamilyBudget.Common.Results.Core;
using FluentValidation.Results;

namespace FamilyBudget.Common.Results;

public class Result
{
    public Status Status { get; }
    public bool IsSuccess { get; }
    public Error Error { get; }

    public Result()
    {
    }

    private Result(Status status, bool isSuccess = true, Error error = null)
    {
        Status = status;
        IsSuccess = isSuccess;
        Error = error switch
        {
            null => Error.None(),
            _ => error
        };
    }

    #region Result

    public static Result Success()
        => new(Status.Success);

    public static Result Failure(Failure failure)
        => new(Status.Failure, false, Error.Create(failure.Name, failure.Message));

    public static Result NotFound(string objectName, Guid id = default)
        => new(Status.NotFound, false, Error.NotFound(objectName, id));

    public static Result DatabaseFailure()
        => new(Status.Failure, false, Error.DatabaseFailure());

    public static Result Unauthorized()
        => new(Status.Unauthorized, false, Error.Unauthorized());

    public static Result ValidationFailure(ValidationResult failure)
        => new(Status.Failure, false, Error.ValidationError(failure));


    #endregion

    #region Result<T>

    public static Result<T> Success<T>(T value)
        => Result<T>.Success(value);

    public static Result<T> Failure<T>(Failure failure)
        => Result<T>.Failure(failure);

    public static Result<T> NotFound<T>(string objectName, Guid id = default)
        => Result<T>.NotFound(objectName, id);

    public static Result<T> DatabaseFailure<T>()
        => Result<T>.DatabaseFailure();

    public static Result<T> Unauthorized<T>()
        => Result<T>.Unauthorized();

    public static Result<T> ValidationFailure<T>(ValidationResult failure)
        => Result<T>.ValidationFailure(failure);

    #endregion
}
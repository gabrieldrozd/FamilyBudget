using FluentValidation.Results;

namespace FamilyBudget.Common;

public class Result<T>
{
    #region Properties

    public bool IsSuccess { get; private init; }

    public bool IsFailure { get; private init; }

    public T Value { get; private init; }

    public string Error { get; private init; }

    #endregion

    public static Result<T> Success(T value)
        => new() { IsSuccess = true, IsFailure = false, Value = value };

    public static Result<T> Failure(string error)
        => new() { IsSuccess = false, IsFailure = true, Error = error };

    public static Result<T> Exists(string problemObject)
        => new() { IsSuccess = false, IsFailure = true, Error = $"{problemObject} already exists or is in use." };

    public static Result<T> NotFound(string objectName)
        => new() { IsSuccess = false, IsFailure = true, Error = "Object not found: " + objectName };

    public static Result<T> DatabaseSaveFailure()
        => new() { IsSuccess = false, IsFailure = true, Error = "Failed to save changes to the database" };

    public static Result<T> ValidationFailure(ValidationResult validationResult)
        => new() { IsSuccess = false, IsFailure = true, Error = string.Join(", ", validationResult.Errors) };
}
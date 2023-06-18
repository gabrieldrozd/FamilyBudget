using FluentValidation.Results;

namespace FamilyBudget.Common.Results.Core;

/// <summary>
/// Error object
/// </summary>
/// <param name="Name">Error name</param>
/// <param name="Message">Error message</param>
public record Error(string Name, string Message)
{
    public static Error Create(string code, string message) => new(code, message);

    public static implicit operator string(Error error) => error.Name;

    #region Internal Errors

    internal static Error None()
        => Create(string.Empty, string.Empty);

    internal static Error NotFound(string objectName, Guid id)
        => id.Equals(Guid.Empty)
            ? Create("NotFound", $"{objectName} was not found.")
            : Create("NotFound", $"{objectName} with: '{id:D}' was not found.");

    internal static Error DatabaseFailure()
        => Create("DatabaseFailure", "Database failed to process changes.");

    internal static Error Unauthorized()
        => Create("Unauthorized", "Invalid credentials. Email or password is incorrect.");

    #endregion

    #region Public Errors

    public static Error Unexpected()
        => Create("Unexpected", "An unexpected error occurred.");

    public static Error ValidationError(ValidationResult failure)
    {
        var errorMessage = string.Join(", ", failure.Errors.Select(x => x.ErrorMessage));
        return Create("Validation", errorMessage);
    }

    #endregion
}
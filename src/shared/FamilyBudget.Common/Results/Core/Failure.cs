using FamilyBudget.Common.Abstractions;

namespace FamilyBudget.Common.Results.Core;

public abstract record Failure : Enumeration<Failure>
{
    public static readonly Failure Database = new DatabaseFail();
    public static readonly Failure Mediator = new MediatorFail();
    public static readonly Failure EmailInUse = new EmailInUseFail();
    public static readonly Failure InvalidCredentials = new InvalidCredentialsFail();

    public abstract string Message { get; }

    private Failure(int value, string name)
        : base(value, name)
    {
    }

    private sealed record DatabaseFail() : Failure(1, "DatabaseFailure")
    {
        public override string Message => "Database failed to process changes.";
    }

    private sealed record MediatorFail() : Failure(2, "MediatorFailure")
    {
        public override string Message => "Mediator failed to process request.";
    }

    private sealed record EmailInUseFail() : Failure(3, "EmailInUseFailure")
    {
        public override string Message => "Email is already in use.";
    }

    private sealed record InvalidCredentialsFail() : Failure(4, "InvalidCredentialsFailure")
    {
        public override string Message => "Invalid credentials.";
    }
}
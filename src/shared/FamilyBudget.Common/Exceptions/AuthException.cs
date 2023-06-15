namespace FamilyBudget.Common.Exceptions;

public abstract class AuthException : Exception
{
    protected AuthException(string message) : base(message)
    {
    }
}
namespace FamilyBudget.Domain.Interfaces.Auth;

public interface IContextFactory
{
    IUserContext Create();
}
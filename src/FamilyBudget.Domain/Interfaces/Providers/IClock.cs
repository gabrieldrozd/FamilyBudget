namespace FamilyBudget.Domain.Interfaces.Providers;

public interface IClock
{
    DateTime Current();
}
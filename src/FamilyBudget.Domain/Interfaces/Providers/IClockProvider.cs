namespace FamilyBudget.Domain.Interfaces.Providers;

public interface IClockProvider
{
    DateTime Current();
}
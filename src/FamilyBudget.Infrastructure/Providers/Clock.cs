using FamilyBudget.Domain.Interfaces.Providers;

namespace FamilyBudget.Infrastructure.Providers;

internal sealed class Clock : IClock
{
    public DateTime Current()
        => DateTime.Now;
}
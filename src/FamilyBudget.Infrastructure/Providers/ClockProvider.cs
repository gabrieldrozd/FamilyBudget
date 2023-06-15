using FamilyBudget.Domain.Interfaces.Providers;

namespace FamilyBudget.Infrastructure.Providers;

internal sealed class ClockProvider : IClockProvider
{
    public DateTime Current()
        => DateTime.Now;
}
using FamilyBudget.Application.DTO;
using FamilyBudget.Domain.Entities.MoneyFlow;

namespace FamilyBudget.Application.Mappings;

public static class IncomeMappings
{
    public static IncomeDto ToDto(Income model)
        => new()
        {
            ExternalId = model.ExternalId,
            Name = model.Name,
            Date = model.Date,
            Amount = model.Amount,
        };
}
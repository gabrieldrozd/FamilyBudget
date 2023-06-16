using FamilyBudget.Application.DTO;
using FamilyBudget.Domain.Entities.MoneyFlow;

namespace FamilyBudget.Application.Mappings;

public static class ExpenseMappings
{
    public static ExpenseDto ToDto(Expense model)
        => new()
        {
            ExternalId = model.ExternalId,
            Name = model.Name,
            Date = model.Date,
            Amount = model.Amount,
        };
}
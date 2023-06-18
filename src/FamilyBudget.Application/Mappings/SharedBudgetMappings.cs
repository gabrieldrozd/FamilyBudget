using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Domain.Entities.Budget;

namespace FamilyBudget.Application.Mappings;

public static class SharedBudgetMappings
{
    public static SharedBudgetDto ToDto(SharedBudget model)
        => new()
        {
            ExternalId = model.ExternalId,
            BudgetExternalId = model.BudgetPlan.ExternalId,
            DateShared = model.DateShared,
            Name = model.BudgetPlan.Name,
            Description = model.BudgetPlan.Description,
            Balance = model.BudgetPlan.Balance,
            StartDate = model.BudgetPlan.StartDate,
            EndDate = model.BudgetPlan.EndDate,
            Incomes = model.BudgetPlan.Incomes.MapTo(IncomeMappings.ToDto),
            Expenses = model.BudgetPlan.Expenses.MapTo(ExpenseMappings.ToDto)
        };

    public static SharedBudgetDetailsDto ToDetailsDto(SharedBudget model)
        => new()
        {
            ExternalId = model.ExternalId,
            BudgetExternalId = model.BudgetPlan.ExternalId,
            DateShared = model.DateShared,
            Name = model.BudgetPlan.Name,
            Description = model.BudgetPlan.Description,
            Balance = model.BudgetPlan.Balance,
            StartDate = model.BudgetPlan.StartDate,
            EndDate = model.BudgetPlan.EndDate,
            Incomes = model.BudgetPlan.Incomes.MapTo(IncomeMappings.ToDto),
            Expenses = model.BudgetPlan.Expenses.MapTo(ExpenseMappings.ToDto),
            Creator = UserMappings.ToBaseDto(model.BudgetPlan.Creator)
        };
}
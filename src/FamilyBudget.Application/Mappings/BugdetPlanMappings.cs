using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Finances.Budget.Commands;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities.Budget;

namespace FamilyBudget.Application.Mappings;

public static class BugdetPlanMappings
{
    public static BudgetPlanDefinition ToDefinition(this CreateBudgetPlan model)
        => new()
        {
            Name = model.Name,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };

    public static BudgetPlanDto ToDto(BudgetPlan model)
        => new()
        {
            ExternalId = model.ExternalId,
            Name = model.Name,
            Description = model.Description,
            Balance = model.Balance,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Incomes = model.Incomes.MapTo(IncomeMappings.ToDto),
            Expenses = model.Expenses.MapTo(ExpenseMappings.ToDto)
        };

    public static BudgetPlanDetailsDto ToDetailsDto(BudgetPlan model)
        => new()
        {
            ExternalId = model.ExternalId,
            Name = model.Name,
            Description = model.Description,
            Balance = model.Balance,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Incomes = model.Incomes.MapTo(IncomeMappings.ToDto),
            Expenses = model.Expenses.MapTo(ExpenseMappings.ToDto),
            Creator = UserMappings.ToBaseDto(model.Creator)
        };
}
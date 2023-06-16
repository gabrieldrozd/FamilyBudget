using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Users.Commands;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Entities.MoneyFlow;

namespace FamilyBudget.Application.Mappings;

public static class UserMappings
{
    public static UserDefinition ToDefinition(RegisterUser model)
        => new()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Role = model.Role
        };

    public static UserDto ToDto(User model)
        => new()
        {
            ExternalId = model.ExternalId,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Role = model.Role.Name,
            BudgetPlans = model.BudgetPlans.MapTo(BugdetPlanMappings.ToDto),
            SharedBudgets = model.SharedBudgets.MapTo(SharedBudgetMappings.ToDto)
        };
}

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
}

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

public static class BugdetPlanMappings
{
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
}
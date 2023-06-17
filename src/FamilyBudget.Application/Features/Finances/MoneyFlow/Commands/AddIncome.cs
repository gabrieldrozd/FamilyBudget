using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands;

// TODO: Add IncomeCategory
public record AddIncome(Guid BudgetPlanId, string Name, DateTime Date, decimal Amount) : ICommand;
using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands;

// TODO: Add ExpenseCategory
public record AddExpense(Guid BudgetPlanId, string Name, DateTime Date, decimal Amount) : ICommand;
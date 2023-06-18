using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands;

public record RemoveExpense(Guid BudgetPlanId, Guid ExpenseId) : ICommand;
using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands;

public record AddIncome(Guid BudgetPlanId, string Name, DateTime Date, decimal Amount, string IncomeType) : ICommand;
using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands;

public record RemoveIncome(Guid BudgetPlanId, Guid IncomeId) : ICommand;
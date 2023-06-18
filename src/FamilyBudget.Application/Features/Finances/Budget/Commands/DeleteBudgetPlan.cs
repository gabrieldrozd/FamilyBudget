using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.Budget.Commands;

public record DeleteBudgetPlan(Guid BudgetPlanId) : ICommand;
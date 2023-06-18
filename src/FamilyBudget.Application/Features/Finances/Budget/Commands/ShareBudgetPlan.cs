using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.Budget.Commands;

public record ShareBudgetPlan(Guid BudgetPlanId, Guid[] UserExternalIds) : ICommand;
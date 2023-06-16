using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.BudgetPlans.Commands;

public record CreateBudgetPlan(string Name, string Description, DateTime StartDate, DateTime EndDate)
    : ICommand;
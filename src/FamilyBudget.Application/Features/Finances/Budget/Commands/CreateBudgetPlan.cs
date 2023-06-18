using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.Budget.Commands;

public record CreateBudgetPlan(string Name, string Description, DateTime StartDate, DateTime EndDate)
    : ICommand;
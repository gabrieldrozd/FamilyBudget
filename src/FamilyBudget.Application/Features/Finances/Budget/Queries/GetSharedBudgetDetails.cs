using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Finances.Budget.Queries;

public record GetSharedBudgetDetails(Guid SharedBudgetId) : IQuery<SharedBudgetDetailsDto>;
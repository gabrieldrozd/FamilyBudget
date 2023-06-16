using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Api.Pagination;

namespace FamilyBudget.Application.Features.BudgetPlans.Queries;

public record BrowseBudgetPlans(Pagination Pagination) : IQuery<PaginatedList<BudgetPlanDto>>;
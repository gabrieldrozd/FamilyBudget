using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Api.Pagination;

namespace FamilyBudget.Application.Features.Finances.BudgetPlans.Queries;

public record BrowseBudgetPlans(Pagination Pagination) : IQuery<PaginatedList<BudgetPlanDto>>;
using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Api.Pagination;

namespace FamilyBudget.Application.Features.Finances.Budget.Queries;

public record BrowseUserBudgetPlans(Guid UserExternalId, Pagination Pagination) : IQuery<PaginatedList<BudgetPlanDto>>;
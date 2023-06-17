using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Finances.BudgetPlans.Queries;
using FamilyBudget.Application.Mappings;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Interfaces.Repositories;

namespace FamilyBudget.Infrastructure.Queries.BudgetPlans;

internal sealed class BrowseBudgetPlansHandler: IQueryHandler<BrowseBudgetPlans, PaginatedList<BudgetPlanDto>>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;

    public BrowseBudgetPlansHandler(IBudgetPlanRepository budgetPlanRepository)
        => _budgetPlanRepository = budgetPlanRepository;

    public async Task<Result<PaginatedList<BudgetPlanDto>>> Handle(BrowseBudgetPlans request, CancellationToken cancellationToken)
    {
        var plans = await _budgetPlanRepository.BrowseAsync(request.Pagination);
        var mapped = plans.MapTo(BugdetPlanMappings.ToDto);

        return Result.Success(mapped);
    }
}

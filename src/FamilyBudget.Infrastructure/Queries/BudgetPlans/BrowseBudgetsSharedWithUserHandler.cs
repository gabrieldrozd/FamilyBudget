using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Finances.Budget.Queries;
using FamilyBudget.Application.Mappings;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Interfaces.Repositories;

namespace FamilyBudget.Infrastructure.Queries.BudgetPlans;

public sealed class BrowseBudgetsSharedWithUserHandler : IQueryHandler<BrowseBudgetsSharedWithUser, PaginatedList<SharedBudgetDto>>
{
    private readonly ISharedBudgetRepository _sharedBudgetRepository;

    public BrowseBudgetsSharedWithUserHandler(ISharedBudgetRepository sharedBudgetRepository)
        => _sharedBudgetRepository = sharedBudgetRepository;

    public async Task<Result<PaginatedList<SharedBudgetDto>>> Handle(
        BrowseBudgetsSharedWithUser request, CancellationToken cancellationToken)
    {
        var plans = await _sharedBudgetRepository.BrowseBudgetsSharedWithUserAsync(request.UserExternalId, request.Pagination);
        var mapped = plans.MapTo(SharedBudgetMappings.ToDto);

        return Result.Success(mapped);
    }
}
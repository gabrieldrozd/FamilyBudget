using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Finances.Budget.Queries;
using FamilyBudget.Application.Mappings;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Repositories;

namespace FamilyBudget.Infrastructure.Queries.BudgetPlans;

internal sealed class GetSharedBudgetDetailsHandler : IQueryHandler<GetSharedBudgetDetails, SharedBudgetDetailsDto>
{
    private readonly ISharedBudgetRepository _sharedBudgetRepository;

    public GetSharedBudgetDetailsHandler(ISharedBudgetRepository sharedBudgetRepository)
        => _sharedBudgetRepository = sharedBudgetRepository;

    public async Task<Result<SharedBudgetDetailsDto>> Handle(GetSharedBudgetDetails request, CancellationToken cancellationToken)
    {
        var sharedBudget = await _sharedBudgetRepository.GetDetails(request.SharedBudgetId);
        if (sharedBudget is null) return Result<SharedBudgetDetailsDto>.NotFound(nameof(BudgetPlan), request.SharedBudgetId);

        var mapped = SharedBudgetMappings.ToDetailsDto(sharedBudget);
        return Result<SharedBudgetDetailsDto>.Success(mapped);
    }
}
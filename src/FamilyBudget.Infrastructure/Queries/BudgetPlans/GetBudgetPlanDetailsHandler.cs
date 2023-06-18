using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Finances.Budget.Queries;
using FamilyBudget.Application.Mappings;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Repositories;

namespace FamilyBudget.Infrastructure.Queries.BudgetPlans;

internal sealed class GetBudgetPlanDetailsHandler : IQueryHandler<GetBudgetPlanDetails, BudgetPlanDetailsDto>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;

    public GetBudgetPlanDetailsHandler(IBudgetPlanRepository budgetPlanRepository)
        => _budgetPlanRepository = budgetPlanRepository;

    public async Task<Result<BudgetPlanDetailsDto>> Handle(GetBudgetPlanDetails request, CancellationToken cancellationToken)
    {
        var budgetPlan = await _budgetPlanRepository.GetDetails(request.BudgetPlanId);
        if (budgetPlan is null) return Result<BudgetPlanDetailsDto>.NotFound(nameof(BudgetPlan), request.BudgetPlanId);

        var mapped = BugdetPlanMappings.ToDetailsDto(budgetPlan);
        return Result<BudgetPlanDetailsDto>.Success(mapped);
    }
}
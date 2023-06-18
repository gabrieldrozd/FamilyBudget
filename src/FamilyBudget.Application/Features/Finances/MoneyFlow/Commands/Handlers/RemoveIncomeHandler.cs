using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Entities.MoneyFlow;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands.Handlers;

internal sealed class RemoveIncomeHandler : ICommandHandler<RemoveIncome>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;
    private readonly IIncomeRepository _incomeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveIncomeHandler(
        IBudgetPlanRepository budgetPlanRepository,
        IIncomeRepository incomeRepository,
        IUnitOfWork unitOfWork)
    {
        _budgetPlanRepository = budgetPlanRepository;
        _incomeRepository = incomeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveIncome request, CancellationToken cancellationToken)
    {
        var budgetPlan = await _budgetPlanRepository.GetByIdAsync(request.BudgetPlanId);
        if (budgetPlan is null) return Result.NotFound(nameof(BudgetPlan), request.BudgetPlanId);

        var income = budgetPlan.Incomes.FirstOrDefault(x => x.ExternalId == request.IncomeId);
        if (income is null) return Result.NotFound(nameof(Income), request.IncomeId);

        _incomeRepository.Remove(income);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result.Success()
            : Result.DatabaseFailure();
    }
}
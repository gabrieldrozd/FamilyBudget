using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Entities.MoneyFlow;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands.Handlers;

internal sealed class AddIncomeHandler : ICommandHandler<AddIncome>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;
    private readonly IIncomeRepository _incomeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddIncomeHandler(
        IBudgetPlanRepository budgetPlanRepository,
        IIncomeRepository incomeRepository,
        IUnitOfWork unitOfWork)
    {
        _budgetPlanRepository = budgetPlanRepository;
        _incomeRepository = incomeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddIncome request, CancellationToken cancellationToken)
    {
        var budgetPlan = await _budgetPlanRepository.GetByIdAsync(request.BudgetPlanId);
        if (budgetPlan is null) return Result.NotFound(nameof(BudgetPlan), request.BudgetPlanId);

        var incomeId = Guid.NewGuid();
        var income = Income.Create(incomeId, request.Name, request.Date, request.Amount, budgetPlan.ExternalId);
        budgetPlan.AddIncome(income);

        _incomeRepository.Insert(income);
        _budgetPlanRepository.Update(budgetPlan);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result.Success(incomeId)
            : Result.DatabaseFailure();
    }
}
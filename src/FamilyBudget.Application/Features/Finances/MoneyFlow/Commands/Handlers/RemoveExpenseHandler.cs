using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Entities.MoneyFlow;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands.Handlers;

internal sealed class RemoveExpenseHandler : ICommandHandler<RemoveExpense>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveExpenseHandler(
        IBudgetPlanRepository budgetPlanRepository,
        IExpenseRepository expenseRepository,
        IUnitOfWork unitOfWork)
    {
        _budgetPlanRepository = budgetPlanRepository;
        _expenseRepository = expenseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveExpense request, CancellationToken cancellationToken)
    {
        var budgetPlan = await _budgetPlanRepository.GetByIdAsync(request.BudgetPlanId);
        if (budgetPlan is null) return Result.NotFound(nameof(BudgetPlan), request.BudgetPlanId);

        var expense = budgetPlan.Expenses.FirstOrDefault(x => x.ExternalId == request.ExpenseId);
        if (expense is null) return Result.NotFound(nameof(Expense), request.ExpenseId);

        _expenseRepository.Remove(expense);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result.Success()
            : Result.DatabaseFailure();
    }
}
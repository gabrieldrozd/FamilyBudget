using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Entities.MoneyFlow;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Application.Features.Finances.MoneyFlow.Commands.Handlers;

internal sealed class AddExpenseHandler : ICommandHandler<AddExpense>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddExpenseHandler(
        IBudgetPlanRepository budgetPlanRepository,
        IExpenseRepository expenseRepository,
        IUnitOfWork unitOfWork)
    {
        _budgetPlanRepository = budgetPlanRepository;
        _expenseRepository = expenseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddExpense request, CancellationToken cancellationToken)
    {
        var budgetPlan = await _budgetPlanRepository.GetByIdAsync(request.BudgetPlanId);
        if (budgetPlan is null) return Result.NotFound(nameof(BudgetPlan), request.BudgetPlanId);

        var expenseId = Guid.NewGuid();
        var expense = Expense.Create(
            expenseId,
            request.Name,
            request.Date,
            request.Amount,
            request.ExpenseCategory,
            budgetPlan.ExternalId);
        budgetPlan.AddExpense(expense);

        _expenseRepository.Insert(expense);
        _budgetPlanRepository.Update(budgetPlan);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result.Success(expenseId)
            : Result.DatabaseFailure();
    }
}
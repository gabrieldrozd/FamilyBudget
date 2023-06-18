using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Application.Features.Finances.Budget.Commands.Handlers;

public sealed class DeleteBudgetPlanHandler : ICommandHandler<DeleteBudgetPlan>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBudgetPlanHandler(
        IBudgetPlanRepository budgetPlanRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _budgetPlanRepository = budgetPlanRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteBudgetPlan request, CancellationToken cancellationToken)
    {
        var budgetPlan = await _budgetPlanRepository.GetByIdAsync(request.BudgetPlanId);
        if (budgetPlan is null) return Result.NotFound(nameof(BudgetPlan), request.BudgetPlanId);

        if (_userContext.Role.Name != Role.Owner.Name) return Result.Unauthorized();

        _budgetPlanRepository.Remove(budgetPlan);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result.Success()
            : Result.DatabaseFailure();
    }
}
using FamilyBudget.Application.Mappings;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Application.Features.Finances.BudgetPlans.Commands.Handlers;

internal sealed class CreateBudgetPlanHandler : ICommandHandler<CreateBudgetPlan>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBudgetPlanHandler(
        IBudgetPlanRepository budgetPlanRepository,
        IUserRepository userRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _budgetPlanRepository = budgetPlanRepository;
        _userRepository = userRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateBudgetPlan request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.ExternalId == _userContext.UserId);
        if (user is null) return Result.NotFound(nameof(User), _userContext.UserId);

        var budgetPlanId = Guid.NewGuid();
        var budgetPlanDefinition = request.ToDefinition();
        var budgetPlan = BudgetPlan.Create(budgetPlanId, budgetPlanDefinition, user.ExternalId);

        _budgetPlanRepository.Insert(budgetPlan);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result.Success(budgetPlanId)
            : Result.DatabaseFailure();
    }
}
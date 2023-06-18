using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Providers;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Application.Features.Finances.Budget.Commands.Handlers;

internal sealed class ShareBudgetPlanHandler : ICommandHandler<ShareBudgetPlan>
{
    private readonly IBudgetPlanRepository _budgetPlanRepository;
    private readonly ISharedBudgetRepository _sharedBudgetRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClock _clock;

    public ShareBudgetPlanHandler(
        IBudgetPlanRepository budgetPlanRepository,
        ISharedBudgetRepository sharedBudgetRepository,
        IUserRepository userRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork,
        IClock clock)
    {
        _budgetPlanRepository = budgetPlanRepository;
        _sharedBudgetRepository = sharedBudgetRepository;
        _userRepository = userRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
        _clock = clock;
    }

    public async Task<Result> Handle(ShareBudgetPlan request, CancellationToken cancellationToken)
    {
        var budgetPlan = await _budgetPlanRepository.GetByIdAsync(request.BudgetPlanId);
        if (budgetPlan is null) return Result.NotFound(nameof(BudgetPlan), request.BudgetPlanId);

        var currentUser = await _userRepository.GetAsync(x => x.ExternalId == _userContext.UserId);
        if (currentUser is null) return Result.NotFound(nameof(User), _userContext.UserId);

        if (budgetPlan.UserId != currentUser.ExternalId) return Result.Unauthorized();

        var users = await _userRepository.GetByIds(request.UserExternalIds);
        if (!users.Any()) return Result.NotFound(nameof(User));

        var sharedBudgets = new List<SharedBudget>();
        foreach (var user in users)
        {
            if (user.SharedBudgets.Any(x => x.BudgetPlanId == budgetPlan.ExternalId))
                continue;

            var sharedBudgetId = Guid.NewGuid();
            var currentDateTime = _clock.Current();
            var sharedBudget = SharedBudget.Create(sharedBudgetId, budgetPlan.ExternalId, user.ExternalId, currentDateTime);
            sharedBudgets.Add(sharedBudget);
        }

        if (!sharedBudgets.Any()) return Result.Success();

        _sharedBudgetRepository.InsertRange(sharedBudgets);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result.Success()
            : Result.DatabaseFailure();
    }
}
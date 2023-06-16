using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Application.Features.Users.Commands.Handlers;

internal sealed class DeleteUserHandler : ICommandHandler<DeleteUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUserRepository userRepository, IUserContext userContext, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var hasPermission = _userContext.Role == Role.Owner;
        if (!hasPermission) return Result<UserDto>.Unauthorized();

        var user = await _userRepository.GetAsync(x => x.ExternalId == request.ExternalId);
        if (user is null) return Result<UserDto>.NotFound(nameof(User), request.ExternalId);

        _userRepository.Remove(user);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result.Success()
            : Result.DatabaseFailure();
    }
}
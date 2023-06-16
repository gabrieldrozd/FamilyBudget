using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Mappings;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Results;
using FamilyBudget.Common.Results.Core;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;
using FamilyBudget.Domain.Interfaces.Services;

namespace FamilyBudget.Application.Features.Users.Commands.Handlers;

internal sealed class RegisterUserHandler : ICommandHandler<RegisterUser, UserDto>
{
    private readonly IIdentityService _identityService;
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(
        IIdentityService identityService,
        IUserRepository userRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _userRepository = userRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        var hasPermission = _userContext.Role == Role.Owner;
        if (!hasPermission) return Result<UserDto>.Unauthorized();

        var doesUserExist = await _userRepository.ExistsAsync(x => x.Email == request.Email);
        if (doesUserExist) return Result<UserDto>.Failure(Failure.EmailInUse);

        var userId = Guid.NewGuid();
        var definition = UserMappings.ToDefinition(request);

        var user = User.Create(userId, definition);
        _identityService.GenerateHashedPassword(user);

        _userRepository.Insert(user);
        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess
            ? Result<UserDto>.Success(UserMappings.ToDto(user))
            : Result<UserDto>.DatabaseFailure();
    }
}
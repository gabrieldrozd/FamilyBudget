using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Models;
using FamilyBudget.Common.Results;
using FamilyBudget.Common.Results.Core;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Services;

namespace FamilyBudget.Application.Features.Auth.Commands.Handlers;

public sealed class LoginUserHandler : ICommandHandler<LoginUser, AccessToken>
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentityService _identityService;

    public LoginUserHandler(IUserRepository userRepository, IIdentityService identityService)
    {
        _userRepository = userRepository;
        _identityService = identityService;
    }

    public async Task<Result<AccessToken>> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.Email == request.Email);
        if (user is null) return Result.Failure<AccessToken>(Failure.InvalidCredentials);

        var loginResult = _identityService.Login(user, request.Password);

        return loginResult.IsSuccess
            ? loginResult
            : Result.Failure<AccessToken>(Failure.InvalidCredentials);
    }
}
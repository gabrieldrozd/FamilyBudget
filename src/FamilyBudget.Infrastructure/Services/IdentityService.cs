using FamilyBudget.Common.Models;
using FamilyBudget.Common.Results;
using FamilyBudget.Common.Results.Core;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Providers;
using FamilyBudget.Domain.Interfaces.Services;
using FamilyBudget.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;

namespace FamilyBudget.Infrastructure.Services;

internal sealed class IdentityService : IIdentityService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly AuthDefaults _defaults;

    public IdentityService(
        IPasswordHasher<User> passwordHasher,
        ITokenProvider tokenProvider,
        AuthDefaults defaults)
    {
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _defaults = defaults;
    }

    public void GenerateHashedPassword(User user)
        => user.SetPasswordHash(_passwordHasher.HashPassword(user, _defaults.Password));

    public Result<AccessToken> Login(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
            return Result.Failure<AccessToken>(Failure.InvalidCredentials);

        var accessToken = _tokenProvider.CreateToken(
            user.ExternalId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role);

        return Result.Success(accessToken);
    }
}
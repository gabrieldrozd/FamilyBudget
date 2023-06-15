using FamilyBudget.Common.Models;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Providers;
using FamilyBudget.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace FamilyBudget.Infrastructure.Services;

internal sealed class IdentityService : IIdentityService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public IdentityService(
        IPasswordHasher<User> passwordHasher,
        ITokenProvider tokenProvider)
    {
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }

    public void GenerateHashedPassword(User user)
    {
        throw new NotImplementedException();
    }

    public Result<AccessToken> Login(User user, string password)
    {
        throw new NotImplementedException();
    }
}
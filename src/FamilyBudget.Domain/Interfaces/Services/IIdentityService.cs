using FamilyBudget.Common.Models;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Entities;

namespace FamilyBudget.Domain.Interfaces.Services;

public interface IIdentityService
{
    void GenerateHashedPassword(User user);

    Result<AccessToken> Login(User user, string password);
}
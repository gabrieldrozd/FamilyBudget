using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Models;

namespace FamilyBudget.Domain.Interfaces.Providers;

public interface ITokenProvider
{
    AccessToken CreateToken(Guid userId, string firstName, string lastName, string email, Role role);
}
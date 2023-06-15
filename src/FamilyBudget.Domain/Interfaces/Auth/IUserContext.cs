using FamilyBudget.Common.Domain.ValueObjects;

namespace FamilyBudget.Domain.Interfaces.Auth;

public interface IUserContext
{
    public bool IsAuthenticated { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}
using FamilyBudget.Common.Domain.Primitives;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Domain.Definitions;

namespace FamilyBudget.Domain.Entities;

public class User : Entity
{
    #region Properties

    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }

    #region Relationships

    public ICollection<BudgetPlan> BudgetPlans { get; set; }
    public ICollection<SharedBudget> SharedBudgets { get; set; }

    #endregion

    #endregion

    #region Constructors

    private User(Guid externalId) : base(externalId)
    {
    }

    private User(Guid externalId, string email, string phoneNumber, string firstName, string lastName, Role role) : this(externalId)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
    }

    #endregion

    #region Static Methods

    public static User Create(Guid userId, UserDefinition definition)
    {
        var role = Role.FromName(definition.Role);
        return new User(userId, definition.Email, definition.PhoneNumber, definition.FirstName, definition.LastName, role);
    }

    #endregion

    #region Public Methods

    public void SetPasswordHash(string passwordHash)
        => PasswordHash = passwordHash;

    #endregion
}
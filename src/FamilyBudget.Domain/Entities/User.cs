using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Domain.Primitives;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Domain.Definitions;

namespace FamilyBudget.Domain.Entities;

public class User : Entity
{
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }

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

    public static User Create(Guid userId, UserDefinition definition)
    {
        var role = Role.FromName(definition.Role);
        return new User(userId, definition.Email, definition.PhoneNumber, definition.FirstName, definition.LastName, role);
    }

    // TODO:
    // - install MediatR -> Configure it
    // - create a command to create user (Member) (Registration and needs to be accepted by Owner)
    // - create UserConfiguration (EF Core)
    // - create migration
    // - create AuthController !!! (JWT Tokens)

    // TODO:
    // - install MediatR -> Configure it
    // - create a command to create user (Member) (Registration and needs to be accepted by Owner)
    // - create UserConfiguration (EF Core)
    // - create migration
    // - create AuthController !!! (JWT Tokens)

    // TODO:
    // - install MediatR -> Configure it
    // - create a command to create user (Member) (Registration and needs to be accepted by Owner)
    // - create UserConfiguration (EF Core)
    // - create migration
    // - create AuthController !!! (JWT Tokens)

    // TODO:
    // - install MediatR -> Configure it
    // - create a command to create user (Member) (Registration and needs to be accepted by Owner)
    // - create UserConfiguration (EF Core)
    // - create migration
    // - create AuthController !!! (JWT Tokens)

    public void SetPasswordHash(string passwordHash)
        => PasswordHash = passwordHash;
}
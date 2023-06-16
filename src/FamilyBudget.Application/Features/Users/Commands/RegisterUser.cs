using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Users.Commands;

public record RegisterUser(string Email, string FirstName, string LastName, string Role) : ICommand<UserBaseDto>;
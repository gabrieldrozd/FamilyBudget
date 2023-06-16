using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Models;

namespace FamilyBudget.Application.Features.Auth.Commands;

public record LoginUser(string Email, string Password) : ICommand<AccessToken>;
using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Users.Commands;

public record DeleteUser(Guid ExternalId) : ICommand;
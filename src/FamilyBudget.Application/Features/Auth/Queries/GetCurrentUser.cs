using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Models;

namespace FamilyBudget.Application.Features.Auth.Queries;

public record GetCurrentUser : IQuery<AccessToken>;
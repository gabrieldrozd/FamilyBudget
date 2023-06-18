using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;

namespace FamilyBudget.Application.Features.Users.Queries;

public record GetAllUsersExceptCurrent : IQuery<List<UserBaseDto>>;
using FamilyBudget.Application.DTO;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Api.Pagination;

namespace FamilyBudget.Application.Features.Users.Queries;

public record BrowseUsers(Pagination Pagination) : IQuery<PaginatedList<UserDto>>;
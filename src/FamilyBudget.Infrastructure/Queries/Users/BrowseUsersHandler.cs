using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Users.Queries;
using FamilyBudget.Application.Mappings;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Interfaces.Repositories;

namespace FamilyBudget.Infrastructure.Queries.Users;

internal sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, PaginatedList<UserBaseDto>>
{
    private readonly IUserRepository _userRepository;

    public BrowseUsersHandler(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<Result<PaginatedList<UserBaseDto>>> Handle(BrowseUsers request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.BrowseAsync(request.Pagination);
        var mapped = users.MapTo(UserMappings.ToBaseDto);

        return Result.Success(mapped);
    }
}
using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Users.Queries;
using FamilyBudget.Application.Mappings;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Repositories;

namespace FamilyBudget.Infrastructure.Queries.Users;

internal sealed class GetAllUsersExceptCurrentHandler : IQueryHandler<GetAllUsersExceptCurrent, List<UserBaseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;

    public GetAllUsersExceptCurrentHandler(IUserRepository userRepository, IUserContext userContext)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }

    public async Task<Result<List<UserBaseDto>>> Handle(GetAllUsersExceptCurrent request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllExcept(_userContext.UserId);
        var mapped = users.MapTo(UserMappings.ToBaseDto);

        return Result.Success(mapped);
    }
}
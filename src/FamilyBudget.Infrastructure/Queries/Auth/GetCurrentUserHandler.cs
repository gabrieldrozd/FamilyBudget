using FamilyBudget.Application.Features.Auth.Queries;
using FamilyBudget.Common.Abstractions.Communication;
using FamilyBudget.Common.Models;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Providers;
using FamilyBudget.Domain.Interfaces.Repositories;

namespace FamilyBudget.Infrastructure.Queries.Auth;

public sealed class GetCurrentUserHandler : IQueryHandler<GetCurrentUser, AccessToken>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserContext _userContext;

    public GetCurrentUserHandler(
        IUserRepository userRepository,
        ITokenProvider tokenProvider,
        IUserContext userContext)
    {
        _userRepository = userRepository;
        _tokenProvider = tokenProvider;
        _userContext = userContext;
    }

    public async Task<Result<AccessToken>> Handle(GetCurrentUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.ExternalId == _userContext.UserId);
        if (user is null) return Result.Unauthorized<AccessToken>();

        var token = _tokenProvider.CreateToken(
            user.ExternalId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role);

        return Result.Success(token);
    }
}
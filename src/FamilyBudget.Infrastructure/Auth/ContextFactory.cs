using FamilyBudget.Domain.Interfaces.Auth;
using Microsoft.AspNetCore.Http;

namespace FamilyBudget.Infrastructure.Auth;

internal class ContextFactory : IContextFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ContextFactory(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public IUserContext Create()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        return httpContext is null
            ? UserContext.Empty
            : new UserContext(httpContext);
    }
}
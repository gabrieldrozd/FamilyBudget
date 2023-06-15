using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Domain.Interfaces.Auth;
using Microsoft.AspNetCore.Http;

namespace FamilyBudget.Infrastructure.Auth;

internal sealed class UserContext : IUserContext
{
    public bool IsAuthenticated { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }

    private UserContext()
    {
    }

    public UserContext(HttpContext httpContext)
    {
        IsAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;

        var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var securityToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

        Populate(securityToken?.Claims);
    }

    public static IUserContext Empty => new UserContext();

    private void Populate(IEnumerable<Claim> claims)
    {
        claims = claims.ToList();
        UserId = claims
            .Where(x => x.Type == "userId")
            .Select(x => Guid.Parse(x.Value))
            .FirstOrDefault();
        Email = claims
            .Where(x => x.Type == "email")
            .Select(x => x.Value)
            .FirstOrDefault();
        Role = claims
            .Where(x => x.Type == "role")
            .Select(x => Role.FromName(x.Value))
            .FirstOrDefault();
    }
}
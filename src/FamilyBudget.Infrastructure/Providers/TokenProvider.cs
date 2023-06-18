using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Models;
using FamilyBudget.Domain.Interfaces.Providers;
using FamilyBudget.Infrastructure.Auth;
using Microsoft.IdentityModel.Tokens;

namespace FamilyBudget.Infrastructure.Providers;

internal sealed class TokenProvider : ITokenProvider
{
    private readonly SigningCredentials _signingCredentials;
    private readonly AuthOptions _options;
    private readonly IClock _clock;

    public TokenProvider(
        AuthOptions options,
        IClock clock)
    {
        _signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)),
            SecurityAlgorithms.HmacSha256);

        _options = options;
        _clock = clock;
    }

    public AccessToken CreateToken(Guid userId, string firstName, string lastName, string email, Role role)
    {
        var expires = _clock.Current().AddMinutes(_options.ExpiryInMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("userId", userId.ToString()),
            new("fullName", $"{firstName} {lastName}"),
            new("email", email),
            new("role", role.Name)
        };


        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: expires,
            signingCredentials: _signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        var accessToken = new AccessToken
        {
            Token = tokenValue,
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Role = role.Name
        };

        return accessToken;
    }
}
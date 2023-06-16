using System.Text;
using FamilyBudget.Domain.Interfaces.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FamilyBudget.Infrastructure.Auth;

internal static class Extensions
{
    private const string OptionsSectionName = "Auth";
    private const string DefaultsSectionName = "AuthDefaults";

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddTransient<IContextFactory, ContextFactory>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());

        var authOptions = services.GetOptions<AuthOptions>(OptionsSectionName);
        services.AddSingleton(authOptions);

        var authDefaults = services.GetOptions<AuthDefaults>(DefaultsSectionName);
        services.AddSingleton(authDefaults);

        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.IssuerSigningKey)),
                };
            });

        services.AddAuthorization();

        return services;
    }
}
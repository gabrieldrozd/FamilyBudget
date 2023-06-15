using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Providers;
using FamilyBudget.Domain.Interfaces.Services;
using FamilyBudget.Infrastructure.Auth;
using FamilyBudget.Infrastructure.Database;
using FamilyBudget.Infrastructure.Middleware;
using FamilyBudget.Infrastructure.Providers;
using FamilyBudget.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDatabase();
        services.AddAuth();

        // Middleware
        services.AddSingleton<ExceptionMiddleware>();

        // Providers
        services.AddSingleton<IClockProvider, ClockProvider>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        // Services
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        // Middleware
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.BindOptions<T>(sectionName);
    }

    private static T BindOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}
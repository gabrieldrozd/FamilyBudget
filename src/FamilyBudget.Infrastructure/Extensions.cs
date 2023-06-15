using FamilyBudget.Infrastructure.Database;
using FamilyBudget.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDatabase(config);

        // Middleware
        services.AddSingleton<ExceptionMiddleware>();

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

    public static T GetOptions<T>(this IConfiguration config, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = config.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}
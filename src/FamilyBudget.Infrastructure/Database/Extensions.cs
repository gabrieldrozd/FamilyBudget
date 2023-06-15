using FamilyBudget.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Infrastructure.Database;

internal static class Extensions
{
    private const string SectionName = "Database";

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var options = services.GetOptions<DbOptions>(SectionName);
        services.AddSingleton(options);

        services.AddDbContext<FamilyBudgetDbContext>(opt =>
        {
            opt.UseNpgsql(options.ConnectionString);
            opt.EnableDetailedErrors();
            opt.EnableSensitiveDataLogging();
        });

        services.AddHostedService<DbInitializer>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // TODO: Add repositories and services
        services.AddRepositories();
        // services.AddServices();
        return services;
    }
}

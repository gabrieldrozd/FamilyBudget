using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Infrastructure.Database;

internal static class Extensions
{
    private const string SectionName = "Database";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DbOptions>(config.GetRequiredSection(SectionName));
        var connectionString = config.GetOptions<DbOptions>(SectionName).ConnectionString;
        services.AddDbContext<FamilyBudgetDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
            opt.EnableDetailedErrors();
            opt.EnableSensitiveDataLogging();
        });

        services.AddHostedService<DbInitializer>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // TODO: Add repositories and services
        // services.AddRepositories();
        // services.AddServices();
        return services;
    }
}

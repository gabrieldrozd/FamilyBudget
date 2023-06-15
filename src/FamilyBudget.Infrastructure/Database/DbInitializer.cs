using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FamilyBudget.Infrastructure.Database;

internal sealed class DbInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(IServiceProvider serviceProvider, ILogger<DbInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FamilyBudgetDbContext>();
            await context.Database.MigrateAsync(cancellationToken);

            // TODO: User seeder here !
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "An error occurred while migrating or initializing the database: {Ex}\\n{ExMessage}\\n{ExStackTrace}",
                ex, ex.Message, ex.StackTrace);
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
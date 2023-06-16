using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FamilyBudget.Infrastructure.Database;

internal sealed class DbInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(
        IServiceProvider serviceProvider,
        ILogger<DbInitializer> logger)
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

            var defaults = scope.ServiceProvider.GetRequiredService<AuthDefaults>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();

            var defaultUser = await context.Users.FirstOrDefaultAsync(u => u.Email == defaults.OwnerEmail, cancellationToken);
            if (defaultUser is null)
            {
                _logger.LogInformation("Seeding owner user...");
                await SeedOwner(context, passwordHasher, defaults);
                _logger.LogInformation("Owner user seeded successfully!");
            }
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

    private static async Task SeedOwner(FamilyBudgetDbContext context, IPasswordHasher<User> passwordHasher, AuthDefaults defaults)
    {
        var userId = Guid.NewGuid();
        var userDefinition = new UserDefinition
        {
            Email = defaults.OwnerEmail,
            FirstName = defaults.OwnerFirstName,
            LastName = defaults.OwnerLastName,
            Role = Role.Owner.Name
        };

        var ownerUser = User.Create(userId, userDefinition);
        var passwordHash = passwordHasher.HashPassword(ownerUser, defaults.Password);
        ownerUser.SetPasswordHash(passwordHash);

        await context.Users.AddAsync(ownerUser);
        var result = await context.SaveChangesAsync();
        Console.WriteLine(result);
    }
}
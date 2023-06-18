using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;
using FamilyBudget.Infrastructure.Database.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Infrastructure.Database.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Base
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        services.AddScoped<IBudgetPlanRepository, BudgetPlanRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<ISharedBudgetRepository, SharedBudgetRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
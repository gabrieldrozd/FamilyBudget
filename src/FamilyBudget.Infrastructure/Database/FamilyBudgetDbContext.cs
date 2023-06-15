using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Entities.MoneyFlow;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infrastructure.Database;

public class FamilyBudgetDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<SharedBudget> SharedBudgets { get; set; }
    public DbSet<BudgetPlan> BudgetPlans { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    public FamilyBudgetDbContext(DbContextOptions<FamilyBudgetDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}
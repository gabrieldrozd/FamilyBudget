using FamilyBudget.Domain.Entities.Budget;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyBudget.Infrastructure.Database.Configurations;

public class BudgetPlanConfiguration : IEntityTypeConfiguration<BudgetPlan>
{
    public void Configure(EntityTypeBuilder<BudgetPlan> builder)
    {
        builder.HasKey(x => x.ExternalId);
        builder.HasAlternateKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Ignore(x => x.Balance);

        #region Incomes

        builder.OwnsMany(x => x.Incomes, income =>
        {
            income.HasKey(x => x.ExternalId);

            income.Property(x => x.Name)
                .IsRequired();

            income.Property(x => x.Date)
                .IsRequired();

            income.Property(x => x.Amount)
                .HasPrecision(15, 2)
                .IsRequired();
        });

        #endregion

        #region Expenses

        builder.OwnsMany(x => x.Expenses, expense =>
        {
            expense.HasKey(x => x.ExternalId);

            expense.Property(x => x.Name)
                .IsRequired();

            expense.Property(x => x.Date)
                .IsRequired();

            expense.Property(x => x.Amount)
                .HasPrecision(15, 2)
                .IsRequired();
        });

        #endregion
    }
}
using FamilyBudget.Domain.Entities.Budget;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyBudget.Infrastructure.Database.Configurations;

public class SharedBudgetConfiguration : IEntityTypeConfiguration<SharedBudget>
{
    public void Configure(EntityTypeBuilder<SharedBudget> builder)
    {
        builder.HasKey(x => x.ExternalId);
        builder.HasAlternateKey(x => x.Id);

        builder.Property(x => x.DateShared)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.SharedBudgets)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.BudgetPlan)
            .WithMany(x => x.SharedBudgets)
            .HasForeignKey(x => x.BudgetPlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
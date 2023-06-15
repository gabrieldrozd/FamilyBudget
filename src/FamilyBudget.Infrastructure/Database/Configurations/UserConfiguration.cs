using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyBudget.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.ExternalId);
        builder.HasAlternateKey(x => x.Id);
        builder.HasAlternateKey(x => x.Email);

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .IsRequired();

        builder.Property(x => x.LastName)
            .IsRequired();

        builder.Property(x => x.Role)
            .HasConversion(x => x.Value, x => Role.FromValue(x))
            .IsRequired();

        builder
            .HasMany(x => x.BudgetPlans)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.SharedBudgets)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
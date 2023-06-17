﻿// <auto-generated />
using System;
using FamilyBudget.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FamilyBudget.Infrastructure.Database.Migrations
{
    [DbContext(typeof(FamilyBudgetDbContext))]
    partial class FamilyBudgetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FamilyBudget.Domain.Entities.Budget.BudgetPlan", b =>
                {
                    b.Property<Guid>("ExternalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ExternalId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BudgetPlans");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.Budget.SharedBudget", b =>
                {
                    b.Property<Guid>("ExternalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BudgetPlanId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateShared")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ExternalId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("BudgetPlanId");

                    b.HasIndex("UserId");

                    b.ToTable("SharedBudgets");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.MoneyFlow.Expense", b =>
                {
                    b.Property<Guid>("ExternalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasPrecision(15, 2)
                        .HasColumnType("numeric(15,2)");

                    b.Property<Guid>("BudgetPlanId")
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ExternalId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("BudgetPlanId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.MoneyFlow.Income", b =>
                {
                    b.Property<Guid>("ExternalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasPrecision(15, 2)
                        .HasColumnType("numeric(15,2)");

                    b.Property<Guid>("BudgetPlanId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("ExternalId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("BudgetPlanId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("ExternalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("ExternalId");

                    b.HasAlternateKey("Email");

                    b.HasAlternateKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.Budget.BudgetPlan", b =>
                {
                    b.HasOne("FamilyBudget.Domain.Entities.User", "Creator")
                        .WithMany("BudgetPlans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.Budget.SharedBudget", b =>
                {
                    b.HasOne("FamilyBudget.Domain.Entities.Budget.BudgetPlan", "BudgetPlan")
                        .WithMany("SharedBudgets")
                        .HasForeignKey("BudgetPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FamilyBudget.Domain.Entities.User", "User")
                        .WithMany("SharedBudgets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BudgetPlan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.MoneyFlow.Expense", b =>
                {
                    b.HasOne("FamilyBudget.Domain.Entities.Budget.BudgetPlan", "BudgetPlan")
                        .WithMany("Expenses")
                        .HasForeignKey("BudgetPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BudgetPlan");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.MoneyFlow.Income", b =>
                {
                    b.HasOne("FamilyBudget.Domain.Entities.Budget.BudgetPlan", "BudgetPlan")
                        .WithMany("Incomes")
                        .HasForeignKey("BudgetPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BudgetPlan");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.Budget.BudgetPlan", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Incomes");

                    b.Navigation("SharedBudgets");
                });

            modelBuilder.Entity("FamilyBudget.Domain.Entities.User", b =>
                {
                    b.Navigation("BudgetPlans");

                    b.Navigation("SharedBudgets");
                });
#pragma warning restore 612, 618
        }
    }
}

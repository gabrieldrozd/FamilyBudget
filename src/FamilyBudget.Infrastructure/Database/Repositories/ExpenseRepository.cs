using FamilyBudget.Domain.Entities.MoneyFlow;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Infrastructure.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infrastructure.Database.Repositories;

internal sealed class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
{
    private readonly DbSet<Expense> _expenses;

    public ExpenseRepository(FamilyBudgetDbContext context) : base(context)
    {
        _expenses = context.Expenses;
    }
}
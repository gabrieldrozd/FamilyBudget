using FamilyBudget.Domain.Entities.MoneyFlow;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Infrastructure.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infrastructure.Database.Repositories;

internal sealed class IncomeRepository : BaseRepository<Income>, IIncomeRepository
{
    private readonly DbSet<Income> _incomes;

    public IncomeRepository(FamilyBudgetDbContext context) : base(context)
    {
        _incomes = context.Incomes;
    }
}
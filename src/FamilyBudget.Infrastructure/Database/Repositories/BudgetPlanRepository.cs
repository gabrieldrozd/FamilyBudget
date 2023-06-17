using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Infrastructure.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infrastructure.Database.Repositories;

internal sealed class BudgetPlanRepository : BaseRepository<BudgetPlan>, IBudgetPlanRepository
{
    private readonly DbSet<BudgetPlan> _budgetPlans;

    public BudgetPlanRepository(FamilyBudgetDbContext context) : base(context)
    {
        _budgetPlans = context.BudgetPlans;
    }

    public async Task<BudgetPlan> GetByIdAsync(Guid id)
    {
        var result = await _budgetPlans
            .Where(x => x.ExternalId == id)
            .Include(x => x.Incomes)
            .Include(x => x.Expenses)
            .AsSplitQuery()
            .AsTracking()
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<PaginatedList<BudgetPlan>> BrowseAsync(Pagination pagination)
    {
        var result = await _budgetPlans
            .AddPagination(pagination)
            .Include(x => x.Incomes)
            .Include(x => x.Expenses)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

        var count = await TotalCountAsync();

        return PaginatedList<BudgetPlan>.Create(pagination, result, count);
    }
}
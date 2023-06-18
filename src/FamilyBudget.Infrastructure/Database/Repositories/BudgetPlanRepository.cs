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

    public async Task<BudgetPlan> GetDetails(Guid id)
    {
        var result = await _budgetPlans
            .Where(x => x.ExternalId == id)
            .Include(x => x.SharedBudgets)
            .Include(x => x.Incomes)
            .Include(x => x.Expenses)
            .Include(x => x.Creator).ThenInclude(x => x.SharedBudgets)
            .Include(x => x.Creator).ThenInclude(x => x.BudgetPlans)
            .AsSplitQuery()
            .AsTracking()
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<BudgetPlan> GetByIdAsync(Guid id)
    {
        var result = await _budgetPlans
            .Where(x => x.ExternalId == id)
            .Include(x => x.Incomes)
            .Include(x => x.Expenses)
            .Include(x => x.Creator)
            .AsSplitQuery()
            .AsTracking()
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<PaginatedList<BudgetPlan>> BrowseUserBudgetPlansAsync(Guid userExternalId, Pagination pagination)
    {
        var result = await _budgetPlans
            .Where(x => x.UserId == userExternalId)
            .AddPagination(pagination)
            .Include(x => x.Incomes)
            .Include(x => x.Expenses)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

        var count = await FilterTotalCountAsync(x => x.UserId == userExternalId);

        return PaginatedList<BudgetPlan>.Create(pagination, result, count);
    }
}
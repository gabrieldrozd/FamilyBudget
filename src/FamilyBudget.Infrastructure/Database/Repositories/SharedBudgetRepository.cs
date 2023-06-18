using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Infrastructure.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infrastructure.Database.Repositories;

internal sealed class SharedBudgetRepository : BaseRepository<SharedBudget>, ISharedBudgetRepository
{
    private readonly DbSet<SharedBudget> _sharedBudgets;

    public SharedBudgetRepository(FamilyBudgetDbContext context) : base(context)
    {
        _sharedBudgets = context.SharedBudgets;
    }

    public async Task<SharedBudget> GetDetails(Guid id)
    {
        var result = await _sharedBudgets
            .Where(x => x.ExternalId == id)
            .Include(x => x.BudgetPlan).ThenInclude(y => y.Expenses)
            .Include(x => x.BudgetPlan).ThenInclude(y => y.Incomes)
            .Include(x => x.User)
            .Include(x => x.User)
            .Include(x => x.BudgetPlan).ThenInclude(y => y.Creator)
            .AsSplitQuery()
            .AsTracking()
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<PaginatedList<SharedBudget>> BrowseBudgetsSharedWithUserAsync(Guid userExternalId, Pagination pagination)
    {
        var result = await _sharedBudgets
            .Where(x => x.UserId == userExternalId)
            .AddPagination(pagination)
            .Include(x => x.BudgetPlan).ThenInclude(y => y.Expenses)
            .Include(x => x.BudgetPlan).ThenInclude(y => y.Incomes)
            .Include(x => x.User)
            .Include(x => x.User)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

        var count = await FilterTotalCountAsync(x => x.UserId == userExternalId);

        return PaginatedList<SharedBudget>.Create(pagination, result, count);
    }
}
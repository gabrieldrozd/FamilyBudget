using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Infrastructure.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infrastructure.Database.Repositories;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(FamilyBudgetDbContext context) : base(context)
    {
        _users = context.Users;
    }

    public async Task<PaginatedList<User>> BrowseAsync(Pagination pagination)
    {
        var result = await _users
            .AddPagination(pagination)
            .Include(x => x.BudgetPlans).ThenInclude(y => y.Incomes)
            .Include(x => x.BudgetPlans).ThenInclude(y => y.Expenses)
            .Include(x => x.SharedBudgets).ThenInclude(y => y.BudgetPlan).ThenInclude(z => z.Incomes)
            .Include(x => x.SharedBudgets).ThenInclude(y => y.BudgetPlan).ThenInclude(z => z.Expenses)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

        var count = await TotalCountAsync();

        return PaginatedList<User>.Create(pagination, result, count);
    }
}
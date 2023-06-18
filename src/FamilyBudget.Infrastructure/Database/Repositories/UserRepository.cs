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
            .Include(x => x.BudgetPlans)
            .Include(x => x.SharedBudgets)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

        var count = await TotalCountAsync();

        return PaginatedList<User>.Create(pagination, result, count);
    }

    public async Task<List<User>> GetByIds(Guid[] userIds)
    {
        var result = await _users
            .Where(x => userIds.Contains(x.ExternalId))
            .Include(x => x.BudgetPlans)
            .Include(x => x.SharedBudgets)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

        return result;
    }

    public async Task<List<User>> GetAllExcept(Guid userId)
    {
        var result = await _users
            .Where(x => !x.ExternalId.Equals(userId))
            .Include(x => x.BudgetPlans)
            .Include(x => x.SharedBudgets)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

        return result;
    }
}
using System.Linq.Expressions;
using FamilyBudget.Common.Domain.Primitives;
using FamilyBudget.Domain.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infrastructure.Database.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : Entity
{
    private readonly FamilyBudgetDbContext _context;

    public BaseRepository(FamilyBudgetDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> criteria)
    {
        var result = await _context
            .Set<TEntity>()
            .AsTracking()
            .FirstOrDefaultAsync(criteria);

        return result;
    }

    public void Insert(TEntity entity)
        => _context.Set<TEntity>().Add(entity);

    public void InsertRange(IEnumerable<TEntity> entities)
        => _context.Set<TEntity>().AddRange(entities);

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);
}
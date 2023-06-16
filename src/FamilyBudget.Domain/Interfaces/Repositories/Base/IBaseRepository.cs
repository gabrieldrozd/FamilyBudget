using System.Linq.Expressions;
using FamilyBudget.Common.Domain.Primitives;

namespace FamilyBudget.Domain.Interfaces.Repositories.Base;

public interface IBaseRepository<TEntity>
    where TEntity : Entity
{
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> criteria);

    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> criteria);

    Task<int> TotalCountAsync();

    Task<int> FilterTotalCountAsync(Expression<Func<TEntity, bool>> predicate);

    void Insert(TEntity entity);

    void InsertRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}
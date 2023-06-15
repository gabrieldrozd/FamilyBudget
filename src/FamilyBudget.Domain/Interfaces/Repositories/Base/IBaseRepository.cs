using System.Linq.Expressions;
using FamilyBudget.Common.Domain.Primitives;

namespace FamilyBudget.Domain.Interfaces.Repositories.Base;

public interface IBaseRepository<TEntity>
    where TEntity : Entity
{
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> criteria);

    void Insert(TEntity entity);

    void InsertRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}
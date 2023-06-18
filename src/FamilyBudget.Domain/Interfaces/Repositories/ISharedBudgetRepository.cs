using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Domain.Interfaces.Repositories;

public interface ISharedBudgetRepository : IBaseRepository<SharedBudget>
{
    Task<SharedBudget> GetDetails(Guid id);

    Task<PaginatedList<SharedBudget>> BrowseBudgetsSharedWithUserAsync(Guid userExternalId, Pagination pagination);
}
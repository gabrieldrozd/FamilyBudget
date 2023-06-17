using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Domain.Interfaces.Repositories;

public interface IBudgetPlanRepository : IBaseRepository<BudgetPlan>
{
    Task<BudgetPlan> GetByIdAsync(Guid id);
    Task<PaginatedList<BudgetPlan>> BrowseAsync(Pagination pagination);
}
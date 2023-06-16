using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<PaginatedList<User>> BrowseAsync(Pagination pagination);
}
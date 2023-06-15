using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Infrastructure.Database.Repositories.Base;

namespace FamilyBudget.Infrastructure.Database.Repositories;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(FamilyBudgetDbContext context) : base(context)
    {
    }
}

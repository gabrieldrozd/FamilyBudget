using FamilyBudget.Common.Results;

namespace FamilyBudget.Domain.Interfaces.Repositories.Base;

public interface IUnitOfWork
{
    Task<Result> CommitAsync();
}
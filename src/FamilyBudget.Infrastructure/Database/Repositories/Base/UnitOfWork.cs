using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Infrastructure.Database.Repositories.Base;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly FamilyBudgetDbContext _context;

    public UnitOfWork(FamilyBudgetDbContext context)
        => _context = context;

    public async Task<Result> CommitAsync()
    {
        bool commitStatus;
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            commitStatus = true;
        }
        catch (Exception)
        {
            commitStatus = false;
            await transaction.RollbackAsync();
        }

        return commitStatus
            ? Result.Success()
            : Result.DatabaseFailure();
    }
}
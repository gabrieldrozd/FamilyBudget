using FamilyBudget.Domain.Entities.MoneyFlow;
using FamilyBudget.Domain.Interfaces.Repositories.Base;

namespace FamilyBudget.Domain.Interfaces.Repositories;

public interface IExpenseRepository : IBaseRepository<Expense>
{
}
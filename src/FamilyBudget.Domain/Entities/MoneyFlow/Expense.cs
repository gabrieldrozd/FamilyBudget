using FamilyBudget.Common.Domain.Primitives;
using FamilyBudget.Domain.Entities.Budget;

namespace FamilyBudget.Domain.Entities.MoneyFlow;

public class Expense : Entity
{
    #region Properties

    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }

    // TODO: Create ExpenseType enumeration
    // public ExpenseType Type { get; set; }

    #region Relationships

    public Guid BudgetPlanId { get; set; }
    public BudgetPlan BudgetPlan { get; set; }

    #endregion

    #endregion

    #region Constructors

    private Expense(Guid externalId) : base(externalId)
    {
    }

    private Expense(Guid externalId, string name, DateTime date, decimal amount, Guid budgetPlanId) : this(externalId)
    {
        Name = name;
        Date = date;
        Amount = amount;
        BudgetPlanId = budgetPlanId;
    }

    #endregion

    #region Static Methods

    public static Expense Create(Guid expenseId, string name, DateTime date, decimal amount, Guid budgetPlanId)
        => new(expenseId, name, date, amount, budgetPlanId);

    #endregion
}
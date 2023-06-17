using FamilyBudget.Common.Domain.Primitives;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.ValueObjects;

namespace FamilyBudget.Domain.Entities.MoneyFlow;

public class Expense : Entity
{
    #region Properties

    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public ExpenseCategory Category { get; set; }

    #region Relationships

    public Guid BudgetPlanId { get; set; }
    public BudgetPlan BudgetPlan { get; set; }

    #endregion

    #endregion

    #region Constructors

    private Expense(Guid externalId) : base(externalId)
    {
    }

    private Expense(Guid externalId, string name, DateTime date, decimal amount, ExpenseCategory category, Guid budgetPlanId)
        : this(externalId)
    {
        Name = name;
        Date = date;
        Amount = amount;
        Category = category;
        BudgetPlanId = budgetPlanId;
    }

    #endregion

    #region Static Methods

    public static Expense Create(Guid expenseId, string name, DateTime date, decimal amount, string expenseCategory, Guid budgetPlanId)
    {
        var category = ExpenseCategory.FromName(expenseCategory);
        return new Expense(expenseId, name, date, amount, category, budgetPlanId);
    }

    #endregion
}
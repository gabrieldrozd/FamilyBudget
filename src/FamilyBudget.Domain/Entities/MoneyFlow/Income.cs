using FamilyBudget.Common.Domain.Primitives;
using FamilyBudget.Domain.Entities.Budget;

namespace FamilyBudget.Domain.Entities.MoneyFlow;

public class Income : Entity
{
    #region Properties

    public DateTime Date { get; set; }
    public decimal Amount { get; set; }

    // TODO: Create IncomeType enumeration
    // public IncomeType Type { get; set; }

    #region Relationships

    public Guid BudgetPlanId { get; set; }
    public BudgetPlan BudgetPlan { get; set; }

    #endregion

    #endregion

    #region Constructors

    private Income(Guid externalId) : base(externalId)
    {
    }

    private Income(Guid externalId, DateTime date, decimal amount, Guid budgetPlanId) : this(externalId)
    {
        Date = date;
        Amount = amount;
        BudgetPlanId = budgetPlanId;
    }

    #endregion

    #region Static Methods

    public static Income Create(Guid incomeId, DateTime date, decimal amount, Guid budgetPlanId)
        => new(incomeId, date, amount, budgetPlanId);

    #endregion
}
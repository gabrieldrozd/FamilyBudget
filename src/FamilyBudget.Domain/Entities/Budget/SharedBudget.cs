using FamilyBudget.Common.Domain.Primitives;

namespace FamilyBudget.Domain.Entities.Budget;

public class SharedBudget : Entity
{
    #region Properties

    // Relationship to User
    public Guid UserId { get; set; }
    public User User { get; set; }

    // Relationship to BudgetPlan
    public Guid BudgetPlanId { get; set; }
    public BudgetPlan BudgetPlan { get; set; }

    public DateTime DateShared { get; set; }

    #endregion

    #region Constructors

    private SharedBudget(Guid externalId) : base(externalId)
    {
    }

    private SharedBudget(Guid externalId, Guid userId, Guid budgetPlanId, DateTime dateShared)
        : this(externalId)
    {
        UserId = userId;
        BudgetPlanId = budgetPlanId;
        DateShared = dateShared;
    }

    #endregion

    #region Static Methods

    public static SharedBudget Create(Guid sharedBudgetId, Guid budgetPlanId, Guid userId, DateTime dateShared)
        => new(sharedBudgetId, userId, budgetPlanId, dateShared);

    #endregion
}
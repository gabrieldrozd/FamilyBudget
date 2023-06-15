using FamilyBudget.Common.Domain.Primitives;
using FamilyBudget.Domain.Definitions;

namespace FamilyBudget.Domain.Entities;

public class BudgetPlan : Entity
{
    #region Properties

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Balance { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    #region Relationships

    public ICollection<Income> Incomes { get; set; }
    public ICollection<Expense> Expenses { get; set; }

    public Guid UserId { get; set; }
    public User Creator { get; set; }

    #endregion

    #endregion

    #region Constructors

    private BudgetPlan(Guid externalId) : base(externalId)
    {
    }

    private BudgetPlan(Guid externalId, string name, string description, decimal balance, DateTime startDate, DateTime endDate, Guid userId)
        : this(externalId)
    {
        Name = name;
        Description = description;
        Balance = balance;
        StartDate = startDate;
        EndDate = endDate;
        Incomes = new List<Income>();
        Expenses = new List<Expense>();
        UserId = userId;
    }

    #endregion

    #region Static Methods

    public static BudgetPlan Create(Guid budgetPlanId, BudgetPlanDefinition definition, Guid userId)
        => new(budgetPlanId, definition.Name, definition.Description, definition.Balance, definition.StartDate, definition.EndDate, userId);

    #endregion

    #region Public Methods

    public void AddIncome(Income income)
    {
        if (Incomes.Contains(income)) return;
        Incomes.Add(income);
    }

    public void AddIncomes(IEnumerable<Income> incomes)
    {
        foreach (var income in incomes)
            AddIncome(income);
    }

    public void AddExpense(Expense expense)
    {
        if (Expenses.Contains(expense)) return;
        Expenses.Add(expense);
    }

    public void AddExpenses(IEnumerable<Expense> expenses)
    {
        foreach (var expense in expenses)
            AddExpense(expense);
    }

    #endregion
}
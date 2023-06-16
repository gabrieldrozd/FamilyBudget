namespace FamilyBudget.Domain.Definitions;

public class BudgetPlanDefinition
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

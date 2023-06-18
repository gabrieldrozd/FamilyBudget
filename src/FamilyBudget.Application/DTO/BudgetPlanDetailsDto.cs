namespace FamilyBudget.Application.DTO;

public class BudgetPlanDetailsDto
{
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Balance { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<IncomeDto> Incomes { get; set; }
    public List<ExpenseDto> Expenses { get; set; }

    public UserBaseDto Creator { get; set; }
}
namespace FamilyBudget.Application.DTO;

public class SharedBudgetDto
{
    public Guid ExternalId { get; set; }
    public Guid BudgetExternalId { get; set; }
    public DateTime DateShared { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Balance { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<IncomeDto> Incomes { get; set; }
    public List<ExpenseDto> Expenses { get; set; }
}
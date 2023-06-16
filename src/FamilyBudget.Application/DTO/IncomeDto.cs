namespace FamilyBudget.Application.DTO;

public class IncomeDto
{
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}
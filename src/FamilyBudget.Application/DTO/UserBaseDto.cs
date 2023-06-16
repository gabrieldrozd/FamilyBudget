namespace FamilyBudget.Application.DTO;

public class UserBaseDto
{
    public Guid ExternalId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public int BudgetPlans { get; set; }
    public int SharedBudgets { get; set; }
}
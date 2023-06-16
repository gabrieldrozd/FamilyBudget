namespace FamilyBudget.Application.DTO;

public class UserDto
{
    public Guid ExternalId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }

    public List<BudgetPlanDto> BudgetPlans { get; set; }
    public List<SharedBudgetDto> SharedBudgets { get; set; }
}
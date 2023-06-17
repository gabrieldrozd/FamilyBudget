using FamilyBudget.Common.Abstractions;

namespace FamilyBudget.Domain.ValueObjects;

public record ExpenseCategory : Enumeration<ExpenseCategory>
{
    public static readonly ExpenseCategory Food = new FoodExpenseCategory();
    public static readonly ExpenseCategory Housing = new HousingExpenseCategory();
    public static readonly ExpenseCategory Transportation = new TransportationExpenseCategory();
    public static readonly ExpenseCategory Clothing = new ClothingExpenseCategory();
    public static readonly ExpenseCategory HealthCare = new HealthCareExpenseCategory();
    public static readonly ExpenseCategory Personal = new PersonalExpenseCategory();
    public static readonly ExpenseCategory Education = new EducationExpenseCategory();
    public static readonly ExpenseCategory Entertainment = new EntertainmentExpenseCategory();
    public static readonly ExpenseCategory Other = new OtherExpenseCategory();

    private ExpenseCategory(int value, string name)
        : base(value, name)
    {
    }

    public override string ToString() => Name;

    private sealed record FoodExpenseCategory() : ExpenseCategory(0, "Food");
    private sealed record HousingExpenseCategory() : ExpenseCategory(1, "Housing");
    private sealed record TransportationExpenseCategory() : ExpenseCategory(2, "Transportation");
    private sealed record ClothingExpenseCategory() : ExpenseCategory(3, "Clothing");
    private sealed record HealthCareExpenseCategory() : ExpenseCategory(4, "Health Care");
    private sealed record PersonalExpenseCategory() : ExpenseCategory(5, "Personal");
    private sealed record EducationExpenseCategory() : ExpenseCategory(6, "Education");
    private sealed record EntertainmentExpenseCategory() : ExpenseCategory(7, "Entertainment");
    private sealed record OtherExpenseCategory() : ExpenseCategory(8, "Other");
}
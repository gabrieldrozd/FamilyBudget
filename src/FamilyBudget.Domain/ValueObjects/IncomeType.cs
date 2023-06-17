using FamilyBudget.Common.Abstractions;

namespace FamilyBudget.Domain.ValueObjects;

public record IncomeType : Enumeration<IncomeType>
{
    public static readonly IncomeType Salary = new SalaryIncomeType();
    public static readonly IncomeType Investment = new InvestmentIncomeType();
    public static readonly IncomeType Gift = new GiftIncomeType();
    public static readonly IncomeType Other = new OtherIncomeType();

    private IncomeType(int value, string name)
        : base(value, name)
    {
    }

    public override string ToString() => Name;

    private sealed record SalaryIncomeType() : IncomeType(0, "Salary");
    private sealed record InvestmentIncomeType() : IncomeType(1, "Investment");
    private sealed record GiftIncomeType() : IncomeType(2, "Gift");
    private sealed record OtherIncomeType() : IncomeType(3, "Other");
}
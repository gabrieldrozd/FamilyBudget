using FamilyBudget.Common.Types;
using FamilyBudget.Common.Types.General;

namespace FamilyBudget.Common.Domain.ValueObjects;

public record Role : Enumeration<Role>
{
    public static readonly Role Member = new MemberRole();
    public static readonly Role Owner = new OwnerRole();

    private Role(int value, string name)
        : base(value, name)
    {
    }

    public override string ToString() => Name;

    private sealed record MemberRole() : Role(0, "Member");
    private sealed record OwnerRole() : Role(1, "Owner");
}
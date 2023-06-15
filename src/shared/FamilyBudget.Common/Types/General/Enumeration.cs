using System.Reflection;

namespace FamilyBudget.Common.Types.General;

public abstract record Enumeration<TEnum>(int Value, string Name)
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    public static bool IsValid(TEnum tEnum)
        => Enumerations.ContainsKey(tEnum.Value);

    public static bool IsValid(int value)
        => Enumerations.ContainsKey(value);

    public static bool IsValid(string name)
        => Enumerations.Values.Any(x => x.Name == name);

    public virtual bool Equals(Enumeration<TEnum> other)
        => other switch
        {
            null => false,
            not null => GetType() == other.GetType() &&
                        Value == other.Value
        };

    public static TEnum FromValue(int value)
        => Enumerations.TryGetValue(value, out var enumeration)
            ? enumeration
            : default;

    public static TEnum FromName(string name)
        => Enumerations.Values.SingleOrDefault(x => x.Name == name);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Name;

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo =>
                (TEnum) fieldInfo.GetValue(default));

        return fieldsForType.ToDictionary(x => x!.Value);
    }
}
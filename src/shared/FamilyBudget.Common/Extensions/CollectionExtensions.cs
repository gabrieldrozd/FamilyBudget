using Microsoft.IdentityModel.Tokens;

namespace FamilyBudget.Common.Extensions;

public static class CollectionExtensions
{
    public static List<T1> MapTo<T, T1>(this IEnumerable<T> source, Func<T, T1> func, bool mapNulls = false)
    {
        if (mapNulls && source is null) return null;
        var enumerable = source as T[] ?? source.ToArray();
        return enumerable.IsNullOrEmpty()
            ? new List<T1>()
            : enumerable.Select(func).ToList();
    }
}
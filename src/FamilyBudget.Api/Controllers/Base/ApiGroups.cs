namespace FamilyBudget.Api.Controllers.Base;

public static class ApiGroups
{
    public const string Users = "users";
    public const string Financial = "financial";

    public static IDictionary<string, string> GetNameValueDictionary()
    {
        var nameValue = new Dictionary<string, string>();

        object structValue = default(ApiGroups);

        foreach (var group in typeof(ApiGroups).GetFields())
        {
            var value = group.GetValue(structValue).ToString();
            var name = group.Name;
            nameValue.Add(name, value);
        }

        return nameValue;
    }
}
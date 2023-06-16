namespace FamilyBudget.Common.Api.Pagination;

public class PaginatedList<T> where T : class
{
    public PaginationInfo Pagination { get; set; }
    public IReadOnlyList<T> List { get; set; }

    private PaginatedList()
    {
    }

    private PaginatedList(Pagination pagination, IReadOnlyList<T> list, int totalItems)
    {
        Pagination = PaginationInfo.Create(pagination.PageIndex, pagination.PageSize, totalItems, list.Count);
        List = list;
    }

    private PaginatedList(PaginationInfo pagination, IReadOnlyList<T> list)
    {
        Pagination = pagination;
        List = list;
    }

    public static PaginatedList<T> Create(Pagination pagination, IEnumerable<T> list, int totalItems)
    {
        var enumerable = list as T[] ?? list.ToArray();

        return new PaginatedList<T>(
            pagination,
            enumerable.ToList(),
            totalItems);
    }

    public static PaginatedList<T> Create(PaginationInfo pagination, IEnumerable<T> list)
    {
        var enumerable = list as T[] ?? list.ToArray();

        return new PaginatedList<T>(
            pagination,
            enumerable.ToList());
    }
}
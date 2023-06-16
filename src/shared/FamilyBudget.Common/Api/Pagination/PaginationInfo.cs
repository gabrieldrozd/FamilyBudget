namespace FamilyBudget.Common.Api.Pagination;

public class PaginationInfo
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int Count { get; set; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageSize * PageIndex < TotalItems;

    private PaginationInfo()
    {
    }

    private PaginationInfo(int pageIndex, int pageSize, int totalItems, int count)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalItems = totalItems;
        Count = count;
    }

    public static PaginationInfo Create(int pageIndex, int pageSize, int totalItems, int count)
        => new(pageIndex, pageSize, totalItems, count);
}
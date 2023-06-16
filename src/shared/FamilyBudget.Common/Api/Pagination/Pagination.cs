using FamilyBudget.Common.Constants;

namespace FamilyBudget.Common.Api.Pagination;

public class Pagination
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int Skip => PageSize * (PageIndex - 1);
    public bool IsAscending { get; set; }

    public Pagination()
    {
        PageSize = PaginationConstants.DefaultPageSize;
        PageIndex = PaginationConstants.DefaultPageIndex;
        IsAscending = PaginationConstants.DefaultOrderByAscending;
    }

    public Pagination(int pageSize, int pageIndex, bool isAscending)
    {
        PageSize = pageSize is < PaginationConstants.MinPageSize or > PaginationConstants.MaxPageSize
            ? PaginationConstants.DefaultPageSize
            : pageSize;

        PageIndex = pageIndex is < PaginationConstants.MinPageIndex or > PaginationConstants.MaxPageIndex
            ? PaginationConstants.DefaultPageIndex
            : pageIndex;

        IsAscending = isAscending;
    }
}
namespace Core.Shared
{
    public interface IQueryObject
    {
        SortBy SortBy { get; set; }
        bool IsSortAscending { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
    }
}

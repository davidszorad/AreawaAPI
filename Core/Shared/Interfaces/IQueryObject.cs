namespace Core.Shared
{
    public interface IQueryObject
    {
        SortBy SortBy { get; }
        bool IsSortDescending { get; }
        int Page { get; }
        int PageSize { get; }
    }
}

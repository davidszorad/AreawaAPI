using Core.Shared;

namespace Core.Reader
{
    public class ReaderQuery : IQueryObject
    {
        public SortBy SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

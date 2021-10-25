using Core.Shared;
using System;

namespace Core.Reader
{
    public class FilterQuery : IQueryObject
    {
        internal FilterQuery()
        {
        }

        public Guid? PublicId { get; internal set; }
        public string ShortId { get; internal set; }

        public SortBy SortBy { get; internal set; }
        public bool IsSortDescending { get; internal set; }
        public int Page { get; internal set; }
        public int PageSize { get; internal set; }
    }
}

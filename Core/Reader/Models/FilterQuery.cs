using System;

namespace Core.Reader
{
    public class FilterQuery
    {
        internal FilterQuery()
        {
        }

        public Guid? PublicId { get; internal set; }
        public string ShortId { get; internal set; }
    }
}

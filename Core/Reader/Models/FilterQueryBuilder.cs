using System;

namespace Core.Reader
{
    public class FilterQueryBuilder
    {
        private readonly FilterQuery _filterQuery = new FilterQuery();

        public FilterQueryBuilder SetPublicId(Guid value)
        {
            _filterQuery.PublicId = value;
            return this;
        }

        public FilterQueryBuilder SetShortId(string value)
        {
            _filterQuery.ShortId = value;
            return this;
        }

        public FilterQuery Build()
        {
            if (!IsValid())
            {
                throw new ArgumentException(nameof(FilterQuery));
            }

            return _filterQuery;
        }

        private bool IsValid()
        {
            if (_filterQuery.PublicId.HasValue || !string.IsNullOrWhiteSpace(_filterQuery.ShortId))
            {
                return true;
            }

            return false;
        }
    }
}

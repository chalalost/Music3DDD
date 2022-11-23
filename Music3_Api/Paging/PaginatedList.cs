using System.Collections.Generic;

namespace Music3_Api.Paging
{
    public class PaginatedList<T> : IPaginatedList<T>
    {
        public IEnumerable<T> Result { get; set; }

        public int TotalCount { get; set; }

        public PaginatedList()
        {
            Result = null;
            TotalCount = 0;
        }
    }

    public class PaginatedListDynamic
    {
        public dynamic Result { get; set; }

        public dynamic TotalCount { get; set; }

        public PaginatedListDynamic()
        {
            Result = null;
            TotalCount = 0;
        }
    }
}

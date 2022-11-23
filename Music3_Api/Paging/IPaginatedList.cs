using System.Collections.Generic;

namespace Music3_Api.Paging
{
    public interface IPaginatedList<T>
    {
        public IEnumerable<T> Result { get; set; }

        public int TotalCount { get; set; }
    }
}

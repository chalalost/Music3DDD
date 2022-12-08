using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class PaginatedListDomainModel<T> : IPaginatedListDomainModel<T>
    {
        public IEnumerable<T> Result { get; set; }

        public int TotalCount { get; set; }

        public PaginatedListDomainModel()
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

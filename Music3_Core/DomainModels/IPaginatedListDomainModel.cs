using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public interface IPaginatedListDomainModel<T>
    {
        public IEnumerable<T> Result { get; set; }

        public int TotalCount { get; set; }
    }
}

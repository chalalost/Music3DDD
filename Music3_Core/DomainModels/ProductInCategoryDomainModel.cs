using Music3_Core.Entities;
using Music3_Core.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class ProductInCategoryDomainModel : BaseEntity
    {
        public int ProductId { get; set; }

        public ProductDomainModel Product { get; set; }

        public int CategoryId { get; set; }

        public CategoryDomainModel Category { get; set; }
    }
}

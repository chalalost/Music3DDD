using Music3_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class OrderDetailDomainModel
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public OrderDomainModel Order { get; set; }

        public ProductDomainModel Product { get; set; }
    }
}

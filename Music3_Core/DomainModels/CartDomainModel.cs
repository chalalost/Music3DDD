using Music3_Core.Entities;
using Music3_Core.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class CartDomainModel : BaseEntity
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Guid UserId { get; set; }

        public ProductDomainModel Product { get; set; }

        public DateTime DateCreated { get; set; }

        public AppUserDomainModel User { get; set; }
    }
}

using Music3_Core.Entities;
using Music3_Core.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class AppUserDomainModel : BaseEntity
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }

        public List<CartDomainModel> Carts { get; set; }

        public List<OrderDomainModel> Orders { get; set; }

        public List<TransactionDomainModel> Transactions { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}

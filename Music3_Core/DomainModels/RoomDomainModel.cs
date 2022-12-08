using Music3_Core.Entities;
using Music3_Core.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class RoomDomainModel : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AppUserDomainModel Admin { get; set; }
        public ICollection<MessageDomainModel> Messages { get; set; }
    }
}

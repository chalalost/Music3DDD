using Music3_Core.Entities;
using Music3_Core.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class MessageDomainModel : BaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ToRoomId { get; set; }
        public AppUserDomainModel FromUser { get; set; }
        public RoomDomainModel ToRoom { get; set; }
    }
}

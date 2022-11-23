using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ToRoomId { get; set; }
        public AppUser FromUser { get; set; }
        public Room ToRoom { get; set; }
    }
}

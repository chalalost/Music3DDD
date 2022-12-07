using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class FeedBackDomainModel
    {
        public long ID { get; set; }
        public DateTime CreateDate { get; set; }
        public string FeedBackContent { get; set; }
        public string Email { get; set; }
    }
}

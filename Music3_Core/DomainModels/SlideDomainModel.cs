using Music3_Core.Entities;
using Music3_Core.Entities.BaseEntity;
using Music3_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class SlideDomainModel : BaseEntity
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }

        public string Image { get; set; }
        public int SortOrder { get; set; }
        public Status Status { set; get; }
        public List<ProductImageDomainModel> ProductImages { get; set; }
    }
}

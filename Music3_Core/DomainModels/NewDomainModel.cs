using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class NewDomainModel
    {
        public long ID { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(200)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(800)]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Details { get; set; }

        public bool? Status { get; set; }
    }
}

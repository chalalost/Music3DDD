using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.Entities
{
    public class Singer
    {
        [Key]
        public long SingerID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string MetaTitle { get; set; }

        [StringLength(800)]
        public string Desciption { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public long? CategoryID { get; set; }

        [StringLength(10)]
        public string Meta { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
        public int ViewCount { get; set; }
    }
}

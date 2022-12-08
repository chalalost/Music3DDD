using Music3_Core.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class AppConfigDomainModel : BaseEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.ViewModels
{
    public class BaseSearchModel
    {
        [DataMember]
        public string KeySearch { get; set; }

        [DataMember]
        //[Range(typeof(int), "1", "1000")]
        public int Skip { get; set; } = 0;


        [DataMember]
        //[Range(typeof(int), "1", "1000")]
        public int Take { get; set; } = 1000;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.QueryCommand
{
    public class DapperParamsQueryCommand
    {
        public string FieldName { get; set; }
        /// <summary>
        /// >; <; =; >=; NOTIN; IN... 
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// AND, OR
        /// </summary>
        public string SqlOperator { get; set; }
        public string ValueCompare { get; set; }
    }
}

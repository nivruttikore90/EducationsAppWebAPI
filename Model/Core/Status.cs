using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Status
    {
        public string StatusTypeCode { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public int SequenceNumber { get; set; }
    }
}

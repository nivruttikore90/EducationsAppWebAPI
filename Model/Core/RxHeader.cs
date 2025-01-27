using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class RxHeader
    {
        public string RxSession { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string TransactionIdentifier { get; set; }
        public string TransactionExternalIdentifier { get; set; }
        public DateTime TransactionTimestamp { get; set; }
        public string TransactionStatus { get; set; }
        public string Environment { get; set; }
        public List<Status> Statuses { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class TransactionRequest<T>
    {
        public RxHeader RxHeader { get; set; }
        public T RxTransactionBody { get; set; }
    }
}

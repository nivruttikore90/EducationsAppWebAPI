using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OperationService : ITransientService, IScopedService, ISingletonService
    {
        private readonly string id = "";
        public OperationService()
        {
            id = Convert.ToString(Guid.NewGuid());
        }

        public string GetOperationID()
        {
            return id;
        }
        public string GetOperationName()
        {
            return id;
        }
    }
}

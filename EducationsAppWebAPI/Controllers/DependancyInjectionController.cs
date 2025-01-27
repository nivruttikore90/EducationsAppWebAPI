using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationsAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependancyInjectionController : ControllerBase
    {
        private readonly ITransientService _transientService;
        private readonly ITransientService _transientService1;

        private readonly IScopedService _scopedService;
        private readonly IScopedService _scopedService1;

        private readonly ISingletonService _singletonService;
        private readonly ISingletonService _singletonService1;
        public DependancyInjectionController(ITransientService transientService, ITransientService transientService1
            , IScopedService scopedService, IScopedService scopedService1, ISingletonService singletonService, ISingletonService singletonService1)
        {
            _transientService = transientService;
            _transientService1 = transientService1;

            _scopedService = scopedService;
            _scopedService1 = scopedService1;

            _singletonService = singletonService;
            _singletonService1 = singletonService1;
        }

        [HttpGet("GetTransactions")]
        public IActionResult GetTransactions()
        {
            string a = _transientService.GetOperationID();
            string b = _transientService1.GetOperationName();

            string c = _scopedService.GetOperationID();
            string d = _scopedService1.GetOperationName();

            string e = _singletonService.GetOperationID();
            string f = _singletonService1.GetOperationID();

            var result = new
            {
                A = a,
                B = b,
                C = c,
                D = d,
                E = e,
                F = f
            };

            return Ok(result);
        }

        [HttpGet("GetTransactionsById")]
        public IActionResult GetTransactionsById()
        {
            string a = _transientService.GetOperationID();
            string b = _transientService1.GetOperationName();

            string c = _scopedService.GetOperationID();
            string d = _scopedService1.GetOperationName();

            string e = _singletonService.GetOperationID();
            string f = _singletonService1.GetOperationID();

            var result = new
            {
                A = a,
                B = b,
                C = c,
                D = d,
                E = e,
                F = f
            };

            return Ok(result);
        }
    }
}

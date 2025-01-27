using EducationsAppWebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace EducationsAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    [ServiceFilter(typeof(LogActionFilter))]
    [ServiceFilter(typeof(CacheResourceFilter))]
    [ServiceFilter(typeof(GlobalExceptionFilter))]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        [ServiceFilter(typeof(ModifyResultFilter))]
        public IActionResult Get()
        {
            return Ok("Hello from GET method.");
        }

        [HttpPost]
        public IActionResult Post([FromBody] SampleModel model)
        {
            if (ModelState.IsValid)
                return Ok($"Hello, {model.Name}");
            return BadRequest("Invalid model.");
        }
    }
}

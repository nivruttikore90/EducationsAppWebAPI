using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationsAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //Cache Implementation using lock
        private readonly ICacheProvider _cacheProvider;
        public EmployeesController(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        [HttpGet("getAllEmployeeInfo")]
        public async Task<IActionResult> getAllEmployeeInfo()  // Use async/await for the async method
        {
            try
            {
                var employees = await _cacheProvider.GetCachedResponse();  // Await the result directly
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });  // Improved error handling
            }
        }

    }
}

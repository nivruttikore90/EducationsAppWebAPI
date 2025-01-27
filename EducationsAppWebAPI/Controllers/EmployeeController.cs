using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Model;

namespace EducationsAppWebAPI.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public EmployeeController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet("getAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            // Try to get the cached list of employees
            if (!_cache.TryGetValue(CacheKeys.Employees, out List<Employee> employees))
            {
                // If not in cache, retrieve the data from DB
                employees = GetEmployeesDetailsFromDB();

                // Define cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5), // Cache expiration
                    SlidingExpiration = TimeSpan.FromMinutes(2),     // Cache sliding expiration
                    Size = 1024                                        // Set the size of the cache item
                };

                // Store the list of employees in the cache
                _cache.Set(CacheKeys.Employees, employees, cacheEntryOptions);
            }

            return Ok(employees);
        }
        private List<Employee> GetEmployeesDetailsFromDB()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", EmailId = "john.doe@example.com" },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", EmailId = "jane.smith@example.com" }
            };
        }

    }
}

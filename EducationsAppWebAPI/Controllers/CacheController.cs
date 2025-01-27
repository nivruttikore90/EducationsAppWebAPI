using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace EducationsAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public CacheController(IDistributedCache cache)
        {
            _distributedCache = cache;
        }

        [HttpPost("SetCacheData")]
        public IActionResult SetCacheData()
        {
            var currentTime = DateTime.Now.ToLocalTime().ToString();
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddYears(1),
                SlidingExpiration = TimeSpan.FromMinutes(1)
            };

            _distributedCache.Set("Time", Encoding.UTF8.GetBytes(currentTime), cacheOptions);

            return Ok(new { Message = "Cache data set successfully", CachedTime = currentTime });
        }

        [HttpGet("GetCacheData")]
        public IActionResult GetCacheData()
        {
            var cachedData = _distributedCache.Get("Time");

            if (cachedData == null)
            {
                return NotFound(new { Message = "No cached data found." });
            }

            var time = Encoding.UTF8.GetString(cachedData);
            return Ok(new { CachedTime = time });
        }

        [HttpDelete("RemoveCacheData")]
        public IActionResult RemoveCacheData()
        {
            _distributedCache.Remove("Time");
            return Ok(new { Message = "Cache data removed successfully" });
        }
    }
}

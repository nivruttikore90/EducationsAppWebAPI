using BLL.Interface;
using Common;
using Microsoft.Extensions.Caching.Memory;
using Model;

namespace BLL.Services
{
    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);  // Locking mechanism
        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<IEnumerable<Employee>> GetCachedResponse()  // Return IEnumerable asynchronously
        {
            try
            {
                // Try to get the cached value first
                if (_cache.TryGetValue(CacheKeys.Employees, out List<Employee> employees))
                {
                    return employees;
                }

                try
                {
                    await Semaphore.WaitAsync();  // Acquire the lock to ensure only one thread fetches the data

                    // Check again in case another thread cached the result while this thread was waiting
                    if (_cache.TryGetValue(CacheKeys.Employees, out employees))
                    {
                        return employees;
                    }

                    // Fetch data from DB if not found in cache
                    employees = GetEmployeesDetailsFromDB();

                    // Cache the result with options
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        SlidingExpiration = TimeSpan.FromMinutes(2),
                        Size = 1024
                    };

                    // Store in the cache
                    _cache.Set(CacheKeys.Employees, employees, cacheEntryOptions);
                }
                catch
                {
                    throw;  // Rethrow the exception to be handled in the controller
                }
                finally
                {
                    Semaphore.Release();  // Ensure the semaphore is always released
                }

                return employees;
            }
            catch
            {
                throw;  // Rethrow the exception to be handled in the controller
            }
        }

        public List<Employee> GetEmployeesDetailsFromDB()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", EmailId = "john.doe@example.com" },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", EmailId = "jane.smith@example.com" }
            };
        }
    }
}

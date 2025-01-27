using Microsoft.AspNetCore.Mvc.Filters;

namespace EducationsAppWebAPI.Filters
{
    public class CacheResourceFilter : IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Checking if response is cached.");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Storing response in cache.");
        }
    }
}

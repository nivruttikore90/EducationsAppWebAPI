using Microsoft.AspNetCore.Mvc.Filters;

namespace EducationsAppWebAPI.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action is about to execute.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action has executed.");
        }
    }
}

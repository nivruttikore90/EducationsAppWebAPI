using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EducationsAppWebAPI.Filters
{
    public class ModifyResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value = "Modified response: " + objectResult.Value;
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // After result has been sent to the client
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EducationsAppWebAPI.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthorized = context.HttpContext.Request.Headers.ContainsKey("Authorization");
            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}

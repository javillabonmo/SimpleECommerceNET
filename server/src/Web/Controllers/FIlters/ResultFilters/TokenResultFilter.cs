using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers.FIlters.ResultFilters
{
    public class TokenResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //cookie
            context.HttpContext.Response.Cookies.Append("Auth-Key", "cookievalue");
            return;
        }
    }
}



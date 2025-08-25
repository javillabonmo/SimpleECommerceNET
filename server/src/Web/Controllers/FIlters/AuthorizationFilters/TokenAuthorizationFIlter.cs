using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers.FIlters.AuthorizationFilters;

public class TokenAuthorizationFIlter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {/*
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token) || string.IsNullOrEmpty(token))
        {
            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            return;
        }
        */
        if (!context.HttpContext.Request.Cookies.ContainsKey("Auth-Key"))
        {
            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            return;
        }

        if (context.HttpContext.Request.Cookies["Auth-Key"] != "cookievalue")
        {
            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            return;
        }
    }
}

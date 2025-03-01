using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Custom_Middleware_Practice.Filters
{
    public class AuthFilter : AuthorizeAttribute,   IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.IsInRole("Admin"))
            {
                context.Result = new ForbidResult(); // 403 Forbidden
            }
        }
    }
}

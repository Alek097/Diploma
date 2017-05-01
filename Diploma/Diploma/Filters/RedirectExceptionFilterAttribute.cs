using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Filters
{
    public class RedirectExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        public RedirectExceptionFilterAttribute() { }

        public override void OnException(ExceptionContext context)
        {
            HttpContext httpContext = context.HttpContext;

            httpContext.Response.StatusCode = 307;
            httpContext.Response.Headers["Location"] = "/#!/error/500/Unknow server error";
            httpContext.Response.ContentType = "text/plain";

            Task waiter = httpContext.Response.WriteAsync("Hello world");
            waiter.Wait();
        }
    }
}

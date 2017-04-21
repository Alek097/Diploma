using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Diploma.Filters
{
    public class ExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {

        private readonly ILogger<ExceptionFilterAttribute> logger;

        public ExceptionFilterAttribute(ILogger<ExceptionFilterAttribute> logger)
        {
            this.logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            this.logger.LogCritical(new EventId(1, "unknow exception"), context.Exception, "Unknow exception");
        }
    }
}

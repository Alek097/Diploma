using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Diploma.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        [Produces("text/html")]
        public string Get()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Bundles", "index.html");

            try
            {
                return System.IO.File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.ToString());
                throw;
            }
        }
    }
}
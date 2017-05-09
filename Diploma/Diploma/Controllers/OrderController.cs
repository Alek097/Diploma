using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Diploma.Filters;
using Diploma.BusinessLogic.Interfaces;
using Diploma.Core;
using Diploma.Core.ViewModels;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Order/[action]")]
    [TypeFilter(typeof(ExceptionFilterAttribute))]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(
            IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ControllerResult<string>> Add([FromBody] IEnumerable<ProductViewModel> products)
        {
            return await this.orderService.AddProduct(products, this.User.Identity.Name);
        }

        [HttpGet]
        public async Task<ControllerResult<IEnumerable<OrderViewModel>>> GetAll()
        {
            return await this.orderService.GetAll();
        }

        public new void Dispose()
        {
            this.orderService.Dispose();
            base.Dispose();
        }
    }
}
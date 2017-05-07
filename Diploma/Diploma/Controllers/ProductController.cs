using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Diploma.Core;
using Microsoft.AspNetCore.Authorization;
using Diploma.Filters;
using Diploma.BusinessLogic.Interfaces;
using Diploma.Core.ViewModels;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Product/[action]")]
    [TypeFilter(typeof(ExceptionFilterAttribute))]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ControllerResult<string>> UploadCover(IFormFile cover)
        {
            return await this.productService.SaveCover(cover);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ControllerResult<IEnumerable<string>>> UploadImages()
        {
            return await this.productService.SaveImages(Request.Form.Files.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ControllerResult> Add([FromBody]ProductViewModel product, string categoryId)
        {
            return await this.productService.AddProduct(product, categoryId, User.Identity.Name);
        }

        [HttpGet]
        public async Task<ControllerResult<ProductViewModel>> GetById(string productId)
        {
            return await this.productService.GetProductById(productId);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ControllerResult> Edit([FromBody]ProductViewModel product, string categoryId)
        {
            return await this.productService.EditProduct(product, categoryId, User.Identity.Name);
        }
    }
}
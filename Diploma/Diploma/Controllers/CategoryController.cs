using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Diploma.Core;
using Diploma.Core.ViewModels;
using Diploma.BusinessLogic.Interfaces;
using Diploma.Filters;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Category/[action]")]
    [TypeFilter(typeof(ExceptionFilterAttribute))]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryController(
            ICategoryService categoryService,
            IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ControllerResult<IEnumerable<CategoryViewModel>>> GetNames()
        {
            return await this.categoryService.GetCategoryNames();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ControllerResult<CategoryViewModel>> Add([FromBody]CategoryViewModel category)
        {
            return await this.categoryService.AddCategory(this.User.Identity.Name, category);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ControllerResult<CategoryViewModel>> Edit([FromBody]CategoryViewModel category)
        {
            return await this.categoryService.EditCategoty(this.User.Identity.Name, category);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ControllerResult> Delete(string id)
        {
            return await this.categoryService.DeleteCategory(this.User.Identity.Name, id);
        }

        [HttpGet]
        public async Task<ControllerResult<CategoryViewModel>> GetCategoryById(string id)
        {
            return await this.categoryService.GetCategoryById(id);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ControllerResult<string>> DeleteProduct(string productId)
        {
            return await this.productService.DeleteProduct(productId, User.Identity.Name);
        }

        [HttpGet]
        public async Task<ControllerResult<IEnumerable<CategoryViewModel>>> GetCategories()
        {
            return await this.categoryService.GetCategories();
        }

        public new void Dispose()
        {
            this.categoryService.Dispose();
            this.productService.Dispose();
            base.Dispose();
        }
    }
}
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

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ControllerResult<IEnumerable<CategoryViewModel>>> GetNames()
        {
            return await this.categoryService.GetCategoryNames();
        }

        [HttpPost]
        public async Task<ControllerResult<CategoryViewModel>> Add([FromBody]CategoryViewModel category)
        {
            return await this.categoryService.AddCategory(this.User.Identity.Name, category);
        }

        [HttpPost]
        public async Task<ControllerResult<CategoryViewModel>> Edit([FromBody]CategoryViewModel category)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ControllerResult> Delete(string id)
        {
            return await this.categoryService.DeleteCategory(this.User.Identity.Name, id);
        }
    }
}
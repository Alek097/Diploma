using System.Collections.Generic;
using System.Threading.Tasks;
using Diploma.Core;
using Diploma.Core.ViewModels;

namespace Diploma.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        Task<ControllerResult<IEnumerable<CategoryViewModel>>> GetCategoryNames();
        Task<ControllerResult<CategoryViewModel>> AddCategory(string name, CategoryViewModel category);
        Task<ControllerResult> DeleteCategory(string name, string id);
    }
}

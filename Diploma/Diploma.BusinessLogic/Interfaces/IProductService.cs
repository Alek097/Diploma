using System.Collections.Generic;
using System.Threading.Tasks;
using Diploma.Core;
using Diploma.Core.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Diploma.BusinessLogic.Interfaces
{
    public interface IProductService
    {
        Task<ControllerResult<string>> SaveCover(IFormFile cover);
        Task<ControllerResult<IEnumerable<string>>> SaveImages(ICollection<IFormFile> images);
        Task<ControllerResult> AddProduct(ProductViewModel product, string categoryId, string name);
        Task<ControllerResult<ProductViewModel>> GetProductById(string productId);
        Task<ControllerResult> EditProduct(ProductViewModel product, string categoryId, string name);
        Task<ControllerResult<string>> DeleteProduct(string productId, string name);
    }
}

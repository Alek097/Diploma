using System.Collections.Generic;
using System.Threading.Tasks;
using Diploma.Core;
using Diploma.Core.ViewModels;

namespace Diploma.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task<ControllerResult<string>> AddProduct(IEnumerable<ProductViewModel> products, string name);
    }
}

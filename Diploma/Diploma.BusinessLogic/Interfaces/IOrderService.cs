using System.Collections.Generic;
using System.Threading.Tasks;
using Diploma.Core;
using Diploma.Core.ViewModels;
using System;

namespace Diploma.BusinessLogic.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task<ControllerResult<string>> AddProduct(IEnumerable<ProductViewModel> products, string name);
        Task<ControllerResult<IEnumerable<OrderViewModel>>> GetAll();
    }
}

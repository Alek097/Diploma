using System.Collections.Generic;

namespace Diploma.Core.ViewModels
{
    public class OrderViewModel
    {
        public decimal TotalPrice { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }

        public OrderViewModel()
        {
            this.Products = new List<ProductViewModel>();
        }
    }
}
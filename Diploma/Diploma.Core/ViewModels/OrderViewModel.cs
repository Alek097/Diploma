using System.Collections.Generic;

namespace Diploma.Core.ViewModels
{
    public class OrderViewModel
    {
        public decimal TotalPrice { get; set; }

        public string CreateDate { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

    }
}
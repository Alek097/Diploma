using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Core.ViewModels
{
    public class CategoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}

using System.Collections.Generic;

namespace Diploma.Core.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<CharacteristicsGroupViewModel> CharacteristicsGroups { get; set; }

        public IEnumerable<CharacteristicViewModel> Characteristics { get; set; }

    }
}
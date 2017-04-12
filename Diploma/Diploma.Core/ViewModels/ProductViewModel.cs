using System.Collections.Generic;

namespace Diploma.Core.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public decimal Price { get; set; }

        public ICollection<CharacteristicsGroupViewModel> CharacteristicsGroups { get; set; }

        public ICollection<CharacteristicViewModel> Characteristics { get; set; }

        public ProductViewModel()
        {
            this.CharacteristicsGroups = new List<CharacteristicsGroupViewModel>();
            this.Characteristics = new List<CharacteristicViewModel>();
        }
    }
}
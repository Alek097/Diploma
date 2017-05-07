using System.Collections.Generic;

namespace Diploma.Core.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public IEnumerable<string> ImagesUrl { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<CharacteristicsGroupViewModel> CharacteristicsGroups { get; set; }

        public IEnumerable<CharacteristicViewModel> Characteristics { get; set; }
    }
}
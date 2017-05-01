using System.Collections.Generic;

namespace Diploma.Core.ViewModels
{
    public class CharacteristicsGroupViewModel
    {
        public string Name { get; set; }

        public IEnumerable<CharacteristicViewModel> Characteristics { get; set; }
    }
}
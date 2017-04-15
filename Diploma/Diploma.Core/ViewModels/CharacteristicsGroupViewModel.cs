using System.Collections.Generic;

namespace Diploma.Core.ViewModels
{
    public class CharacteristicsGroupViewModel
    {
        public string Name { get; set; }

        public ICollection<CharacteristicViewModel> Characteristics { get; set; }

        public CharacteristicsGroupViewModel()
        {
            this.Characteristics = new List<CharacteristicViewModel>();
        }
    }
}
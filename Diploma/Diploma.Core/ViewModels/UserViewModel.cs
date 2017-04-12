using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Core.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }

        public bool IsBanned { get; set; }

        public bool IsAuthorize { get; set; }

        public string UserName { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }

        public UserViewModel()
        {
            this.Orders = new List<OrderViewModel>();
            this.Addresses = new List<AddressViewModel>();
        }
    }
}

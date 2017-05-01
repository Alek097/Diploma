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

        public string Role { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }

        public IEnumerable<AddressViewModel> Addresses { get; set; }

    }
}

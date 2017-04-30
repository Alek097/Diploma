using System;
using System.ComponentModel.DataAnnotations;

namespace Diploma.Core.ViewModels
{
    public class AddressViewModel
    {

        public string Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string PostCode { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
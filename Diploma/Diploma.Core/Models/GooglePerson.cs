using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Core.Models
{
    public class GooglePerson
    {
        public string id { get; set; }

        public string email { get; set; }

        public string given_name { get; set; }

        public string family_name { get; set; }

        public string link { get; set; }

        public string picture { get; set; }

        public GooglePerson(string json)
        {
            GooglePerson temp = JsonConvert.DeserializeObject<GooglePerson>(json);

            this.id = temp.id;
            this.email = temp.email;
            this.given_name = temp.given_name;
            this.family_name = temp.family_name;
            this.link = temp.link;
            this.picture = temp.picture;
        }

        public GooglePerson() { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Core.ConfigureModels
{
    public class OAuth
    {
        public string Name { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string GetCodeUrl { get; set; }

        public string GetCodeParameters { get; set; }

        public string GetAccessTokenUrl { get; set; }

        public string GetAccessTokenParameters { get; set; }

        public string Description { get; set; }

        public string LogoUrl { get; set; }
    }
}

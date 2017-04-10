using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Core.OAuthResults
{
    public class OAuthResult
    {
        public string AccessToken { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string Provider { get; set; }
    }
}

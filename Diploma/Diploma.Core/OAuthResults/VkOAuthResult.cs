using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Diploma.Core.OAuthResults
{
    public class VkOAuthResult : IOAuthResult
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string user_id { get; set; }

        public string email { get; set; }

        public VkOAuthResult(string json)
        {
            VkOAuthResult temp = JsonConvert.DeserializeObject<VkOAuthResult>(json);

            this.access_token = temp.access_token;
            this.expires_in = temp.expires_in;
            this.user_id = temp.user_id;
            this.email = temp.email;
        }

        public VkOAuthResult() { }

        public async Task<OAuthResult> ToOAuthResultAsync()
        {
            return await Task.Run(() =>
            {
                return new OAuthResult()
                {
                    AccessToken = this.access_token,
                    Email = this.email,
                    UserId = this.user_id,
                    Provider = "Vk"
                };
            });
        }
    }
}

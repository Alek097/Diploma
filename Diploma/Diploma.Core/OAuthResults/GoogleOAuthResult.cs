using Diploma.Core.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Diploma.Core.OAuthResults
{
    public class GoogleOAuthResult : IOAuthResult
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string id_token { get; set; }

        public string token_type { get; set; }

        public GoogleOAuthResult(string json)
        {
            GoogleOAuthResult temp = JsonConvert.DeserializeObject<GoogleOAuthResult>(json);

            this.access_token = temp.access_token;
            this.expires_in = temp.expires_in;
            this.id_token = temp.id_token;
            this.token_type = temp.token_type;
        }

        public GoogleOAuthResult() { }

        public async Task<OAuthResult> ToOAuthResultAsync()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage message = await client.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={this.access_token}");

            string personJson = await message.Content.ReadAsStringAsync();

            GooglePerson person = new GooglePerson(personJson);

            return new OAuthResult()
            {
                AccessToken = this.access_token,
                Provider = "GooglePlus",
                Email = person.email,
                UserId = person.id
            };
        }
    }
}

using Diploma.Core.ConfigureModels;
using Diploma.Data;
using Diploma.Data.Models;
using Diploma.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly IContext context;
        private readonly List<OAuth> oauth;

        public AuthorizeRepository(IContext context, IOptions<List<OAuth>> oauth)
        {
            this.context = context;
            this.oauth = oauth.Value;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public async Task<string> GetRedirectUrl(string provider)
        {
            OAuth OAuth = this.oauth.First((oa) => oa.Name.ToUpper() == provider.ToUpper());

            string state = $"{OAuth.Name.ToUpper()}_{Guid.NewGuid()}";

            this.context.OAuthStates.Add(new OAuthState()
            {
                State = state
            });

            await this.context.SaveChangesAsync();

            return $"{string.Format(OAuth.Url, OAuth.Client_id)}&{OAuth.Parameters}&state={state}";
        }
    }
}

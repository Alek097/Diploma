using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class OAuthStateRepository : Repository<OAuthState, Guid>, IOAuthStateRepository
    {
        public OAuthStateRepository(IContext context) : base(context)
        {
        }

        public override IList<OAuthState> Get()
        {
            return base.context.OAuthStates.ToList();
        }
    }
}

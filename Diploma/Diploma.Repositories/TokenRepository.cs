using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class TokenRepository : Repository<Token, Guid>, ITokenRepository
    {
        public TokenRepository(IContext context) : base(context)
        {
        }

        public override IList<Token> Get()
        {
            return base.context.Tokens.ToList();
        }
    }
}

using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class EditEmailConfirmMessageRepository : Repository<EditEmailConfirmMessage, Guid>, IEditEmailConfirmMessageRepository
    {
        public EditEmailConfirmMessageRepository(IContext context) : base(context)
        {
        }

        public override IList<EditEmailConfirmMessage> Get()
        {
            return base.context.EditEmailConfirmMessages.ToList();
        }
    }
}

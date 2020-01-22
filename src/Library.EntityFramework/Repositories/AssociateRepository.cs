using System;
using System.Threading.Tasks;
using Association.Domain.Entities;
using Association.Domain.Repositories;
using Association.Domain.ValueObjects;
using Common.Application.Events;
using Library.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Library.EntityFramework.Repositories
{
    public class AssociateRepository : EfRepository<Associate>, IAssociateRepository
    {
        public AssociateRepository(GameSociationContext context, IEventBus eventBus) : base(context, eventBus)
        {
        }

        public async Task<Associate> GetByTag(Tag tag)
        {
            var associate = await Context.Set<Associate>().FirstOrDefaultAsync(x => x.Tag.Equals(tag)).ConfigureAwait(false);
            if (associate == null)
                throw new InvalidOperationException("No associate found with this tag");
            return associate;
        }
    }
}
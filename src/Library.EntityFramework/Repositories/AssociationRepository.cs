using System;
using System.Threading.Tasks;
using Association.Domain.Repositories;
using Common.Application.Events;
using Library.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Library.EntityFramework.Repositories
{
    public class AssociationRepository : EfRepository<Association.Domain.Entities.Association>, IAssociationRepository
    {
        public AssociationRepository(GameSociationContext context, IEventBus eventBus) : base(context, eventBus)
        {
        }

        public async Task<Association.Domain.Entities.Association> GetByName(string name)
        {
            var association = await Context.Set<Association.Domain.Entities.Association>().FirstOrDefaultAsync(x => x.Name == name);

            if(association == null)
                throw new InvalidOperationException($"Association with name {name} doesn't exist");

            return association;
        }
    }
}
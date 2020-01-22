using Association.Domain.Repositories;
using Common.Application.Events;
using Marten;

namespace Library.MartenEventStore.Repositories
{
    public class AssociationRepository : MartenEventStoreRepository<Association.Domain.Entities.Association>, IAssociationRepository
    {
        public AssociationRepository(IDocumentSession session, IEventBus eventBus) : base(session, eventBus)
        {
        }
    }
}
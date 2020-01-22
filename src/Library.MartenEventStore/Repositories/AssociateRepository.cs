using Association.Domain.Entities;
using Association.Domain.Repositories;
using Common.Application.Events;
using Marten;

namespace Library.MartenEventStore.Repositories
{
    public class AssociateRepository : MartenEventStoreRepository<Associate>, IAssociateRepository
    {
        public AssociateRepository(IDocumentSession session, IEventBus eventBus) : base(session, eventBus)
        {
        }
    }
}
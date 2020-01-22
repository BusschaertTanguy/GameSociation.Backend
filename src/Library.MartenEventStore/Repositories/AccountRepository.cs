using Account.Domain.Repositories;
using Common.Application.Events;
using Marten;

namespace Library.MartenEventStore.Repositories
{
    public class AccountRepository : MartenEventStoreRepository<Account.Domain.Entities.Account>, IAccountRepository
    {
        public AccountRepository(IDocumentSession session, IEventBus eventBus) : base(session, eventBus)
        {
        }
    }
}
using Account.Domain.Repositories;
using Common.Application.Events;
using Library.EntityFramework.Contexts;

namespace Library.EntityFramework.Repositories
{
    public class AccountRepository : EfRepository<Account.Domain.Entities.Account>, IAccountRepository
    {
        public AccountRepository(GameSociationContext context, IEventBus eventBus) : base(context, eventBus)
        {
        }
    }
}
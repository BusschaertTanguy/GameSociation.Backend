using System;
using Account.Domain.Events;
using Marten.Events.Projections;

namespace Library.MartenEventStore.Projections
{
    public class AccountProjection : ViewProjection<Account.Application.Projections.AccountProjection, Guid>
    {
        public AccountProjection()
        {
            ProjectEvent<AccountCreated>(Persist);
        }

        private static void Persist(Account.Application.Projections.AccountProjection projection, AccountCreated @event)
        {
            projection.Id = @event.Id;
            projection.Email = @event.Email;
            projection.Password = @event.Password;
        }
    }
}
using System;
using Account.Application.Views;
using Account.Domain.Events;
using Marten.Events.Projections;

namespace Library.MartenEventStore.Projections
{
    public class AccountProjection : ViewProjection<AccountView, Guid>
    {
        public AccountProjection()
        {
            ProjectEvent<AccountCreated>(Persist);
        }

        private static void Persist(AccountView view, AccountCreated @event)
        {
            view.Id = @event.Id;
            view.Email = @event.Email;
            view.Password = @event.Password;
        }
    }
}

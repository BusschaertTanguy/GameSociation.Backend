using Account.Domain.Events;
using Common.Domain.Entities;

namespace Account.Domain.Entities
{
    public class Account : Aggregate
    {
        private string _email;
        private string _password;

        public Account()
        {
        }

        public Account(string email, string password, string username)
        {
            RaiseEvent(new AccountCreated(Id, email, password, username), Apply);
        }

        private void Apply(AccountCreated @event)
        {
            Id = @event.Id;
            _email = @event.Email;
            _password = @event.Password;
        }
    }
}
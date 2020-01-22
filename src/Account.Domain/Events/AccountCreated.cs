using System;
using Common.Domain.Events;

namespace Account.Domain.Events
{
    public class AccountCreated : IEvent
    {
        public AccountCreated(Guid id, string email, string password, string username)
        {
            Id = id;
            Email = email;
            Password = password;
            Username = username;
        }

        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }
        public string Username { get; }
    }
}
using System;
using Common.Application.Views;

namespace Account.Application.Views
{
    public class AccountView : IView
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
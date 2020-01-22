using Common.Application.Commands;

namespace Account.Application.Commands.Register
{
    public class Register : ICommand
    {
        public Register(string email, string password, string username)
        {
            Email = email;
            Password = password;
            Username = username;
        }

        public string Email { get; }
        public string Password { get; }
        public string Username { get; }
    }
}
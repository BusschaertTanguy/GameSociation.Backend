using Common.Application.Queries;

namespace Account.Application.Queries.Login
{
    public class Login : IQuery<LoginResult>
    {
        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
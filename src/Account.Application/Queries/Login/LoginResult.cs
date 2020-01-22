using System;

namespace Account.Application.Queries.Login
{
    public class LoginResult
    {
        public LoginResult(Guid accountId, string token)
        {
            AccountId = accountId;
            Token = token;
        }

        public Guid AccountId { get; }
        public string Token { get; }
    }
}
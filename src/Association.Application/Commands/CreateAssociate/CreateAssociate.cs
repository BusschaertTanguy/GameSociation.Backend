using System;
using Common.Application.Commands;

namespace Association.Application.Commands.CreateAssociate
{
    public class CreateAssociate : ICommand
    {
        public CreateAssociate(Guid accountId, string username)
        {
            AccountId = accountId;
            Username = username;
        }

        public Guid AccountId { get; }
        public string Username { get; }
    }
}
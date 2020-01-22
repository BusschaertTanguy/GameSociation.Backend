using System;
using Common.Domain.Events;

namespace Association.Domain.Events
{
    public class AssociateCreated : IEvent
    {
        public AssociateCreated(Guid id, Guid accountId, string username, int tagNumber)
        {
            Id = id;
            AccountId = accountId;
            Username = username;
            TagNumber = tagNumber;
        }

        public Guid AccountId { get; }
        public string Username { get; }
        public int TagNumber { get; }
        public Guid Id { get; }
    }
}
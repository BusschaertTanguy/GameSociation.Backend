using System;
using Association.Domain.Events;
using Association.Domain.ValueObjects;
using Common.Domain.Entities;

namespace Association.Domain.Entities
{
    public class Associate : Aggregate
    {
        private Guid _accountId;
        private Tag _tag;

        public Associate()
        {
        }

        public Associate(Guid accountId, string username, int tagNumber)
        {
            RaiseEvent(new AssociateCreated(Id, accountId, username, tagNumber), Apply);
        }

        public Association CreateAssociation(string name)
        {
            return new Association(Id, name);
        }

        private void Apply(AssociateCreated @event)
        {
            Id = @event.Id;
            _accountId = @event.AccountId;
            _tag = new Tag(@event.Username, @event.TagNumber);
        }
    }
}
using System;
using Common.Domain.Events;

namespace Association.Domain.Events
{
    public class AssociateLeft : IEvent
    {
        public AssociateLeft(Guid associationId, Guid associateId)
        {
            Id = associationId;
            AssociateId = associateId;
        }

        public Guid Id { get; }
        public Guid AssociateId { get; }
    }
}

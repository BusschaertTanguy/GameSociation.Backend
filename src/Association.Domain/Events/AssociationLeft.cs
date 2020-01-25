using System;
using Common.Domain.Events;

namespace Association.Domain.Events
{
    public class AssociationLeft : IEvent
    {
        public AssociationLeft(Guid associationId, Guid associateId)
        {
            Id = associationId;
            AssociateId = associateId;
        }

        public Guid Id { get; }
        public Guid AssociateId { get; }
    }
}

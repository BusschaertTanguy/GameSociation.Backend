using System;
using Common.Domain.Events;

namespace Association.Domain.Events
{
    public class AssociationCreated : IEvent
    {
        public AssociationCreated(Guid id, Guid ownerId, string name, int membershipRoleId, int membershipStatusId)
        {
            Id = id;
            OwnerId = ownerId;
            Name = name;
            MembershipRoleId = membershipRoleId;
            MembershipStatusId = membershipStatusId;
        }

        public Guid Id { get; }
        public Guid OwnerId { get; }
        public string Name { get; }
        public int MembershipRoleId { get; }
        public int MembershipStatusId { get; }
    }
}
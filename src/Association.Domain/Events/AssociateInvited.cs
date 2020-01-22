using System;
using Common.Domain.Events;

namespace Association.Domain.Events
{
    public class AssociateInvited : IEvent
    {
        public AssociateInvited(Guid id, Guid associateId, int membershipRoleId, int membershipStatusId)
        {
            Id = id;
            AssociateId = associateId;
            MembershipRoleId = membershipRoleId;
            MembershipStatusId = membershipStatusId;
        }

        public Guid AssociateId { get; }
        public int MembershipRoleId { get; }
        public int MembershipStatusId { get; }
        public Guid Id { get; }
    }
}
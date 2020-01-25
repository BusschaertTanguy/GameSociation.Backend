using System;

namespace Association.Application.Views
{
    public class MembershipView
    {
        public MembershipView(Guid associationId, AssociateView associate, int role, int status)
        {
            AssociationId = associationId;
            Associate = associate;
            Role = role;
            Status = status;
        }

        public Guid AssociationId { get; }
        public AssociateView Associate { get; }
        public int Role { get; }
        public int Status { get; }
    }
}
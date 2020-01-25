using System;

namespace Association.Application.Views
{
    public class InvitationView
    {
        public InvitationView(Guid associationId, Guid associateId, string associationName)
        {
            AssociationId = associationId;
            AssociateId = associateId;
            AssociationName = associationName;
        }

        public Guid AssociationId { get; }
        public Guid AssociateId { get; }
        public string AssociationName { get; }
    }
}
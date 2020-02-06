using System;
using System.Collections.Generic;
using Common.Application.Projections;

namespace Association.Application.Projections
{
    public class AssociateProjection : IProjectionRoot
    {
        public TagProjection Tag { get; set; }
        public Guid AccountId { get; set; }
        public IList<Guid> OwnedAssociationIds { get; set; }
        public IList<Guid> JoinedAssociationIds { get; set; }
        public IList<InvitationProjection> Invitations { get; set; }
        public Guid Id { get; set; }
    }
}
using System;
using Common.Application.Projections;

namespace Association.Application.Projections
{
    public class InvitationProjection :  IProjection
    {
        public Guid AssociationId { get; set; }
        public Guid AssociateId { get; set; }
    }
}

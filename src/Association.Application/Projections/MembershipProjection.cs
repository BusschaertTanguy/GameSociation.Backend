using System;
using Common.Application.Projections;

namespace Association.Application.Projections
{
    public class MembershipProjection : IProjection
    {
        public Guid AssociationId { get; set; }
        public Guid AssociateId { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Common.Application.Projections;

namespace Association.Application.Projections
{
    public class AssociationProjection : IProjectionRoot
    {
        public string Name { get; set; }
        public List<MembershipProjection> Members { get; set; }
        public Guid Id { get; set; }
    }
}
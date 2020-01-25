using System;
using System.Collections.Generic;
using Common.Application.Projections;

namespace Association.Application.Projections
{
    public class AssociationProjection : IProjectionRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<MembershipProjection> Members { get; set; }
    }
}
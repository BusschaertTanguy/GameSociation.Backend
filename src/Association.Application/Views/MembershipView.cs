using System;
using Common.Application.Views;

namespace Association.Application.Views
{
    public class MembershipView : IProjection
    {
        public Guid AssociationId { get; set; }
        public Guid AssociateId { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
    }
}
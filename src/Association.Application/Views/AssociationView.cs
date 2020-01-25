using System;
using System.Collections.Generic;

namespace Association.Application.Views
{
    public class AssociationView
    {
        public AssociationView(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }

    public class AssociationDetailView : AssociationView
    {
        public AssociationDetailView(Guid id, string name, IEnumerable<MembershipView> members) : base(id, name)
        {
            Members = members;
        }

        public IEnumerable<MembershipView> Members { get; }
    }
}
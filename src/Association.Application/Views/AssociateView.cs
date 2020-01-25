using System;
using System.Collections.Generic;

namespace Association.Application.Views
{
    public class AssociateView
    {
        public AssociateView(Guid id, Guid accountId, TagView tag)
        {
            Id = id;
            AccountId = accountId;
            Tag = tag;
        }

        public Guid Id { get; }
        public Guid AccountId { get; }
        public TagView Tag { get; }
    }

    public class AssociateDetailView : AssociateView
    {
        public AssociateDetailView(Guid id, Guid accountId, TagView tag, IEnumerable<Guid> joinedAssociation, IEnumerable<Guid> ownedAssociations) : base(id, accountId, tag)
        {
            JoinedAssociation = joinedAssociation;
            OwnedAssociations = ownedAssociations;
        }

        public IEnumerable<Guid> JoinedAssociation { get; }
        public IEnumerable<Guid> OwnedAssociations { get; }
    }
}
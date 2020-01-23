using System;
using System.Collections.Generic;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetJoinedAssociations
{
    public class GetJoinedAssociations : IQuery<List<AssociationView>>
    {
        public GetJoinedAssociations(Guid associateId)
        {
            AssociateId = associateId;
        }

        public Guid AssociateId { get; }
    }
}
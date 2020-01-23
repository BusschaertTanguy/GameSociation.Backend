using System;
using System.Collections.Generic;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetOwnedAssociations
{
    public class GetOwnedAssociations : IQuery<List<AssociationView>>
    {
        public GetOwnedAssociations(Guid associateId)
        {
            AssociateId = associateId;
        }

        public Guid AssociateId { get; }
    }
}
using System;
using System.Collections.Generic;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociateAssociations
{
    public class GetAssociateAssociations : IQuery<List<AssociationView>>
    {
        public GetAssociateAssociations(Guid associateId)
        {
            AssociateId = associateId;
        }

        public Guid AssociateId { get; }
    }
}
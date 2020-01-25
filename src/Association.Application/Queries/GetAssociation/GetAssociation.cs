using System;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociation
{
    public class GetAssociation : IQuery<AssociationDetailView>
    {
        public GetAssociation(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
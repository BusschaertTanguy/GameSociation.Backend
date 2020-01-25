using System;
using System.Collections.Generic;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociateInvitations
{
    public class GetAssociateInvitations : IQuery<List<InvitationView>>
    {
        public GetAssociateInvitations(Guid associateId)
        {
            AssociateId = associateId;
        }

        public Guid AssociateId { get; }
    }
}
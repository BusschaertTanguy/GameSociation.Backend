using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Projections;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociateInvitations
{
    public class GetAssociateInvitationsHandler : IQueryHandler<GetAssociateInvitations, List<InvitationView>>
    {
        private readonly IQueryProcessor _queryProcessor;

        public GetAssociateInvitationsHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<List<InvitationView>> Handle(GetAssociateInvitations request, CancellationToken cancellationToken)
        {
            var associateProjection = _queryProcessor.Query<AssociateProjection>().FirstOrDefault(x => x.Id == request.AssociateId);
            if (associateProjection?.Invitations == null)
                return Task.FromResult(new List<InvitationView>());

            var associationIds = associateProjection.Invitations.Select(x => x.AssociationId).ToList();
            var associations = _queryProcessor
                .Query<AssociationProjection>()
                .Where(x => associationIds.Contains(x.Id))
                .Select(x => new { x.Id, x.Name })
                .ToList();

            var invitations = new List<InvitationView>();

            foreach (var invitationProjection in associateProjection.Invitations)
            {
                var associationName = associations.FirstOrDefault(x => x.Id == invitationProjection.AssociationId)?.Name ?? string.Empty;
                var invitation = new InvitationView(invitationProjection.AssociationId, invitationProjection.AssociateId, associationName);
                invitations.Add(invitation);
            }

            return Task.FromResult(invitations);
        }
    }
}
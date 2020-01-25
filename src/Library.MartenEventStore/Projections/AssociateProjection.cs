using System;
using System.Collections.Generic;
using System.Linq;
using Association.Application.Projections;
using Association.Domain.Events;
using Marten.Events.Projections;

namespace Library.MartenEventStore.Projections
{
    public class AssociateProjection : ViewProjection<Association.Application.Projections.AssociateProjection, Guid>
    {
        public AssociateProjection()
        {
            ProjectEvent<AssociateCreated>(Persist);
            ProjectEvent<AssociationCreated>(x => x.OwnerId, Persist);
            ProjectEvent<AssociateInvited>(x => x.AssociateId, Persist);
            ProjectEvent<InvitationAccepted>(x => x.AssociateId, Persist);
            ProjectEvent<InvitationRefused>(x => x.AssociateId, Persist);
            ProjectEvent<AssociationLeft>(x => x.AssociateId, Persist);
        }

        private static void Persist(Association.Application.Projections.AssociateProjection projection, AssociateCreated @event)
        {
            projection.Id = @event.Id;
            projection.AccountId = @event.AccountId;
            projection.Tag = new TagProjection
            {
                Number = @event.TagNumber,
                Username = @event.Username,
            };
        }

        private static void Persist(Association.Application.Projections.AssociateProjection projection, AssociationCreated @event)
        {
            if (projection.OwnedAssociationIds == null)
                projection.OwnedAssociationIds = new List<Guid>();

            projection.OwnedAssociationIds.Add(@event.Id);
        }

        private static void Persist(Association.Application.Projections.AssociateProjection projection, AssociateInvited @event)
        {
            if (projection.Invitations == null)
                projection.Invitations = new List<InvitationProjection>();

            projection.Invitations.Add(new InvitationProjection
            {
                AssociationId = @event.Id,
                AssociateId = @event.AssociateId
            });
        }

        private static void Persist(Association.Application.Projections.AssociateProjection projection, InvitationAccepted @event)
        {
            var invitation = projection.Invitations.FirstOrDefault(x => x.AssociationId == @event.Id && x.AssociateId == @event.AssociateId);
            if (invitation == null)
                return;

            projection.Invitations.Remove(invitation);

            if(projection.JoinedAssociationIds == null)
                projection.JoinedAssociationIds = new List<Guid>();

            projection.JoinedAssociationIds.Add(@event.Id);
        }

        private static void Persist(Association.Application.Projections.AssociateProjection projection, InvitationRefused @event)
        {
            var invitation = projection.Invitations.FirstOrDefault(x => x.AssociationId == @event.Id && x.AssociateId == @event.AssociateId);
            if (invitation == null)
                return;

            projection.Invitations.Remove(invitation);
        }

        private static void Persist(Association.Application.Projections.AssociateProjection projection, AssociationLeft @event)
        {
            var association = projection.JoinedAssociationIds.FirstOrDefault(id => @event.Id == id);
            if (association == Guid.Empty)
                return;

            projection.JoinedAssociationIds.Remove(association);
        }
    }
}
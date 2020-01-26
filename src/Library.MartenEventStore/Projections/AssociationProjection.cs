using System;
using System.Collections.Generic;
using System.Linq;
using Association.Application.Projections;
using Association.Domain.Enumerations;
using Association.Domain.Events;
using Marten.Events.Projections;

namespace Library.MartenEventStore.Projections
{
    public class AssociationProjection : ViewProjection<Association.Application.Projections.AssociationProjection, Guid>
    {
        public AssociationProjection()
        {
            ProjectEvent<AssociationCreated>(Persist);
            ProjectEvent<AssociateInvited>(x => x.Id, Persist);
            ProjectEvent<InvitationAccepted>(x => x.Id, Persist);
            ProjectEvent<InvitationRefused>(x => x.Id, Persist);
            ProjectEvent<AssociateLeft>(x => x.Id, Persist);
            ProjectEvent<AssociateKicked>(x => x.Id, Persist);
            ProjectEvent<AssociatePromoted>(x => x.Id, Persist);
            ProjectEvent<AssociateDemoted>(x => x.Id, Persist);
        }

        private static void Persist(Association.Application.Projections.AssociationProjection projection, AssociationCreated @event)
        {
            projection.Id = @event.Id;
            projection.Name = @event.Name;
            projection.Members = new List<MembershipProjection>
            {
                new MembershipProjection
                {
                    AssociateId = @event.OwnerId,
                    AssociationId = @event.Id,
                    Role = @event.MembershipRoleId,
                    Status = @event.MembershipStatusId
                }
            };
        }

        private static void Persist(Association.Application.Projections.AssociationProjection projection, AssociateInvited @event)
        {
            projection.Members.Add(new MembershipProjection
            {
                AssociateId = @event.AssociateId,
                AssociationId = @event.Id,
                Role = @event.MembershipRoleId,
                Status = @event.MembershipStatusId
            });
        }

        private static void Persist(Association.Application.Projections.AssociationProjection projection, InvitationAccepted @event)
        {
            var member = projection.Members?.FirstOrDefault(x => x.AssociateId == @event.AssociateId);
            if (member == null)
                return;

            member.Status = MembershipStatus.Accepted.Id;
        }

        private static void Persist(Association.Application.Projections.AssociationProjection projection, InvitationRefused @event)
        {
            var member = projection.Members?.FirstOrDefault(x => x.AssociateId == @event.AssociateId);
            if (member == null)
                return;

            member.Status = MembershipStatus.Refused.Id;
        }

        private static void Persist(Association.Application.Projections.AssociationProjection projection, AssociateLeft @event)
        {
            var member = projection.Members?.FirstOrDefault(x => x.AssociateId == @event.AssociateId);
            if (member == null)
                return;

            member.Status = MembershipStatus.Left.Id;
        }

        private static void Persist(Association.Application.Projections.AssociationProjection projection, AssociateKicked @event)
        {
            var member = projection.Members?.FirstOrDefault(x => x.AssociateId == @event.AssociateId);
            if (member == null)
                return;

            member.Status = MembershipStatus.Kicked.Id;
        }

        private static void Persist(Association.Application.Projections.AssociationProjection projection, AssociatePromoted @event)
        {
            var member = projection.Members?.FirstOrDefault(x => x.AssociateId == @event.AssociateId);
            if (member == null)
                return;

            member.Role = MembershipRole.Admin.Id;
        }

        private static void Persist(Association.Application.Projections.AssociationProjection projection, AssociateDemoted @event)
        {
            var member = projection.Members?.FirstOrDefault(x => x.AssociateId == @event.AssociateId);
            if (member == null)
                return;

            member.Role = MembershipRole.Member.Id;
        }
    }
}
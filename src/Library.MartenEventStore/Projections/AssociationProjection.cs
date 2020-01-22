using System;
using System.Collections.Generic;
using Association.Application.Views;
using Association.Domain.Events;
using Marten.Events.Projections;

namespace Library.MartenEventStore.Projections
{
    public class AssociationProjection : ViewProjection<AssociationView, Guid>
    {
        public AssociationProjection()
        {
            ProjectEvent<AssociationCreated>(Persist);
            ProjectEvent<AssociateInvited>(x => x.Id, Persist);
        }

        private static void Persist(AssociationView view, AssociationCreated @event)
        {
            view.Id = @event.Id;
            view.Name = @event.Name;
            view.Members = new List<MembershipView>
            {
                new MembershipView
                {
                    AssociateId = @event.OwnerId,
                    AssociationId = @event.Id,
                    Role = @event.MembershipRoleId,
                    Status = @event.MembershipStatusId
                }
            };
        }

        private static void Persist(AssociationView view, AssociateInvited @event)
        {
            view.Members.Add(new MembershipView
            {
                AssociateId = @event.AssociateId,
                AssociationId = @event.Id,
                Role = @event.MembershipRoleId,
                Status = @event.MembershipStatusId
            });
        }
    }
}
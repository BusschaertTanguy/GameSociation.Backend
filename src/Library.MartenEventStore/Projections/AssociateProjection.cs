using System;
using System.Collections.Generic;
using Association.Application.Views;
using Association.Domain.Events;
using Marten.Events.Projections;

namespace Library.MartenEventStore.Projections
{
    public class AssociateProjection : ViewProjection<AssociateView, Guid>
    {
        public AssociateProjection()
        {
            ProjectEvent<AssociateCreated>(Persist);
            ProjectEvent<AssociationCreated>(x => x.OwnerId, Persist);
        }

        private static void Persist(AssociateView view, AssociateCreated @event)
        {
            view.Id = @event.Id;
            view.AccountId = @event.AccountId;
            view.Tag = new TagView
            {
                Number = @event.TagNumber,
                Username = @event.Username,
            };
        }

        private static void Persist(AssociateView view, AssociationCreated @event)
        {
            if(view.OwnedAssociationIds == null)
                view.OwnedAssociationIds = new List<Guid>();

            view.OwnedAssociationIds.Add(@event.Id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Association.Domain.Enumerations;
using Association.Domain.Events;
using Association.Domain.ValueObjects;
using Common.Domain.Entities;
using Common.Domain.Enumerations;

namespace Association.Domain.Entities
{
    public class Association : Aggregate
    {
        private string _name;
        private readonly List<Membership> _members = new List<Membership>();

        public Association()
        {
        }

        public Association(Guid ownerId, string name)
        {
            RaiseEvent(new AssociationCreated(Id, ownerId, name, MembershipRole.Owner.Id, MembershipStatus.Accepted.Id), Apply);
        }

        public void Invite(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Admin);

            var alreadyInAssociation = _members.Any(x => x.IsAssociate(associateId));
            if (alreadyInAssociation)
                throw new InvalidOperationException("Associate already has a membership");

            RaiseEvent(new AssociateInvited(Id, associateId, MembershipRole.Owner.Id, MembershipStatus.Accepted.Id), Apply);
        }

        private void Authorize(Guid responsibleId, MembershipRole requiredRole)
        {
            var membership = _members.FirstOrDefault(x => x.IsAssociate(responsibleId));

            if (membership == null)
                throw new InvalidOperationException("You're not part of this association");

            if (!membership.HasEquivalentRole(requiredRole))
                throw new InvalidOperationException("You don't have the rights to perform this action");
        }

        private void Apply(AssociationCreated @event)
        {
            Id = @event.Id;
            _name = @event.Name;
            var role = Enumeration.GetAll<MembershipRole>().First(x => x.Id == @event.MembershipRoleId);
            var status = Enumeration.GetAll<MembershipStatus>().First(x => x.Id == @event.MembershipStatusId);
            _members.Add(new Membership(Id, @event.OwnerId, role, status));
        }

        private void Apply(AssociateInvited @event)
        {
            var role = Enumeration.GetAll<MembershipRole>().First(x => x.Id == @event.MembershipRoleId);
            var status = Enumeration.GetAll<MembershipStatus>().First(x => x.Id == @event.MembershipStatusId);
            var membership = new Membership(Id, @event.AssociateId, role, status);
            _members.Add(membership);
        }
    }
}
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
        private readonly List<Membership> _members = new List<Membership>();
        private string _name;

        public Association()
        {
        }

        public Association(Guid ownerId, string name)
        {
            RaiseEvent(new AssociationCreated(Id, ownerId, name, MembershipRole.Owner.Id, MembershipStatus.Accepted.Id), Apply);
        }

        #region Public methods

        public void Invite(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Admin);

            var alreadyInAssociation = _members.Any(x => x.IsAssociate(associateId));
            if (alreadyInAssociation)
                throw new InvalidOperationException("Associate already has a membership");

            RaiseEvent(new AssociateInvited(Id, associateId, MembershipRole.Member.Id, MembershipStatus.Pending.Id), Apply);
        }

        public void AcceptInvitation(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Member);
            if (responsibleId != associateId)
                throw new InvalidOperationException("Only the associate can accept his invitation");

            RaiseEvent(new InvitationAccepted(Id, associateId), Apply);
        }

        public void RefuseInvitation(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Member);
            if (responsibleId != associateId)
                throw new InvalidOperationException("Only the associate can accept his invitation");

            RaiseEvent(new InvitationRefused(Id, associateId), Apply);
        }

        #endregion

        #region Private methods: Events

        private void Apply(AssociationCreated @event)
        {
            Id = @event.Id;
            _name = @event.Name;
            AddMember(@event.MembershipRoleId, @event.MembershipStatusId, @event.OwnerId);
        }

        private void Apply(AssociateInvited @event)
        {
            AddMember(@event.MembershipRoleId, @event.MembershipStatusId, @event.AssociateId);
        }

        private void Apply(InvitationAccepted @event)
        {
            var membership = GetMembership(@event.AssociateId);
            var acceptedMembership = membership.Accept();
            _members[_members.IndexOf(membership)] = acceptedMembership;
        }

        private void Apply(InvitationRefused @event)
        {
            var membership = GetMembership(@event.AssociateId);
            var refusedMembership = membership.Refuse();
            _members[_members.IndexOf(membership)] = refusedMembership;
        }

        #endregion

        #region Private methods: Utility

        private void Authorize(Guid responsibleId, MembershipRole requiredRole)
        {
            var membership = GetMembership(responsibleId);

            if (!membership.HasEquivalentRole(requiredRole))
                throw new InvalidOperationException("You don't have the rights to perform this action");
        }

        private Membership GetMembership(Guid associateId)
        {
            var membership = _members.FirstOrDefault(x => x.IsAssociate(associateId));

            if (membership == null)
                throw new InvalidOperationException("You're not part of this association");

            return membership;
        }

        private void AddMember(int roleId, int statusId, Guid associateId)
        {
            var role = Enumeration.GetAll<MembershipRole>().First(x => x.Id == roleId);
            var status = Enumeration.GetAll<MembershipStatus>().First(x => x.Id == statusId);
            var membership = new Membership(Id, associateId, role, status);
            _members.Add(membership);
        }

        #endregion
    }
}
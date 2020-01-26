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

            if (IsMember(associateId))
                throw new InvalidOperationException("Associate already has a membership");

            RaiseEvent(new AssociateInvited(Id, associateId, MembershipRole.Member.Id, MembershipStatus.Pending.Id), Apply);
        }

        public void AcceptInvitation(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Member);

            if (!IsMember(associateId))
                throw new InvalidOperationException("Associate is not part of the association");

            if (responsibleId != associateId)
                throw new InvalidOperationException("Only the associate can accept his invitation");

            var membership = GetMembership(associateId);
            if (!membership.IsPending)
                throw new InvalidOperationException("Can only accept a pending invitation");

            RaiseEvent(new InvitationAccepted(Id, associateId), Apply);
        }

        public void RefuseInvitation(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Member);

            if (!IsMember(associateId))
                throw new InvalidOperationException("Associate is not part of the association");

            if (responsibleId != associateId)
                throw new InvalidOperationException("Only the associate can accept his invitation");

            var membership = GetMembership(associateId);
            if (!membership.IsPending)
                throw new InvalidOperationException("Can only refuse a pending invitation");

            RaiseEvent(new InvitationRefused(Id, associateId), Apply);
        }

        public void Leave(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Member);

            if (!IsMember(associateId))
                throw new InvalidOperationException("Associate is not part of the association");

            if (responsibleId != associateId)
                throw new InvalidOperationException("Only the associate self can choose to leave");

            var membership = GetMembership(associateId);
            if (!membership.IsAccepted)
                throw new InvalidOperationException("Can only leave an accepted membership");

            RaiseEvent(new AssociateLeft(Id, associateId), Apply);
        }

        public void Kick(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Admin);

            if (!IsMember(associateId))
                throw new InvalidOperationException("Associate is not part of the association");

            if (responsibleId == associateId)
                throw new InvalidOperationException("Can't kick yourself, leave if you want to quit");

            var membership = GetMembership(associateId);
            if (!membership.IsAccepted)
                throw new InvalidOperationException("Can only kick an accepted membership");

            RaiseEvent(new AssociateKicked(Id, associateId), Apply);
        }

        public void Promote(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Admin);

            if (!IsMember(associateId))
                throw new InvalidOperationException("Associate is not part of the association");

            if (responsibleId == associateId)
                throw new InvalidOperationException("Can't promote yourself");

            var membership = GetMembership(associateId);
            if (!membership.IsAccepted)
                throw new InvalidOperationException("Can only promote accepted member");

            if(!membership.CanPromote)
                throw new InvalidOperationException("Can only promote associates with status Member");

            RaiseEvent(new AssociatePromoted(Id, associateId), Apply);
        }

        public void Demote(Guid responsibleId, Guid associateId)
        {
            Authorize(responsibleId, MembershipRole.Admin);

            if (!IsMember(associateId))
                throw new InvalidOperationException("Associate is not part of the association");

            if (responsibleId == associateId)
                throw new InvalidOperationException("Can't demote yourself");

            var membership = GetMembership(associateId);
            if (!membership.IsAccepted)
                throw new InvalidOperationException("Can only demote accepted member");

            if (!membership.CanDemote)
                throw new InvalidOperationException("Can only demote associates with status Admin");

            RaiseEvent(new AssociateDemoted(Id, associateId), Apply);
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
            ReplaceMembership(membership, acceptedMembership);
        }

        private void Apply(InvitationRefused @event)
        {
            var membership = GetMembership(@event.AssociateId);
            var refusedMembership = membership.Refuse();
            ReplaceMembership(membership, refusedMembership);
        }

        private void Apply(AssociateLeft @event)
        {
            var membership = GetMembership(@event.AssociateId);
            var leftMembership = membership.Leave();
            ReplaceMembership(membership, leftMembership);
        }

        private void Apply(AssociateKicked @event)
        {
            var membership = GetMembership(@event.AssociateId);
            var kickedMembership = membership.Kick();
            ReplaceMembership(membership, kickedMembership);
        }

        private void Apply(AssociatePromoted @event)
        {
            var membership = GetMembership(@event.AssociateId);
            var promotedMembership = membership.Promote();
            ReplaceMembership(membership, promotedMembership);
        }

        private void Apply(AssociateDemoted @event)
        {
            var membership = GetMembership(@event.AssociateId);
            var demotedMembership = membership.Demote();
            ReplaceMembership(membership, demotedMembership);
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

        private bool IsMember(Guid associateId)
        {
            return _members.Any(x => x.IsAssociate(associateId));
        }

        private void AddMember(int roleId, int statusId, Guid associateId)
        {
            var role = Enumeration.GetAll<MembershipRole>().First(x => x.Id == roleId);
            var status = Enumeration.GetAll<MembershipStatus>().First(x => x.Id == statusId);
            var membership = new Membership(Id, associateId, role, status);
            _members.Add(membership);
        }

        private void ReplaceMembership(Membership oldMembership, Membership newMembership)
        {
            _members[_members.IndexOf(oldMembership)] = newMembership;
        }

        #endregion
    }
}
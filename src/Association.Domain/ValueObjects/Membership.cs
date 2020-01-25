using System;
using System.Collections.Generic;
using Association.Domain.Enumerations;
using Common.Domain.ValueObjects;

namespace Association.Domain.ValueObjects
{
    public class Membership : ValueObject
    {
        private readonly Guid _associationId;
        private readonly Guid _associateId;
        private readonly MembershipRole _role;
        private readonly MembershipStatus _status;

        public Membership(Guid associationId, Guid associateId, MembershipRole role, MembershipStatus status)
        {
            _associationId = associationId;
            _associateId = associateId;
            _role = role;
            _status = status;
        }

        public bool IsAssociate(Guid associateId) => _associateId == associateId;
        public bool HasEquivalentRole(MembershipRole role) => _role.IsEquivalent(role);
        public bool IsPending => _status.Equals(MembershipStatus.Pending);
        public bool IsAccepted => _status.Equals(MembershipStatus.Accepted);

        public Membership Accept()
        {
            return new Membership(_associationId, _associateId, _role, MembershipStatus.Accepted);
        }

        public Membership Refuse()
        {
            return new Membership(_associationId, _associateId, _role, MembershipStatus.Refused);
        }

        public Membership Leave()
        {
            return new Membership(_associationId, _associateId, _role, MembershipStatus.Left);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _associationId;
            yield return _associateId;
        }
    }
}
using System.Linq;
using Common.Domain.Enumerations;

namespace Association.Domain.Enumerations
{
    public class MembershipRole : Enumeration
    {
        private readonly int _authority;

        public static MembershipRole Owner = new MembershipRole(0, "Owner", 2);
        public static MembershipRole Admin = new MembershipRole(1, "Admin", 1);
        public static MembershipRole Member = new MembershipRole(2, "Member", 0);

        private MembershipRole(int id, string value, int authority) : base(id, value)
        {
            _authority = authority;
        }

        public bool IsEquivalent(MembershipRole otherRole) => _authority >= otherRole._authority;
    }
}
using Common.Domain.Enumerations;

namespace Association.Domain.Enumerations
{
    public class MembershipStatus : Enumeration
    {
        public static MembershipStatus Pending = new MembershipStatus(0, "Pending");
        public static MembershipStatus Accepted = new MembershipStatus(1, "Accepted");
        public static MembershipStatus Refused = new MembershipStatus(2, "Refused");
        public static MembershipStatus Left = new MembershipStatus(3, "Left");
        public static MembershipStatus Kicked = new MembershipStatus(4, "Kicked");

        private MembershipStatus(int id, string value) : base(id, value)
        {
        }
    }
}
using System.Linq;
using Association.Domain.Entities;
using Association.Domain.Enumerations;
using Common.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.EntityFramework.Mappings
{
    public class MembershipMapping : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("Memberships");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AssociateId).IsRequired();

            builder.Property(x => x.Role).HasConversion(
                x => x.Id,
                x => Enumeration.GetAll<MembershipRole>().First(y => y.Id == x)
            ).IsRequired();

            builder.Property(x => x.Status).HasConversion(
                x => x.Id,
                x => Enumeration.GetAll<MembershipStatus>().First(y => y.Id == x)
            ).IsRequired();
        }
    }
}
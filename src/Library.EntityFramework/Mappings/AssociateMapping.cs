using Association.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.EntityFramework.Mappings
{
    public class AssociateMapping : IEntityTypeConfiguration<Associate>
    {
        public void Configure(EntityTypeBuilder<Associate> builder)
        {
            builder.ToTable("Associates");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.AccountId).IsUnique();
            builder.Property(x => x.AccountId).IsRequired();
            builder.OwnsOne(x => x.Tag, tag =>
            {
                tag.Property(x => x.Username).IsRequired();
                tag.Property(x => x.Number).IsRequired();
                tag.ToTable("Tags");
            });
        }
    }
}
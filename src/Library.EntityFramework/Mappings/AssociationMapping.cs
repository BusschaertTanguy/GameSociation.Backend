using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.EntityFramework.Mappings
{
    public class AssociationMapping : IEntityTypeConfiguration<Association.Domain.Entities.Association>
    {
        public void Configure(EntityTypeBuilder<Association.Domain.Entities.Association> builder)
        {
            builder.ToTable("Associations");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Metadata.FindNavigation(nameof(Association.Domain.Entities.Association.Members))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
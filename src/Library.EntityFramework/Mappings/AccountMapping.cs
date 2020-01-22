using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.EntityFramework.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account.Domain.Entities.Account>
    {
        public void Configure(EntityTypeBuilder<Account.Domain.Entities.Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Password).IsRequired();
        }
    }
}
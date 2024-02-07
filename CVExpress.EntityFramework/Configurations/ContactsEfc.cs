using CVExpress.Entities.Efos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVExpress.EntityFramework.Configurations
{
    public class ContactsEfc : IEntityTypeConfiguration<ContactsEfo>
    {
        public void Configure(EntityTypeBuilder<ContactsEfo> builder)
        {
            builder.ToTable("Contacts");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.Subject).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Message).IsRequired().HasMaxLength(250);
        }
    }
}

using CVExpress.Entities.Efos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVExpress.EntityFramework.Configurations
{
    public class RegisterUsersEfc : IEntityTypeConfiguration<RegisterUsersEfo>
    {
        public void Configure(EntityTypeBuilder<RegisterUsersEfo> builder)
        {
            builder.ToTable("RegisterUsers");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(100);
            builder.Property(p => p.BirtDate).IsRequired();
            builder.Property(p => p.Location).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Country).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Nationality).IsRequired().HasMaxLength(50);
            builder.Property(p => p.PhoneNumber).IsRequired();
        }
    }
}

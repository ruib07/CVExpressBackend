using CVExpress.Entities.Efos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CVExpress.EntityFramework.Configurations
{
    public class ExperienceEfc : IEntityTypeConfiguration<ExperienceEfo>
    {
        public void Configure(EntityTypeBuilder<ExperienceEfo> builder)
        {
            builder.ToTable("Experiences");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Function).IsRequired().HasMaxLength(50);
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.EndDate).IsRequired();
            builder.Property(p => p.Entity).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(250);
            builder.Property(p => p.User_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(p => p.Users)
                .WithMany()
                .HasForeignKey(p => p.User_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

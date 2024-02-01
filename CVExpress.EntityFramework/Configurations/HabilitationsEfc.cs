using CVExpress.Entities.Efos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVExpress.EntityFramework.Configurations
{
    public class HabilitationsEfc : IEntityTypeConfiguration<HabilitationsEfo>
    {
        public void Configure(EntityTypeBuilder<HabilitationsEfo> builder)
        {
            builder.ToTable("Habilitations");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Designation).IsRequired().HasMaxLength(50);
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.EndDate).IsRequired();
            builder.Property(p => p.Institution).IsRequired().HasMaxLength(50);
            builder.Property(p => p.FormationArea).IsRequired().HasMaxLength(50);
            builder.Property(p => p.User_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(p => p.Users)
                .WithMany()
                .HasForeignKey(p => p.User_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

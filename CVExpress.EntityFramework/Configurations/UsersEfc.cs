﻿using CVExpress.Entities.Efos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVExpress.EntityFramework.Configurations
{
    public class UsersEfc : IEntityTypeConfiguration<UsersEfo>
    {
        public void Configure(EntityTypeBuilder<UsersEfo> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.BirtDate).IsRequired();
            builder.Property(p => p.Location).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Country).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Nationality).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.Password).IsRequired().HasMaxLength(100);
            builder.Property(p => p.RegisterUser_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(p => p.RegisterUsers)
                .WithOne()
                .HasForeignKey<UsersEfo>(p => p.RegisterUser_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

// <copyright file="TourConfiguration.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace ORM.Configuration
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.NameTour)
                .IsRequired().HasMaxLength(64);
            builder.Property(t => t.DateStart)
                .IsRequired();
            builder.Property(t => t.DateEnd)
                .IsRequired();
            builder.Property(t => t.Price)
                .IsRequired();


            builder
                .HasOne(t => t.Employee)
                .WithMany(e => e.Tours)
                .HasForeignKey("EmployeeId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

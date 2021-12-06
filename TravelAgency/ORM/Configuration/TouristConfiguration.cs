// <copyright file="TouristConfiguration.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace ORM.Configuration
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TouristConfiguration:IEntityTypeConfiguration<Tourist>
    {
        public void Configure(EntityTypeBuilder<Tourist> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FirstName)
                .IsRequired();
            builder.Property(t => t.LastName)
                .IsRequired();
            builder.Property(t => t.Birthday)
                .IsRequired();
            builder.Property(t => t.Phone)
                .IsRequired();
            builder.Property(t => t.Email)
                .IsRequired();

            builder.HasMany(t => t.Tours)
                .WithMany(t => t.Tourists);
        }
    }
}
// <copyright file="CityConfiguration.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace ORM.Configuration
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CityConfiguration:IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.NameCity)
                .IsRequired();

            builder
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey("CountryId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
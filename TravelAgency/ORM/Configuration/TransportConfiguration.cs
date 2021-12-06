// <copyright file="TransportConfiguration.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace ORM.Configuration
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TransportConfiguration:IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.NameTransport)
                .IsRequired();

            builder.HasMany(t => t.Tours)
                .WithMany(t => t.Transports);
        }
    }
}
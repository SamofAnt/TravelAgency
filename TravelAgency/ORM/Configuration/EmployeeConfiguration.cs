// <copyright file="EmployeeConfiguration.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace ORM.Configuration
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeConfiguration:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName)
                .IsRequired();
            builder.Property(e => e.LastName)
                .IsRequired();
            builder.Property(e => e.Phone)
                .IsRequired();
            builder.Property(e => e.Email)
                .IsRequired();
            builder.Property(e => e.Position)
                .IsRequired();
            builder.Property(e => e.Birthday)
                .IsRequired();
        }
    }
}
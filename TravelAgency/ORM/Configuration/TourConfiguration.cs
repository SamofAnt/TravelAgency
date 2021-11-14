using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM.Configuration
{
    class TourConfiguration: IEntityTypeConfiguration<Tour>
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
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

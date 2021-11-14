using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM.Configuration
{
    public class AttractionConfiguration:IEntityTypeConfiguration<Attraction>   
    {
        public void Configure(EntityTypeBuilder<Attraction> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("ID_ATTRACTION");

            builder.Property(a => a.NameAttraction)
                .IsRequired();


            builder
                .HasOne(a => a.City)
                .WithMany(c=>c.Attractions)
                .HasForeignKey(a=>a.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
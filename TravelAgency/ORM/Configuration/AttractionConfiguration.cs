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

            builder.Property(a => a.NameAttraction)
                .IsRequired();


            builder
                .HasOne(a => a.City)
                .WithMany(c => c.Attractions)
                .HasForeignKey("CityId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM.Configuration
{
    public class CityConfiguration:IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("ID_CITY");
            builder.Property(c => c.NameCity)
                .IsRequired();


            builder
                .HasOne(c=>c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c=>c.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
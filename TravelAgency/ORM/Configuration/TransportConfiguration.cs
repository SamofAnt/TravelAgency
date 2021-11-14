using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM.Configuration
{
    public class TransportConfiguration:IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("ID_TRANSPORT");

            builder.Property(t => t.NameTransport)
                .IsRequired();


            builder.HasMany(t => t.Tours)
                .WithMany(t => t.Transports);
        }
    }
}
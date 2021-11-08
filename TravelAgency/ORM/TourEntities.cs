


namespace ORM
{
    using System;
    using System.Data.Entity;
    using Domain;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure;

    public class TourEntities:DbContext
    {
        public TourEntities():base("name=Tour")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Для Туриста
            modelBuilder.Entity<Tourist>().HasKey(t => t.Id);
            modelBuilder.Entity<Tourist>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Tourist>().Property(t => t.FirstName)
                .IsRequired().HasMaxLength(64);
            modelBuilder.Entity<Tourist>().Property(t => t.LastName)
                .IsRequired().HasMaxLength(64);
            modelBuilder.Entity<Tourist>().Property(t => t.Birthday)
                .IsRequired();
            modelBuilder.Entity<Tourist>().Property(t => t.Phone).IsRequired();

            //Для Тура
            modelBuilder.Entity<Tour>().HasKey(t => t.Id);
            modelBuilder.Entity<Tour>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Tour>().Property(t => t.NameTour)
                .IsRequired().HasMaxLength(64);
            modelBuilder.Entity<Tour>().Property(t => t.DateStart)
                .IsRequired();
            modelBuilder.Entity<Tour>().Property(t => t.DateEnd)
                .IsRequired();
            modelBuilder.Entity<Tour>().Property(t => t.Price)
                .IsRequired();

            //связь многие-ко-многим Тура и Туриста
            modelBuilder.Entity<Tourist>()
                .HasMany(t => t.Tours)
                .WithMany(t => t.Tourists);

            //Для отеля
            modelBuilder.Entity<Hotel>().
                HasKey(h => h.Id);
            modelBuilder.Entity<Hotel>().Property(h => h.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Hotel>().Property(h => h.NameHotel)
                .IsRequired().HasMaxLength(64);
            modelBuilder.Entity<Hotel>().Property(h => h.Class)
                .IsRequired();

            //связь многие-ко-многим Отеля и Тура
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Tours)
                .WithMany(t => t.Hotels);

            //Для Страны
            modelBuilder.Entity<Country>().HasKey(c => c.Id);
            modelBuilder.Entity<Country>().Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Country>().Property(c => c.NameCountry)
                .IsRequired().HasMaxLength(64);

            //Связь один-ко-многим для Отеля и Страны
            modelBuilder.Entity<Hotel>()
                .HasRequired<Country>(h => h.Country)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h=>h.CountryId)
                .WillCascadeOnDelete(false);

            //Для Города
            modelBuilder.Entity<City>().HasKey(c => c.Id);
            modelBuilder.Entity<City>().Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<City>().Property(c => c.NameCity)
                .IsRequired().HasMaxLength(64);

            modelBuilder.Entity<City>()
                .HasRequired<Country>(c=>c.Country)
                .WithMany(c=>c.Cities)
                .HasForeignKey(c=>c.CountryId)
                .WillCascadeOnDelete(false);

            //для Достопримечательности
            modelBuilder.Entity<Attraction>().HasKey(a => a.Id);
            modelBuilder.Entity<Attraction>().Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Attraction>().Property(a => a.NameAttraction)
                .IsRequired().HasMaxLength(64);

            modelBuilder.Entity<Attraction>()
                .HasRequired<City>(a=>a.City)
                .WithMany(c=>c.Attractions)
                .HasForeignKey(a=>a.CityId)
                .WillCascadeOnDelete(false);
        }

        public virtual DbSet<Attraction> Attractions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Tourist> Tourists { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }


    }
}

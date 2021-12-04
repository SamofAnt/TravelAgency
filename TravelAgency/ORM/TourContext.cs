// <copyright file="TourContext.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

using System.Reflection;
using ORM.Configuration;

namespace ORM
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer;

    using System.ComponentModel.DataAnnotations.Schema;
  
    using System.Data.Entity.Infrastructure;
    using Domain;

    /// <summary>
    /// 
    /// </summary>
    public class TourContext : DbContext
    {
        public TourContext()
        {
            Database.EnsureCreated();
        }

        public TourContext(DbContextOptions<TourContext> options) : base(options) { }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-K8R5DB7;Initial Catalog=TourAgency;Integrated Security=true");
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            var countries = new Country[]
            {
                new Country(1,"Russia"),
                new Country(2, "USA"),
                new Country(3, "Italy")
            };
            modelBuilder.Entity<Country>().HasData(countries);
            modelBuilder.Entity<City>().HasData(new City() { Id= 1, NameCity = "Moscow", CountryId = 1});
            modelBuilder.Entity<Hotel>().HasData(new Hotel() { Id = 1, NameHotel = "Novotel", CountryId = 1 });
            modelBuilder.Entity<Attraction>().HasData(new Attraction() { Id = 1, NameAttraction = "Museum", CityId = 1});
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                Id = 1,
                LastName = "Samofalov",
                FirstName = "Anton",
                Phone = "+7(915)-356-08-98",
                Email = "samofalov@gmail.com",
                Birthday = DateTime.Now,
                Position = "CEO"
            });
            modelBuilder.Entity<Transport>().HasData(new Transport() { Id = 1, NameTransport = "Bus BSU"});
            modelBuilder.Entity<Tourist>().HasData(new Tourist()
            {
                Id = 1,
                LastName = "Pyaternev",
                FirstName = "Alex",
                Phone = "+7(915)-356-08-98",
                Email = "alex@gmail.com",
                Birthday = DateTime.Now
            });
        }

        public virtual DbSet<Attraction> Attraction { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Tourist> Tourist { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<Transport> Transport { get; set; }
    }
}

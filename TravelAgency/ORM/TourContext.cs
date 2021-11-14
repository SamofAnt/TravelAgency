// <copyright file="TourEntities.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

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
        private readonly string _connectionString;
        public TourContext()
        {
            _connectionString = "Data Source=DESKTOP-K8R5DB7;Initial Catalog=TourAgency;Integrated Security=true";
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            (new TouristConfiguration()).Configure(modelBuilder.Entity<Tourist>());

            (new EmployeeConfiguration()).Configure(modelBuilder.Entity<Employee>());
            
            (new TransportConfiguration()).Configure(modelBuilder.Entity<Transport>());

            (new TourConfiguration()).Configure(modelBuilder.Entity<Tour>());

            (new HotelConfiguration()).Configure(modelBuilder.Entity<Hotel>());

            (new CountryConfiguration()).Configure(modelBuilder.Entity<Country>());

            (new CityConfiguration()).Configure(modelBuilder.Entity<City>());
            
            (new AttractionConfiguration()).Configure(modelBuilder.Entity<Attraction>());
            
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

// <copyright file="CountryRepositoryTest.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace Repository.Tests
{
    using Domain;
    using ORM.Repositories.Interfaces;
    using Xunit;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using ORM;
    using ORM.Repositories;

    /// <summary>
    /// Класс для тестов репозитория страны.
    /// </summary>
    public class CountryRepositoryTest
    {
        /// <summary>
        /// Тест на добавление страны без городов и отелей.
        /// </summary>
        [Fact]
        public void Add_WhenHaveNoCitiesAndHotels()
        {
            IRepository<Country> sut = GetInMemoryCountryRepository();
            Country country = new Country(sut.GetAll().Count() + 1, "France");

            Country savedPerson = sut.Create(country);

            Assert.Equal(4, sut.GetAll().Count());
            Assert.Equal("France", savedPerson.NameCountry);
            Assert.Equal(0, savedPerson.Cities.Count);
            Assert.Equal(0, savedPerson.Hotels.Count);
        }

        /// <summary>
        /// Тест на добавление страны с городом.
        /// </summary>
        [Fact]
        public void Add_WhenHaveSingleCity()
        {
            IRepository<Country> sut = GetInMemoryCountryRepository();
            Country country = new Country()
            {
                Id = sut.GetAll().Count() + 1,
                NameCountry = "France",
                Cities = new HashSet<City>()
                {
                    new City(2,"Paris")
                }
            };

            Country savedCountry = sut.Create(country);

            Assert.Equal(4, sut.GetAll().Count());
            Assert.Equal("France", savedCountry.NameCountry);
            Assert.Equal(1, savedCountry.Cities.Count);
            Assert.Equal("Paris", savedCountry.Cities.ToList()[0].NameCity);
        }

        /// <summary>
        /// Тест на добавление страны с отелем.
        /// </summary>
        [Fact]
        public void Add_WhenHaveSingleHotel()
        {
            IRepository<Country> sut = GetInMemoryCountryRepository();
            Country country = new Country()
            {
                Id = sut.GetAll().Count() + 1,
                NameCountry = "France",
                Hotels = new HashSet<Hotel>()
                {
                    new Hotel()
                    {
                        Id=2,
                        NameHotel = "Radisson"
                    }
                }
            };

            Country savedCountry = sut.Create(country);

            Assert.Equal(4, sut.GetAll().Count());
            Assert.Equal("France", savedCountry.NameCountry);
            Assert.Equal(1, savedCountry.Hotels.Count);
            Assert.Equal("Radisson", savedCountry.Hotels.ToList()[0].NameHotel);
        }

        /// <summary>
        /// Тест на добавление страны с городом и отелем.
        /// </summary>
        [Fact]
        public void Add_WhenHaveSingleHotelAndCity()
        {
            IRepository<Country> sut = GetInMemoryCountryRepository();
            Country country = new Country()
            {
                Id = sut.GetAll().Count() + 1,
                NameCountry = "France",
                Hotels = new HashSet<Hotel>()
                {
                    new Hotel()
                    {
                        Id=2,
                        NameHotel = "Radisson"
                    }
                },
                Cities = new HashSet<City>()
                {
                    new City()
                    {
                        Id = 2,
                        NameCity = "Paris"
                    }
                }
             
            };

            Country savedCountry = sut.Create(country);

            Assert.Equal(4, sut.GetAll().Count());
            Assert.Equal("France", savedCountry.NameCountry);
            Assert.Equal(1, savedCountry.Hotels.Count);
            Assert.Equal("Radisson", savedCountry.Hotels.ToList()[0].NameHotel);
            Assert.Equal(1, savedCountry.Cities.Count);
            Assert.Equal("Paris", savedCountry.Cities.ToList()[0].NameCity);
        }

        /// <summary>
        /// Тест на успешное удаление страны.
        /// </summary>
        [Fact]
        public void Delete_WhenNoCitiesAndHotels()
        {
            IRepository<Country> sut = GetInMemoryCountryRepository();

            Country deleteCountry = sut.GetById(1);
            sut.Delete(1);
            

            Assert.Equal(2, sut.GetAll().Count());
            Assert.Null( sut.GetAll().FirstOrDefault(c=>c.NameCountry== deleteCountry.NameCountry));
        }

        /// <summary>
        /// Тест на успешное обновление страны.
        /// </summary>
        [Fact]
        public void Update_WhenNoCitiesAndHotels()
        {
            IRepository<Country> sut = GetInMemoryCountryRepository();

            Country updateCountry = sut.GetById(1);
            updateCountry.NameCountry = "England";
            sut.Update(updateCountry);

            Assert.Equal("England", sut.GetById(1).NameCountry);
        }

        /// <summary>
        /// Получение объекта репозитория страны.
        /// </summary>
        /// <returns>объект репозитория страны.</returns>
        private IRepository<Country> GetInMemoryCountryRepository()
        {
            DbContextOptions<TourContext> options;
            var builder = new DbContextOptionsBuilder<TourContext>();
            builder.UseInMemoryDatabase(databaseName: "CountryDb");
            options = builder.Options;
            TourContext tourContext = new TourContext(options);
            tourContext.Database.EnsureDeleted();
            tourContext.Database.EnsureCreated();
            return new CountryRepository(tourContext);
        }
    }
}

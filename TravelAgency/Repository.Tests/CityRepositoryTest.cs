// <copyright file="CityRepositoryTest.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace Repository.Tests
{
    using Domain;
    using ORM;
    using ORM.Repositories;
    using ORM.Repositories.Interfaces;
    using Xunit;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Класс для тестов репозитория города.
    /// </summary>
    public class CityRepositoryTest
    {
        /// <summary>
        /// Тест для добавления города без страны и достопримечательности.
        /// </summary>
        [Fact]
        public void Add_WhenHaveNoCountryAndAttraction()
        {
            IRepository<City> sut = GetInMemoryCityRepository();
            City city= new City(sut.GetAll().Count() + 1, "Paris");

            City savedCity = sut.Create(city);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Paris", savedCity.NameCity);
            Assert.Equal(0, savedCity.Attractions.Count);
            Assert.Null(savedCity.Country);
        }
        
        /// <summary>
        /// Тест на добавление города без страны.
        /// </summary>
        [Fact]
        public void Add_WhenHaveCountry()
        {
            IRepository<City> sut = GetInMemoryCityRepository();
            City city = new City()
            {
                Id = sut.GetAll().Count() + 1,
                NameCity = "London",
                Country = new Country(4, "England")
            };

            City savedCity = sut.Create(city);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("London", savedCity.NameCity);
            Assert.Equal("England", savedCity.Country.NameCountry);
            Assert.Equal(1, savedCity.Country.Cities.Count);
        }

        /// <summary>
        /// Тест на добавление города с достопримечательности.
        /// </summary>
        [Fact]
        public void Add_WhenHaveSingleAttraction()
        {
            IRepository<City> sut = GetInMemoryCityRepository();
            City city = new City()
            {
                Id = sut.GetAll().Count() + 1,
                NameCity = "London",
                Attractions = new HashSet<Attraction>()
                {
                    new Attraction(2, "Kremlin")
                }
            };

            City savedCity = sut.Create(city);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("London", savedCity.NameCity);
            Assert.Equal(1, savedCity.Attractions.Count);
            Assert.Equal("Kremlin", savedCity.Attractions.ToList()[0].NameAttraction);
        }

        /// <summary>
        /// Тест на успешное удаление города.
        /// </summary>
        [Fact]
        public void Delete_WhenNoCountryAndAttractions()
        {
            IRepository<City> sut = GetInMemoryCityRepository();
            sut.Create(GenerateCity(1, "Tested", GenerateCountry(4, "Lol")));
            City deleteCity = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.NameCity == deleteCity.NameCity));
        }

        /// <summary>
        /// Тест на успешное обновление города.
        /// </summary>
        [Fact]
        public void Update_WhenNoCountryAndAttractions()
        {
            IRepository<City> sut = GetInMemoryCityRepository();
            sut.Create(GenerateCity(1, "Tested", GenerateCountry(4, "Lol")));

            City updateCity = sut.GetById(1);
            updateCity.NameCity = "London";
            sut.Update(updateCity);

            Assert.Equal("London", sut.GetById(1).NameCity);
        }

        /// <summary>
        /// Получение объекта репозитория города.
        /// </summary>
        /// <returns>объект репозитория города.</returns>
        private IRepository<City> GetInMemoryCityRepository()
        {
            DbContextOptions<TourContext> options;
            var builder = new DbContextOptionsBuilder<TourContext>();
            builder.UseInMemoryDatabase(databaseName: "CityDb");
            options = builder.Options;
            TourContext tourContext = new TourContext(options);
            tourContext.Database.EnsureDeleted();
            tourContext.Database.EnsureCreated();
            return new CityRepository(tourContext);
        }

        /// <summary>
        /// Создание сущности города.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="nameCity">Название города.</param>
        /// <param name="country">Ссылка на страну.</param>
        /// <returns>Город.</returns>
        private City GenerateCity(int id, string nameCity, Country country) => new()
        {
            Id = id,
            NameCity = nameCity,
            Country = country
        };

        /// <summary>
        /// Создание сущности города.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="nameCountry">Название</param>
        /// <returns>Страна</returns>
        private Country GenerateCountry(int id, string nameCounty) => new(id, nameCounty);
    }
}

// <copyright file="HotelRepositoryTest.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace Repository.Tests
{
    using Domain;
    using ORM;
    using ORM.Repositories;
    using ORM.Repositories.Interfaces;
    using Xunit;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Класс для тестов репозитория отеля.
    /// </summary>
    public class HotelRepositoryTest
    {
        /// <summary>
        /// Тест на добавление отеля без страны
        /// </summary>
        [Fact]
        public void Add_WhenHaveNoCountry()
        {
            IRepository<Hotel> sut = GetInMemoryCityRepository();
            Hotel hotel = new Hotel(sut.GetAll().Count() + 1, "Radisson", 5);

            Hotel savedHotel = sut.Create(hotel);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Radisson", savedHotel.NameHotel);
            Assert.Null(savedHotel.Country);
        }
        
        /// <summary>
        /// Тест на добавление отеля с страной.
        /// </summary>
        [Fact]
        public void Add_WhenHaveCountry()
        {
            IRepository<Hotel> sut = GetInMemoryCityRepository();
            Hotel hotel = new Hotel()
            {
                Id = sut.GetAll().Count() + 1,
                NameHotel = "Radisson",
                ClassHotel = 5,
                Country = new Country(4, "England")
            };

            Hotel savedHotel = sut.Create(hotel);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Radisson", savedHotel.NameHotel);
            Assert.Equal("England", savedHotel.Country.NameCountry);
            Assert.Equal(1, savedHotel.Country.Hotels.Count);
        }

        /// <summary>
        /// Тест на успешное удаление отеля.
        /// </summary>
        [Fact]
        public void Delete_ValidData_Success()
        {
            IRepository<Hotel> sut = GetInMemoryCityRepository();
            sut.Create(GenerateHotel(1, "Radisson", 5, GenerateCountry(4, "China")));
            
            Hotel deleteCity = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.NameHotel == deleteCity.NameHotel));
        }

        /// <summary>
        /// Тест на успешное обновление отеля.
        /// </summary>
        [Fact]
        public void Update_ValidData_Success()
        {
            IRepository<Hotel> sut = GetInMemoryCityRepository();
            sut.Create(GenerateHotel(1, "Radisson", 5, GenerateCountry(4, "China")));

            Hotel updateCity = sut.GetById(1);
            updateCity.NameHotel = "London";
            sut.Update(updateCity);

            Assert.Equal("London", sut.GetById(1).NameHotel);
        }

        /// <summary>
        /// Получение объекта репозитория отеля.
        /// </summary>
        /// <returns>объект репозитория отеля.</returns>
        private IRepository<Hotel> GetInMemoryCityRepository()
        {
            DbContextOptions<TourContext> options;
            var builder = new DbContextOptionsBuilder<TourContext>();
            builder.UseInMemoryDatabase(databaseName: "HotelDb");
            options = builder.Options;
            TourContext tourContext = new TourContext(options);
            tourContext.Database.EnsureDeleted();
            tourContext.Database.EnsureCreated();
            return new HotelRepository(tourContext);
        }

        /// <summary>
        /// Создание сущности отеля
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="nameHotel">Название отеля.</param>
        /// <param name="classHotel">Класс отеля.</param>
        /// <param name="country">Ссылка на объект страны.</param>
        /// <returns>Отель</returns>
        private Hotel GenerateHotel(int id, string nameHotel, int classHotel, Country country)
        {
            return new Hotel
            {
                Id = id,
                NameHotel = nameHotel,
                ClassHotel = classHotel,
                Country = country
            };
        }

        /// <summary>
        /// Создание сущности города.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="nameCountry">Название</param>
        /// <returns>Страна</returns>
        private Country GenerateCountry(int id, string nameCountry) => new(id, nameCountry);
    }
}

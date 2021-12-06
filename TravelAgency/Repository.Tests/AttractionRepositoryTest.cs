// <copyright file="AttractionRepositoryTest.cs" company="Самофалов А.П.">
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
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Класс для тестов репозитория достопримечтальности.
    /// </summary>
    public class AttractionRepositoryTest
    {
        /// <summary>
        /// Тест для добавления достопримечательности без города
        /// </summary>
        [Fact]
        public void Add_WhenHaveNoCity()
        {
            IRepository<Attraction> sut = GetInMemoryAttractionRepository();
            Attraction attraction = new Attraction(sut.GetAll().Count() + 1, "Kremlin");

            Attraction savedAttraction = sut.Create(attraction);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Kremlin", savedAttraction.NameAttraction);
            Assert.Null(savedAttraction.City);
        }

        /// <summary>
        /// Тест для добавления достопримечательности c городом
        /// </summary>
        [Fact]
        public void Add_ValidData_Success()
        {
            IRepository<Attraction> sut = GetInMemoryAttractionRepository();
            Attraction attraction = new Attraction()
            {
                Id = sut.GetAll().Count() + 1,
                NameAttraction = "Kremlin",
                City = new City(4, "Moscow")
            };

            Attraction savedAttraction = sut.Create(attraction);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Kremlin", savedAttraction.NameAttraction);
            Assert.Equal("Moscow", savedAttraction.City.NameCity);
        }

        /// <summary>
        /// Тест для успешного удаления достопримечательности.
        /// </summary>
        [Fact]
        public void Delete_ValidData_Success()
        {
            IRepository<Attraction> sut = GetInMemoryAttractionRepository();
            sut.Create(GenerateAttraction(1, "Tester", GenerateCity(1, "tests", GenerateCountry(4, "China"))));

            Attraction deleteAttraction = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.NameAttraction == deleteAttraction.NameAttraction));
        }


        /// <summary>
        /// Тест для успешного обновления достопримечательности.
        /// </summary>
        [Fact]
        public void Update_ValidData_Success()
        {
            IRepository<Attraction> sut = GetInMemoryAttractionRepository();
            sut.Create(GenerateAttraction(1, "Tester", GenerateCity(1, "tests", GenerateCountry(4, "China"))));

            Attraction updateAttraction = sut.GetById(1);
            updateAttraction.NameAttraction = "The Eiffel Tower";
            sut.Update(updateAttraction);

            Assert.Equal("The Eiffel Tower", sut.GetById(1).NameAttraction);
        }

        /// <summary>
        /// Получение объекта репозитория достопримечательности.
        /// </summary>
        /// <returns>объект репозитория достопримечательности.</returns>
        private IRepository<Attraction> GetInMemoryAttractionRepository()
        {
            DbContextOptions<TourContext> options;
            var builder = new DbContextOptionsBuilder<TourContext>();
            builder.UseInMemoryDatabase(databaseName: "CityDB");
            options = builder.Options;
            TourContext tourContext = new TourContext(options);
            tourContext.Database.EnsureDeleted();
            tourContext.Database.EnsureCreated();
            return new AttractionRepository(tourContext);
        }
        
        /// <summary>
        /// Создание сущности достпримечательность.
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="nameAttraction">Название достопримечательноти.</param>
        /// <param name="city">Ссылка на город</param>
        /// <returns>Достопримечательность.</returns>
        private Attraction GenerateAttraction(int id, string nameAttraction, City city) => new()
        {
            Id = 1,
            NameAttraction = nameAttraction,
            City = city
        };

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
        private Country GenerateCountry(int id, string nameCountry) => new(id, nameCountry);
    }
}

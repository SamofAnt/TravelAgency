// <copyright file="TouristRepositoryTest.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace Repository.Tests
{
    using Domain;
    using ORM;
    using ORM.Repositories;
    using ORM.Repositories.Interfaces;
    using Xunit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Класс для тестов репозитория туриста.
    /// </summary>
    public class TouristRepositoryTest
    {
        /// <summary>
        /// Тест на добавление туриста без тура.
        /// </summary>
        [Fact]
        public void Add_WhenHaveNoTour()
        {
            IRepository<Tourist> sut = GetInMemoryTouristRepository();
            Tourist tourist = new Tourist()
            {
                Id = sut.GetAll().Count() + 1,
                LastName = "Samofalov",
                FirstName = "Anton",
                Phone = "+7(915)-356-08-98",
                Email = "samofalov@gmail.com",
                Birthday = DateTime.Now
            };

            Tourist savedTourist = sut.Create(tourist);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Samofalov A.", savedTourist.FullName);
            Assert.NotNull(savedTourist.Tours);
        }

        /// <summary>
        /// Тест на добавление туриста с туром.
        /// </summary>
        [Fact]
        public void Add_WhenHaveTour()
        {
            IRepository<Tourist> sut = GetInMemoryTouristRepository();
            Tourist transport = new Tourist()
            {
                Id = sut.GetAll().Count() + 1,
                LastName = "Samofalov",
                FirstName = "Anton",
                Phone = "+7(915)-356-08-98",
                Email = "samofalov@gmail.com",
                Birthday = DateTime.Now
            };
            transport.AddTour(new Tour()
            {
                Id = 1,
                NameTour = "Gelengik",
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now.AddDays(1),
                MaxTourists = 5,
                Price = 30000
            });

            Tourist savedTourist = sut.Create(transport);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Samofalov A.", savedTourist.FullName);
            Assert.Equal("Gelengik", savedTourist.Tours.First().NameTour);
        }

        /// <summary>
        /// Тест на успешное удаление туриста.
        /// </summary>
        [Fact]
        public void Delete_ValidData_Success()
        {
            IRepository<Tourist> sut = GetInMemoryTouristRepository();
            sut.Create(GenerateTourist(1, "Samofalov", "Anton", "7986547521", "anton@anton.ru", DateTime.Now));

            Tourist deleteTourist = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.FirstName == deleteTourist.FirstName));
        }

        /// <summary>
        /// Тест на успешное обновление туриста.
        /// </summary>
        [Fact]
        public void Update_ValidData_Success()
        {
            IRepository<Tourist> sut = GetInMemoryTouristRepository();
            sut.Create(GenerateTourist(1, "Samofalov", "Anton", "7986547521", "anton@anton.ru", DateTime.Now));

            Tourist updateTourist = sut.GetById(1);
            updateTourist.FirstName = "Alexander";
            sut.Update(updateTourist);

            Assert.Equal("Alexander", sut.GetById(1).FirstName);
        }

        /// <summary>
        /// Получение объекта репозитория туриста.
        /// </summary>
        /// <returns>объект репозитория туриста.</returns>
        private IRepository<Tourist> GetInMemoryTouristRepository()
        {
            DbContextOptions<TourContext> options;
            var builder = new DbContextOptionsBuilder<TourContext>();
            builder.UseInMemoryDatabase(databaseName: "TouristDb");
            options = builder.Options;
            TourContext tourContext = new TourContext(options);
            tourContext.Database.EnsureDeleted();
            tourContext.Database.EnsureCreated();
            return new TouristRepository(tourContext);
        }

        /// <summary>
        /// Создание сущности туриста.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="phone">Телефон.</param>
        /// <param name="email">Почта.</param>
        /// <param name="birthday">Дата рождения.</param>
        /// <returns>Турист.</returns>
        private Tourist GenerateTourist(int id, string lastName, string firstName, string phone, string email, DateTime birthday)
        {
            return new Tourist()
            {
                Id = id,
                LastName = lastName,
                FirstName = firstName,
                Phone = phone,
                Email = email,
                Birthday = birthday
            };
        }
    }
}

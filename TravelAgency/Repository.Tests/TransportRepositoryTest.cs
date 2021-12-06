// <copyright file="TransportRepositoryTest.cs" company="Самофалов А.П.">
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
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Класс для тестов репозитория транспорта.
    /// </summary>
    public class TransportRepositoryTest
    {
        /// <summary>
        /// Тест на добавление транспорта без тура.
        /// </summary>
        [Fact]
        public void Add_WhenHaveNoTour()
        {
            IRepository<Transport> sut = GetInMemoryTransportRepository();
            Transport transport = new Transport()
            {
                Id = sut.GetAll().Count() + 1,
                NameTransport = "Bus MIIT"
            };

            Transport savedTransport = sut.Create(transport);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Bus MIIT", savedTransport.NameTransport);
            Assert.NotNull(savedTransport.Tours);
        }

        /// <summary>
        /// Тест на добавление транспорта с тура.
        /// </summary>
        [Fact]
        public void Add_WhenHaveTour()
        {
            IRepository<Transport> sut = GetInMemoryTransportRepository();
            Transport transport = new Transport()
            {
                Id = sut.GetAll().Count() + 1,
                NameTransport = "Bus MIIT"
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

            Transport savedTransport = sut.Create(transport);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Bus MIIT", savedTransport.NameTransport);
            Assert.Equal("Gelengik", savedTransport.Tours.First().NameTour);
        }

        /// <summary>
        /// Тест на успешное удаление транспорта.
        /// </summary>
        [Fact]
        public void Delete_ValidData_Success()
        {
            IRepository<Transport> sut = GetInMemoryTransportRepository();
            sut.Create(GenereateTransport(1, "MIIT"));

            Transport deleteTransport = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.NameTransport == deleteTransport.NameTransport));
        }

        /// <summary>
        /// Тест на успешное обновление транспорта.
        /// </summary>
        [Fact]
        public void Update_ValidData_Success()
        {
            IRepository<Transport> sut = GetInMemoryTransportRepository();
            sut.Create(GenereateTransport(1, "MIIT"));

            Transport updateTransport = sut.GetById(1);
            updateTransport.NameTransport = "Train MIIT";
            sut.Update(updateTransport);

            Assert.Equal("Train MIIT", sut.GetById(1).NameTransport);
        }

        /// <summary>
        /// Получение объекта репозитория транспорта.
        /// </summary>
        /// <returns>объект репозитория транспорта.</returns>
        private IRepository<Transport> GetInMemoryTransportRepository()
        {
            DbContextOptions<TourContext> options;
            var builder = new DbContextOptionsBuilder<TourContext>();
            builder.UseInMemoryDatabase(databaseName: "TransportDb");
            options = builder.Options;
            TourContext tourContext = new TourContext(options);
            tourContext.Database.EnsureDeleted();
            tourContext.Database.EnsureCreated();
            return new TransportRepository(tourContext);
        }

        /// <summary>
        /// Создание сушности транспорт.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="nameTransport">Название транспорта.</param>
        /// <returns>Транспорт.</returns>
        private Transport GenereateTransport(int id, string nameTransport)
        {
            return new Transport
            {
                Id = id,
                NameTransport = nameTransport
            };
        }
    }
}

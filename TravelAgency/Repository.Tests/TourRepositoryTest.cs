// <copyright file="TourRepositoryTest.cs" company="Самофалов А.П.">
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
    using System;

    /// <summary>
    /// Класс для тестов репозитория тура.
    /// </summary>
    public class TourRepositoryTest
    {
        /// <summary>
        /// Тест на добавление тура с сотрудником.
        /// </summary>
        [Fact]
        public void Add_SingleTour_WithEmployee()
        {
            IRepository<Tour> sut = GetInMemoryTourRepository();

            var employee = GenerateEmployee(2, "Ivan", "Ivanov", "79151234567", "noreply@gmail.com", DateTime.Now, "CEO");
            var tour = GenerateTour(1, "Test", DateTime.Now, DateTime.Now.AddDays(1), 0m, 0, employee);
            
            Tour savedTour = sut.Create(tour);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Test", savedTour.NameTour);
            Assert.Equal("Ivanov I.", savedTour.Employee.FullName);
            Assert.Equal(0, savedTour.Hotels.Count);
            Assert.Equal(0, savedTour.Tourists.Count);
            Assert.Equal(0, savedTour.Transports.Count);
        }

        /// <summary>
        /// Тест на добавление тура с отелем.
        /// </summary>
        [Fact]
        public void Add_WhenHaveSingleHotel()
        {
            IRepository<Tour> sut = GetInMemoryTourRepository();

            var employee = GenerateEmployee(2, "Ivan", "Ivanov", "79151234567", "noreply@gmail.com", DateTime.Now, "CEO");
            var hotel = new Hotel()
            {
                Id = 2,
                NameHotel = "Radisson",
                ClassHotel = 5
            };
            var tour = GenerateTour(1, "Test", DateTime.Now, DateTime.Now.AddDays(1), 0m, 0, employee);
            tour.Hotels = new HashSet<Hotel> { hotel };

            Tour savedTour = sut.Create(tour);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Test", savedTour.NameTour);
            Assert.Equal(1, savedTour.Hotels.Count);
            Assert.Equal("Radisson", savedTour.Hotels.ToList()[0].NameHotel);
        }

        /// <summary>
        /// Тест на добавление тура с туристом.
        /// </summary>
        [Fact]
        public void Add_WhenHaveSingleTourist()
        {
            IRepository<Tour> sut = GetInMemoryTourRepository();

            var employee = GenerateEmployee(2, "Ivan", "Ivanov", "79151234567", "noreply@gmail.com", DateTime.Now, "CEO");
            var tour = GenerateTour(1, "Test", DateTime.Now, DateTime.Now.AddDays(1), 0m, 0, employee);
            var tourist = new Tourist()
            {
                Id = 2,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Birthday = DateTime.Now,
                Email = "noreply@gmail.com",
                Phone = "79151234567",
            };
            tour.Tourists = new HashSet<Tourist> { tourist };

            Tour savedTour = sut.Create(tour);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Test", savedTour.NameTour);
            Assert.Equal(1, savedTour.Tourists.Count);
            Assert.Equal("Ivanov I.", savedTour.Tourists.ToList()[0].FullName);
        }

        /// <summary>
        /// Тест на добавление тура с транспортом.
        /// </summary>
        [Fact]
        public void Add_WhenHaveSingleTransport()
        {
            IRepository<Tour> sut = GetInMemoryTourRepository();
            var employee = GenerateEmployee(2, "Ivan", "Ivanov", "79151234567", "noreply@gmail.com", DateTime.Now, "CEO");
            var tour = GenerateTour(1, "Test", DateTime.Now, DateTime.Now.AddDays(1), 0m, 0, employee);
            var transport = new Transport()
            {
                Id = 2,
                NameTransport = "Bus"
            };
            tour.Transports = new HashSet<Transport> { transport };

            Tour savedTour = sut.Create(tour);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Test", savedTour.NameTour);
            Assert.Equal(1, savedTour.Transports.Count);
            Assert.Equal("Bus", savedTour.Transports.ToList()[0].NameTransport);
        }

        /// <summary>
        /// Тест на успешное удаление тура с сотрудником.
        /// </summary>
        [Fact]
        public void Delete_Single_ValidData_Success()
        {
            IRepository<Tour> sut = GetInMemoryTourRepository();
            var employee = GenerateEmployee(2, "Ivan", "Ivanov", "79151234567", "noreply@gmail.com", DateTime.Now, "CEO");
            var tour = GenerateTour(1, "Test", DateTime.Now, DateTime.Now.AddDays(1), 0m, 0, employee);

            Tour savedTour = sut.Create(tour);

            Tour deleteTour = sut.GetById(1);
            sut.Delete(1);

            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(t => t.NameTour == deleteTour.NameTour));
        }

        /// <summary>
        /// Тест на успешное удаление тура с отелем.
        /// </summary>
        [Fact]
        public void Delete_WhenHaveHotel_ValidData_Success()
        {
            IRepository<Tour> sut = GetInMemoryTourRepository();
            var employee = GenerateEmployee(2, "Ivan", "Ivanov", "79151234567", "noreply@gmail.com", DateTime.Now, "CEO");
            var tour = GenerateTour(1, "Test", DateTime.Now, DateTime.Now.AddDays(1), 0m, 0, employee);

            var hotel = new Hotel()
            {
                Id = 2,
                NameHotel = "Radisson",
                ClassHotel = 5
            };
            tour.Hotels = new HashSet<Hotel> { hotel };

            Tour savedTour = sut.Create(tour);

            Tour deleteTour = sut.GetById(1);
            sut.Delete(1);

            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(t => t.NameTour == deleteTour.NameTour));
            Assert.Equal(1, tour.Hotels.Count);
        }

        /// <summary>
        /// Тест на успешное обновление тура.
        /// </summary>
        [Fact]
        public void Update_Single_ValidData_Success()
        {
            IRepository<Tour> sut = GetInMemoryTourRepository();
            var employee = GenerateEmployee(2, "Ivan", "Ivanov", "79151234567", "noreply@gmail.com", DateTime.Now, "CEO");
            var tour = GenerateTour(1, "Test", DateTime.Now, DateTime.Now.AddDays(1), 0m, 0, employee);

            Tour savedTour = sut.Create(tour);

            Tour updateTour = sut.GetById(1);
            updateTour.NameTour = "TestTour";
            sut.Update(updateTour);

            Assert.Equal("TestTour", sut.GetById(1).NameTour);
        }

        /// <summary>
        /// Получение объекта репозитория тура.
        /// </summary>
        /// <returns>объект репозитория тура.</returns>
        private IRepository<Tour> GetInMemoryTourRepository()
        {
            DbContextOptions<TourContext> options;
            var builder = new DbContextOptionsBuilder<TourContext>();
            builder.UseInMemoryDatabase(databaseName: "TourDb");
            options = builder.Options;
            TourContext tourContext = new TourContext(options);
            tourContext.Database.EnsureDeleted();
            tourContext.Database.EnsureCreated();
            return new TourRepository(tourContext);
        }

        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="nameTour">Название тура.</param>
        /// <param name="dateStart">Дата начала.</param>
        /// <param name="dateEnd">Дата окончания.</param>
        /// <param name="price">Цена.</param>
        /// <param name="maxTourists">Максимальное количество туристов.</param>
        /// <param name="employee">Ссылка на объект сотрудника.</param>
        /// <returns>Тур.</returns>
        private Tour GenerateTour(int id, string nameTour, DateTime dateStart, DateTime dateEnd, decimal price, int maxTourists, Employee employee)
        {
            return new Tour()
            {
                Id = id,
                NameTour = nameTour,
                DateStart = dateStart,
                DateEnd = dateEnd,
                Price = price,
                MaxTourists = maxTourists,
                Employee = employee
            };
        }

        /// <summary>
        /// Создание сущности сотрудника.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="phone">Телефон.</param>
        /// <param name="email">Почта.</param>
        /// <param name="birthday">Дата рождения.</param>
        /// <param name="position">Должность.</param>
        /// <returns>Сотрудник.</returns>
        private Employee GenerateEmployee(int id, string firstName, string lastName, string phone, string email, DateTime birthday, string position) =>
            new(id, lastName, firstName, phone, email, birthday, position);
    }
}

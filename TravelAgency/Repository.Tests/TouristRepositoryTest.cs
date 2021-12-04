﻿namespace Repository.Tests
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
    /// 
    /// </summary>
    public class TouristRepositoryTest
    {
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

            Assert.Equal(2, sut.GetAll().Count());
            Assert.Equal("Samofalov A.", savedTourist.FullName);
            Assert.NotNull(savedTourist.Tours);
        }
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

            Assert.Equal(2, sut.GetAll().Count());
            Assert.Equal("Samofalov A.", savedTourist.FullName);
            Assert.Equal("Gelengik", savedTourist.Tours.First().NameTour);
        }

        [Fact]
        public void Delete_ValidData_Success()
        {
            IRepository<Tourist> sut = GetInMemoryTouristRepository();

            Tourist deleteTourist = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.FirstName == deleteTourist.FirstName));
        }

        [Fact]
        public void Update_ValidData_Success()
        {
            IRepository<Tourist> sut = GetInMemoryTouristRepository();

            Tourist updateTourist = sut.GetById(1);
            updateTourist.FirstName = "Alexander";
            sut.Update(updateTourist);

            Assert.Equal("Alexander", sut.GetById(1).FirstName);
        }

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
    }
}
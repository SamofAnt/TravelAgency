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
    /// Тест
    /// </summary>
    public class TransportRepositoryTest
    {
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

            Assert.Equal(2, sut.GetAll().Count());
            Assert.Equal("Bus MIIT", savedTransport.NameTransport);
            Assert.NotNull(savedTransport.Tours);
        }
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

            Assert.Equal(2, sut.GetAll().Count());
            Assert.Equal("Bus MIIT", savedTransport.NameTransport);
            Assert.Equal("Gelengik", savedTransport.Tours.First().NameTour);
        }

        [Fact]
        public void Delete_ValidData_Success()
        {
            IRepository<Transport> sut = GetInMemoryTransportRepository();

            Transport deleteTransport = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.NameTransport == deleteTransport.NameTransport));
        }

        [Fact]
        public void Update_ValidData_Success()
        {
            IRepository<Transport> sut = GetInMemoryTransportRepository();

            Transport updateTransport = sut.GetById(1);
            updateTransport.NameTransport = "Train MIIT";
            sut.Update(updateTransport);

            Assert.Equal("Train MIIT", sut.GetById(1).NameTransport);
        }

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
    }
}

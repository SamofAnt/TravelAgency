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
    /// 
    /// </summary>
    public class AttractionRepositoryTest
    {
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
        
        private Attraction GenerateAttraction(int id, string nameAttraction, City city) => new()
        {
            Id = 1,
            NameAttraction = nameAttraction,
            City = city
        };
        private City GenerateCity(int id, string nameCity, Country country) => new()
        {
            Id = id,
            NameCity = nameCity,
            Country = country
        };
        private Country GenerateCountry(int id, string nameCounty) => new(id, nameCounty);
    }
}

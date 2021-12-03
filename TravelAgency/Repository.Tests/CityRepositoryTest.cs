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
    public class CityRepositoryTest
    {

        [Fact]
        public void Add_WhenHaveNoCountryAndAttraction()
        {
            IRepository<City> sut = GetInMemoryCityRepository();
            City city= new City(sut.GetAll().Count() + 1, "Paris");

            City savedCity = sut.Create(city);

            Assert.Equal(2, sut.GetAll().Count());
            Assert.Equal("Paris", savedCity.NameCity);
            Assert.Equal(0, savedCity.Attractions.Count);
            Assert.Null(savedCity.Country);
        }
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

            Assert.Equal(2, sut.GetAll().Count());
            Assert.Equal("London", savedCity.NameCity);
            Assert.Equal("England", savedCity.Country.NameCountry);
            Assert.Equal(1, savedCity.Country.Cities.Count);
        }

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

            Assert.Equal(2, sut.GetAll().Count());
            Assert.Equal("London", savedCity.NameCity);
            Assert.Equal(1, savedCity.Attractions.Count);
            Assert.Equal("Kremlin", savedCity.Attractions.ToList()[0].NameAttraction);
        }

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

        [Fact]
        public void Delete_WhenNoCitiesAndHotels()
        {
            IRepository<City> sut = GetInMemoryCityRepository();

            City deleteCity = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.NameCity == deleteCity.NameCity));
        }

        [Fact]
        public void Update_WhenNoCitiesAndHotels()
        {
            IRepository<City> sut = GetInMemoryCityRepository();

            City updateCity = sut.GetById(1);
            updateCity.NameCity = "London";
            sut.Update(updateCity);

            Assert.Equal("London", sut.GetById(1).NameCity);
        }
    }
}

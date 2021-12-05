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
    /// 
    /// </summary>
    public class HotelRepositoryTest
    {
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
        private Country GenerateCountry(int id, string nameCounty) => new(id, nameCounty);
    }
}

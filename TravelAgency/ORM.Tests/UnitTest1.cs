using Domain;
using Microsoft.EntityFrameworkCore;
using ORM.Repositories;
using System;
using Xunit;

namespace ORM.Tests
{
    public class TourContextTest
    {
        public TourContextTest()
        {
            InitContext();
        }
        private TourContext _tourContext;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TourContext>()
                .UseInMemoryDatabase(databaseName:"db");
            var context = new TourContext(builder.Options);
            var country = new Country("USA");
            context.Country.Add(country);
            int chanhed = context.SaveChanges();
            _tourContext = context;
        }
        [Fact]
        public void Test1()
        {
            string expectedValue = "USA";
            var rep = new CountryRepository(_tourContext);
            Country result = rep.GetById(1);
            Assert.Equal(expectedValue, result.NameCountry);
        }
    }
}

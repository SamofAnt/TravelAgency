using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Domain;
using ORM.Repositories;
using TestSupport.EfHelpers;

namespace ORM.Tests
{
    public class BaseMapTests
    {
   
        [Fact]
        public void TestSqliteInMemoryOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<TourContext>();
            using var context = new TourContext(options);

            context.Database.EnsureCreated();
        }

        [Fact]
        public void TestEnsureDeletedEnsureCreatedOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<TourContext>();
            using var context = new TourContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Fact]
        public void ExampleIdentityResolutionBad()
        {
            //SETUP
            var options = SqliteInMemory
                .CreateOptions<TourContext>();
            using var context = new TourContext(options);

            context.Database.EnsureCreated();

            //ATTEMPT
            var country = context.Country.First();
            country.NameCountry = "France";

            //VERIFY
            var verifyBook = context.Country.First();
            verifyBook.NameCountry.Equals("France");
        }
       

    }
}

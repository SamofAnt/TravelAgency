// <copyright file="BaseMapTests.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace ORM.Tests
{
    using Xunit;
    using System.Linq;
    using TestSupport.EfHelpers;
    
    /// <summary>
    /// Класс для тестов маппингов.
    /// </summary>
    public class BaseMapTests
    {
        /// <summary>
        /// Тест на успешное создание базы данных.
        /// </summary>
        [Fact]
        public void TestSqliteInMemoryOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<TourContext>();
            using var context = new TourContext(options);

            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Тест на успешное удаление и создание базы данных.
        /// </summary>
        [Fact]
        public void TestEnsureDeletedEnsureCreatedOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<TourContext>();
            using var context = new TourContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Тест на успешное обращение к базе данных.
        /// </summary>
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

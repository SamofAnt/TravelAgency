// <copyright file="BaseMapTests.cs" company="��������� �.�.">
// Copyright (c) ��������� �.�.. All rights reserved.
// </copyright>

namespace ORM.Tests
{
    using Xunit;
    using System.Linq;
    using TestSupport.EfHelpers;
    
    /// <summary>
    /// ����� ��� ������ ���������.
    /// </summary>
    public class BaseMapTests
    {
        /// <summary>
        /// ���� �� �������� �������� ���� ������.
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
        /// ���� �� �������� �������� � �������� ���� ������.
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
        /// ���� �� �������� ��������� � ���� ������.
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

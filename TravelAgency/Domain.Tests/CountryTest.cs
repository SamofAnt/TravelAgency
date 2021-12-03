using System;
namespace Domain.Tests
{
    using NUnit.Framework;

    [TestFixture]
    class CountryTest
    {
        private Country country;

        [SetUp]
        public void Setup() => this.country = new Country(1, "France");

        [Test]
        public void ToString_ValidData_Success()
        {

            //act
            var result = this.country.ToString();
            //assert
            Assert.AreEqual("France", result);
        }

        [Test]
        public void Ctor_WrongData_EmptyNameCountry_Fail() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Country(1, ""));

        [Test]
        public void AddHotelToCountry_ValidData_Success()
        {

            //arrange
            var hotel = new Hotel(1, "Novotel", 5);
            //act
            var result = this.country.AddHotel(hotel);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddCityToCountry_ValidData_Success()
        {
            //arrange
            var city = new City(1, "Paris");
            //act
            var result = this.country.AddCity(city);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddHotelToCountry_WrongParameter_Fail()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _ = this.country.AddHotel(null));
            Assert.That(ex.ParamName, Is.EqualTo("hotel"));
        }

        [Test]
        public void AddCityToCountry_WrongParameter_Fail()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _ = this.country.AddCity(null));
            Assert.That(ex.ParamName, Is.EqualTo("city"));
        }
    }
}

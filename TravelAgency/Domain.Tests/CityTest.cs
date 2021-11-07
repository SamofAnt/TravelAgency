
namespace Domain.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    class CityTest
    {

        [Test]
        public void ToString_ValidData_Success()
        {
            //arrange
            var city = new City(1, "Paris");
            //act
            var result = city.ToString();
            //assert
            Assert.AreEqual("Paris", result);
        }

        [Test]
        public void Ctor_WrongData_EmptyNameCity_Fail() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new City(1, ""));

    }
}

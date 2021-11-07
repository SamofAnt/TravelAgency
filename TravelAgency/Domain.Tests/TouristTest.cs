using System;

namespace Domain.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class TouristTest
    {
        private Tourist tourist;

        [SetUp]
        public void Setup()
        {
            this.tourist = new Tourist(1, "Самофалов", "Антон");
        }
        [Test]
        public void ToString_ValidData_Success()
        {
            //arrange
           
            //act
            var result = this.tourist.ToString();
            //assert
            Assert.AreEqual("Самофалов А.", result);
        }


        [Test]
        public void Ctor_WrongData_EmptyFirstName_Fail() => Assert.Throws<ArgumentOutOfRangeException>(() => _=new Tourist(1, "Самофалов", ""));

        [Test]

        public void AddTourToTourist_ValidData_Success()
        {
            var tour = new Domain.Tour(1, "Планета Тур");

            var result = this.tourist.AddTour(tour);

            Assert.AreEqual(true, result);
        }
    }
}
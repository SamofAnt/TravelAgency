namespace Domain.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class HotelTest
    {
        private Hotel hotel;
        private Tour tour;
        [SetUp]
        public void Setup()
        {
            this.hotel = new Hotel(1, "Radisson", 5);
            this.tour = new Domain.Tour( "Планета Тур", DateTime.Now, 500, DateTime.Now, 23);
        }

        [Test]
        public void ToString_ValidData_Success()
        {

            //act 
            var result = this.hotel.ToString();
            //
            Assert.AreEqual("Radisson 5", result);
        }

        [Test]
        public void Ctor_WrongData_EmptyNameHotel_Fail() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Hotel(1, "", 5));

        [Test]
        public void AddTourToHotel_ValidData_Success()
        {
            //arrange

            //act
            var result = this.hotel.AddTour(tour);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddTourToHotel_WrongParameter_Fail()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _ = this.hotel.AddTour(null));
            Assert.That(ex.ParamName, Is.EqualTo("tour"));
        }
    }
}

namespace Domain.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    class TourTest
    {
        [Test]
        public void ToString_ValidData_Success()
        {
            //arrange
            var tour = new Tour("Планета Тур", DateTime.Now, 500m, DateTime.Now, 23);
            //act
            var result = tour.ToString();
            //assert
            Assert.AreEqual($"Tour: Планета Тур\nДата начала: { DateTime.Now}\nДата окончания: { DateTime.Now}\nЦена: 500\nМаксимальное кол-во туристов: 23", result);
        }

        [Test]
        public void Ctor_WrongData_EmptyNameTour_Fail() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Tour( "",DateTime.Now, 500, DateTime.Now, 23));

    }
}

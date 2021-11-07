using System;

namespace Domain.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class TouristTest
    {
        private Tourist tourist;
        private Tour tour;

        [SetUp]
        public void Setup()
        {
            this.tourist = new Tourist(1, "Самофалов", "Антон", "+7(915)-356-08-98", "samofalov@gmail.com", DateTime.Now);
            this.tour = new Domain.Tour(1, "Планета Тур", DateTime.Now, 500, DateTime.Now, 23);

        }
        [Test]
        public void ToString_ValidData_Success()
        {
            //arrange
           
            //act
            var result = this.tourist.ToString();
            //assert
            Assert.AreEqual($"Полное имя: Самофалов А.\nEmail: samofalov@gmail.com\nДата рождения: {DateTime.Now}\nНомер телефона: +7(915)-356-08-98", result);
        }


        [Test]
        public void Ctor_WrongData_EmptyFirstName_Fail() => Assert.Throws<ArgumentOutOfRangeException>(() => _= new Tourist(1, "Самофалов", "", "+7(915)-356-08-98", "samofalov@gmail.com", DateTime.Now));

        [Test]

        public void AddTourToTourist_ValidData_Success()
        {
            //arrange
           
            //act
            var result = this.tourist.AddTour(tour);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddTourToTourist_WrongParameter_Fail()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _ = this.tourist.AddTour(null));
            Assert.That(ex.ParamName, Is.EqualTo("tour"));
        }
    }
}
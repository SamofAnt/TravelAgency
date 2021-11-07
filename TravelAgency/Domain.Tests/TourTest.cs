
namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    class TourTest
    {
        //[Test]
        //public void ToStringFull_ValidData_Success()
        //{
        //    //arrange
        //    var hotel = new Domain.Hotel(1, "Radisson", 5);

        //    var tourist = new Domain.Tourist(1, "Самофалов", "Антон", "+7(915)-356-08-98", "samofalov@gmail.com", DateTime.Now);
        //    var transport = new Transport(1, "Airplane");
        //    ISet<Tourist> tourists = new HashSet<Tourist>();
        //    tourists.Add(tourist);
        //    ISet<Hotel> hotels = new HashSet<Hotel>();
        //    hotels.Add(hotel);
        //    ISet<Transport> transports = new HashSet<Transport>();
        //    transports.Add(transport);
        //    var emp = new Employee(1, "Samofalov", "Anton", "+7(915)-356-08-98", "samofalov@gmail.com", DateTime.Now, "CEO");
        //    var tour = new Domain.Tour(1, "Планета Тур", DateTime.Now, 500, DateTime.Now, 23, tourists, hotels, emp, transports);
        //    //act
        //    var result = tour.ToString();
        //    //assert
        //    Assert.AreEqual($"Tour: Планета Тур\nДата начала: {DateTime.Now}\nДата окончания: {DateTime.Now}\nЦена: 500\nМаксимальное кол-во туристов: 23\n\nTourists: Полное имя: Самофалов А.\nEmail: samofalov@gmail.com\nДата рождения: 08.11.2021 0:59:54\nНомер телефона: +7(915)-356-08-98\n\nHotels: Radisson 5\n\nEmployee: Samofalov A. +7(915)-356-08-98 {DateTime.Now} samofalov@gmail.com CEO\n\nTransport: Airplane", result);
        //}

        [Test]
        public void ToString_ValidData_Success()
        {
            //arrange
            var tour = new Tour(1, "Планета Тур", DateTime.Now, 500, DateTime.Now, 23);
            //act
            var result = tour.ToString();
            //assert
            Assert.AreEqual($"Tour: Планета Тур\nДата начала: { DateTime.Now}\nДата окончания: { DateTime.Now}\nЦена: 500\nМаксимальное кол-во туристов: 23", result);
        }

        [Test]
        public void Ctor_WrongData_EmptyNameTour_Fail() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Tour(1, "",DateTime.Now, 500, DateTime.Now, 23));

    }
}

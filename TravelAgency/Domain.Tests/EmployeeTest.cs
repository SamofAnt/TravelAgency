
namespace Domain.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    class EmployeeTest
    {
        private Employee emp;

        [SetUp]
        public void Setup()
        {
            this.emp = new Employee(1, "Samofalov", "Anton", "+7(915)-356-08-98", "samofalov@gmail.com", DateTime.Now, "CEO");
            
        }

        [Test]
        public void ToString_ValidData_Success()
        {
            //arrange
            var emp = new Employee(1, "Samofalov", "Anton", "+7(915)-356-08-98", "samofalov@gmail.com",DateTime.Now, "CEO");
            //act
            var result = emp.ToString();
            //assert
            Assert.AreEqual($"Samofalov A. +7(915)-356-08-98 {DateTime.Now} samofalov@gmail.com CEO", result);
        }

        [Test]
        public void Ctor_WrongData_EmptyNameEmp_Fail() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new City(1, ""));

        [Test]
        public void AddTourToEmployee_ValidData_Success()
        {
            //arrange
            var tour = new Domain.Tour(1, "Планета Тур", DateTime.Now, 500, DateTime.Now, 23);

            //act
            var result = this.emp.AddTour(tour);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddTourToEmployee_WrongParameter_Fail()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _ = this.emp.AddTour(null));
            Assert.That(ex.ParamName, Is.EqualTo("tour"));
        }
    }
}

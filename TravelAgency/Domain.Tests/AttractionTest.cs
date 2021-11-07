using System;

namespace Domain.Tests
{
    using NUnit.Framework;

    [TestFixture]
    class AttractionTest
    {

        [Test]
        public void ToString_ValidData_Success()
        {
            //arrange
            var attraction = new Attraction(1, "The Eiffel Tower");
            //act
            var result = attraction.ToString();
            //assert
            Assert.AreEqual("The Eiffel Tower", result);
        }

        [Test]
        public void Ctor_WrongData_EmptyNameAttraction_Fail() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Attraction(1, ""));
    }
}

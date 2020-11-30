using humidicConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HumidicUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPropertyLevel()
        {
            //Arrange
            Humidity h = new Humidity();

            float Level = 50;

            //Act

            h.Level = Level;

            // Assert

            Assert.AreEqual(h.Level, Level);

        }


        [TestMethod]
        public void TestPropertyDate()
        {
            //Arrange
            Humidity h = new Humidity();

           DateTime date =  new DateTime(29 / 11 / 2020 / 10 /15);

            //Act

            h.Date = date;

            // Assert

            Assert.AreEqual(h.Date, date);

        }
    }
}

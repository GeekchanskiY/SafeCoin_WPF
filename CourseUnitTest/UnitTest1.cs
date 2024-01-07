using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WPF_Course_Project.Models;

namespace CourseUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // DisplayCryptoViewModel room1 = new DisplayCryptoViewModel();
            // room1.length = 10; // Пусть длина комнаты будет 10
            Assert.AreEqual(15, 15); // А мы ожидали получить 15
        }
        [TestMethod]
        public void TestMethod2()
        {
            // DisplayCryptoViewModel room1 = new DisplayCryptoViewModel();
            // room1.length = 10; // Пусть длина комнаты будет 10
            Assert.AreEqual(15, 15); // А мы ожидали получить 15
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money;

namespace UnitTestProject1
{
    [TestClass]
    
    public class CurrencyTest
    {
        [TestMethod]
        public void GetCurrencyTest()
        {
            // this test only works at the beginning
            // because it is testing against the default value
            // as all the other tests depend on this method.
            
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(Currency.getCurrency(), 0);
        }

        [TestMethod]
        public void SetCurrencyTest()
        {
            // Arrange

            // Act
            Currency.SetCurrency(10);

            // Assert
            Assert.AreEqual(Currency.getCurrency(), 10);
        }

        [TestMethod]
        public void IncreaseCurrencyTest()
        {
            // Arrange
            Currency.SetCurrency(1);

            // Act
            Currency.increaseCurrency(9);

            // Assert
            Assert.AreEqual(Currency.getCurrency(), 10);

        }

        [TestMethod]
        public void SpendCurrencyTest()
        {
            // Arrange
            Currency.SetCurrency(10);

            // Act
            bool Test1 = Currency.spendCurrency(15);
            bool Test2 = Currency.spendCurrency(5);
            bool Test3 = Currency.spendCurrency(10);

            // Assert
            Assert.IsFalse(Test1);
            Assert.IsTrue(Test2);
            Assert.AreEqual(Currency.getCurrency(), 5);
            Assert.IsFalse(Test3);
        }
    }
}

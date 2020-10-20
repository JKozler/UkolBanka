using System;
using UkolBanka;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        Depozitnics dep;
        Kreditnics kred;
        public UnitTest1()
        {
            dep = new Depozitnics("Unit Test", 5000.00);
            kred = new Kreditnics("Unit Test", 5000.00, 2000.00, false);
        }
        [TestMethod]
        public void Calculate_Earnings_Dep()
        {
            DateTime dt = new DateTime(2021, 5, 20);

            string result = dep.CalculateEarnings(dt);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Calculate_Spend_Kred()
        {
            DateTime dt = new DateTime(2021, 5, 20);

            string result = kred.CalculateSpendings(dt);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Take_In_Money_Kred()
        {
            double m = 2050.00;

            double result = kred.TakeInMoney(m);

            Assert.IsTrue(result == kred.ActualSpend);
        }
        [TestMethod]
        public void Min_Vklad_Dep()
        {
            double m = 5001.00;

            double result = dep.MinVklad(m);

            Assert.IsTrue(result == dep.Vklad);
        }
    }
}

using NUnit.Framework;
using System.Collections.Generic;
using System;
using PayrollApp.Model.Payroll;
using PayrollApp.Model;
using PayrollApp.ViewModel;

namespace PayrollApp.Test
{
    public class SalesmanPayTest
    {
        [SetUp]
        public void Setup()
        {
            PayTest.CreateWorkers();
        }

        [TestCase("01.01.2021", 0)]
        [TestCase("12.12.2031", 10)]
        [TestCase("12.12.2041", 20)]
        public void GetExperienceTest(string chosenDate, int exp)
        {
            DateTime date = DateTime.Parse(chosenDate);
            Worker worker = PayTest.workers[0];
            SalesmanPay SaleP = new SalesmanPay();
            PayViewModel.SetInstance(worker, SaleP);


            int result = SaleP.GetExperience(date);
            int expected = exp;


            Assert.AreEqual(expected, result);

        }

        [TestCase("12.12.2031", 55492.780)]
        [TestCase("12.12.2041", 60492.780)]
        [TestCase("12.12.2026", 52938.690)]
        public void GetSalaryTest(string chosenDate, decimal summ)
        {
            DateTime date = DateTime.Parse(chosenDate);
            Worker worker = PayTest.workers[0];
            SalesmanPay SaleP = new SalesmanPay();
            PayViewModel.SetInstance(worker, SaleP);

            var result = SaleP.GetSalary(date);
            decimal expected = summ;

            Assert.AreEqual(expected, result);
        }
    }
}

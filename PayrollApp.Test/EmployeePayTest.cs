using NUnit.Framework;
using System.Collections.Generic;
using System;
using PayrollApp.Model.Payroll;

namespace PayrollApp.Test
{
    public class EmployeePayTest
    {
        EmployeePay Emp = new EmployeePay()
        {
            EmploymentDate = DateTime.Parse("01.01.1990"),
            Rate = 20000
        };
        

        [TestCase("01.01.1990", 0)]
        [TestCase("02.02.2000", 10)]
        [TestCase("02.02.2010", 20)]
        public void GetExperienceTest(string chosenDate, int exp)
        {
            DateTime date = DateTime.Parse(chosenDate);

            int result = Emp.GetExperience(date);
            int expected = exp;
            

            Assert.AreEqual(expected, result);

        }

        [TestCase("02.02.2000", 26000)]
        [TestCase("02.02.2020", 26000)]
        [TestCase("02.02.1995", 23000)]
        public void GetSalaryTest(string chosenDate, decimal summ)
        {
            DateTime date = DateTime.Parse(chosenDate);

            var result = Emp.GetSalary(date);
            decimal expected = summ;

            Assert.AreEqual(expected, result);
        }
    }
}

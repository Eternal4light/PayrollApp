using NUnit.Framework;
using System.Collections.Generic;
using System;
using PayrollApp.Model;
using PayrollApp.ViewModel;

namespace PayrollApp.Test
{
    public class PayTest
    {
        public static List<Worker> workers = new List<Worker>();
        public static void CreateWorkers()
        {
            Worker w1 = new Salesman()
            {
                Subordinates = new List<Worker>(),
                Rate = 50000,
                EmploymentDate = DateTime.Now

            };
            Worker w2 = new Manager()
            {
                Subordinates = new List<Worker>(),
                EmploymentDate = DateTime.Now,
                Rate = 40000,
                Chief = w1
            };
            Worker w2a = new Manager()
            {
                Subordinates = new List<Worker>(),
                EmploymentDate = DateTime.Now,
                Rate = 40000,
                Chief = w1
            };
            Worker w3 = new Employee()
            {
                Subordinates = new List<Worker>(),
                EmploymentDate = DateTime.Now,
                Rate = 20000,
                Chief = w2
            };
            Worker w3a = new Employee()
            {
                Subordinates = new List<Worker>(),
                EmploymentDate = DateTime.Now,
                Rate = 20000,
                Chief = w2a
            };
            w1.Subordinates.Add(w2);
            w1.Subordinates.Add(w2a);
            w2.Subordinates.Add(w3);
            w2a.Subordinates.Add(w3a);
            workers.Add(w1);
            workers.Add(w2);
            workers.Add(w2a);
            workers.Add(w3);
            workers.Add(w3a);
        }
        [SetUp]
        public void Setup()
        {
            CreateWorkers();
        }

        [TestCase(0, 50360.6)]
        [TestCase(1, 40100)]
        [TestCase(2, 40100)]
        [TestCase(3, 20000)]
        [TestCase(4, 20000)]
        public void CalculateSalaryTest(int n, decimal summ)
        {
            Worker w1 = workers[n];
            var vm = new PayViewModel();
            DateTime date = DateTime.Now;

            var result = vm.CalculateSalary(w1, date);
            decimal expected = summ;

            NUnit.Framework.Assert.AreEqual(expected, result);
        }
    }
}
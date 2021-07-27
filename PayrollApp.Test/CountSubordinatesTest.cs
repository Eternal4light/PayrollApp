using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PayrollApp.Model;
using PayrollApp.ViewModel;

namespace PayrollApp.Test
{
    public class CountSubordinatesTest
    {
        [SetUp]
        public void Setup()
        {
            PayTest.CreateWorkers();
        }

        [TestCase(0, 2)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        public void GetImmediateSubsTest(int n, int exp)
        {
            Worker worker = PayTest.workers[n];
            var vm = new CountSubordinates();

            int res = vm.GetImmediateSubs(worker).Count;

            Assert.AreEqual(res, exp);  
        }

        [TestCase(0,2)]
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        public void GetSecondarySubs(int n, int exp)
        {
            Worker worker = PayTest.workers[n];
            var vm = new CountSubordinates();

            int res = vm.GetSecondarySubs(worker).Count;

            Assert.AreEqual(res, exp);
        }

        [TestCase(0, 4)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        public void GetAllSubsTest(int n, int exp)
        {
            Worker worker = PayTest.workers[n];
            var vm = new CountSubordinates();

            int res = vm.GetAllSubs(worker).Count;

            Assert.AreEqual(res, exp);
        }
    }
}

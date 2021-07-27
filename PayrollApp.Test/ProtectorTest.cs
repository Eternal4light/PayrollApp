using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PayrollApp.Utility;

namespace PayrollApp.Test
{
    public class ProtectorTest
    {
        [TestCase("Fruit", "Fruit", true)]
        [TestCase("Fruit", "fruit", false)]
        public void GetSafePasswordTest(string password, string secPass, bool exp)
        {
           password = Protector.GetSafePassword(password);
            secPass = Protector.GetSafePassword(secPass);
            bool res = (password == secPass);

            Assert.AreEqual(res, exp);
        }
    }
}

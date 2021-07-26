using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PayrollApp.Model;

namespace PayrollApp.Test
{
    public class ApplicationContextTest
    {
        [Test]
        public void GetTestContext()
        {
            ApplicationContext db = new ApplicationContext();

            Assert.NotNull(db.Workers);
            Assert.NotNull(db.Authorizers);
        }
        
        

    }
}

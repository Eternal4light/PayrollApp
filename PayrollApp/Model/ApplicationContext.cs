using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PayrollApp.Model.Entity;

namespace PayrollApp.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Authorizer> Authorizers { get; set; }
        public ApplicationContext() : base("DefaultConnection") { }
    }
}

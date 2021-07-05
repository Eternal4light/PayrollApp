using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Model
{
    public abstract class Worker
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Position { get; set; }
        public Decimal Rate { get; set; }

        //for Chief
        public virtual List<Worker> Subordinates{ get; set; }

        //for Subs
        public Guid ChiefId { get; set; }   //key
        public virtual Worker Chief { get; set; }
    }
}

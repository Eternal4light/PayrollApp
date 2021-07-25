using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Model.Entity
{
    public class Authorizer
    {
        [Key]
        public Guid WorkerId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

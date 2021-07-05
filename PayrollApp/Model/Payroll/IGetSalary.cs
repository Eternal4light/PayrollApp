using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Model.Payroll
{
    public interface IGetSalary
    {
        decimal GetSalary(DateTime chosenDate);
    }
}

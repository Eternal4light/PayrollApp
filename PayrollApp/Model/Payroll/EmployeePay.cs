using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Model.Payroll
{
    public class EmployeePay :Employee, IGetSalary, IGetExperience
    {
        public int Experience;
        public int GetExperience(DateTime chosenDate)
        {
            Experience = (int)Math.Floor(((chosenDate - EmploymentDate).TotalDays) / 365.25);
            if (Experience < 0) { Experience = 0; }
            return Experience;
        }
        public decimal GetSalary(DateTime chosenDate)
        {
            GetExperience(chosenDate);
            decimal extraProfitKoef = Experience * 0.03M;
            if(extraProfitKoef > 0.3M) { extraProfitKoef = 0.3M; }
            decimal extraProfit = extraProfitKoef * Rate;
            decimal salary = Rate + extraProfit;

            return salary;
        }
        
    }
}

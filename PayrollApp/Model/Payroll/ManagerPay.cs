using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Model.Payroll
{
    public class ManagerPay : Manager, IGetSalary, IGetExperience
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
            decimal extraProfitKoef = Experience * 0.05M;
            if (extraProfitKoef > 0.4M) { extraProfitKoef = 0.4M; }
            decimal extraProfit = extraProfitKoef * Rate;

            decimal summSubSalary = 0;
            var vm = new ViewModel.PayViewModel();
            try
            {

                if(Subordinates != null & Subordinates.Count > 0)
                {
                    foreach (var el in Subordinates)
                    {
                     summSubSalary += vm.CalculateSalary(el, chosenDate);
                    }
                }
            }
            catch { }
            decimal subordinateProfit = summSubSalary * 0.005M;
            return Rate + extraProfit + subordinateProfit;
        }
    }
}

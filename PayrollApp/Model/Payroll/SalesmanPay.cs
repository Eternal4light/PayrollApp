using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Model.Payroll
{
    public class SalesmanPay : Salesman, IGetExperience, IGetSalary
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
            decimal extraProfitKoef = Experience * 0.01M;
            if (extraProfitKoef > 0.35M) { extraProfitKoef = 0.35M; }
            decimal extraProfit = extraProfitKoef * Rate;

            decimal summSubSalary = 0;
            var vm = new ViewModel.PayViewModel();
            try
            {
                foreach (var el in Subordinates)
                {
                        summSubSalary += vm.CalculateSalary(el, chosenDate);
                        summSubSalary = CicleCalc(el, summSubSalary, vm, chosenDate);
                }
                
            }
            catch { }
            decimal subordinateProfit = summSubSalary * 0.003M;
            return Rate + extraProfit + subordinateProfit;
        }
        public decimal CicleCalc (Worker el, decimal summSubSalary, ViewModel.PayViewModel vm, DateTime chosenDate)
        {
            
            if (el.Subordinates != null & el.Subordinates.Count > 0)
            {
                
                foreach (var subEl in el.Subordinates)
                {
                       summSubSalary += vm.CalculateSalary(subEl, chosenDate);
                       summSubSalary = CicleCalc(subEl, summSubSalary, vm, chosenDate);  
                }
            }
            return summSubSalary;
        }
    }
}

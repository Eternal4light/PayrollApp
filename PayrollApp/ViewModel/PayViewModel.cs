using PayrollApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.ViewModel
{
    public class PayViewModel
    {
        public decimal CalculateSalary(Worker worker, DateTime date)
        {
                string type = worker.GetType().Name;
            
                if (type.Contains("Salesman"))
                {
                    var workerPayModel = new Model.Payroll.SalesmanPay();
                    SetInstance(worker, workerPayModel);
                    return workerPayModel.GetSalary(date);
                }
                else if (type.Contains("Manager"))
                {
                    var workerPayModel = new Model.Payroll.ManagerPay();
                    SetInstance(worker, workerPayModel);
                    return workerPayModel.GetSalary(date);
                }
                else if (type.Contains("Employee"))
                {
                    var workerPayModel = new Model.Payroll.EmployeePay();
                    SetInstance(worker, workerPayModel);
                    return workerPayModel.GetSalary(date);
                }
                else
                {
                    var workerPayModel = new Model.Payroll.EmployeePay();
                    SetInstance(worker, workerPayModel);
                    return workerPayModel.GetSalary(date);
                }
        }
        private static void SetInstance(Worker worker, Worker workerPayModel)
        {
            workerPayModel.Rate = worker.Rate;
            workerPayModel.EmploymentDate = worker.EmploymentDate;

            if (worker.Subordinates != null & worker.Subordinates.Count > 0)
            {
                workerPayModel.Subordinates = worker.Subordinates;
            }
        }
    }
}

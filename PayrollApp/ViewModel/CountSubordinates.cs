using PayrollApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PayrollApp.ViewModel
{
    public class CountSubordinates
    {
        public List<Worker> GetImmediateSubs(Worker worker)
        {
            var immediateSubs = new List<Worker>();
            if (worker.Subordinates.Count > 0)
            {
                foreach (var sub in worker.Subordinates)
                {
                    immediateSubs.Add(sub);
                }
            }
            return immediateSubs;
        }
        public List<Worker> GetSecondarySubs(Worker worker)
        {
            List<Worker> secondarySubs = new List<Worker>();
            List<Worker> res = new List<Worker>();

            var immediateSubs = GetImmediateSubs(worker);
            if (immediateSubs.Count > 0)
            {
                foreach (var iSub in immediateSubs)
                {
                    secondarySubs.AddRange(GetImmediateSubs(iSub));
                    secondarySubs.AddRange(GetSecondarySubs(iSub));
                }
            }
            return secondarySubs;
        }
        public List<Worker> GetAllSubs(Worker worker)
        {
            var result = new List<Worker>();
            result.AddRange(GetImmediateSubs(worker));
            result.AddRange(GetSecondarySubs(worker));
            return result;
        }
    }
}

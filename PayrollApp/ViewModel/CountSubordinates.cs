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
            List<List<Worker>> secondaryGroups = new List<List<Worker>>();
            List<Worker> res = new List<Worker>();

            var immediateSubs = GetImmediateSubs(worker);
            if (immediateSubs.Count > 0)
            {
                foreach (var iSub in immediateSubs)
                {
                   secondaryGroups.Add(GetImmediateSubs(iSub));
                }

                foreach( List<Worker> el in secondaryGroups)
                {
                    res.AddRange(el);
                }

            }
        }
    }
}

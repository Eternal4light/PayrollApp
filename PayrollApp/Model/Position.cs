using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Model
{
    public class Position
    {
        public List<string> CodeNames;
        
        public Position(string a, string b, string c)
        {
            CodeNames = new List<string> {a,b,c};
        }
    }

    public class Chiefs
    {
        public List<Worker> AllChiefs = new List<Worker>();
        public Chiefs(List<Worker> wList)
        {
            
            foreach ( var el in wList)
            {
                if (el is Employee == false)
                {
                    AllChiefs.Add(el);
                }
            }
            
        }
        public Chiefs(Worker worker)
        {
            if (worker is Employee == false)
            {
                AllChiefs.Add(worker);
            }
        }
    }
}

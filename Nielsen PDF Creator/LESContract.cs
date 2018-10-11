using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nielsen_PDF_Creator
{
    class LESContract : Contract
    {
        List<String> workOrders;
        public LESContract() : base()
        {
            workOrders = new List<string>();
        }

        public void addWO(String wo)
        {
            workOrders.Add(wo);
        }

        public bool removeWO(String wo)
        {
            return workOrders.Remove(wo);
        }

        public int woCount()
        {
            return workOrders.Count;
        }

        public String woAt(int index)
        {
            if (index < 0 || index > woCount())
            {
                return "Error, index out of range";
            }
            return workOrders.ElementAt(index);
        }
    }
}

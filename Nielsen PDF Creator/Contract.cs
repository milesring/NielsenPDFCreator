using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nielsen_PDF_Creator
{
    public class Contract
    {
        public String contractNum;
        public String contractName;
        private List<String> labels;
        private List<String> contractors;

        public Contract()
        {
            contractNum = "";
            contractName = "";
            labels = new List<String>();
            contractors = new List<String>();
        }

        public void addPDF(String label)
        {
            labels.Add(label);
        }

        public bool removePDF(String label)
        {
            return labels.Remove(label);
        }

        public int labelCount()
        {
            return labels.Count;
        }

        public String labelAt(int index)
        {
            if (index < 0 || index > labelCount())
            {
                return "Error, index out of range";
            }
            return labels.ElementAt(index);
        }

        public void addContractor(String contractor)
        {
            contractors.Add(contractor);
        }

        public bool removeContractor(String contractor)
        {
            return contractors.Remove(contractor);
        }

        public int contractorCount()
        {
            return contractors.Count;
        }

        public String contractorAt(int index)
        {
            if (index < 0 || index > contractorCount())
            {
                return "Error, index out of range";
            }
            return contractors.ElementAt(index);
        }
    }
}

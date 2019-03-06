using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nielsen_PDF_Creator
{
    public partial class Form2 : Form
    {

        private List<String> buildQueueRef;

        public Form2(ref List<String> buildQueue)
        {
            InitializeComponent();
            buildQueueRef = buildQueue;
            FillBuildQueue();
        }

        private void FillBuildQueue()
        {
            clb_BuildQueue.Items.Clear();
            foreach  (String pdf in buildQueueRef)
            {
                clb_BuildQueue.Items.Add(pdf);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            foreach (String item in clb_BuildQueue.CheckedItems)
            {
                buildQueueRef.Remove(item);
            }

            FillBuildQueue();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

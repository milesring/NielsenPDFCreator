using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nielsen_PDF_Creator
{
    public partial class QueueForm : Form
    {
        private QueueItem tempItem;
        private List<QueueItem> buildQueueRef;

        public QueueForm(ref List<QueueItem> buildQueue)
        {
            InitializeComponent();
            buildQueueRef = buildQueue;
            FillBuildQueue();
        }

        private void FillBuildQueue()
        {
            clb_BuildQueue.Items.Clear();
            foreach  (QueueItem pdf in buildQueueRef)
            {
                clb_BuildQueue.Items.Add(pdf);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            foreach (QueueItem item in clb_BuildQueue.CheckedItems)
            {
                buildQueueRef.Remove(item);
            }

            FillBuildQueue();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            switch (clb_BuildQueue.CheckedItems.Count)
            {
                case 0:
                    MessageBox.Show("Please select an item to edit");
                    break;
                case 1:
                    tempItem = (QueueItem)clb_BuildQueue.CheckedItems[0];
                    CommandEditForm commandEditForm = new CommandEditForm(tempItem.getCommand(), (QueueForm)this.TopLevelControl);
                    commandEditForm.Show();
                    break;
                default:
                    MessageBox.Show("Please check only 1 item to edit");
                    break;
            }
        }


        public void UpdateCommand(String c){
            tempItem.setCommand(c);
        }
    }
}

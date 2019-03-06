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
    public partial class CommandEditForm : Form
    {
        private String command;
        private String originalCommand;
        private QueueForm queueForm;

        public CommandEditForm(String c, QueueForm qf)
        {
            command = c;
            originalCommand = c;
            queueForm = qf;
            InitializeComponent();
        }

        private void CommandEditForm_Load(object sender, EventArgs e)
        {
            tb_commandedit.Text = command;
        }

        private void btn_revert_Click(object sender, EventArgs e)
        {
            RevertCommand();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            RevertCommand();
            this.Close();
        }

        private void RevertCommand()
        {
            tb_commandedit.Text = originalCommand;
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Update command?",
                                     "Confirm Update",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                queueForm.UpdateCommand(tb_commandedit.Text);
                this.Close();
            }
            
        }
    }
}

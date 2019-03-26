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
    public partial class ContractEditForm : Form
    {
        
        public ContractEditForm()
        {
            InitializeComponent();
        }

        private void ContractEditForm_Load(object sender, EventArgs e)
        {
            /*foreach(Contract contract in settings.contractList)
            {
                cb_Contracts.Items.Add(contract);
            }*/
        }

        private void cb_Contracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO FINISH DISPLAY OF PROPERTIES
            grpBx_contractInfo.Controls.Clear();

            
            Contract contract = (Contract)cb_Contracts.SelectedItem;
            grpBx_contractInfo.Text = contract.contractName + " Properties";

            Label lbl_contractName = new Label();
            lbl_contractName.Text = "Name:";
            lbl_contractName.Size = new Size(10, 20);
            lbl_contractName.Location = new System.Drawing.Point(5, 25);
            grpBx_contractInfo.Controls.Add(lbl_contractName);

            TextBox tb_contractName = new TextBox();
            tb_contractName.Text = contract.contractName;
            tb_contractName.Location = new System.Drawing.Point(20, 25);
            grpBx_contractInfo.Controls.Add(tb_contractName);

        }
    }
}

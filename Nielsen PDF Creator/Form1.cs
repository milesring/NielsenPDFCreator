using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nielsen_PDF_Creator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.ContractList = null;
            BuildContracts();
            for (int i = 0; i < Properties.Settings.Default.ContractList.Count; i++)
            {
                combo_contracts.Items.Add(Properties.Settings.Default.ContractList.ElementAt(i).contractName);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void list_contract_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void text_pdf1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox listBox = (CheckedListBox)sender;
            listBox.ClearSelected();
        }


        private void button_pdf1browse_Click(object sender, EventArgs e)
        {

            string fileName = null;

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                    int index = panel_pdfInput.Controls.IndexOf((Button)sender);
                    panel_pdfInput.Controls[index - 1].Text = fileName;
                    //text_pdf1.Text = fileName;
                }
            }
        }

        private void combo_contracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_pdfInput.Visible = true;
            panel_pdfInput.Enabled = true;
            Label pdflabel = (Label)panel_pdfInput.Controls[0];
            Label contractorLabel = (Label)panel_pdfInput.Controls[1];
            panel_pdfInput.Controls.Clear();
            panel_pdfInput.Controls.Add(pdflabel);
            panel_pdfInput.Controls.Add(contractorLabel);

            //search contract list for index of contract name
            int index = Properties.Settings.Default.ContractList.FindIndex(x => x.contractName.Equals(combo_contracts.Text));

            //build buttons & textboxes from that index in list
            for (int i = 0; i < Properties.Settings.Default.ContractList.ElementAt(index).labelCount(); i++)
            {
                TextBox textBox = new TextBox();
                panel_pdfInput.Controls.Add(textBox);
                textBox.Text = "Select file...";
                textBox.ReadOnly = true;
                textBox.Location = new System.Drawing.Point(5, 20 + i * 30);
                textBox.Width = 230;
                textBox.Height = 23;

                Button button = new Button();
                panel_pdfInput.Controls.Add(button);
                button.Text = Properties.Settings.Default.ContractList.ElementAt(index).labelAt(i);
                button.Anchor = AnchorStyles.Top;
                button.Location = new System.Drawing.Point(240, 20 + i * 30);
                button.Width = 75;
                button.Height = 23;
                button.Click += new EventHandler(button_pdf1browse_Click);
                button.Visible = true;
                button.Enabled = true;
            }

            //build checkboxes for contractors
            if (Properties.Settings.Default.ContractList.ElementAt(index).contractorCount() > 0)
            {
                CheckedListBox checkedList = new CheckedListBox();
                for (int i = 0; i < Properties.Settings.Default.ContractList.ElementAt(index).contractorCount(); i++)
                {
                    checkedList.Items.Add(Properties.Settings.Default.ContractList.ElementAt(index).contractorAt(i), true);
                }
                panel_pdfInput.Controls.Add(checkedList);
                checkedList.Location = new System.Drawing.Point(428, 20);
                checkedList.BackColor = System.Drawing.Color.FromName("Control");
                checkedList.BorderStyle = BorderStyle.None;
                checkedList.CheckOnClick = true;
                checkedList.SelectedIndexChanged += new EventHandler(checkedListBox_SelectedIndexChanged);
            }


        }

        private void BuildContracts()
        {

            if (Properties.Settings.Default.ContractList == null)
            {
                List<Contract> ContractList = new List<Contract>();
                Properties.Settings.Default.ContractList = ContractList;
            }

            BuildURD();
            BuildSTL();
            Properties.Settings.Default.Save();

        }

        private void BuildURD()
        {
            Contract URD = new Contract();
            URD.contractName = "URD";
            URD.contractNum = "213640";
            URD.addPDF("Invoice");
            URD.addPDF("Billing");
            URD.addPDF("Total");

            Properties.Settings.Default.ContractList.Add(URD);
        }

        private void BuildLES()
        {

            
        }

        private void BuildSTL()
        {
            Contract STL = new Contract();
            STL.contractName = "STL";
            STL.contractNum = "224709";
            STL.addPDF("Invoice");
            STL.addPDF("Billing");
            STL.addPDF("Total");
            STL.addContractor("Jeff");
            STL.addContractor("Day Electric");
            STL.addContractor("Jesse");
            Properties.Settings.Default.ContractList.Add(STL);

        }

        private void BuildHourly()
        {
            Contract Hourly = new Contract();
            Hourly.contractName = "Hourly";
            Hourly.contractNum = "234606-R1";
            Hourly.addPDF("Invoice");
            Hourly.addPDF("Billing");
            Hourly.addPDF("Total");
            Properties.Settings.Default.ContractList.Add(Hourly);
        }

        private void BuildMetro()
        {
            Contract Metro = new Contract();
            Metro.contractName = "Metro";
            Metro.contractNum = "202757";
            Metro.addPDF("Invoice");
            Metro.addPDF("Billing");
            Metro.addPDF("Total");
            Properties.Settings.Default.ContractList.Add(Metro);

        }

        private void BuildRural()
        {
            Contract Rural = new Contract();
            Rural.contractName = "Rural";
            Rural.contractNum = "201026";
            Rural.addPDF("Invoice");
            Rural.addPDF("Billing");
            Rural.addPDF("Total");
            Properties.Settings.Default.ContractList.Add(Rural);
        }

        private void button_build_Click(object sender, EventArgs e)
        {
            String command = "";
            for (int i = 2; i < panel_pdfInput.Controls.Count; i++)
             {
                 if(panel_pdfInput.Controls[i] is TextBox)
                 {
                     command += " " + "\"" + panel_pdfInput.Controls[i].Text + "\"";
                 }

                 if(panel_pdfInput.Controls[i] is CheckedListBox)
                 {
                     CheckedListBox listbox = (CheckedListBox)panel_pdfInput.Controls[i];
                     foreach (var item in listbox.CheckedItems)
                     {
                         command += " " + "\"" + item.ToString() + " " + dateTime.ToString() + "\"";
                     }
                 }

             }

             command += " " + "cat";
             command += " " + "output";
             command += " " + "\"Test Report" + " " + dateTime.Text+ ".pdf\"";
             

            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "pdftk";
            process.StartInfo.Arguments = command;
            process.Start();
            process.WaitForExit();// Waits here for the process to exit.

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            label_Status.Text = "";
            if (Properties.Settings.Default.ContractList == null)
            {
                BuildContracts();
            }
            for (int i = 0; i < Properties.Settings.Default.ContractList.Count; i++)
            {
                combo_contracts.Items.Add(Properties.Settings.Default.ContractList.ElementAt(i).contractName);
            }

            button_build.Enabled = false;

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
                if (Properties.Settings.Default.LastFilePath.Equals("") && textbox_WorkingFolder.Text.Equals(""))
                {
                    openFileDialog1.InitialDirectory = "c:\\";
                }
                else if (!textbox_WorkingFolder.Text.Equals(""))
                {
                    openFileDialog1.InitialDirectory = textbox_WorkingFolder.Text;
                }
                else
                {
                    openFileDialog1.InitialDirectory = Properties.Settings.Default.LastFilePath;
                }
                
                openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                    Properties.Settings.Default.LastFilePath = Path.GetDirectoryName(fileName);
                    Properties.Settings.Default.Save();
                    int index = panel_pdfInput.Controls.IndexOf((Button)sender);
                    panel_pdfInput.Controls[index - 1].Text = fileName;
                }
            }
        }

        private void combo_contracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_pdfInput.Visible = true;
            panel_pdfInput.Enabled = true;
            label_Status.Text = "";
            if (!textbox_WorkingFolder.Text.Equals(""))
            {
                button_build.Enabled = true;
            }

            if (combo_contracts.Text.Equals("LES"))
            {
                panel_pdfInput.Controls.Clear();
                Label label_WO = new Label();
                label_WO.Text = "Active Work Orders";
                panel_pdfInput.Controls.Add(label_WO);

                CheckedListBox checkedListBoxWOs = new CheckedListBox();
                foreach (var item in Properties.Settings.Default.ContractList)
                {
                    if(item is LESContract)
                    {
                        LESContract contract = (LESContract)item;
                        for (int i = 0; i < contract.woCount(); i++)
                        {
                            checkedListBoxWOs.Items.Add(contract.woAt(i), false);
                        }
                    }
                }
                checkedListBoxWOs.Location = new System.Drawing.Point(5, 30);
                checkedListBoxWOs.BackColor = System.Drawing.Color.FromName("Control");
                checkedListBoxWOs.BorderStyle = BorderStyle.None;
                checkedListBoxWOs.CheckOnClick = true;
                checkedListBoxWOs.SelectedIndexChanged += new EventHandler(checkedListBox_SelectedIndexChanged);
                panel_pdfInput.Controls.Add(checkedListBoxWOs);
               
            }
            else
            {      
                panel_pdfInput.Controls.Clear();

                Label pdfLabel = new Label();
                pdfLabel.Location = new System.Drawing.Point(12, 4);
                pdfLabel.Text = "PDFs";
                Label contractorLabel = new Label();
                contractorLabel.Location = new System.Drawing.Point(428, 4);
                contractorLabel.Text = "Contractors";

                panel_pdfInput.Controls.Add(pdfLabel);
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
                    textBox.Location = new System.Drawing.Point(5, 30 + i * 30);
                    textBox.Width = 230;
                    textBox.Height = 23;

                    Button button = new Button();
                    panel_pdfInput.Controls.Add(button);
                    button.Text = Properties.Settings.Default.ContractList.ElementAt(index).labelAt(i);
                    button.Anchor = AnchorStyles.Top;
                    button.Location = new System.Drawing.Point(240, 30 + i * 30);
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
                    checkedList.Location = new System.Drawing.Point(428, 30);
                    checkedList.BackColor = System.Drawing.Color.FromName("Control");
                    checkedList.BorderStyle = BorderStyle.None;
                    checkedList.CheckOnClick = true;
                    checkedList.SelectedIndexChanged += new EventHandler(checkedListBox_SelectedIndexChanged);
                }
            }


        }

        private void BuildContracts()
        {
            List<Contract> ContractList = new List<Contract>();
            Properties.Settings.Default.ContractList = ContractList;

            BuildURD();
            BuildSTL();
            BuildHourly();
            BuildMetro();
            BuildRural();
            BuildLES();

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

            URD.addContractor("CBT");
            URD.addContractor("Central States");
            URD.addContractor("Crew 24 Chris");
            URD.addContractor("Fitzgerald");
            URD.addContractor("Jeff");
            URD.addContractor("Jesse");


            Properties.Settings.Default.ContractList.Add(URD);
        }

        private void BuildLES()
        {
            LESContract LES = new LESContract();
            LES.contractName = "LES";

            LES.addWO("5028651");
            LES.addWO("5028652");
            LES.addWO("5028653");
            LES.addWO("5028657");
            LES.addWO("5028658");
            LES.addWO("5028661");
            LES.addWO("5028702");
            LES.addWO("5028704");
            LES.addWO("5028705");
            LES.addWO("5029268");
            LES.addWO("5031156");

            LES.addContractor("CBT");
            LES.addContractor("Simon");
            LES.addContractor("Atlas");
            LES.addContractor("Vicomm");

            Properties.Settings.Default.ContractList.Add(LES);
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
            STL.addContractor("Melvin");

            Properties.Settings.Default.ContractList.Add(STL);

        }

        private void BuildHourly()
        {
            Contract Hourly = new Contract();
            Hourly.contractName = "HR";
            Hourly.contractNum = "234606-R1";
            Hourly.addPDF("Invoice");
            Hourly.addPDF("Billing");
            Hourly.addPDF("Total");

            Hourly.addContractor("Dean");
            Hourly.addContractor("Ed");

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

            Metro.addContractor("Crew 24 Chris");
            Metro.addContractor("Omaha Concrete Sawing");
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

            Rural.addContractor("Crew 24 Chris");

            Properties.Settings.Default.ContractList.Add(Rural);
        }

        private void button_build_Click(object sender, EventArgs e)
        {
            int exitCode = 0;

            if (combo_contracts.Text.Equals("LES"))
            {
              exitCode = BuildLESPDFs();
            }
            else
            {
              exitCode = BuildOPPDPDFs();
            }

          

            if (exitCode == 0)
            {
                label_Status.Text = "Success!";
                label_Status.ForeColor = Color.Green;
            }
            else
            {
                label_Status.Text = "Failure!";
                label_Status.ForeColor = Color.Red;
            }

        }

        private int BuildOPPDPDFs()
        {
            int exitCode = 0;
            String command = "";
            for (int i = 2; i < panel_pdfInput.Controls.Count; i++)
            {
                if (panel_pdfInput.Controls[i] is TextBox)
                {
                    if (panel_pdfInput.Controls[i].Text.Equals("Select file..."))
                    {
                        Button button = (Button)panel_pdfInput.Controls[i + 1];
                        System.Windows.Forms.MessageBox.Show("No file selected for " + button.Text);
                        return -1;
                    }

                    if (panel_pdfInput.Controls[i + 1].Text.Equals("Total"))
                    {
                        command += BuildSubs();
                    }
                    command += " " + "\"" + panel_pdfInput.Controls[i].Text + "\"";
                }

            }

            command += " " + "cat";
            command += " " + "output";
            command += " " + "\"" + textbox_WorkingFolder.Text + "\\" + combo_contracts.Text + " " + "TJ Report" + " " + dateTime.Text + ".pdf\"";


            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "pdftk";
            process.StartInfo.Arguments = command;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();// Waits here for the process to exit.

            exitCode += process.ExitCode;

            command = "";

            for (int i = 2; i < panel_pdfInput.Controls.Count; i++)
            {
                if (panel_pdfInput.Controls[i] is TextBox)
                {
                    if (panel_pdfInput.Controls[i + 1].Text.Equals("Invoice") || panel_pdfInput.Controls[i + 1].Text.Equals("Billing"))
                    {
                        command += " " + "\"" + panel_pdfInput.Controls[i].Text + "\"";
                    }
                }
            }

            command += " " + "cat";
            command += " " + "output";
            command += " " + "\"" + textbox_WorkingFolder.Text + "\\" + combo_contracts.Text + " " + "Nielsen Invoice for WE "
                + dateTime.Text + " " + "Contract # "
                + Properties.Settings.Default.ContractList.Find(x => x.contractName.Equals(combo_contracts.Text)).contractNum + ".pdf";

            process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "pdftk";
            process.StartInfo.Arguments = command;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();// Waits here for the process to exit.

            exitCode += process.ExitCode;
            return exitCode;
        }

        private int BuildLESPDFs()
        {
            int exitCode = 0;



            return exitCode;
        }

        private string BuildSubs()
        {
            String command = "";
            foreach (Control control in panel_pdfInput.Controls)
            {

                String contract = combo_contracts.Text;

                if (control is CheckedListBox)
                {
                    CheckedListBox listbox = (CheckedListBox)control;
                    foreach (var item in listbox.CheckedItems)
                    {
                        command += " " + "\"" + textbox_WorkingFolder.Text + "\\" + contract + " " + item.ToString() + " " + dateTime.Text + ".pdf\"";
                        if (item.ToString().Equals("CBT"))
                        {
                            command += " " + "\"" + textbox_WorkingFolder.Text + "\\" + contract + " " + item.ToString() + " Payment" + " " + dateTime.Text + ".pdf\"";
                        }
                    }
                }
            }
            return command;
        }

        private void button_WorkingFolder_Click(object sender, EventArgs e)
        {
            using (new OffsetWinDialog(this) { PreferredOffset = new Point(75, 75) })
            using (new SizeWinDialog(this) { PreferredSize = new Size(800, 800) })
            {
                using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
                {
                    if (!Properties.Settings.Default.LastFilePath.Equals(""))
                    {
                        folderBrowserDialog1.SelectedPath = Properties.Settings.Default.LastFilePath;
                    }
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textbox_WorkingFolder.Text = folderBrowserDialog1.SelectedPath;
                    }
                }
            }
        }

        private void textbox_WorkingFolder_TextChanged(object sender, EventArgs e)
        {
            if (!combo_contracts.Text.Equals("Select.."))
            {
                button_build.Enabled = true;
            }
        }
    }
}

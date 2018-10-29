using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Nielsen_PDF_Creator
{
    public partial class Form1 : Form
    {
        private List<String> fileList;
        private List<String> subList;
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
            textbox_WorkingFolder.Enabled = false;
            button_WorkingFolder.Enabled = false;

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
            panel_Contractors.Visible = true;
            panel_Contractors.Enabled = true;
            button_WorkingFolder.Enabled = true;
            textbox_WorkingFolder.Enabled = true;
            label_Status.Text = "";
            textbox_WorkingFolder.Text = "";
            button_build.Enabled = false;

            if (combo_contracts.Text.Equals("LES"))
            {

                DisplayLESWOs();
            }
            else
            {
                DisplayOPPD();
            }


        }

        private void LESWOCheckboxChanged(object sender, EventArgs e)
        {
            //CheckedListBox listBox = (CheckedListBox)sender;
            //listBox.ClearSelected();
            DisplayLESInput();
        }
        
        private void DisplayLESWOs()
        {
            panel_pdfInput.Controls.Clear();
            panel_Contractors.Controls.Clear();

            Label label_WO = new Label();
            label_WO.Text = "Active Work Orders";
            panel_pdfInput.Controls.Add(label_WO);

            CheckedListBox checkedListBoxWOs = new CheckedListBox();
            checkedListBoxWOs.Size = new Size(checkedListBoxWOs.Size.Width, checkedListBoxWOs.Size.Height * 2);

            LESContract contract = (LESContract)Properties.Settings.Default.ContractList.Find(x => x is LESContract);

            for (int i = 0; i < contract.woCount(); i++){
                checkedListBoxWOs.Items.Add(contract.woAt(i), false);
            }

            checkedListBoxWOs.Location = new System.Drawing.Point(5, 30);
            checkedListBoxWOs.BackColor = System.Drawing.Color.FromName("Control");
            checkedListBoxWOs.BorderStyle = BorderStyle.None;
            checkedListBoxWOs.CheckOnClick = true;
            checkedListBoxWOs.SelectedIndexChanged += new EventHandler(LESWOCheckboxChanged);
            panel_pdfInput.Controls.Add(checkedListBoxWOs);

            
            
    
        }

        private void DisplayLESInput()
        {
            panel_Contractors.Controls.Clear();
            LESContract contract = (LESContract)Properties.Settings.Default.ContractList.Find(x => x is LESContract);

            CheckedListBox clb = null;
            for (int i = 0; i < panel_pdfInput.Controls.Count; i++)
            {
                if(panel_pdfInput.Controls[i] is CheckedListBox)
                {
                    clb = (CheckedListBox)panel_pdfInput.Controls[i];
                    break;
                }
            }

            Label label_input = new Label();
            label_input.Text = "PDF Input";

            panel_Contractors.Controls.Add(label_input);
            String[] woRequirements = new String[] {
                "Nielsen Invoice", "LES Invoice Excel",
                "A LES by Date", "Production for WO",
                "B Year To Date Remaining Balance",
                "L Total"
            };
            int y = 0;
            for(int i = 0; i < clb.CheckedItems.Count; i++)
            {
                Label label = new Label();
                label.Text = clb.CheckedItems[i].ToString();
                y = 30 + i * 40 * woRequirements.Length;
                label.Location = new System.Drawing.Point(0, y);
                panel_Contractors.Controls.Add(label);

                for(int j = 0; j < woRequirements.Length; j++)
                {
                    
                    TextBox textBox = new TextBox();
                    textBox.Text = woRequirements[j];
                    textBox.ReadOnly = true;
                    textBox.Location = new System.Drawing.Point(5, y + 30 + 30 * j);
                    textBox.Width = 100;
                    textBox.Height = 23;
                    panel_Contractors.Controls.Add(textBox);

                    Button button = new Button();
                    button.Text = woRequirements[j];
                    button.Anchor = AnchorStyles.Top;
                    button.Location = new System.Drawing.Point(120, y + 30 + 30 * j);
                    button.Width = 75;
                    button.Height = 23;
                    button.Click += new EventHandler(button_pdf1browse_Click);
                    button.Visible = true;
                    button.Enabled = true;
                    panel_Contractors.Controls.Add(button);
                }
            }

            CheckedListBox contractors = new CheckedListBox();
            contractors.Location = new System.Drawing.Point(5, y + 35 * woRequirements.Length);
            contractors.BackColor = System.Drawing.Color.FromName("Control");
            contractors.BorderStyle = BorderStyle.None;
            contractors.CheckOnClick = true;
            for (int i = 0; i < contract.contractorCount(); i++)
            {
                contractors.Items.Add(contract.contractorAt(i), false);
            }
            panel_Contractors.Controls.Add(contractors);
        }

        private void DisplayOPPD()
        {
            panel_pdfInput.Controls.Clear();
            panel_Contractors.Controls.Clear();
            Label pdfLabel = new Label();
            pdfLabel.Location = new System.Drawing.Point(12, 4);
            pdfLabel.Text = "PDFs";
            Label contractorLabel = new Label();
            contractorLabel.Location = new System.Drawing.Point(12, 4);
            contractorLabel.Text = "Contractors";

            panel_pdfInput.Controls.Add(pdfLabel);
            panel_Contractors.Controls.Add(contractorLabel);

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
                panel_Contractors.Controls.Add(checkedList);
                checkedList.Location = new System.Drawing.Point(0, 30);
                checkedList.BackColor = System.Drawing.Color.FromName("Control");
                checkedList.BorderStyle = BorderStyle.None;
                checkedList.CheckOnClick = true;
                checkedList.SelectedIndexChanged += new EventHandler(checkedListBox_SelectedIndexChanged);
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
            LES.addWO("5028659");
            LES.addWO("5028703");


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
            Metro.addContractor("Fitzgerald");
            Metro.addContractor("Central States");
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
            Rural.addContractor("Jesse");
            Rural.addContractor("Jeff");

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
            fileList = new List<String>();
            

            //build TJ Report
            //aggregate main PDFs for report
            for (int i = 1; i < panel_pdfInput.Controls.Count; i++)
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
                        subList = BuildSubsList();
                    }
                    fileList.Add(panel_pdfInput.Controls[i].Text);
                }

            }

            for (int i = 0; i < fileList.Count; i++)
            {
                if(i == 2)
                {
                    foreach(String subItem in subList)
                    {
                        command += "\"" + subItem + "\" ";
                    }
                }
                command += "\"" + fileList[i] + "\" ";
            }

            command +="cat";
            command += " " + "output";
            command += " " + "\"" + textbox_WorkingFolder.Text + "\\" + combo_contracts.Text + " " + "TJ Report" + " " + dateTime.Text + ".pdf\"";

            Process process = new Process();
            process.StartInfo.FileName = "pdftk";
            process.StartInfo.Arguments = command;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

            exitCode += process.ExitCode;


            //Build Invoice PDF
            command = "";
            command += "\"" + fileList[0] + "\" ";
            command += "\"" + fileList[1] + "\" ";

            command += " " + "cat";
            command += " " + "output";
            command += " " + "\"" + textbox_WorkingFolder.Text + "\\" + combo_contracts.Text + " " + "Nielsen Invoice for WE "
                + dateTime.Text + " " + "Contract # "
                + Properties.Settings.Default.ContractList.Find(x => x.contractName.Equals(combo_contracts.Text)).contractNum + ".pdf";

            process = new Process();
            process.StartInfo.FileName = "pdftk";
            process.StartInfo.Arguments = command;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

            exitCode += process.ExitCode;

            //Ask to clean up files
            if(exitCode == 0)
            {
                var confirmResult = MessageBox.Show("Would you like to remove files used to build reports?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    bool tryAgain;
                    // If 'Yes', do something here.
                    for (int i = 0; i < fileList.Count; i++)
                    {
                        tryAgain = true;
                        while (tryAgain)
                        {
                            try
                            {
                                File.Delete(fileList[i]);
                                tryAgain = false;
                            }
                            catch (DirectoryNotFoundException e)
                            {
                                MessageBox.Show("File: "+fileList[i]+" was already deleted.");
                                tryAgain = false;

                            }
                            catch (IOException e)
                            {
                                MessageBox.Show("Please close "+fileList[i]);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Unknown error for "+fileList[i]);
                                tryAgain = false;
                            }
                        }
                    }

                }
                else
                {
                    // If 'No', do something here.
                }
            }
            return exitCode;
        }

        private int BuildLESPDFs()
        {
            int exitCode = 0;
            List<string> workOrders = new List<string>();
            fileList = new List<string>();

            for (int i = 0; i < panel_Contractors.Controls.Count; i++)
            {
                if(panel_Contractors.Controls[i] is Label)
                {
                    workOrders.Add(panel_Contractors.Controls[i].Text);
                }
                else if(panel_Contractors.Controls[i] is TextBox)
                {
                    fileList.Add(panel_Contractors.Controls[i].Text);
                }
            }


            return exitCode;
        }

        private string BuildSubs()
        {
            String command = "";
            foreach (Control control in panel_Contractors.Controls)
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

        private List<String> BuildSubsList()
        {
            List<String> subList = new List<string>();
            foreach (Control control in panel_Contractors.Controls)
            {

                String contract = combo_contracts.Text;

                if (control is CheckedListBox)
                {
                    CheckedListBox listbox = (CheckedListBox)control;
                    foreach (var item in listbox.CheckedItems)
                    {
                        subList.Add(textbox_WorkingFolder.Text + "\\" + contract + " " + item.ToString() + " " + dateTime.Text + ".pdf");
                        if (item.ToString().Equals("CBT"))
                        {
                            subList.Add(textbox_WorkingFolder.Text + "\\" + contract + " " + item.ToString() + " Payment" + " " + dateTime.Text + ".pdf");
                        }
                    }
                }
            }
            return subList;
        }

        private void button_WorkingFolder_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.ValidateNames = false;
                openFileDialog1.CheckFileExists = false;
                openFileDialog1.CheckPathExists = true;

                openFileDialog1.FileName = "Folder Selection.";
                int removalNum = openFileDialog1.FileName.Count();
                String path = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName.Remove(openFileDialog1.FileName.Length - removalNum, removalNum);
                    textbox_WorkingFolder.Text = path;
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

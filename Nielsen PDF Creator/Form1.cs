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
        private String standardError = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label_Status.Text = "";
            Properties.Settings.Default.ContractList = null;

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
            BuildCustom();
           
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
            URD.addContractor("JJ Drilling");


            Properties.Settings.Default.ContractList.Add(URD);
        }

        private void BuildLES()
        {
            LESContract LES = new LESContract();
            LES.contractName = "LES 2018";

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
            LES.addWO("5029267");
            LES.addWO("5028654");
            LES.addWO("5028660");

            LES.addContractor("CBT");
            LES.addContractor("Simon");
            LES.addContractor("Atlas");
            LES.addContractor("Vicomm");
            LES.addContractor("GPS");

            Properties.Settings.Default.ContractList.Add(LES);

            LESContract LES2019 = new LESContract();
            LES2019.contractName = "LES 2019";
            LES2019.addWO("5031483");
            LES2019.addWO("5031499");
            LES2019.addContractor("CBT");
            LES2019.addContractor("Simon");
            LES2019.addContractor("Atlas");
            LES2019.addContractor("Vicomm");
            LES2019.addContractor("GPS");
            Properties.Settings.Default.ContractList.Add(LES2019);

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

        private void BuildCustom()
        {
            Contract custom = new Contract();
            custom.contractName = "Add new...";

            Properties.Settings.Default.ContractList.Add(custom);
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

            LESContract contract = (LESContract)Properties.Settings.Default.ContractList.Find(x => x is LESContract && x.contractName == combo_contracts.Text);

            for (int i = 0; i < contract.woCount(); i++)
            {
                checkedListBoxWOs.Items.Add(contract.woAt(i), false);
            }

            checkedListBoxWOs.Location = new System.Drawing.Point(5, 30);
            checkedListBoxWOs.BackColor = System.Drawing.Color.FromName("Control");
            checkedListBoxWOs.BorderStyle = BorderStyle.None;
            checkedListBoxWOs.CheckOnClick = false;
            //checkedListBoxWOs.Anchor = AnchorStyles.Bottom & AnchorStyles.Top & AnchorStyles.Left & AnchorStyles.Right;
            checkedListBoxWOs.Size = new Size(panel_pdfInput.Size.Width, panel_pdfInput.Size.Height);
            checkedListBoxWOs.SelectedIndexChanged += new EventHandler(LESWOCheckboxChanged);

            //CHANGE TO THIS 11/9/18
            //checkedListBoxWOs.ItemCheck += new EventHandler(LESWOCheckboxChanged);
            panel_pdfInput.Controls.Add(checkedListBoxWOs);

            

        
    }

        private void DisplayLESInput()
        {
            panel_Contractors.Controls.Clear();
            LESContract contract = (LESContract)Properties.Settings.Default.ContractList.Find(x => x is LESContract);

            CheckedListBox clb = null;
            for (int i = 0; i < panel_pdfInput.Controls.Count; i++)
            {
                if (panel_pdfInput.Controls[i] is CheckedListBox)
                {
                    clb = (CheckedListBox)panel_pdfInput.Controls[i];
                    break;
                }
            }
            if (clb.CheckedItems.Count > 0)
            {
                Panel inputPanel;
                CheckBox checkBox;
                TextBox textBox;
                Button button;

                Label label = new Label();
                label.Text = "PDF Input";

                panel_Contractors.Controls.Add(label);
                String[] tjReportRequirements = new string[]
                {
                "Cam", "Kevin", "Masoud", "LES Overall", "Retainage"
                };

                String[] tjReportRequirementDescriptions = new string[]
                {
                    "Report from Access, DD Cam (from beginning of week to billing date)",
                    "Report from Access, DD Kevin (from beginning of week to billing date)",
                    "Report from Access, DD Masoud (from beginning of week to billing date)",
                    "Report from Access, B Year to Date Overall (from 1/1/[year] to billing date)",
                    "PDF of Retainage Excel sheet"
                };

                String[] woRequirements = new String[]
                {
                "Nielsen Invoice", "LES Invoice Excel",
                "A LES by Date", "Production for WO",
                "B Year To Date Remaining Balance",
                "L Total"
                };

                String[] requirementDescriptions = new String[]
                {
                    "Invoice made from QB", "Invoice made from excel sheet",
                    "Report from Access, A LES by Date (beginning of week to end of week)",
                    "Production for the specific work order sent from Nielsen in Lincoln",
                    "Report from Access, B Year to Date Remaining Balance (from 1/1/[year] to billing date)",
                    "Report from Access, L Total"

                };


                for (int i = 0; i < clb.CheckedItems.Count; i++)
                {
                    label = new Label();
                    label.Text = clb.CheckedItems[i].ToString();
                    label.Location = new System.Drawing.Point(0, panel_Contractors.Controls[panel_Contractors.Controls.Count - 1].Location.Y + label.Size.Height * 2);

                    panel_Contractors.Controls.Add(label);


                    for (int j = 0; j < woRequirements.Length; j++)
                    {
                        inputPanel = new Panel();
                        inputPanel.Anchor = AnchorStyles.Top;
                        inputPanel.Size = new Size(300, 25);
                        inputPanel.Location = new System.Drawing.Point(0, panel_Contractors.Controls[panel_Contractors.Controls.Count - 1].Location.Y + inputPanel.Size.Height);

                        checkBox = new CheckBox();
                        checkBox.Checked = true;
                        checkBox.CheckedChanged += new EventHandler(checkbox_Custom_CheckChanged);
                        checkBox.Size = new Size(20, 23);
                        checkBox.Location = new System.Drawing.Point(10, 0);

                        ToolTip toolTip = new ToolTip();
                        toolTip.AutoPopDelay = 5000;
                        toolTip.InitialDelay = 1000;
                        toolTip.ReshowDelay = 500;
                        toolTip.SetToolTip(checkBox, "Enables or disables the pdf in the report");

                        inputPanel.Controls.Add(checkBox);

                        textBox = new TextBox();
                        textBox.Text = woRequirements[j];
                        textBox.ReadOnly = true;
                        textBox.Location = new System.Drawing.Point(30, 0);
                        textBox.Width = 100;
                        textBox.Height = 23;

                        toolTip = new ToolTip();
                        toolTip.AutoPopDelay = 5000;
                        toolTip.InitialDelay = 1000;
                        toolTip.ReshowDelay = 500;
                        toolTip.SetToolTip(textBox, requirementDescriptions[j]);

                        inputPanel.Controls.Add(textBox);

                        button = new Button();
                        button.Text = woRequirements[j];
                        button.Anchor = AnchorStyles.Top;
                        button.Location = new System.Drawing.Point(135, 0);
                        button.Width = 75;
                        button.Height = 23;
                        button.Click += new EventHandler(button_pdf1browse_Click);
                        button.Visible = true;
                        button.Enabled = true;

                        toolTip = new ToolTip();
                        toolTip.AutoPopDelay = 5000;
                        toolTip.InitialDelay = 1000;
                        toolTip.ReshowDelay = 500;
                        toolTip.SetToolTip(button, requirementDescriptions[j]);

                        inputPanel.Controls.Add(button);
                        panel_Contractors.Controls.Add(inputPanel);
                    }
                }

                label = new Label();
                label.Text = "Contractors";
                label.Location = new System.Drawing.Point(0, panel_Contractors.Controls[panel_Contractors.Controls.Count - 1].Location.Y + label.Size.Height * 2);
                panel_Contractors.Controls.Add(label);

                for (int i = 0; i < contract.contractorCount(); i++)
                {
                    inputPanel = new Panel();
                    inputPanel.Anchor = AnchorStyles.Top;
                    inputPanel.Size = new Size(300, 25);
                    inputPanel.Location = new System.Drawing.Point(0, panel_Contractors.Controls[panel_Contractors.Controls.Count - 1].Location.Y + inputPanel.Size.Height);

                    checkBox = new CheckBox();
                    checkBox.Checked = true;
                    checkBox.CheckedChanged += new EventHandler(checkbox_Custom_CheckChanged);
                    checkBox.Size = new Size(20, 23);
                    checkBox.Location = new System.Drawing.Point(10, 0);

                    ToolTip toolTip = new ToolTip();
                    toolTip.AutoPopDelay = 5000;
                    toolTip.InitialDelay = 1000;
                    toolTip.ReshowDelay = 500;
                    toolTip.SetToolTip(checkBox, "Enables or disables the pdf in the report");

                    inputPanel.Controls.Add(checkBox);

                    textBox = new TextBox();
                    textBox.Text = contract.contractorAt(i);
                    textBox.ReadOnly = true;
                    textBox.Location = new System.Drawing.Point(30, 0);
                    textBox.Width = 100;
                    textBox.Height = 23;
                    inputPanel.Controls.Add(textBox);

                    button = new Button();
                    button.Text = contract.contractorAt(i);
                    button.Anchor = AnchorStyles.Top;
                    button.Location = new System.Drawing.Point(135, 0);
                    button.Width = 75;
                    button.Height = 23;
                    button.Click += new EventHandler(button_pdf1browse_Click);
                    button.Visible = true;
                    button.Enabled = true;

                    toolTip = new ToolTip();
                    toolTip.AutoPopDelay = 5000;
                    toolTip.InitialDelay = 1000;
                    toolTip.ReshowDelay = 500;
                    toolTip.SetToolTip(button, "Report from Access, C Sub by Sub Number and Date");

                    inputPanel.Controls.Add(button);
                    panel_Contractors.Controls.Add(inputPanel);
                }

                label = new Label();
                label.Text = "TJ Report";
                label.Location = new System.Drawing.Point(0, panel_Contractors.Controls[panel_Contractors.Controls.Count - 1].Location.Y + label.Size.Height * 2);
                panel_Contractors.Controls.Add(label);

                for (int i = 0; i < tjReportRequirements.Length; i++)
                {
                    inputPanel = new Panel();
                    inputPanel.Anchor = AnchorStyles.Top;
                    inputPanel.Size = new Size(300, 25);
                    inputPanel.Location = new System.Drawing.Point(0, panel_Contractors.Controls[panel_Contractors.Controls.Count - 1].Location.Y + inputPanel.Size.Height);

                    checkBox = new CheckBox();
                    checkBox.Checked = true;
                    checkBox.CheckedChanged += new EventHandler(checkbox_Custom_CheckChanged);
                    checkBox.Size = new Size(20, 23);
                    checkBox.Location = new System.Drawing.Point(10, 0);

                    ToolTip toolTip = new ToolTip();
                    toolTip.AutoPopDelay = 5000;
                    toolTip.InitialDelay = 1000;
                    toolTip.ReshowDelay = 500;
                    toolTip.SetToolTip(checkBox, "Enables or disables the pdf in the report");

                    inputPanel.Controls.Add(checkBox);

                    textBox = new TextBox();
                    textBox.Text = tjReportRequirements[i];
                    textBox.ReadOnly = true;
                    textBox.Location = new System.Drawing.Point(30, 0);
                    textBox.Width = 100;
                    textBox.Height = 23;
                    inputPanel.Controls.Add(textBox);

                    button = new Button();
                    button.Text = tjReportRequirements[i];
                    button.Anchor = AnchorStyles.Top;
                    button.Location = new System.Drawing.Point(135, 0);
                    button.Width = 75;
                    button.Height = 23;
                    button.Click += new EventHandler(button_pdf1browse_Click);
                    button.Visible = true;
                    button.Enabled = true;

                    toolTip = new ToolTip();
                    toolTip.AutoPopDelay = 5000;
                    toolTip.InitialDelay = 1000;
                    toolTip.ReshowDelay = 500;
                    toolTip.SetToolTip(button, tjReportRequirementDescriptions[i]);

                    inputPanel.Controls.Add(button);
                    panel_Contractors.Controls.Add(inputPanel);
                }
            }
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

        private void DisplayContractBuilder()
        {
            //TODO: move from combo box of contracts to a settings window
            panel_pdfInput.Controls.Clear();
            panel_Contractors.Controls.Clear();
            button_build.Enabled = false;
            label_Status.Text = "";

            //TODO: display type of contract desired in checkbox/radio/whatever
            //TODO: add contract name
            //TODO: add contract number
            //TODO: add contractors to contract
            //TODO:
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
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            while (!process.StandardError.EndOfStream)
            {
                standardError += process.StandardError.ReadLine() + "\n";
            }
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
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            while (!process.StandardError.EndOfStream)
            {
                standardError += process.StandardError.ReadLine() + "\n";
            }
            process.WaitForExit();

            exitCode += process.ExitCode;

            return exitCode;
        }

        private int BuildLESPDFs()
        {
            int exitCode = 0;
            String command = "";
            List<string> workOrders = new List<string>();
            fileList = new List<string>();

            for (int i = 0; i < panel_Contractors.Controls.Count; i++)
            {
                if (panel_Contractors.Controls[i].GetType() == typeof(Panel))
                {
                    foreach (Control control in panel_Contractors.Controls[i].Controls)
                    {
                        if (control.GetType() == typeof(TextBox))
                        {
                            TextBox tb = (TextBox)control;
                            if (tb.Enabled)
                            {
                                fileList.Add(tb.Text);
                            }
                        }
                    }
                }
            }

            foreach (String file in fileList)
            {
                command += "\"" + file +"\" ";
            }
            command += "cat";
            command += " " + "output";
            command += " " + "\"" + textbox_WorkingFolder.Text + "\\" + combo_contracts.Text + " " + "TJ Report" + " " + dateTime.Text + ".pdf\"";

            Process process = new Process();
            process.StartInfo.FileName = "pdftk";
            process.StartInfo.Arguments = command;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            while (!process.StandardError.EndOfStream)
            {
                standardError += process.StandardError.ReadLine()+"\n";
            }
            process.WaitForExit();

            exitCode += process.ExitCode;
            return exitCode;
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

        private void FileCleanup()
        {
                var confirmResult = MessageBox.Show("Would you like to remove files used to build reports?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    bool tryAgain;
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
                                MessageBox.Show("File: " + fileList[i] + " was already deleted.");
                                tryAgain = false;

                            }
                            catch (IOException e)
                            {
                                MessageBox.Show("Please close " + fileList[i]);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Unknown error for " + fileList[i]);
                                tryAgain = false;
                            }
                        }
                    }

                }
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
                    textbox_WorkingFolder.Text = trimPath(openFileDialog1.FileName);
                }
            }
        }

        private String trimPath(String path)
        {
            String returnPath = "";
            string[] folders = path.Split('\\');
            for (int i = 0; i < folders.Length - 1; i++)
            {
                if (i == folders.Length - 2)
                {
                    returnPath += folders[i];
                }
                else
                {
                    returnPath += folders[i] + "\\";
                }
            }
            return returnPath;
        }

        private void textbox_WorkingFolder_TextChanged(object sender, EventArgs e)
        {
            if (!combo_contracts.Text.Equals("Select.."))
            {
                button_build.Enabled = true;
            }
        }

        private void checkbox_Custom_CheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Panel panel = (Panel)cb.Parent;

            foreach (Control control in panel.Controls)
            {
                if (control.GetType() != typeof(CheckBox))
                {
                    control.Enabled = cb.Checked;
                }
            }
        }

        private void button_build_Click(object sender, EventArgs e)
        {
            int exitCode = 0;

            if (combo_contracts.Text.Contains("LES"))
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
                
                //CURRENTLY NOT NEEDED(DANGEROUS) 11/9/18
                //FileCleanup();
            }
            else
            {

                label_Status.Text = "Failure!";
                label_Status.ForeColor = Color.Red;
                MessageBox.Show(standardError);
                standardError = "";
            }

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
                    Button senderButton = (Button)sender;
                    int index = senderButton.Parent.Controls.IndexOf(senderButton);
                    senderButton.Parent.Controls[index - 1].Text = fileName;

                    if(textbox_WorkingFolder.Text == "")
                    {
                        textbox_WorkingFolder.Text = trimPath(openFileDialog1.FileName);
                    }
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

            if (combo_contracts.Text.Contains("LES"))
            {

                DisplayLESWOs();
            }
            else if(combo_contracts.Text.Equals("Add new..."))
            {
                DisplayContractBuilder();
            }
            else
            {
                DisplayOPPD();
            }


        }

        private void LESWOCheckboxChanged(object sender, EventArgs e)
        {
            DisplayLESInput();
            label_Status.Text = "";
        }

    }
}

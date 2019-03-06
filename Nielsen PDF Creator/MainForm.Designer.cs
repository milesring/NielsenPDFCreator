namespace Nielsen_PDF_Creator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_contractbox = new System.Windows.Forms.Label();
            this.combo_contracts = new System.Windows.Forms.ComboBox();
            this.button_build = new System.Windows.Forms.Button();
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.label_date = new System.Windows.Forms.Label();
            this.panel_pdfInput = new System.Windows.Forms.Panel();
            this.label_WorkingFolder = new System.Windows.Forms.Label();
            this.textbox_WorkingFolder = new System.Windows.Forms.TextBox();
            this.button_WorkingFolder = new System.Windows.Forms.Button();
            this.label_Status = new System.Windows.Forms.Label();
            this.panel_Contractors = new System.Windows.Forms.Panel();
            this.btn_addtoQueue = new System.Windows.Forms.Button();
            this.btn_viewQueue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_contractbox
            // 
            this.label_contractbox.AutoSize = true;
            this.label_contractbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_contractbox.Location = new System.Drawing.Point(18, 39);
            this.label_contractbox.Name = "label_contractbox";
            this.label_contractbox.Size = new System.Drawing.Size(57, 16);
            this.label_contractbox.TabIndex = 1;
            this.label_contractbox.Text = "Contract";
            // 
            // combo_contracts
            // 
            this.combo_contracts.FormattingEnabled = true;
            this.combo_contracts.Location = new System.Drawing.Point(81, 39);
            this.combo_contracts.Name = "combo_contracts";
            this.combo_contracts.Size = new System.Drawing.Size(121, 21);
            this.combo_contracts.TabIndex = 3;
            this.combo_contracts.Text = "Select..";
            this.combo_contracts.SelectedIndexChanged += new System.EventHandler(this.combo_contracts_SelectedIndexChanged);
            // 
            // button_build
            // 
            this.button_build.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_build.Location = new System.Drawing.Point(674, 383);
            this.button_build.Name = "button_build";
            this.button_build.Size = new System.Drawing.Size(114, 23);
            this.button_build.TabIndex = 4;
            this.button_build.Text = "Build PDFs";
            this.button_build.UseVisualStyleBackColor = true;
            this.button_build.Click += new System.EventHandler(this.button_build_Click);
            // 
            // dateTime
            // 
            this.dateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTime.CustomFormat = "M-d-yy";
            this.dateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTime.Location = new System.Drawing.Point(721, 39);
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(67, 20);
            this.dateTime.TabIndex = 5;
            // 
            // label_date
            // 
            this.label_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_date.AutoSize = true;
            this.label_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_date.Location = new System.Drawing.Point(648, 39);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(67, 16);
            this.label_date.TabIndex = 6;
            this.label_date.Text = "PDF Date";
            // 
            // panel_pdfInput
            // 
            this.panel_pdfInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_pdfInput.Enabled = false;
            this.panel_pdfInput.Location = new System.Drawing.Point(21, 108);
            this.panel_pdfInput.Name = "panel_pdfInput";
            this.panel_pdfInput.Size = new System.Drawing.Size(314, 319);
            this.panel_pdfInput.TabIndex = 7;
            this.panel_pdfInput.Visible = false;
            // 
            // label_WorkingFolder
            // 
            this.label_WorkingFolder.AutoSize = true;
            this.label_WorkingFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_WorkingFolder.Location = new System.Drawing.Point(286, 39);
            this.label_WorkingFolder.Name = "label_WorkingFolder";
            this.label_WorkingFolder.Size = new System.Drawing.Size(100, 16);
            this.label_WorkingFolder.TabIndex = 8;
            this.label_WorkingFolder.Text = "Working Folder";
            // 
            // textbox_WorkingFolder
            // 
            this.textbox_WorkingFolder.Location = new System.Drawing.Point(392, 40);
            this.textbox_WorkingFolder.Name = "textbox_WorkingFolder";
            this.textbox_WorkingFolder.Size = new System.Drawing.Size(128, 20);
            this.textbox_WorkingFolder.TabIndex = 9;
            this.textbox_WorkingFolder.TextChanged += new System.EventHandler(this.textbox_WorkingFolder_TextChanged);
            // 
            // button_WorkingFolder
            // 
            this.button_WorkingFolder.Location = new System.Drawing.Point(526, 38);
            this.button_WorkingFolder.Name = "button_WorkingFolder";
            this.button_WorkingFolder.Size = new System.Drawing.Size(75, 23);
            this.button_WorkingFolder.TabIndex = 10;
            this.button_WorkingFolder.Text = "Browse";
            this.button_WorkingFolder.UseVisualStyleBackColor = true;
            this.button_WorkingFolder.Click += new System.EventHandler(this.button_WorkingFolder_Click);
            // 
            // label_Status
            // 
            this.label_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Status.AutoSize = true;
            this.label_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Status.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Status.Location = new System.Drawing.Point(681, 416);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(68, 25);
            this.label_Status.TabIndex = 11;
            this.label_Status.Text = "Status";
            // 
            // panel_Contractors
            // 
            this.panel_Contractors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Contractors.AutoScroll = true;
            this.panel_Contractors.Location = new System.Drawing.Point(352, 108);
            this.panel_Contractors.Name = "panel_Contractors";
            this.panel_Contractors.Size = new System.Drawing.Size(303, 319);
            this.panel_Contractors.TabIndex = 12;
            // 
            // btn_addtoQueue
            // 
            this.btn_addtoQueue.Location = new System.Drawing.Point(674, 325);
            this.btn_addtoQueue.Name = "btn_addtoQueue";
            this.btn_addtoQueue.Size = new System.Drawing.Size(114, 23);
            this.btn_addtoQueue.TabIndex = 13;
            this.btn_addtoQueue.Text = "Add to Queue";
            this.btn_addtoQueue.UseVisualStyleBackColor = true;
            this.btn_addtoQueue.Click += new System.EventHandler(this.btn_addtoQueue_Click);
            // 
            // btn_viewQueue
            // 
            this.btn_viewQueue.Location = new System.Drawing.Point(674, 354);
            this.btn_viewQueue.Name = "btn_viewQueue";
            this.btn_viewQueue.Size = new System.Drawing.Size(114, 23);
            this.btn_viewQueue.TabIndex = 14;
            this.btn_viewQueue.Text = "View Queue";
            this.btn_viewQueue.UseVisualStyleBackColor = true;
            this.btn_viewQueue.Click += new System.EventHandler(this.btn_viewQueue_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_viewQueue);
            this.Controls.Add(this.btn_addtoQueue);
            this.Controls.Add(this.panel_Contractors);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.button_WorkingFolder);
            this.Controls.Add(this.textbox_WorkingFolder);
            this.Controls.Add(this.label_WorkingFolder);
            this.Controls.Add(this.panel_pdfInput);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.dateTime);
            this.Controls.Add(this.button_build);
            this.Controls.Add(this.combo_contracts);
            this.Controls.Add(this.label_contractbox);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "Form1";
            this.Text = "Nielsen PDF Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_contractbox;
        private System.Windows.Forms.ComboBox combo_contracts;
        private System.Windows.Forms.Button button_build;
        private System.Windows.Forms.DateTimePicker dateTime;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.Panel panel_pdfInput;
        private System.Windows.Forms.Label label_WorkingFolder;
        private System.Windows.Forms.TextBox textbox_WorkingFolder;
        private System.Windows.Forms.Button button_WorkingFolder;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Panel panel_Contractors;
        private System.Windows.Forms.Button btn_addtoQueue;
        private System.Windows.Forms.Button btn_viewQueue;
    }
}


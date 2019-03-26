namespace Nielsen_PDF_Creator
{
    partial class ContractEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractEditForm));
            this.cb_Contracts = new System.Windows.Forms.ComboBox();
            this.grpBx_contractInfo = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // cb_Contracts
            // 
            this.cb_Contracts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cb_Contracts.FormattingEnabled = true;
            this.cb_Contracts.Location = new System.Drawing.Point(12, 12);
            this.cb_Contracts.Name = "cb_Contracts";
            this.cb_Contracts.Size = new System.Drawing.Size(168, 332);
            this.cb_Contracts.TabIndex = 0;
            this.cb_Contracts.SelectedIndexChanged += new System.EventHandler(this.cb_Contracts_SelectedIndexChanged);
            // 
            // grpBx_contractInfo
            // 
            this.grpBx_contractInfo.Location = new System.Drawing.Point(200, 12);
            this.grpBx_contractInfo.Name = "grpBx_contractInfo";
            this.grpBx_contractInfo.Size = new System.Drawing.Size(319, 332);
            this.grpBx_contractInfo.TabIndex = 1;
            this.grpBx_contractInfo.TabStop = false;
            this.grpBx_contractInfo.Text = "Contract";
            // 
            // ContractEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 450);
            this.Controls.Add(this.grpBx_contractInfo);
            this.Controls.Add(this.cb_Contracts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContractEditForm";
            this.Text = "Edit Contracts";
            this.Load += new System.EventHandler(this.ContractEditForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_Contracts;
        private System.Windows.Forms.GroupBox grpBx_contractInfo;
    }
}
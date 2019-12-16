namespace eRepublik_Wiki_Citizen_Page_Creator
{
    partial class frmMain
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
            this.btnGO = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cmbDrafts = new System.Windows.Forms.ComboBox();
            this.lblDraft = new System.Windows.Forms.Label();
            this.chkRomania = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnGO
            // 
            this.btnGO.Location = new System.Drawing.Point(14, 94);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(352, 23);
            this.btnGO.TabIndex = 0;
            this.btnGO.Text = "Create and Copy to Clipboard";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(119, 9);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(143, 20);
            this.txtID.TabIndex = 1;
            // 
            // cmbDrafts
            // 
            this.cmbDrafts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrafts.FormattingEnabled = true;
            this.cmbDrafts.Location = new System.Drawing.Point(50, 35);
            this.cmbDrafts.Name = "cmbDrafts";
            this.cmbDrafts.Size = new System.Drawing.Size(316, 21);
            this.cmbDrafts.TabIndex = 2;
            // 
            // lblDraft
            // 
            this.lblDraft.AutoSize = true;
            this.lblDraft.Location = new System.Drawing.Point(11, 38);
            this.lblDraft.Name = "lblDraft";
            this.lblDraft.Size = new System.Drawing.Size(33, 13);
            this.lblDraft.TabIndex = 3;
            this.lblDraft.Text = "Draft:";
            // 
            // chkRomania
            // 
            this.chkRomania.AutoSize = true;
            this.chkRomania.Checked = true;
            this.chkRomania.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRomania.Location = new System.Drawing.Point(14, 62);
            this.chkRomania.Name = "chkRomania";
            this.chkRomania.Size = new System.Drawing.Size(68, 17);
            this.chkRomania.TabIndex = 4;
            this.chkRomania.Text = "Romania";
            this.chkRomania.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 129);
            this.Controls.Add(this.chkRomania);
            this.Controls.Add(this.lblDraft);
            this.Controls.Add(this.cmbDrafts);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnGO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eRepublik Wiki Citizen Page Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.ComboBox cmbDrafts;
        private System.Windows.Forms.Label lblDraft;
        private System.Windows.Forms.CheckBox chkRomania;
    }
}


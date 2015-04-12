namespace Foole.WC3Proxy
{
    partial class ServerInfoDlg
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
            System.Windows.Forms.Label serverAddressLabel;
            System.Windows.Forms.Label versionLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerInfoDlg));
            this.serverAddressTextBox = new System.Windows.Forms.TextBox();
            this.expansionCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.versionComboBox = new System.Windows.Forms.ComboBox();
            serverAddressLabel = new System.Windows.Forms.Label();
            versionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serverAddressLabel
            // 
            serverAddressLabel.AutoSize = true;
            serverAddressLabel.Location = new System.Drawing.Point(8, 15);
            serverAddressLabel.Name = "serverAddressLabel";
            serverAddressLabel.Size = new System.Drawing.Size(81, 13);
            serverAddressLabel.TabIndex = 0;
            serverAddressLabel.Text = "Server address:";
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new System.Drawing.Point(12, 43);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new System.Drawing.Size(72, 13);
            versionLabel.TabIndex = 6;
            versionLabel.Text = "WC3 Version:";
            // 
            // serverAddressTextBox
            // 
            this.serverAddressTextBox.Location = new System.Drawing.Point(95, 12);
            this.serverAddressTextBox.Name = "serverAddressTextBox";
            this.serverAddressTextBox.Size = new System.Drawing.Size(151, 20);
            this.serverAddressTextBox.TabIndex = 1;
            // 
            // expansionCheckBox
            // 
            this.expansionCheckBox.AutoSize = true;
            this.expansionCheckBox.Location = new System.Drawing.Point(11, 73);
            this.expansionCheckBox.Name = "expansionCheckBox";
            this.expansionCheckBox.Size = new System.Drawing.Size(95, 17);
            this.expansionCheckBox.TabIndex = 2;
            this.expansionCheckBox.Text = "Frozen Throne";
            this.expansionCheckBox.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(70, 103);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(83, 28);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(159, 103);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(83, 28);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // versionComboBox
            // 
            this.versionComboBox.FormattingEnabled = true;
            this.versionComboBox.Location = new System.Drawing.Point(95, 38);
            this.versionComboBox.Name = "versionComboBox";
            this.versionComboBox.Size = new System.Drawing.Size(151, 21);
            this.versionComboBox.TabIndex = 5;
            // 
            // ServerInfoDlg
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(254, 143);
            this.Controls.Add(versionLabel);
            this.Controls.Add(this.versionComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.expansionCheckBox);
            this.Controls.Add(this.serverAddressTextBox);
            this.Controls.Add(serverAddressLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerInfoDlg";
            this.Text = "Server information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverAddressTextBox;
        private System.Windows.Forms.CheckBox expansionCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox versionComboBox;
    }
}
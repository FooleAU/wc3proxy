namespace Foole.WC3Proxy
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label gameNameLabel;
            System.Windows.Forms.Label mapLabel;
            System.Windows.Forms.Label gamePortLabel;
            System.Windows.Forms.Label serverAddressLabel;
            System.Windows.Forms.Label playerCountLabel;
            System.Windows.Forms.Label clientCountLabel;
            System.Windows.Forms.ToolStripMenuItem toolsMenu;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolsLaunchWarcraftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamePortValueLabel = new System.Windows.Forms.Label();
            this.serverAddressValueLabel = new System.Windows.Forms.Label();
            this.mapValueLabel = new System.Windows.Forms.Label();
            this.gameNameValueLabel = new System.Windows.Forms.Label();
            this.playerCountLValueLabel = new System.Windows.Forms.Label();
            this.clientCountValueLabel = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileChangeServerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.iconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.iconExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gameNameLabel = new System.Windows.Forms.Label();
            mapLabel = new System.Windows.Forms.Label();
            gamePortLabel = new System.Windows.Forms.Label();
            serverAddressLabel = new System.Windows.Forms.Label();
            playerCountLabel = new System.Windows.Forms.Label();
            clientCountLabel = new System.Windows.Forms.Label();
            toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.iconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameNameLabel
            // 
            gameNameLabel.AutoSize = true;
            gameNameLabel.Location = new System.Drawing.Point(3, 28);
            gameNameLabel.Name = "gameNameLabel";
            gameNameLabel.Size = new System.Drawing.Size(67, 13);
            gameNameLabel.TabIndex = 3;
            gameNameLabel.Text = "Game name:";
            // 
            // mapLabel
            // 
            mapLabel.AutoSize = true;
            mapLabel.Location = new System.Drawing.Point(3, 48);
            mapLabel.Name = "mapLabel";
            mapLabel.Size = new System.Drawing.Size(31, 13);
            mapLabel.TabIndex = 5;
            mapLabel.Text = "Map:";
            // 
            // gamePortLabel
            // 
            gamePortLabel.AutoSize = true;
            gamePortLabel.Location = new System.Drawing.Point(3, 68);
            gamePortLabel.Name = "gamePortLabel";
            gamePortLabel.Size = new System.Drawing.Size(59, 13);
            gamePortLabel.TabIndex = 7;
            gamePortLabel.Text = "Game port:";
            // 
            // serverAddressLabel
            // 
            serverAddressLabel.AutoSize = true;
            serverAddressLabel.Location = new System.Drawing.Point(3, 8);
            serverAddressLabel.Name = "serverAddressLabel";
            serverAddressLabel.Size = new System.Drawing.Size(81, 13);
            serverAddressLabel.TabIndex = 2;
            serverAddressLabel.Text = "Server address:";
            // 
            // playerCountLabel
            // 
            playerCountLabel.AutoSize = true;
            playerCountLabel.Location = new System.Drawing.Point(3, 88);
            playerCountLabel.Name = "playerCountLabel";
            playerCountLabel.Size = new System.Drawing.Size(44, 13);
            playerCountLabel.TabIndex = 9;
            playerCountLabel.Text = "Players:";
            // 
            // clientCountLabel
            // 
            clientCountLabel.AutoSize = true;
            clientCountLabel.Location = new System.Drawing.Point(3, 108);
            clientCountLabel.Name = "clientCountLabel";
            clientCountLabel.Size = new System.Drawing.Size(41, 13);
            clientCountLabel.TabIndex = 11;
            clientCountLabel.Text = "Clients:";
            // 
            // toolsMenu
            // 
            toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsLaunchWarcraftMenuItem});
            toolsMenu.Name = "toolsMenu";
            toolsMenu.Size = new System.Drawing.Size(48, 20);
            toolsMenu.Text = "Tools";
            // 
            // toolsLaunchWarcraftMenuItem
            // 
            this.toolsLaunchWarcraftMenuItem.Name = "toolsLaunchWarcraftMenuItem";
            this.toolsLaunchWarcraftMenuItem.Size = new System.Drawing.Size(173, 22);
            this.toolsLaunchWarcraftMenuItem.Text = "Launch Warcraft III";
            this.toolsLaunchWarcraftMenuItem.Click += new System.EventHandler(this.LaunchWarcraftMenuItem_Click);
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(clientCountLabel, 0, 6);
            tableLayoutPanel.Controls.Add(playerCountLabel, 0, 5);
            tableLayoutPanel.Controls.Add(serverAddressLabel, 0, 1);
            tableLayoutPanel.Controls.Add(this.gamePortValueLabel, 1, 4);
            tableLayoutPanel.Controls.Add(this.serverAddressValueLabel, 1, 1);
            tableLayoutPanel.Controls.Add(gamePortLabel, 0, 4);
            tableLayoutPanel.Controls.Add(gameNameLabel, 0, 2);
            tableLayoutPanel.Controls.Add(this.mapValueLabel, 1, 3);
            tableLayoutPanel.Controls.Add(this.gameNameValueLabel, 1, 2);
            tableLayoutPanel.Controls.Add(mapLabel, 0, 3);
            tableLayoutPanel.Controls.Add(this.playerCountLValueLabel, 1, 5);
            tableLayoutPanel.Controls.Add(this.clientCountValueLabel, 1, 6);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 7;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new System.Drawing.Size(343, 135);
            tableLayoutPanel.TabIndex = 9;
            // 
            // gamePortValueLabel
            // 
            this.gamePortValueLabel.AutoEllipsis = true;
            this.gamePortValueLabel.AutoSize = true;
            this.gamePortValueLabel.Location = new System.Drawing.Point(123, 68);
            this.gamePortValueLabel.Name = "gamePortValueLabel";
            this.gamePortValueLabel.Size = new System.Drawing.Size(33, 13);
            this.gamePortValueLabel.TabIndex = 8;
            this.gamePortValueLabel.Text = "(N/A)";
            // 
            // serverAddressValueLabel
            // 
            this.serverAddressValueLabel.AutoEllipsis = true;
            this.serverAddressValueLabel.AutoSize = true;
            this.serverAddressValueLabel.Location = new System.Drawing.Point(123, 8);
            this.serverAddressValueLabel.Name = "serverAddressValueLabel";
            this.serverAddressValueLabel.Size = new System.Drawing.Size(47, 13);
            this.serverAddressValueLabel.TabIndex = 4;
            this.serverAddressValueLabel.Text = "(Not set)";
            // 
            // mapValueLabel
            // 
            this.mapValueLabel.AutoEllipsis = true;
            this.mapValueLabel.AutoSize = true;
            this.mapValueLabel.Location = new System.Drawing.Point(123, 48);
            this.mapValueLabel.Name = "mapValueLabel";
            this.mapValueLabel.Size = new System.Drawing.Size(33, 13);
            this.mapValueLabel.TabIndex = 6;
            this.mapValueLabel.Text = "(N/A)";
            // 
            // gameNameValueLabel
            // 
            this.gameNameValueLabel.AutoEllipsis = true;
            this.gameNameValueLabel.AutoSize = true;
            this.gameNameValueLabel.Location = new System.Drawing.Point(123, 28);
            this.gameNameValueLabel.Name = "gameNameValueLabel";
            this.gameNameValueLabel.Size = new System.Drawing.Size(69, 13);
            this.gameNameValueLabel.TabIndex = 4;
            this.gameNameValueLabel.Text = "(None found)";
            // 
            // playerCountLValueLabel
            // 
            this.playerCountLValueLabel.AutoEllipsis = true;
            this.playerCountLValueLabel.AutoSize = true;
            this.playerCountLValueLabel.Location = new System.Drawing.Point(123, 88);
            this.playerCountLValueLabel.Name = "playerCountLValueLabel";
            this.playerCountLValueLabel.Size = new System.Drawing.Size(33, 13);
            this.playerCountLValueLabel.TabIndex = 10;
            this.playerCountLValueLabel.Text = "(N/A)";
            // 
            // clientCountValueLabel
            // 
            this.clientCountValueLabel.AutoEllipsis = true;
            this.clientCountValueLabel.AutoSize = true;
            this.clientCountValueLabel.Location = new System.Drawing.Point(123, 108);
            this.clientCountValueLabel.Name = "clientCountValueLabel";
            this.clientCountValueLabel.Size = new System.Drawing.Size(13, 13);
            this.clientCountValueLabel.TabIndex = 12;
            this.clientCountValueLabel.Text = "0";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            toolsMenu,
            this.helpMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(343, 24);
            this.mainMenu.TabIndex = 0;
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileChangeServerMenuItem,
            this.fileExitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // fileChangeServerMenuItem
            // 
            this.fileChangeServerMenuItem.Name = "fileChangeServerMenuItem";
            this.fileChangeServerMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fileChangeServerMenuItem.Text = "Change server";
            this.fileChangeServerMenuItem.Click += new System.EventHandler(this.ChangeServerMenuItem_Click);
            // 
            // fileExitMenuItem
            // 
            this.fileExitMenuItem.Name = "fileExitMenuItem";
            this.fileExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fileExitMenuItem.Text = "E&xit";
            this.fileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAboutMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // helpAboutMenuItem
            // 
            this.helpAboutMenuItem.Name = "helpAboutMenuItem";
            this.helpAboutMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helpAboutMenuItem.Text = "&About";
            this.helpAboutMenuItem.Click += new System.EventHandler(this.HelpAboutMenuItem_Click);
            // 
            // icon
            // 
            this.icon.ContextMenuStrip = this.iconMenu;
            this.icon.Icon = ((System.Drawing.Icon)(resources.GetObject("icon.Icon")));
            this.icon.Text = "WC3 Proxy";
            this.icon.Visible = true;
            this.icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Icon_MouseDoubleClick);
            // 
            // iconMenu
            // 
            this.iconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconExitMenuItem});
            this.iconMenu.Name = "mIconMenu";
            this.iconMenu.ShowImageMargin = false;
            this.iconMenu.Size = new System.Drawing.Size(68, 26);
            // 
            // iconExitMenuItem
            // 
            this.iconExitMenuItem.Name = "iconExitMenuItem";
            this.iconExitMenuItem.Size = new System.Drawing.Size(67, 22);
            this.iconExitMenuItem.Text = "Exit";
            this.iconExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 159);
            this.Controls.Add(tableLayoutPanel);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Foole\'s WC3 Proxy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.iconMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsLaunchWarcraftMenuItem;
        private System.Windows.Forms.Label gameNameValueLabel;
        private System.Windows.Forms.Label mapValueLabel;
        private System.Windows.Forms.Label gamePortValueLabel;
        private System.Windows.Forms.Label serverAddressValueLabel;
        private System.Windows.Forms.Label playerCountLValueLabel;
        private System.Windows.Forms.Label clientCountValueLabel;
        private System.Windows.Forms.NotifyIcon icon;
        private System.Windows.Forms.ContextMenuStrip iconMenu;
        private System.Windows.Forms.ToolStripMenuItem iconExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileChangeServerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem helpAboutMenuItem;
    }
}
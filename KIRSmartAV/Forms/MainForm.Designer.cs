namespace KIRSmartAV.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imgTools = new System.Windows.Forms.ImageList(this.components);
            this.imgButtonsTab = new System.Windows.Forms.ImageList(this.components);
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblAppDesc = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdSettings = new System.Windows.Forms.LinkLabel();
            this.cmdMinimize = new System.Windows.Forms.LinkLabel();
            this.cmdAbout = new System.Windows.Forms.LinkLabel();
            this.lblStat_Version = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblStat_Database = new System.Windows.Forms.Label();
            this.lblStat_QuickFix = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvTools = new KIRSmartAV.Core.Controls.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imgTools
            // 
            this.imgTools.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTools.ImageStream")));
            this.imgTools.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTools.Images.SetKeyName(0, "burn.png");
            this.imgTools.Images.SetKeyName(1, "folder-search-result.png");
            this.imgTools.Images.SetKeyName(2, "drive--plus.png");
            this.imgTools.Images.SetKeyName(3, "computer--plus.png");
            this.imgTools.Images.SetKeyName(4, "lock-warning.gif");
            // 
            // imgButtonsTab
            // 
            this.imgButtonsTab.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgButtonsTab.ImageStream")));
            this.imgButtonsTab.TransparentColor = System.Drawing.Color.Transparent;
            this.imgButtonsTab.Images.SetKeyName(0, "home.png");
            this.imgButtonsTab.Images.SetKeyName(1, "toolbox.png");
            this.imgButtonsTab.Images.SetKeyName(2, "information-italic.png");
            this.imgButtonsTab.Images.SetKeyName(3, "gear.png");
            this.imgButtonsTab.Images.SetKeyName(4, "application-dock-270.png");
            // 
            // lblAppTitle
            // 
            resources.ApplyResources(this.lblAppTitle, "lblAppTitle");
            this.lblAppTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAppTitle.ForeColor = System.Drawing.Color.Black;
            this.lblAppTitle.Name = "lblAppTitle";
            // 
            // lblAppDesc
            // 
            resources.ApplyResources(this.lblAppDesc, "lblAppDesc");
            this.lblAppDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblAppDesc.ForeColor = System.Drawing.Color.Black;
            this.lblAppDesc.Name = "lblAppDesc";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ImageList = this.imgButtonsTab;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.cmdSettings);
            this.tabPage1.Controls.Add(this.cmdMinimize);
            this.tabPage1.Controls.Add(this.cmdAbout);
            this.tabPage1.Controls.Add(this.lblStat_Version);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.lblStat_Database);
            this.tabPage1.Controls.Add(this.lblStat_QuickFix);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdSettings
            // 
            resources.ApplyResources(this.cmdSettings, "cmdSettings");
            this.cmdSettings.ImageList = this.imgButtonsTab;
            this.cmdSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.TabStop = true;
            this.cmdSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdSettings_LinkClicked);
            // 
            // cmdMinimize
            // 
            resources.ApplyResources(this.cmdMinimize, "cmdMinimize");
            this.cmdMinimize.ImageList = this.imgButtonsTab;
            this.cmdMinimize.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.cmdMinimize.Name = "cmdMinimize";
            this.cmdMinimize.TabStop = true;
            this.cmdMinimize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdMinimize_LinkClicked);
            // 
            // cmdAbout
            // 
            resources.ApplyResources(this.cmdAbout, "cmdAbout");
            this.cmdAbout.ImageList = this.imgButtonsTab;
            this.cmdAbout.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.TabStop = true;
            this.cmdAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdAbout_LinkClicked);
            // 
            // lblStat_Version
            // 
            resources.ApplyResources(this.lblStat_Version, "lblStat_Version");
            this.lblStat_Version.Name = "lblStat_Version";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lblStat_Database
            // 
            resources.ApplyResources(this.lblStat_Database, "lblStat_Database");
            this.lblStat_Database.Name = "lblStat_Database";
            // 
            // lblStat_QuickFix
            // 
            resources.ApplyResources(this.lblStat_QuickFix, "lblStat_QuickFix");
            this.lblStat_QuickFix.Name = "lblStat_QuickFix";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.lvTools);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvTools
            // 
            resources.ApplyResources(this.lvTools, "lvTools");
            this.lvTools.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvTools.LargeImageList = this.imgTools;
            this.lvTools.Name = "lvTools";
            this.lvTools.SmallImageList = this.imgTools;
            this.lvTools.UseCompatibleStateImageBehavior = false;
            this.lvTools.View = System.Windows.Forms.View.Details;
            this.lvTools.DoubleClick += new System.EventHandler(this.lvTools_DoubleClick);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::KIRSmartAV.Properties.Resources.logoSmall;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblAppTitle);
            this.Controls.Add(this.lblAppDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppDesc;
        private System.Windows.Forms.ImageList imgTools;
        private System.Windows.Forms.ImageList imgButtonsTab;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Core.Controls.ListViewEx lvTools;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblStat_Version;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStat_Database;
        private System.Windows.Forms.Label lblStat_QuickFix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel cmdSettings;
        private System.Windows.Forms.LinkLabel cmdMinimize;
        private System.Windows.Forms.LinkLabel cmdAbout;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}


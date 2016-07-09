namespace KIRSmartAV.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.chkRunOnStartup = new System.Windows.Forms.CheckBox();
            this.chkEnableQuickFix = new System.Windows.Forms.CheckBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVirusChestDir = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.LinkLabel();
            this.chkElevationManifest = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdUnset = new System.Windows.Forms.LinkLabel();
            this.chkProcessSubdir = new System.Windows.Forms.CheckBox();
            this.pnQuickFix = new System.Windows.Forms.Panel();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.pnQuickFix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkRunOnStartup
            // 
            resources.ApplyResources(this.chkRunOnStartup, "chkRunOnStartup");
            this.chkRunOnStartup.Name = "chkRunOnStartup";
            this.chkRunOnStartup.UseVisualStyleBackColor = true;
            // 
            // chkEnableQuickFix
            // 
            resources.ApplyResources(this.chkEnableQuickFix, "chkEnableQuickFix");
            this.chkEnableQuickFix.Name = "chkEnableQuickFix";
            this.chkEnableQuickFix.UseVisualStyleBackColor = true;
            this.chkEnableQuickFix.CheckedChanged += new System.EventHandler(this.chkEnableQuickFix_CheckedChanged);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtVirusChestDir
            // 
            resources.ApplyResources(this.txtVirusChestDir, "txtVirusChestDir");
            this.txtVirusChestDir.Name = "txtVirusChestDir";
            this.txtVirusChestDir.ReadOnly = true;
            // 
            // cmdBrowse
            // 
            resources.ApplyResources(this.cmdBrowse, "cmdBrowse");
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.TabStop = true;
            this.cmdBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdBrowse_LinkClicked);
            // 
            // chkElevationManifest
            // 
            resources.ApplyResources(this.chkElevationManifest, "chkElevationManifest");
            this.chkElevationManifest.Name = "chkElevationManifest";
            this.chkElevationManifest.UseVisualStyleBackColor = true;
            this.chkElevationManifest.CheckedChanged += new System.EventHandler(this.chkElevationManifest_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Name = "label2";
            // 
            // fbd
            // 
            resources.ApplyResources(this.fbd, "fbd");
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdUnset
            // 
            resources.ApplyResources(this.cmdUnset, "cmdUnset");
            this.cmdUnset.Name = "cmdUnset";
            this.cmdUnset.TabStop = true;
            this.cmdUnset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdUnset_LinkClicked);
            // 
            // chkProcessSubdir
            // 
            resources.ApplyResources(this.chkProcessSubdir, "chkProcessSubdir");
            this.chkProcessSubdir.Name = "chkProcessSubdir";
            this.chkProcessSubdir.UseVisualStyleBackColor = true;
            // 
            // pnQuickFix
            // 
            resources.ApplyResources(this.pnQuickFix, "pnQuickFix");
            this.pnQuickFix.Controls.Add(this.pictureBox6);
            this.pnQuickFix.Controls.Add(this.chkProcessSubdir);
            this.pnQuickFix.Name = "pnQuickFix";
            // 
            // pictureBox6
            // 
            resources.ApplyResources(this.pictureBox6, "pictureBox6");
            this.pictureBox6.Image = global::KIRSmartAV.imageResources.folder_tree;
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            resources.ApplyResources(this.pictureBox7, "pictureBox7");
            this.pictureBox7.Image = global::KIRSmartAV.imageResources.locale_alternate;
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox5
            // 
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Image = global::KIRSmartAV.imageResources.exclamation;
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Image = global::KIRSmartAV.imageResources.folder_exclamation;
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::KIRSmartAV.imageResources.geolocation;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::KIRSmartAV.imageResources.application_run;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cboLanguage
            // 
            resources.ApplyResources(this.cboLanguage, "cboLanguage");
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Items.AddRange(new object[] {
            resources.GetString("cboLanguage.Items"),
            resources.GetString("cboLanguage.Items1")});
            this.cboLanguage.Name = "cboLanguage";
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pnQuickFix);
            this.Controls.Add(this.cmdUnset);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.chkElevationManifest);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.txtVirusChestDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chkEnableQuickFix);
            this.Controls.Add(this.chkRunOnStartup);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.pnQuickFix.ResumeLayout(false);
            this.pnQuickFix.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRunOnStartup;
        private System.Windows.Forms.CheckBox chkEnableQuickFix;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVirusChestDir;
        private System.Windows.Forms.LinkLabel cmdBrowse;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.CheckBox chkElevationManifest;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.LinkLabel cmdUnset;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.CheckBox chkProcessSubdir;
        private System.Windows.Forms.Panel pnQuickFix;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboLanguage;
    }
}
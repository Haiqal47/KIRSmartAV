namespace KIRSmartAV.ToolsForms
{
    partial class frmKProtect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKProtect));
            this.imgTexts = new System.Windows.Forms.ImageList(this.components);
            this.cmdApplyHotfix = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmdRegeditToggle = new System.Windows.Forms.LinkLabel();
            this.cmdAutorunToggle = new System.Windows.Forms.LinkLabel();
            this.lblRegedit = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAutorun = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdIniToggle = new System.Windows.Forms.LinkLabel();
            this.lbINI = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imgTexts
            // 
            this.imgTexts.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTexts.ImageStream")));
            this.imgTexts.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTexts.Images.SetKeyName(0, "circled_play-24.png");
            this.imgTexts.Images.SetKeyName(1, "code_file-24.png");
            this.imgTexts.Images.SetKeyName(2, "code_file-24.png");
            this.imgTexts.Images.SetKeyName(3, "registry_editor-24.png");
            // 
            // cmdApplyHotfix
            // 
            resources.ApplyResources(this.cmdApplyHotfix, "cmdApplyHotfix");
            this.cmdApplyHotfix.Name = "cmdApplyHotfix";
            this.cmdApplyHotfix.TabStop = true;
            this.cmdApplyHotfix.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdApplyHotfix_LinkClicked);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ImageList = this.imgTexts;
            this.label13.Name = "label13";
            // 
            // cmdRegeditToggle
            // 
            resources.ApplyResources(this.cmdRegeditToggle, "cmdRegeditToggle");
            this.cmdRegeditToggle.Name = "cmdRegeditToggle";
            this.cmdRegeditToggle.TabStop = true;
            this.cmdRegeditToggle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdRegeditToggle_LinkClicked);
            // 
            // cmdAutorunToggle
            // 
            resources.ApplyResources(this.cmdAutorunToggle, "cmdAutorunToggle");
            this.cmdAutorunToggle.Name = "cmdAutorunToggle";
            this.cmdAutorunToggle.TabStop = true;
            this.cmdAutorunToggle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdAutorunToggle_LinkClicked);
            // 
            // lblRegedit
            // 
            resources.ApplyResources(this.lblRegedit, "lblRegedit");
            this.lblRegedit.Name = "lblRegedit";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // lblAutorun
            // 
            resources.ApplyResources(this.lblAutorun, "lblAutorun");
            this.lblAutorun.Name = "lblAutorun";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ImageList = this.imgTexts;
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageList = this.imgTexts;
            this.label3.Name = "label3";
            // 
            // cmdIniToggle
            // 
            resources.ApplyResources(this.cmdIniToggle, "cmdIniToggle");
            this.cmdIniToggle.Name = "cmdIniToggle";
            this.cmdIniToggle.TabStop = true;
            this.cmdIniToggle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdIniToggle_LinkClicked);
            // 
            // lbINI
            // 
            resources.ApplyResources(this.lbINI, "lbINI");
            this.lbINI.Name = "lbINI";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ImageList = this.imgTexts;
            this.label4.Name = "label4";
            // 
            // frmKProtect
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdIniToggle);
            this.Controls.Add(this.lbINI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdApplyHotfix);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmdRegeditToggle);
            this.Controls.Add(this.cmdAutorunToggle);
            this.Controls.Add(this.lblRegedit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblAutorun);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKProtect";
            this.Load += new System.EventHandler(this.frmKProtect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imgTexts;
        private System.Windows.Forms.LinkLabel cmdApplyHotfix;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.LinkLabel cmdRegeditToggle;
        private System.Windows.Forms.LinkLabel cmdAutorunToggle;
        private System.Windows.Forms.Label lblRegedit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblAutorun;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel cmdIniToggle;
        private System.Windows.Forms.Label lbINI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}
namespace KIRSmartAV.ToolsForms
{
    partial class frmKCure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKCure));
            this.cmdPerbarui = new System.Windows.Forms.LinkLabel();
            this.cboDiskalepas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSafeChestToggle = new System.Windows.Forms.LinkLabel();
            this.lblSafeChest = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.imgTexts = new System.Windows.Forms.ImageList(this.components);
            this.cmdAutorunToggle = new System.Windows.Forms.LinkLabel();
            this.lblAutorun = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdRecyclerToggle = new System.Windows.Forms.LinkLabel();
            this.lblRecycler = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdPerbarui
            // 
            resources.ApplyResources(this.cmdPerbarui, "cmdPerbarui");
            this.cmdPerbarui.Name = "cmdPerbarui";
            this.cmdPerbarui.TabStop = true;
            this.cmdPerbarui.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdPerbarui_LinkClicked);
            // 
            // cboDiskalepas
            // 
            resources.ApplyResources(this.cboDiskalepas, "cboDiskalepas");
            this.cboDiskalepas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiskalepas.FormattingEnabled = true;
            this.cboDiskalepas.Name = "cboDiskalepas";
            this.cboDiskalepas.SelectedIndexChanged += new System.EventHandler(this.cboDiskalepas_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmdSafeChestToggle
            // 
            resources.ApplyResources(this.cmdSafeChestToggle, "cmdSafeChestToggle");
            this.cmdSafeChestToggle.Name = "cmdSafeChestToggle";
            this.cmdSafeChestToggle.TabStop = true;
            this.cmdSafeChestToggle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdSafeChestToggle_LinkClicked);
            // 
            // lblSafeChest
            // 
            resources.ApplyResources(this.lblSafeChest, "lblSafeChest");
            this.lblSafeChest.Name = "lblSafeChest";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ImageList = this.imgTexts;
            this.label11.Name = "label11";
            // 
            // imgTexts
            // 
            this.imgTexts.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTexts.ImageStream")));
            this.imgTexts.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTexts.Images.SetKeyName(0, "full_trash-24.png");
            this.imgTexts.Images.SetKeyName(1, "external_link-24.png");
            this.imgTexts.Images.SetKeyName(2, "likes_folder-24.png");
            // 
            // cmdAutorunToggle
            // 
            resources.ApplyResources(this.cmdAutorunToggle, "cmdAutorunToggle");
            this.cmdAutorunToggle.Name = "cmdAutorunToggle";
            this.cmdAutorunToggle.TabStop = true;
            this.cmdAutorunToggle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdAutorunToggle_LinkClicked);
            // 
            // lblAutorun
            // 
            resources.ApplyResources(this.lblAutorun, "lblAutorun");
            this.lblAutorun.Name = "lblAutorun";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ImageList = this.imgTexts;
            this.label8.Name = "label8";
            // 
            // cmdRecyclerToggle
            // 
            resources.ApplyResources(this.cmdRecyclerToggle, "cmdRecyclerToggle");
            this.cmdRecyclerToggle.Name = "cmdRecyclerToggle";
            this.cmdRecyclerToggle.TabStop = true;
            this.cmdRecyclerToggle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdRecyclerToggle_LinkClicked);
            // 
            // lblRecycler
            // 
            resources.ApplyResources(this.lblRecycler, "lblRecycler");
            this.lblRecycler.Name = "lblRecycler";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ImageList = this.imgTexts;
            this.label4.Name = "label4";
            // 
            // frmKCure
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdSafeChestToggle);
            this.Controls.Add(this.lblSafeChest);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmdAutorunToggle);
            this.Controls.Add(this.lblAutorun);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmdRecyclerToggle);
            this.Controls.Add(this.lblRecycler);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdPerbarui);
            this.Controls.Add(this.cboDiskalepas);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKCure";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel cmdPerbarui;
        private System.Windows.Forms.ComboBox cboDiskalepas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel cmdSafeChestToggle;
        private System.Windows.Forms.Label lblSafeChest;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.LinkLabel cmdAutorunToggle;
        private System.Windows.Forms.Label lblAutorun;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel cmdRecyclerToggle;
        private System.Windows.Forms.Label lblRecycler;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ImageList imgTexts;
    }
}
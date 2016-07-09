namespace KIRSmartAV.ToolsForms
{
    partial class frmKUnhide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKUnhide));
            this.label1 = new System.Windows.Forms.Label();
            this.cboDiskalepas = new System.Windows.Forms.ComboBox();
            this.cmdPerbaiki = new System.Windows.Forms.Button();
            this.imgButtons = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUsed = new System.Windows.Forms.Label();
            this.lblFree = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.cmdPerbarui = new System.Windows.Forms.LinkLabel();
            this.prgProgress = new System.Windows.Forms.ProgressBar();
            this.capacityChart1 = new KIRSmartAV.Core.Controls.CapacityChart();
            this.bwPerbaiki = new System.ComponentModel.BackgroundWorker();
            this.chkFullCheck = new System.Windows.Forms.CheckBox();
            this.chkHapusShortcut = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cboDiskalepas
            // 
            resources.ApplyResources(this.cboDiskalepas, "cboDiskalepas");
            this.cboDiskalepas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiskalepas.FormattingEnabled = true;
            this.cboDiskalepas.Name = "cboDiskalepas";
            this.cboDiskalepas.SelectedIndexChanged += new System.EventHandler(this.cboDiskalepas_SelectedIndexChanged);
            // 
            // cmdPerbaiki
            // 
            resources.ApplyResources(this.cmdPerbaiki, "cmdPerbaiki");
            this.cmdPerbaiki.ImageList = this.imgButtons;
            this.cmdPerbaiki.Name = "cmdPerbaiki";
            this.cmdPerbaiki.UseVisualStyleBackColor = true;
            this.cmdPerbaiki.Click += new System.EventHandler(this.cmdPerbaiki_Click);
            // 
            // imgButtons
            // 
            this.imgButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgButtons.ImageStream")));
            this.imgButtons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgButtons.Images.SetKeyName(0, "folder-smiley.png");
            this.imgButtons.Images.SetKeyName(1, "cross-octagon.png");
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblUsed
            // 
            resources.ApplyResources(this.lblUsed, "lblUsed");
            this.lblUsed.ForeColor = System.Drawing.Color.Blue;
            this.lblUsed.Name = "lblUsed";
            // 
            // lblFree
            // 
            resources.ApplyResources(this.lblFree, "lblFree");
            this.lblFree.ForeColor = System.Drawing.Color.Magenta;
            this.lblFree.Name = "lblFree";
            // 
            // lblTotal
            // 
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.Name = "lblTotal";
            // 
            // cmdPerbarui
            // 
            resources.ApplyResources(this.cmdPerbarui, "cmdPerbarui");
            this.cmdPerbarui.Name = "cmdPerbarui";
            this.cmdPerbarui.TabStop = true;
            this.cmdPerbarui.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdPerbarui_LinkClicked);
            // 
            // prgProgress
            // 
            resources.ApplyResources(this.prgProgress, "prgProgress");
            this.prgProgress.Name = "prgProgress";
            // 
            // capacityChart1
            // 
            resources.ApplyResources(this.capacityChart1, "capacityChart1");
            this.capacityChart1.Name = "capacityChart1";
            // 
            // bwPerbaiki
            // 
            this.bwPerbaiki.WorkerReportsProgress = true;
            this.bwPerbaiki.WorkerSupportsCancellation = true;
            this.bwPerbaiki.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwPerbaiki_DoWork);
            this.bwPerbaiki.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwPerbaiki_ProgressChanged);
            this.bwPerbaiki.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwPerbaiki_RunWorkerCompleted);
            // 
            // chkFullCheck
            // 
            resources.ApplyResources(this.chkFullCheck, "chkFullCheck");
            this.chkFullCheck.Name = "chkFullCheck";
            this.chkFullCheck.UseVisualStyleBackColor = true;
            // 
            // chkHapusShortcut
            // 
            resources.ApplyResources(this.chkHapusShortcut, "chkHapusShortcut");
            this.chkHapusShortcut.Name = "chkHapusShortcut";
            this.chkHapusShortcut.UseVisualStyleBackColor = true;
            // 
            // frmKUnhide
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkHapusShortcut);
            this.Controls.Add(this.chkFullCheck);
            this.Controls.Add(this.prgProgress);
            this.Controls.Add(this.cmdPerbarui);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblFree);
            this.Controls.Add(this.lblUsed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.capacityChart1);
            this.Controls.Add(this.cmdPerbaiki);
            this.Controls.Add(this.cboDiskalepas);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKUnhide";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDiskalepas;
        private System.Windows.Forms.Button cmdPerbaiki;
        private System.Windows.Forms.ImageList imgButtons;
        private Core.Controls.CapacityChart capacityChart1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUsed;
        private System.Windows.Forms.Label lblFree;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.LinkLabel cmdPerbarui;
        private System.Windows.Forms.ProgressBar prgProgress;
        private System.ComponentModel.BackgroundWorker bwPerbaiki;
        private System.Windows.Forms.CheckBox chkFullCheck;
        private System.Windows.Forms.CheckBox chkHapusShortcut;
    }
}
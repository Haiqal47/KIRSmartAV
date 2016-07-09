namespace KIRSmartAV.ToolsForms
{
    partial class frmKClamAV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKClamAV));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblVirname = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.prgStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmdStartScan = new System.Windows.Forms.Button();
            this.imgButtons = new System.Windows.Forms.ImageList(this.components);
            this.cmdStopScan = new System.Windows.Forms.Button();
            this.imgListView = new System.Windows.Forms.ImageList(this.components);
            this.lvDeteksi = new KIRSmartAV.Core.Controls.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bwPindai = new System.ComponentModel.BackgroundWorker();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.bwPerbaiki = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            // 
            // cmdBrowse
            // 
            resources.ApplyResources(this.cmdBrowse, "cmdBrowse");
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.TabStop = true;
            this.cmdBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdBrowse_LinkClicked);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.lblFile);
            this.groupBox1.Controls.Add(this.lblVirname);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblFilePath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lblFile
            // 
            resources.ApplyResources(this.lblFile, "lblFile");
            this.lblFile.Name = "lblFile";
            // 
            // lblVirname
            // 
            resources.ApplyResources(this.lblVirname, "lblVirname");
            this.lblVirname.Name = "lblVirname";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblFilePath
            // 
            resources.ApplyResources(this.lblFilePath, "lblFilePath");
            this.lblFilePath.BackColor = System.Drawing.SystemColors.Control;
            this.lblFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblFilePath.Name = "lblFilePath";
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
            // prgStatus
            // 
            resources.ApplyResources(this.prgStatus, "prgStatus");
            this.prgStatus.Name = "prgStatus";
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            // 
            // cmdStartScan
            // 
            resources.ApplyResources(this.cmdStartScan, "cmdStartScan");
            this.cmdStartScan.ImageList = this.imgButtons;
            this.cmdStartScan.Name = "cmdStartScan";
            this.cmdStartScan.UseVisualStyleBackColor = true;
            this.cmdStartScan.Click += new System.EventHandler(this.cmdStartScan_Click);
            // 
            // imgButtons
            // 
            this.imgButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgButtons.ImageStream")));
            this.imgButtons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgButtons.Images.SetKeyName(0, "control.png");
            this.imgButtons.Images.SetKeyName(1, "control-stop-square.png");
            this.imgButtons.Images.SetKeyName(2, "tick-shield.png");
            // 
            // cmdStopScan
            // 
            resources.ApplyResources(this.cmdStopScan, "cmdStopScan");
            this.cmdStopScan.ImageList = this.imgButtons;
            this.cmdStopScan.Name = "cmdStopScan";
            this.cmdStopScan.UseVisualStyleBackColor = true;
            this.cmdStopScan.Click += new System.EventHandler(this.cmdStopScan_Click);
            // 
            // imgListView
            // 
            this.imgListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListView.ImageStream")));
            this.imgListView.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListView.Images.SetKeyName(0, "bug.png");
            this.imgListView.Images.SetKeyName(1, "plus-shield.png");
            this.imgListView.Images.SetKeyName(2, "cross-shield.png");
            // 
            // lvDeteksi
            // 
            resources.ApplyResources(this.lvDeteksi, "lvDeteksi");
            this.lvDeteksi.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvDeteksi.FullRowSelect = true;
            this.lvDeteksi.GridLines = true;
            this.lvDeteksi.LargeImageList = this.imgListView;
            this.lvDeteksi.Name = "lvDeteksi";
            this.lvDeteksi.SmallImageList = this.imgListView;
            this.lvDeteksi.UseCompatibleStateImageBehavior = false;
            this.lvDeteksi.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // bwPindai
            // 
            this.bwPindai.WorkerReportsProgress = true;
            this.bwPindai.WorkerSupportsCancellation = true;
            this.bwPindai.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwPindai_DoWork);
            this.bwPindai.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwPindai_ProgressChanged);
            this.bwPindai.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwPindai_RunWorkerCompleted);
            // 
            // fbd
            // 
            resources.ApplyResources(this.fbd, "fbd");
            this.fbd.ShowNewFolderButton = false;
            // 
            // bwPerbaiki
            // 
            this.bwPerbaiki.WorkerReportsProgress = true;
            this.bwPerbaiki.WorkerSupportsCancellation = true;
            this.bwPerbaiki.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwPerbaiki_DoWork);
            this.bwPerbaiki.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwPerbaiki_ProgressChanged);
            this.bwPerbaiki.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwPerbaiki_RunWorkerCompleted);
            // 
            // frmKClamAV
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdStopScan);
            this.Controls.Add(this.cmdStartScan);
            this.Controls.Add(this.lvDeteksi);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.prgStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmKClamAV";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.LinkLabel cmdBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblVirname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lblFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar prgStatus;
        private System.Windows.Forms.Label lblStatus;
        private Core.Controls.ListViewEx lvDeteksi;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imgListView;
        private System.Windows.Forms.Button cmdStartScan;
        private System.Windows.Forms.ImageList imgButtons;
        private System.Windows.Forms.Button cmdStopScan;
        private System.ComponentModel.BackgroundWorker bwPindai;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.ComponentModel.BackgroundWorker bwPerbaiki;
    }
}
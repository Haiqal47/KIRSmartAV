namespace KIRSmartAV.ToolsForms
{
    partial class frmKCryptor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKCryptor));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdOutputEncode = new System.Windows.Forms.LinkLabel();
            this.txtOutputEncode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdInputEncode = new System.Windows.Forms.LinkLabel();
            this.txtInputEncode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmdDecodeOutput = new System.Windows.Forms.LinkLabel();
            this.txtOutputDecode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdDecodeInput = new System.Windows.Forms.LinkLabel();
            this.txtInputDecode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.cmdExecute = new System.Windows.Forms.Button();
            this.prgStatus = new System.Windows.Forms.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdOutputEncode);
            this.tabPage1.Controls.Add(this.txtOutputEncode);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cmdInputEncode);
            this.tabPage1.Controls.Add(this.txtInputEncode);
            this.tabPage1.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Tag = "ENCODE";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdOutputEncode
            // 
            resources.ApplyResources(this.cmdOutputEncode, "cmdOutputEncode");
            this.cmdOutputEncode.Name = "cmdOutputEncode";
            this.cmdOutputEncode.TabStop = true;
            this.cmdOutputEncode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdOutputEncode_LinkClicked);
            // 
            // txtOutputEncode
            // 
            resources.ApplyResources(this.txtOutputEncode, "txtOutputEncode");
            this.txtOutputEncode.Name = "txtOutputEncode";
            this.txtOutputEncode.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmdInputEncode
            // 
            resources.ApplyResources(this.cmdInputEncode, "cmdInputEncode");
            this.cmdInputEncode.Name = "cmdInputEncode";
            this.cmdInputEncode.TabStop = true;
            this.cmdInputEncode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdInputEncode_LinkClicked);
            // 
            // txtInputEncode
            // 
            resources.ApplyResources(this.txtInputEncode, "txtInputEncode");
            this.txtInputEncode.Name = "txtInputEncode";
            this.txtInputEncode.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cmdDecodeOutput);
            this.tabPage2.Controls.Add(this.txtOutputDecode);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cmdDecodeInput);
            this.tabPage2.Controls.Add(this.txtInputDecode);
            this.tabPage2.Controls.Add(this.label4);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Tag = "DECODE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmdDecodeOutput
            // 
            resources.ApplyResources(this.cmdDecodeOutput, "cmdDecodeOutput");
            this.cmdDecodeOutput.Name = "cmdDecodeOutput";
            this.cmdDecodeOutput.TabStop = true;
            this.cmdDecodeOutput.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdDecodeOutput_LinkClicked);
            // 
            // txtOutputDecode
            // 
            resources.ApplyResources(this.txtOutputDecode, "txtOutputDecode");
            this.txtOutputDecode.Name = "txtOutputDecode";
            this.txtOutputDecode.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmdDecodeInput
            // 
            resources.ApplyResources(this.cmdDecodeInput, "cmdDecodeInput");
            this.cmdDecodeInput.Name = "cmdDecodeInput";
            this.cmdDecodeInput.TabStop = true;
            this.cmdDecodeInput.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdDecodeInput_LinkClicked);
            // 
            // txtInputDecode
            // 
            resources.ApplyResources(this.txtInputDecode, "txtInputDecode");
            this.txtInputDecode.Name = "txtInputDecode";
            this.txtInputDecode.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ofd
            // 
            this.ofd.SupportMultiDottedExtensions = true;
            // 
            // sfd
            // 
            this.sfd.SupportMultiDottedExtensions = true;
            // 
            // cmdExecute
            // 
            resources.ApplyResources(this.cmdExecute, "cmdExecute");
            this.cmdExecute.Name = "cmdExecute";
            this.cmdExecute.Tag = "START";
            this.cmdExecute.UseVisualStyleBackColor = true;
            this.cmdExecute.Click += new System.EventHandler(this.cmdExecute_Click);
            // 
            // prgStatus
            // 
            resources.ApplyResources(this.prgStatus, "prgStatus");
            this.prgStatus.Name = "prgStatus";
            // 
            // frmKCryptor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.prgStatus);
            this.Controls.Add(this.cmdExecute);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmKCryptor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.LinkLabel cmdOutputEncode;
        private System.Windows.Forms.TextBox txtOutputEncode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel cmdInputEncode;
        private System.Windows.Forms.TextBox txtInputEncode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel cmdDecodeOutput;
        private System.Windows.Forms.TextBox txtOutputDecode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel cmdDecodeInput;
        private System.Windows.Forms.TextBox txtInputDecode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.Button cmdExecute;
        private System.Windows.Forms.ProgressBar prgStatus;
    }
}
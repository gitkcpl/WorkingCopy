namespace TaxProGSTApiWinFormsDemo
{
    partial class frmAPISetting
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
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAPISetting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDSCMsg = new System.Windows.Forms.Label();
            this.lnkReqASPAc = new System.Windows.Forms.LinkLabel();
            this.btnGenASPDSC = new System.Windows.Forms.Button();
            this.txtDSCPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.txtAspPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEncASPSecret = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAspAcPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAspUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDownLoadApiSetting = new System.Windows.Forms.Button();
            this.btnSandboxSetting = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtUrlAspUtil = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtUrlLedger = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUrlReturn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUrlAuth = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAspWebsite = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtASPName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.btnRegenerateAspDscCertificate = new System.Windows.Forms.Button();
            this.btnGetApiBal = new System.Windows.Forms.Button();
            this.lblAPIMsg = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDSCMsg);
            this.groupBox1.Controls.Add(this.lnkReqASPAc);
            this.groupBox1.Controls.Add(this.btnGenASPDSC);
            this.groupBox1.Controls.Add(this.txtDSCPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(15, 65);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1071, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "One Time ASP Registration";
            // 
            // lblDSCMsg
            // 
            this.lblDSCMsg.AutoSize = true;
            this.lblDSCMsg.ForeColor = System.Drawing.Color.Blue;
            this.lblDSCMsg.Location = new System.Drawing.Point(432, 77);
            this.lblDSCMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDSCMsg.Name = "lblDSCMsg";
            this.lblDSCMsg.Size = new System.Drawing.Size(0, 17);
            this.lblDSCMsg.TabIndex = 4;
            // 
            // lnkReqASPAc
            // 
            this.lnkReqASPAc.AutoSize = true;
            this.lnkReqASPAc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkReqASPAc.Location = new System.Drawing.Point(8, 77);
            this.lnkReqASPAc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkReqASPAc.Name = "lnkReqASPAc";
            this.lnkReqASPAc.Size = new System.Drawing.Size(353, 20);
            this.lnkReqASPAc.TabIndex = 3;
            this.lnkReqASPAc.TabStop = true;
            this.lnkReqASPAc.Text = "Request ASP Account  (For GST API Access)";
            this.lnkReqASPAc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReqASPAc_LinkClicked);
            // 
            // btnGenASPDSC
            // 
            this.btnGenASPDSC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenASPDSC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGenASPDSC.Location = new System.Drawing.Point(489, 29);
            this.btnGenASPDSC.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenASPDSC.Name = "btnGenASPDSC";
            this.btnGenASPDSC.Size = new System.Drawing.Size(340, 34);
            this.btnGenASPDSC.TabIndex = 2;
            this.btnGenASPDSC.Text = "&Generate ASP Self Signed Cert.**";
            this.btnGenASPDSC.UseVisualStyleBackColor = true;
            this.btnGenASPDSC.Click += new System.EventHandler(this.btnGenASPDSC_Click);
            // 
            // txtDSCPassword
            // 
            this.txtDSCPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDSCPassword.Location = new System.Drawing.Point(304, 31);
            this.txtDSCPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtDSCPassword.Name = "txtDSCPassword";
            this.txtDSCPassword.Size = new System.Drawing.Size(175, 27);
            this.txtDSCPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Self Sign Certificate Password:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDecrypt);
            this.groupBox2.Controls.Add(this.txtAspPassword);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtEncASPSecret);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtAspAcPassword);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtAspUserId);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox2.Location = new System.Drawing.Point(15, 176);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1071, 169);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ASP Details";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecrypt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDecrypt.Location = new System.Drawing.Point(262, 128);
            this.btnDecrypt.Margin = new System.Windows.Forms.Padding(4);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(184, 34);
            this.btnDecrypt.TabIndex = 7;
            this.btnDecrypt.Text = "Decrypt ASP Key";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // txtAspPassword
            // 
            this.txtAspPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAspPassword.Location = new System.Drawing.Point(454, 130);
            this.txtAspPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtAspPassword.Name = "txtAspPassword";
            this.txtAspPassword.Size = new System.Drawing.Size(608, 27);
            this.txtAspPassword.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(8, 134);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(206, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "ASP Sec Key* (Password)";
            // 
            // txtEncASPSecret
            // 
            this.txtEncASPSecret.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEncASPSecret.Location = new System.Drawing.Point(454, 96);
            this.txtEncASPSecret.Margin = new System.Windows.Forms.Padding(4);
            this.txtEncASPSecret.Name = "txtEncASPSecret";
            this.txtEncASPSecret.Size = new System.Drawing.Size(608, 27);
            this.txtEncASPSecret.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(8, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(346, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Paste ASP Key (Encrypted) received in eMail";
            // 
            // txtAspAcPassword
            // 
            this.txtAspAcPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAspAcPassword.Location = new System.Drawing.Point(454, 66);
            this.txtAspAcPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtAspAcPassword.Name = "txtAspAcPassword";
            this.txtAspAcPassword.Size = new System.Drawing.Size(175, 27);
            this.txtAspAcPassword.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(8, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "ASP Account Password *";
            // 
            // txtAspUserId
            // 
            this.txtAspUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAspUserId.Location = new System.Drawing.Point(454, 36);
            this.txtAspUserId.Margin = new System.Windows.Forms.Padding(4);
            this.txtAspUserId.Name = "txtAspUserId";
            this.txtAspUserId.Size = new System.Drawing.Size(175, 27);
            this.txtAspUserId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "ASP User ID *";
            // 
            // btnDownLoadApiSetting
            // 
            this.btnDownLoadApiSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownLoadApiSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDownLoadApiSetting.Location = new System.Drawing.Point(89, 354);
            this.btnDownLoadApiSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownLoadApiSetting.Name = "btnDownLoadApiSetting";
            this.btnDownLoadApiSetting.Size = new System.Drawing.Size(346, 36);
            this.btnDownLoadApiSetting.TabIndex = 2;
            this.btnDownLoadApiSetting.Text = "Download &Production API Settings";
            this.btnDownLoadApiSetting.UseVisualStyleBackColor = true;
            this.btnDownLoadApiSetting.Click += new System.EventHandler(this.btnDownLoadApiSetting_Click);
            // 
            // btnSandboxSetting
            // 
            this.btnSandboxSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSandboxSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSandboxSetting.Location = new System.Drawing.Point(504, 354);
            this.btnSandboxSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnSandboxSetting.Name = "btnSandboxSetting";
            this.btnSandboxSetting.Size = new System.Drawing.Size(345, 36);
            this.btnSandboxSetting.TabIndex = 3;
            this.btnSandboxSetting.Text = "&Sandbox API Settings";
            this.btnSandboxSetting.UseVisualStyleBackColor = true;
            this.btnSandboxSetting.Click += new System.EventHandler(this.btnSandboxSetting_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtUrlAspUtil);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtUrlLedger);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtUrlReturn);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtUrlAuth);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtAspWebsite);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtASPName);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox3.Location = new System.Drawing.Point(15, 396);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1071, 199);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "API URL Settings";
            // 
            // txtUrlAspUtil
            // 
            this.txtUrlAspUtil.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrlAspUtil.Location = new System.Drawing.Point(161, 165);
            this.txtUrlAspUtil.Margin = new System.Windows.Forms.Padding(4);
            this.txtUrlAspUtil.Name = "txtUrlAspUtil";
            this.txtUrlAspUtil.Size = new System.Drawing.Size(900, 27);
            this.txtUrlAspUtil.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.Location = new System.Drawing.Point(8, 169);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Asp Api URL *";
            // 
            // txtUrlLedger
            // 
            this.txtUrlLedger.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrlLedger.Location = new System.Drawing.Point(161, 136);
            this.txtUrlLedger.Margin = new System.Windows.Forms.Padding(4);
            this.txtUrlLedger.Name = "txtUrlLedger";
            this.txtUrlLedger.Size = new System.Drawing.Size(900, 27);
            this.txtUrlLedger.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(8, 140);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "Ledger URL *";
            // 
            // txtUrlReturn
            // 
            this.txtUrlReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrlReturn.Location = new System.Drawing.Point(161, 107);
            this.txtUrlReturn.Margin = new System.Windows.Forms.Padding(4);
            this.txtUrlReturn.Name = "txtUrlReturn";
            this.txtUrlReturn.Size = new System.Drawing.Size(900, 27);
            this.txtUrlReturn.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(8, 111);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "Return URL *";
            // 
            // txtUrlAuth
            // 
            this.txtUrlAuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrlAuth.Location = new System.Drawing.Point(161, 77);
            this.txtUrlAuth.Margin = new System.Windows.Forms.Padding(4);
            this.txtUrlAuth.Name = "txtUrlAuth";
            this.txtUrlAuth.Size = new System.Drawing.Size(900, 27);
            this.txtUrlAuth.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(8, 81);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "Auth URL *";
            // 
            // txtAspWebsite
            // 
            this.txtAspWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAspWebsite.Location = new System.Drawing.Point(161, 49);
            this.txtAspWebsite.Margin = new System.Windows.Forms.Padding(4);
            this.txtAspWebsite.Name = "txtAspWebsite";
            this.txtAspWebsite.Size = new System.Drawing.Size(900, 27);
            this.txtAspWebsite.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(8, 52);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "ASP Website";
            // 
            // txtASPName
            // 
            this.txtASPName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtASPName.Location = new System.Drawing.Point(161, 20);
            this.txtASPName.Margin = new System.Windows.Forms.Padding(4);
            this.txtASPName.Name = "txtASPName";
            this.txtASPName.Size = new System.Drawing.Size(900, 27);
            this.txtASPName.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(8, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "ASP Name";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(21, 603);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(265, 20);
            this.label12.TabIndex = 5;
            this.label12.Text = "* Mandatory Fields for API Access";
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSaveSetting.Location = new System.Drawing.Point(15, 634);
            this.btnSaveSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(189, 36);
            this.btnSaveSetting.TabIndex = 6;
            this.btnSaveSetting.Text = "&Save Settings";
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // btnRegenerateAspDscCertificate
            // 
            this.btnRegenerateAspDscCertificate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegenerateAspDscCertificate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRegenerateAspDscCertificate.Location = new System.Drawing.Point(211, 634);
            this.btnRegenerateAspDscCertificate.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegenerateAspDscCertificate.Name = "btnRegenerateAspDscCertificate";
            this.btnRegenerateAspDscCertificate.Size = new System.Drawing.Size(382, 36);
            this.btnRegenerateAspDscCertificate.TabIndex = 7;
            this.btnRegenerateAspDscCertificate.Text = "&Regenerate and Register ASP Cert.";
            this.btnRegenerateAspDscCertificate.UseVisualStyleBackColor = true;
            this.btnRegenerateAspDscCertificate.Click += new System.EventHandler(this.btnRegenerateAspDscCertificate_Click);
            // 
            // btnGetApiBal
            // 
            this.btnGetApiBal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetApiBal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGetApiBal.Location = new System.Drawing.Point(601, 634);
            this.btnGetApiBal.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetApiBal.Name = "btnGetApiBal";
            this.btnGetApiBal.Size = new System.Drawing.Size(189, 36);
            this.btnGetApiBal.TabIndex = 8;
            this.btnGetApiBal.Text = "Get API &Balance";
            this.btnGetApiBal.UseVisualStyleBackColor = true;
            this.btnGetApiBal.Click += new System.EventHandler(this.btnGetApiBal_Click);
            // 
            // lblAPIMsg
            // 
            this.lblAPIMsg.AutoSize = true;
            this.lblAPIMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAPIMsg.ForeColor = System.Drawing.Color.Blue;
            this.lblAPIMsg.Location = new System.Drawing.Point(172, 687);
            this.lblAPIMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAPIMsg.Name = "lblAPIMsg";
            this.lblAPIMsg.Size = new System.Drawing.Size(0, 17);
            this.lblAPIMsg.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label13.Location = new System.Drawing.Point(302, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(413, 32);
            this.label13.TabIndex = 10;
            this.label13.Text = "ASP Details and API Settings";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Location = new System.Drawing.Point(798, 634);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 36);
            this.button1.TabIndex = 11;
            this.button1.Text = "&Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmAPISetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1098, 726);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblAPIMsg);
            this.Controls.Add(this.btnGetApiBal);
            this.Controls.Add(this.btnRegenerateAspDscCertificate);
            this.Controls.Add(this.btnSaveSetting);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSandboxSetting);
            this.Controls.Add(this.btnDownLoadApiSetting);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAPISetting";
            this.Text = "ASP Details and API Setting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenASPDSC;
        private System.Windows.Forms.TextBox txtDSCPassword;
        private System.Windows.Forms.LinkLabel lnkReqASPAc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAspAcPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAspUserId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEncASPSecret;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAspPassword;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnDownLoadApiSetting;
        private System.Windows.Forms.Button btnSandboxSetting;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtASPName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAspWebsite;
        private System.Windows.Forms.TextBox txtUrlAuth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUrlReturn;
        private System.Windows.Forms.TextBox txtUrlLedger;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtUrlAspUtil;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnSaveSetting;
        private System.Windows.Forms.Button btnRegenerateAspDscCertificate;
        private System.Windows.Forms.Button btnGetApiBal;
        private System.Windows.Forms.Label lblAPIMsg;
        private System.Windows.Forms.Label lblDSCMsg;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
    }
}
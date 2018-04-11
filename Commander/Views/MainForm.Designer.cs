namespace Commander.Views
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsShowUi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnAutoStart = new System.Windows.Forms.ToolStripMenuItem();
            this.pACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnUpdatePac = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnEditUserPacRule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnCopyPacUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnIEproxy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnProxyN = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnProxyG = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnProxyPAC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenucfg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExeFile = new System.Windows.Forms.TextBox();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "这是一个启动器";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsShowUi,
            this.tsbtnAutoStart,
            this.pACToolStripMenuItem,
            this.tsbtnIEproxy,
            this.tsmenucfg,
            this.tsbtnExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 136);
            // 
            // tsShowUi
            // 
            this.tsShowUi.Name = "tsShowUi";
            this.tsShowUi.Size = new System.Drawing.Size(148, 22);
            this.tsShowUi.Text = "显示主界面";
            this.tsShowUi.Click += new System.EventHandler(this.tsShowUi_Click);
            // 
            // tsbtnAutoStart
            // 
            this.tsbtnAutoStart.Name = "tsbtnAutoStart";
            this.tsbtnAutoStart.Size = new System.Drawing.Size(148, 22);
            this.tsbtnAutoStart.Text = "开机自启动";
            this.tsbtnAutoStart.Click += new System.EventHandler(this.tsbtnAutoStart_Click);
            // 
            // pACToolStripMenuItem
            // 
            this.pACToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnUpdatePac,
            this.tsbtnEditUserPacRule,
            this.tsbtnCopyPacUrl});
            this.pACToolStripMenuItem.Name = "pACToolStripMenuItem";
            this.pACToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.pACToolStripMenuItem.Text = "PAC";
            // 
            // tsbtnUpdatePac
            // 
            this.tsbtnUpdatePac.Name = "tsbtnUpdatePac";
            this.tsbtnUpdatePac.Size = new System.Drawing.Size(171, 22);
            this.tsbtnUpdatePac.Text = "更新PAC文件";
            this.tsbtnUpdatePac.Click += new System.EventHandler(this.tsbtnUpdatePac_Click);
            // 
            // tsbtnEditUserPacRule
            // 
            this.tsbtnEditUserPacRule.Name = "tsbtnEditUserPacRule";
            this.tsbtnEditUserPacRule.Size = new System.Drawing.Size(171, 22);
            this.tsbtnEditUserPacRule.Text = "编辑用户规则";
            this.tsbtnEditUserPacRule.Click += new System.EventHandler(this.tsbtnEditUserPacRule_Click);
            // 
            // tsbtnCopyPacUrl
            // 
            this.tsbtnCopyPacUrl.Name = "tsbtnCopyPacUrl";
            this.tsbtnCopyPacUrl.Size = new System.Drawing.Size(171, 22);
            this.tsbtnCopyPacUrl.Text = "复制PAC文件地址";
            this.tsbtnCopyPacUrl.Click += new System.EventHandler(this.tsbtnCopyPacUrl_Click);
            // 
            // tsbtnIEproxy
            // 
            this.tsbtnIEproxy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnProxyN,
            this.tsbtnProxyG,
            this.tsbtnProxyPAC});
            this.tsbtnIEproxy.Name = "tsbtnIEproxy";
            this.tsbtnIEproxy.Size = new System.Drawing.Size(148, 22);
            this.tsbtnIEproxy.Text = "系统代理模式";
            // 
            // tsbtnProxyN
            // 
            this.tsbtnProxyN.Name = "tsbtnProxyN";
            this.tsbtnProxyN.Size = new System.Drawing.Size(163, 22);
            this.tsbtnProxyN.Text = "不设置代理";
            this.tsbtnProxyN.Click += new System.EventHandler(this.tsbtnProxyN_Click);
            // 
            // tsbtnProxyG
            // 
            this.tsbtnProxyG.Name = "tsbtnProxyG";
            this.tsbtnProxyG.Size = new System.Drawing.Size(163, 22);
            this.tsbtnProxyG.Text = "设置IE全局代理";
            this.tsbtnProxyG.Click += new System.EventHandler(this.tsbtnProxyG_Click);
            // 
            // tsbtnProxyPAC
            // 
            this.tsbtnProxyPAC.Name = "tsbtnProxyPAC";
            this.tsbtnProxyPAC.Size = new System.Drawing.Size(163, 22);
            this.tsbtnProxyPAC.Text = "设置IE-PAC代理";
            this.tsbtnProxyPAC.Click += new System.EventHandler(this.tsbtnProxyPAC_Click);
            // 
            // tsmenucfg
            // 
            this.tsmenucfg.Name = "tsmenucfg";
            this.tsmenucfg.Size = new System.Drawing.Size(148, 22);
            this.tsmenucfg.Text = "配置文件";
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(148, 22);
            this.tsbtnExit.Text = "退出";
            this.tsbtnExit.Click += new System.EventHandler(this.tsbtnExit_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.WindowText;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.LawnGreen;
            this.richTextBox1.Location = new System.Drawing.Point(12, 60);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(643, 269);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "ready";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "执行程序路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "命令参数";
            // 
            // txtExeFile
            // 
            this.txtExeFile.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExeFile.ForeColor = System.Drawing.Color.Green;
            this.txtExeFile.Location = new System.Drawing.Point(95, 6);
            this.txtExeFile.Name = "txtExeFile";
            this.txtExeFile.Size = new System.Drawing.Size(490, 22);
            this.txtExeFile.TabIndex = 2;
            this.txtExeFile.DoubleClick += new System.EventHandler(this.txtExeFile_DoubleClick);
            // 
            // txtArguments
            // 
            this.txtArguments.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArguments.ForeColor = System.Drawing.Color.Green;
            this.txtArguments.Location = new System.Drawing.Point(95, 33);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(444, 22);
            this.txtArguments.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(591, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 48);
            this.button1.TabIndex = 3;
            this.button1.Text = "启动";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnStartup_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Font = new System.Drawing.Font("Consolas", 8F);
            this.btnConfig.Location = new System.Drawing.Point(545, 33);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(40, 21);
            this.btnConfig.TabIndex = 4;
            this.btnConfig.Text = "...";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 341);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.txtExeFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "命令行启动器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExeFile;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsbtnExit;
        private System.Windows.Forms.ToolStripMenuItem tsbtnAutoStart;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmenucfg;
        private System.Windows.Forms.ToolStripMenuItem tsShowUi;
        private System.Windows.Forms.ToolStripMenuItem tsbtnIEproxy;
        private System.Windows.Forms.ToolStripMenuItem tsbtnProxyN;
        private System.Windows.Forms.ToolStripMenuItem tsbtnProxyG;
        private System.Windows.Forms.ToolStripMenuItem tsbtnProxyPAC;
        private System.Windows.Forms.ToolStripMenuItem pACToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbtnUpdatePac;
        private System.Windows.Forms.ToolStripMenuItem tsbtnEditUserPacRule;
        private System.Windows.Forms.ToolStripMenuItem tsbtnCopyPacUrl;
    }
}


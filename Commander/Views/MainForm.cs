using Commander.Services;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Commander.Views
{
    public partial class MainForm : Form
    {
        private bool allowClose;
        private bool allowVisible;

        public MainForm()
        {
            InitializeComponent();

            this.txtExeFile.Text = Program.Conifg.Data.FileName;
            this.txtArguments.Text = Program.Conifg.Data.Arguments.FirstOrDefault(x => x.Checked)?.Arguments;
            if (!string.IsNullOrEmpty(this.txtExeFile.Text))
            {
                this.notifyIcon1.Text = this.txtExeFile.Text;
            }


            RefreshAutoStartButtonStatus();
            LoadConfigForMenu();
            RefreshProxyStatus();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            btnStartup_Click(null, null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void process_richtext_clear()
        {
            this.richTextBox1.SafeInvoke(() =>
            {
                this.richTextBox1.Clear();
            });
        }

        private void process_OutputDataReceived(string message, Color color)
        {
            if (!String.IsNullOrEmpty(message))
            {
                this.richTextBox1.SafeInvoke(new Action(() =>
                {
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.SelectionLength = 0;

                    richTextBox1.SelectionColor = color;
                    richTextBox1.AppendText(message + "\r\n");
                    richTextBox1.SelectionColor = richTextBox1.ForeColor;
                }));
            }
        }

        private void btnStartup_Click(object sender, EventArgs e)
        {
            if (this.button1.Text == "停止")
            {
                KillProcess();

                this.txtExeFile.Enabled = true;
                this.txtArguments.Enabled = true;
                this.button1.Text = "执行";

                process_richtext_clear();
            }
            else
            {
                Startup();
            }
        }

        private void Startup()
        {
            if (string.IsNullOrEmpty(txtExeFile.Text) || !File.Exists(txtExeFile.Text))
            {
                return;
            }

            KillProcess();
            this.notifyIcon1.Text = this.txtExeFile.Text;

            var process = new Process();
            process.StartInfo.FileName = this.txtExeFile.Text;
            process.StartInfo.Arguments = this.txtArguments.Text;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.EnableRaisingEvents = true;

            process.ErrorDataReceived += (s, e) => process_OutputDataReceived(e.Data, Color.Red);
            process.OutputDataReceived += (s, e) => process_OutputDataReceived(e.Data, Color.LawnGreen);

            var result = process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            if (result)
            {
                process_richtext_clear();
                this.txtExeFile.Enabled = false;
                this.txtArguments.Enabled = false;
                this.button1.Text = "停止";
            }
        }

        private void txtExeFile_DoubleClick(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { DefaultExt = "*.exe" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.txtExeFile.Text = dlg.FileName;
            }
        }

        private void KillProcess()
        {
            foreach (var item in Process.GetProcesses())
            {
                try
                {
                    if (item.MainModule.FileName == this.txtExeFile.Text)
                    {
                        item.Kill();
                    }
                }
                catch
                {
                }
            }
        }

        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            KillProcess();
            SimpleHTTPServer.GetOrStartDefaultServer().Stop();
            allowClose = true;
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                this.Hide();
                e.Cancel = true;
            }
            base.OnFormClosing(e);

            Program.Conifg.Data.FileName = txtExeFile.Text;

            if (!Program.Conifg.Data.Arguments.Any(x => x.Arguments == txtArguments.Text.Trim()))
            {
                Program.Conifg.Data.Arguments.Add(new ArgumentInfo
                {
                    Checked = true,
                    AliasName = "default",
                    Arguments = txtArguments.Text.Trim()
                });
            }

            Program.Conifg.SaveChanges();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs marg && marg.Button == MouseButtons.Left)
            {
                allowVisible = true;
                Show();
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!allowVisible)
            {
                value = false;
                if (!this.IsHandleCreated) CreateHandle();
            }

            base.SetVisibleCore(value);
        }

        private void tsbtnAutoStart_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtExeFile.Text))
            {
                MessageBox.Show("请先填写执行程序路径!");
                return;
            }

            if (tsbtnAutoStart.Checked)
            {
                TaskSchedulerHelpers.DeleteTask(getTaskName());
            }
            else
            {
                TaskSchedulerHelpers.SetAutoStartup(getTaskName(), Assembly.GetEntryAssembly().Location);
            }

            RefreshAutoStartButtonStatus();
        }

        private void RefreshAutoStartButtonStatus()
        {
            if (!string.IsNullOrEmpty(getTaskName()))
            {
                tsbtnAutoStart.Checked = TaskSchedulerHelpers.IsExist(getTaskName());
            }
        }

        private string getTaskName()
        {
            if (!string.IsNullOrEmpty(txtExeFile.Text))
            {
                var exe = new FileInfo(this.txtExeFile.Text.Trim());
                if (exe.Exists) return "startup-" + exe.Name;
            }

            return string.Empty;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Program.Conifg.Data.FileName = txtExeFile.Text.Trim();
            var dlg = new ArgumentsConfigForm(Program.Conifg);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                LoadConfigForMenu();

                var argument = Program.Conifg.Data.Arguments.FirstOrDefault(x => x.Checked);
                this.txtArguments.Text = argument.Arguments;

                Restart();
            }
        }

        private void Restart()
        {
            if (this.button1.Text == "停止")
                Startup();
        }

        private void LoadConfigForMenu()
        {
            tsmenucfg.DropDownItems.Clear();
            foreach (var arg in Program.Conifg.Data.Arguments)
            {
                var menuitem = new ToolStripMenuItem(arg.AliasName);
                menuitem.Checked = arg.Checked;
                menuitem.Click += Menuitem_Click;
                tsmenucfg.DropDownItems.Add(menuitem);
            }
        }

        private void Menuitem_Click(object sender, EventArgs e)
        {
            var clickItem = (sender as ToolStripMenuItem);
            foreach (ToolStripMenuItem item in tsmenucfg.DropDownItems)
            {
                item.Checked = item == clickItem;
                var argument = Program.Conifg.Data.Arguments.FirstOrDefault(x => x.AliasName == item.Text);
                if (argument != null)
                {
                    argument.Checked = item.Checked;
                    if (argument.Checked)
                    {
                        this.txtArguments.Text = argument.Arguments;
                    }
                }
            }

            Restart();
        }

        private void tsShowUi_Click(object sender, EventArgs e)
        {
            allowVisible = true;
            Show();
        }

        private void tsbtnProxyG_Click(object sender, EventArgs e)
        {
            ProxyRoutines.SetProxy(Program.Conifg.Data.IEProxyString);
            SaveProxyChanges("G");
        }

        private void tsbtnProxyN_Click(object sender, EventArgs e)
        {
            ProxyRoutines.SetNoneProxy();
            SaveProxyChanges("N");
        }

        private void tsbtnProxyPAC_Click(object sender, EventArgs e)
        {
            var server = SimpleHTTPServer.GetOrStartDefaultServer();
            ProxyRoutines.SetAutoConfigURL($"http://127.0.0.1:{server.Port}/{GFWListUpdater.PAC_FILE}");
            SaveProxyChanges("P");
        }

        private void RefreshProxyStatus()
        {
            if (Program.Conifg.Data.IEProxyMode == "N")
            {
                tsbtnProxyN_Click(null, null);
            }
            if (Program.Conifg.Data.IEProxyMode == "G")
            {
                tsbtnProxyG_Click(null, null);
            }
            if (Program.Conifg.Data.IEProxyMode == "P")
            {
                tsbtnProxyPAC_Click(null, null);
            }
        }

        private void SaveProxyChanges(string mode)
        {
            if (Program.Conifg.Data.IEProxyMode != mode)
            {
                Program.Conifg.Data.IEProxyMode = mode;
                Program.Conifg.SaveChanges();
            }

            tsbtnProxyG.Checked = false;
            tsbtnProxyN.Checked = false;
            tsbtnProxyPAC.Checked = false;

            if (mode == "N") tsbtnProxyN.Checked = true;
            if (mode == "P") tsbtnProxyPAC.Checked = true;
            if (mode == "G") tsbtnProxyG.Checked = true;
        }

        private void tsbtnUpdatePac_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(5000, string.Empty, "开始更新，请稍后....", ToolTipIcon.Info);

            new GFWListUpdater().UpdatePACFromGFWList(
                exp => notifyIcon1.ShowBalloonTip(3000, string.Empty, exp.Message, ToolTipIcon.Error),
                () => notifyIcon1.ShowBalloonTip(3000, string.Empty, "更新成功", ToolTipIcon.Info));
        }

        private void tsbtnEditUserPacRule_Click(object sender, EventArgs e)
        {
            if (!File.Exists(GFWListUpdater.USER_RULE_FILE))
            {
                File.WriteAllLines(GFWListUpdater.USER_RULE_FILE, new[] {
                    "!Put user rules line by line in this file.",
                    "!See https://adblockplus.org/en/filter-cheatsheet" });
            }

            Process.Start(GFWListUpdater.USER_RULE_FILE);
        }

        private void tsbtnCopyPacUrl_Click(object sender, EventArgs e)
        {
            var pacurl = SimpleHTTPServer.GetOrStartDefaultServer().PACUrl;
            notifyIcon1.ShowBalloonTip(3000, string.Empty, pacurl, ToolTipIcon.Info);
            Clipboard.SetText(pacurl);
        }
    }
}

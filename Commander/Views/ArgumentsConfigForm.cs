using Commander.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Commander.Views
{
    public partial class ArgumentsConfigForm : Form
    {
        private JsonConfig<ConfigData> _cfg;
        public ArgumentsConfigForm(JsonConfig<ConfigData> cfg)
        {
            InitializeComponent();

            _cfg = cfg;
            this.txtPort.Text = _cfg.Data.IEProxyPort;
            this.cboxHttp.Text = _cfg.Data.IEProxy;
            this.txtPacPort.Text = _cfg.Data.PACPort.ToString();
        }

        private void ArgumentsConfigForm_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            foreach (var arg in _cfg.Data.Arguments)
            {
                var item = new ListViewItem();
                item.Text = arg.AliasName;
                item.ToolTipText = arg.Arguments;
                item.SubItems.Add(arg.Arguments);
                listView1.Items.Add(item);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var item = listView1.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
            var argument = _cfg.Data.Arguments.FirstOrDefault(x => x.AliasName == item.Text);

            _cfg.Data.Arguments.Remove(argument);
            ArgumentsConfigForm_Load(null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var item = _cfg.Data.Arguments.FirstOrDefault(x => x.AliasName == txtName.Text.Trim());
            if (item != null)
            {
                item.Arguments = txtArgument.Text.Trim();
            }
            else
            {
                _cfg.Data.Arguments.Add(new ArgumentInfo
                {
                    AliasName = this.txtName.Text.Trim(),
                    Arguments = this.txtArgument.Text.Trim()
                });
            }

            ArgumentsConfigForm_Load(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems == null) return;
            var item = listView1.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
            if (item == null) return;

            var argument = _cfg.Data.Arguments.FirstOrDefault(x => x.AliasName == item.Text);
            argument.AliasName = txtName.Text.Trim();
            argument.Arguments = txtArgument.Text.Trim();

            SaveConfigs();
        }

        private void btnSaveAndExit_Click(object sender, EventArgs e)
        {
            SaveConfigs();
            this.DialogResult = DialogResult.OK;
        }

        private void SaveConfigs()
        {
            _cfg.Data.IEProxy = this.cboxHttp.Text.Trim();
            _cfg.Data.IEProxyPort = this.txtPort.Text.Trim();
            _cfg.Data.PACPort = int.Parse(this.txtPacPort.Text.Trim());
            _cfg.SaveChanges();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = listView1.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
            if (item == null) return;

            this.txtName.Text = item.Text;
            this.txtArgument.Text = item.ToolTipText;

            foreach (var argument in _cfg.Data.Arguments)
            {
                argument.Checked = argument.AliasName == item.Text;
            }
        }
    }
}

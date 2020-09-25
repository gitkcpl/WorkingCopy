using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Konto.Core.Shared.Libs
{
    public partial class lkpAction : UserControl
    {
        public lkpAction()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler NewButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler EditButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler OkButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler CancelButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler GridSettingsButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ColumnSettingsButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler CancelSettingsButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ResetSettingsButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler SaveSettingsButtonClick;

        private void newSimpleButton_Click(object sender, EventArgs e)
        {
            this.NewButtonClick?.Invoke(this, e);
        }

        private void editSimpleButton_Click(object sender, EventArgs e)
        {
            this.EditButtonClick?.Invoke(this, e);
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            this.OkButtonClick?.Invoke(this, e);
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.CancelButtonClick?.Invoke(this, e);
        }

        private void cancelBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancelSettingsButtonClick?.Invoke(this, e);
        }

        private void columnBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ColumnSettingsButtonClick?.Invoke(this, e);
        }

        private void resetGridBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ResetSettingsButtonClick?.Invoke(this, e);
        }

        private void saveSettingsBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SaveSettingsButtonClick?.Invoke(this, e);
        }

        private void gridBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.GridSettingsButtonClick?.Invoke(this, e);
        }
        private void SetPermission(bool _create,bool _edit, bool _settings)
        {
            newSimpleButton.Visible = _create;
            editSimpleButton.Visible = _edit;
            settingDropDownButton.Visible = _settings; 
        }
    }
}

using Konto.App.Shared;
using Konto.Data.Models.Admin;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Konto.Core.Shared.Libs
{
    public partial class NavAction : UserControl
    {
        public int ModuleId { get; set; }

        public NavAction()
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
        public event EventHandler FilterButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ListButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler PrintButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler FirstButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler NextButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler PrevButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler LastButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler SettingButtonClick;

        public int TotalRecord { get; set; }
        public int RecPos { get; set; }

        private void newSimpleButton_Click(object sender, EventArgs e)
        {
            this.NewButtonClick?.Invoke(this, e);
            NavigationEnabled(false);
        }
        public void NavigationEnabled(bool _state)
        {
            firstSimpleButton.Enabled = _state;
            nextSimpleButton.Enabled = _state;
            lastSimpleButton.Enabled = _state;
            prevSimpleButton.Enabled = _state;
            if (!_state)
            {
                this.RecPos = 0;
                this.TotalRecord = 0;
            }
            recordTextBoxExt.Text = "Record " + (this.RecPos + 1) + " of " + this.TotalRecord;
        }

        private void filterSimpleButton_Click(object sender, EventArgs e)
        {
            this.FilterButtonClick?.Invoke(this, e);
            if(this.TotalRecord  > 0)
                recordTextBoxExt.Text = "Record " + (this.RecPos + 1) + " of " + this.TotalRecord;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.PrintButtonClick?.Invoke(this, e);
        }

        private void firstSimpleButton_Click(object sender, EventArgs e)
        {
            this.FirstButtonClick?.Invoke(this, e);
            recordTextBoxExt.Text = "Record " + (this.RecPos + 1) + " of " + this.TotalRecord;
        }

        private void prevSimpleButton_Click(object sender, EventArgs e)
        {
            this.PrevButtonClick?.Invoke(this, e);
            recordTextBoxExt.Text = "Record " + (this.RecPos + 1) + " of " + this.TotalRecord;
        }

        private void nextSimpleButton_Click(object sender, EventArgs e)
        {
            this.NextButtonClick?.Invoke(this, e);
            recordTextBoxExt.Text = "Record " + (this.RecPos + 1) + " of " + this.TotalRecord;
        }

        private void lastSimpleButton_Click(object sender, EventArgs e)
        {
            this.LastButtonClick?.Invoke(this, e);
            recordTextBoxExt.Text = "Record " + (this.RecPos + 1) + " of " + this.TotalRecord;
        }

        private void listSimpleButton_Click(object sender, EventArgs e)
        {
            this.ListButtonClick?.Invoke(this, e);
        }
        public void SetPermission(bool _create,bool _view,bool _print)
        {
            newSimpleButton.Visible = _create;
            printSimpleButton.Visible = _print;
            filterSimpleButton.Visible = _view;
        }

        private void settingsSimpleButton_Click(object sender, EventArgs e)
        {
            this.SettingButtonClick?.Invoke(this, e);
        }
    }
}

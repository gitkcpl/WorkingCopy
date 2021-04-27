using Konto.Core.Shared.Frms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Konto.Core.Shared.Libs
{
    public partial class ListAction : UserControl
    {
        public int ModuleId { get; set; }
        public ListAction()
        {
            InitializeComponent();
            
        }

       public void RemoveRefresh()
        {
            refreshSimpleButton.Visible = false;
        }
        public void SetPermission(bool _delete,bool _create, bool _export,bool _print, bool _settings,bool _modfiy, bool _view)
        {
            deleteBarButtonItem1.Enabled = _delete;
            cancelInvoiceButtonItem.Enabled = _delete;
            newSimpleButton.Visible = _create;
            exportDropDownButton.Visible = _export;
            printSimpleButton.Visible = _print;
            settingDropDownButton.Visible = _settings;
            editSimpleButton.Visible = _view || _modfiy;
            refreshSimpleButton.Visible = _view || _modfiy;
        }
        

        public void EditDeleteDisabled(bool _mode)
        {
            var frm = this.ParentForm as KontoMetroForm;

            if (frm == null)
            {
                editSimpleButton.Enabled = _mode;
                deleteBarButtonItem1.Enabled = _mode;
                cancelInvoiceButtonItem.Enabled = _mode;
                printSimpleButton.Enabled = _mode;
                return;
            }

            if(frm.Modify_Permission)
                editSimpleButton.Enabled = _mode;

            if (frm.Delete_Permission)
            {
                deleteBarButtonItem1.Enabled = _mode;
                cancelInvoiceButtonItem.Enabled = _mode;
            }

            if(frm.Print_Permission)
                printSimpleButton.Enabled = _mode;
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
        public event EventHandler DeleteButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler CancleRecordClick;


        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler PrintButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler RefreshButtonClick;

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

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ExcelButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ImportButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler WordButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler PdfButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler EmailButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler SmsButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler WhatsAppButtonClick;

        private void newSimpleButton_Click(object sender, EventArgs e)
        {
            this.NewButtonClick?.Invoke(this, e);
        }

        private void editSimpleButton_Click(object sender, EventArgs e)
        {
            this.EditButtonClick?.Invoke(this, e);
        }

       

        private void printSimpleButton_Click(object sender, EventArgs e)
        {
            this.PrintButtonClick?.Invoke(this, e);
        }

        private void refreshSimpleButton_Click(object sender, EventArgs e)
        {
            this.RefreshButtonClick?.Invoke(this, e);
        }

        private void cancelBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancelSettingsButtonClick?.Invoke(this, e);
        }

        private void columnBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ColumnSettingsButtonClick?.Invoke(this, e);
        }

        private void excelBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ExcelButtonClick?.Invoke(this, e);
        }

        private void pdfBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.PdfButtonClick?.Invoke(this, e);
        }

        private void resetGridBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ResetSettingsButtonClick?.Invoke(this, e);
        }

        private void saveSettingsBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SaveSettingsButtonClick?.Invoke(this, e);
        }

        private void wordBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.WordButtonClick?.Invoke(this, e);
        }

        public void SettingsButtonEnable(bool _mode)
        {
            newSimpleButton.Enabled = _mode;
            editSimpleButton.Enabled = _mode;
            deleteBarButtonItem1.Enabled = _mode;
            cancelInvoiceButtonItem.Enabled = _mode;
            refreshSimpleButton.Enabled = _mode;
            printSimpleButton.Enabled = _mode;
            exportDropDownButton.Enabled = _mode;
            messageDropDownButton.Enabled = _mode;
        }

        private void gridBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.GridSettingsButtonClick?.Invoke(this, e);
        }

        private void importButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportButtonClick?.Invoke(this, e);
        }

        private void deleteBarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteButtonClick.Invoke(this, e);
        }

        private void cancelInvoiceButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancleRecordClick.Invoke(this, e);
        }
    }
}

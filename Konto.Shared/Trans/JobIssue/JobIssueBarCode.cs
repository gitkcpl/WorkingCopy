using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Data.Models.Transaction.TradingDto;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Trans.JobIssue
{
    public partial class JobIssueBarCode : KontoForm
    {
        public List<JobIssueBarcodeDto> DelProdOut = new List<JobIssueBarcodeDto>();
        public JobIssueBarcodeDto _Data = new JobIssueBarcodeDto();
        public decimal OrderQty { get; set; }

        public JobIssueBarCode()
        {
            InitializeComponent();
            this.Shown += JobIssueBarCode_Shown;
            this.barcodeNoTextEdit.KeyDown += BarcodeNoTextEdit_KeyDown;
            gridView1.KeyDown += GridView1_KeyDown;
            ChangeQtysimpleButton.Click += ChangeQtysimpleButton_Click;
        }

        private void ChangeQtysimpleButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QtyEdit.Text))
            {
                MessageBox.Show("Please Enter Qty!!!");
                return;
            }
            if (string.IsNullOrEmpty(barcodeNoTextEdit.Text.Trim())) return;
            try
            {
                var _trans = this.bindingSource1.DataSource as List<JobIssueBarcodeDto>;
                if (_trans == null)
                    _trans = new List<JobIssueBarcodeDto>();
                if (_trans.Any(x => x.Barcode == barcodeNoTextEdit.Text.Trim()))
                {
                    MessageBox.Show("Barcode No Already Exist");
                    barcodeNoTextEdit.Text = string.Empty;
                    return;
                }
                decimal qty = Convert.ToDecimal(QtyEdit.Text);

                if (_Data.OrgQty >= qty)
                {
                    _Data.Qty = qty;
                    _trans.Add(_Data);

                    _Data = new JobIssueBarcodeDto();
                    barcodeNoTextEdit.Text = string.Empty;
                    QtyEdit.Text = string.Empty;
                    ProducttextEdit.Text = string.Empty;

                    this.bindingSource1.DataSource = _trans;
                    this.gridControl1.RefreshDataSource();
                    barcodeNoTextEdit.Text = string.Empty;
                    barcodeNoTextEdit.Focus();
                }
                else
                {
                    MessageBox.Show("Qty can not greater than pending!!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Log.Error(ex.ToString(), "Job Issue Barcode");
            }
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)

            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as JobIssueBarcodeDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelProdOut.Add(row);
            } 
        }
        private void JobIssueBarCode_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = barcodeNoTextEdit;
        }

        private void BarcodeNoTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;
            if (string.IsNullOrEmpty(barcodeNoTextEdit.Text.Trim())) return;
            try
            {
                var _trans = this.bindingSource1.DataSource as List<JobIssueBarcodeDto>;
                if (_trans == null)
                    _trans = new List<JobIssueBarcodeDto>();
                if (_trans.Any(x=>x.Barcode == barcodeNoTextEdit.Text.Trim()))
                {
                    MessageBox.Show("Barcode No Already Exist");
                    barcodeNoTextEdit.Text = string.Empty;
                    return;
                }
                using (var db = new KontoContext())
                {
                    db.Database.CommandTimeout = 0;
                    var _model = db.Database.SqlQuery<DetailStockDto>(
                            "dbo.GetBoxTakaByBarcode @compid={0} ,@barcodeno={1}",
                            KontoGlobals.CompanyId, barcodeNoTextEdit.Text.Trim()).FirstOrDefault();
                    if (_model == null)
                    {
                        MessageBox.Show("Barcode does not exists,Please enter valid barcode");
                        return;
                    }
                    _Data = new JobIssueBarcodeDto()
                    {
                        Barcode = _model.Extra1,
                        ChallanNo = _model.VoucherNo,
                        Color = _model.ColorName,
                        VoucherNo = _model.VoucherNo,
                        ColorId = _model.ColorId,
                        Id = _model.Id,
                        LotNo = _model.LotNo,
                        Product = _model.YarnName,
                        ProductId = _model.ProductId,
                        Qty = (decimal)_model.Qty,
                        RefId = _model.RefId,
                        TransId = _model.TransId,
                        Weaver = _model.Weaver,
                        VoucherDate = (int) _model.VoucherDate,SrNo = _model.SrNo,
                        OrgQty = (decimal)_model.OrgQty
                    };
                    ProducttextEdit.Text = _Data.Product;
                    QtyEdit.Text = _model.OrgQty.ToString();
                    // _trans.Add(_barcode);
                    // barcodeNoTextEdit.Text = string.Empty;
                }
               // this.bindingSource1.DataSource = _trans;
               // this.gridControl1.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Log.Error(ex.ToString(), "Job Issue Barcode");
            }
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        { 
            if(JobIssPara.Job_Issue_Against_Order)
            {
                var _trans = this.bindingSource1.DataSource as List<JobIssueBarcodeDto>;
                decimal TotalQty = _trans.Sum(k => k.Qty);

                if (TotalQty > OrderQty)
                {
                    MessageBox.Show("Qty can not grater than order Qty!!!");
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
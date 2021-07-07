using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Trans.ST
{
    public partial class StockRecvIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<GrnTransDto> DelTrans = new List<GrnTransDto>();
        

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;

        public StockRecvIndex()
        {
            InitializeComponent();
            this.Load += GrnIndex_Load;
            
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged; 
            this.Shown += ScIndex_Shown;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Stock_Transfer_Received_Index;
            this.GridLayoutFile = KontoFileLayout.Stock_Transfer_Received_Trans;
            okSimpleButton.Enabled = false;
            FillLookup();
          //  SetParameter();
           

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            barcodetextEdit.KeyDown += BarcodetextEdit_KeyDown;
            gridView1.MouseUp += GridView1_MouseUp;
            gridView1.DoubleClick += GridView1_DoubleClick;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            challanNoTextEdit.ButtonClick += ChallanNoTextEdit_ButtonClick;
            //voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
            okSimpleButton.Click += OkSimpleButton_Click;
            //this.FirstActiveControl = voucherLookup1;
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            var trans = grnTransDtoBindingSource1.DataSource as List<GrnTransDto>;
            using (var db = new KontoContext())
            {
                foreach (var item in trans)
                {
                    var cm = db.Challans.Find(item.ChallanId);
                    var ct = db.ChallanTranses.Find(item.Id);
                    ct.IsReceived = true;
                    ct.ReceiveDateTime = DateTime.Now;
                    ct.ReceivedById = Convert.ToInt32(empLookup1.SelectedValue);
                    ct.BranchId = KontoGlobals.BranchId;
                    StockEffect.StockReceivedAgainstTransfer(cm, ct, "transfer", db);
                   
                }
                db.SaveChanges();
            }
            MessageBox.Show("Stock received Sucessfully");
            grnTransDtoBindingSource1.DataSource = new List<GrnDto>();
            gridControl1.RefreshDataSource();
            challanNoTextEdit.Text = string.Empty;
            challanNoTextEdit.Focus();

        }

        private void ChallanNoTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(challanNoTextEdit.Text)) return;
                if (Convert.ToInt32(empLookup1.SelectedValue) == 0)
                {
                    MessageBox.Show("Invalid Received By Employee");
                    empLookup1.Focus();
                   
                    return;
                }
                using (var db = new KontoContext())
                {
                    var _tran = (from c in db.Challans
                                 join ct in db.ChallanTranses on c.Id equals ct.ChallanId
                                 join p in db.Products on ct.ProductId equals p.Id
                                 join cl in db.ColorModels on p.ColorId equals cl.Id into cl_j
                                 from cl in cl_j.DefaultIfEmpty()
                                 join v in db.Vouchers on c.VoucherId equals v.Id
                                 where c.VoucherNo == challanNoTextEdit.Text.Trim()
                                 & v.VTypeId == (int)VoucherTypeEnum.Stock_Transfer
                                 && !ct.IsReceived && c.ToBranchId == KontoGlobals.BranchId
                                 && !ct.IsDeleted && !c.IsDeleted
                                 select new GrnTransDto
                                 {
                                     BarcodeNo = p.BarCode,
                                     ProductId = p.Id,
                                     ProductName = p.ProductName,
                                     ColorId = ct.ColorId ?? 0,
                                     ColorName = cl.ColorName,
                                     Qty = ct.Qty,
                                     Pcs = ct.Pcs,
                                     Rate = ct.Rate,
                                     Remark = ct.Remark,
                                     Id = ct.Id,
                                     ChallanId = ct.ChallanId,
                                     UomId = ct.UomId ?? 0,
                                     
                }).ToList();

                    foreach (var item in _tran)
                    {
                        item.FromStock = DbUtils.GetCurrentStock(item.ProductId, KontoGlobals.BranchId);
                    }
                    
                    if (_tran.Count > 0)
                        okSimpleButton.Enabled = true;

                    grnTransDtoBindingSource1.DataSource = _tran;
                    gridControl1.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void BarcodetextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode!= Keys.Enter) return;
                if (Convert.ToInt32(empLookup1.SelectedValue)==0)
                {
                    MessageBox.Show("Invalid Received By Employee");
                    empLookup1.Focus();
                    e.Handled = true;
                    return;
                }
                if (string.IsNullOrEmpty(barcodetextEdit.Text))
                {
                    MessageBox.Show("Invalid Barcode");
                    barcodetextEdit.Focus();
                    e.Handled = true;
                    return;
                }

                var model = DbUtils.GetPendingStockTransfer(barcodetextEdit.Text.Trim());

                if (model == null)
                {
                    MessageBox.Show("Barcode Not Found For Receive");
                    barcodetextEdit.Text = string.Empty;
                    barcodetextEdit.Focus();
                    e.Handled = true;
                    return;
                }
                var trans = grnTransDtoBindingSource1.DataSource as List<GrnTransDto>;
                model.FromStock = DbUtils.GetCurrentStock(model.ProductId, KontoGlobals.BranchId);
                trans.Insert(0,model);
                gridControl1.RefreshDataSource();
                okSimpleButton.Enabled = false;
                using (var db = new KontoContext())
                {
                    var cm = db.Challans.Find(model.ChallanId);
                    var ct = db.ChallanTranses.Find(model.Id);
                    ct.IsReceived = true;
                    ct.ReceiveDateTime = DateTime.Now;
                    ct.ReceivedById = Convert.ToInt32(empLookup1.SelectedValue);
                    ct.BranchId = KontoGlobals.BranchId;
                    StockEffect.StockReceivedAgainstTransfer(cm, ct, "transfer", db);
                    db.SaveChanges();
                }

                barcodetextEdit.Text = string.Empty;
                e.Handled = true;
                barcodetextEdit.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     

        private void ScIndex_Shown(object sender, EventArgs e)
        {
            // colSgst.OptionsColumn.AllowFocus = true;
            colSgst.OptionsColumn.AllowFocus = true;
            colSgst.OptionsColumn.AllowEdit = true;
            //colCgst.OptionsColumn.AllowFocus = true;
            colCgst.OptionsColumn.AllowFocus = true;
            colCgst.OptionsColumn.AllowEdit = true;
            // colIgst.OptionsColumn.AllowFocus = true;
            colIgst.OptionsColumn.AllowFocus = true;
            colIgst.OptionsColumn.AllowEdit = true;
        }
        


        #region UDF
      
       
     
      
       
       
       
        private void FillLookup()
        {
        
            using (var db = new KontoContext())
            {
               

               

                var _uomlist = (from p in db.Uoms
                                where !p.IsDeleted & p.IsActive
                                orderby p.UnitName
                                select new UomLookupDto()
                                {
                                    DisplayText = p.UnitName,
                                    Id = p.Id,RateOn = p.RateOn
                                }).ToList();

               
               
                uomRepositoryItemLookUpEdit.DataSource = _uomlist;
              
               
            }
        }

     

     

        

        #endregion

        #region GridView
        private void GridView1_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo ViewInfo = view.GetViewInfo() as GridViewInfo;
                GridState prevState = view.State;
                if ((e.Button & MouseButtons.Left) != 0)
                {
                    if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new Point(e.X, e.Y)))
                    {
                        ViewInfo.SelectionInfo.ClearPressedInfo();
                        args.Handled = true;
                    }
                }
            }
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
                Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
                bounds.Width -= 10;
                bounds.Height -= 3;
                bounds.Y += 3;
                headerEdit.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                headerEdit.EditValue = hi.Column.Caption;
                headerEdit.Show();
                headerEdit.Focus();
                activeCol = hi.Column;
            }
        }
       

        private void GridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        
     
    

     

    
       
      

        #endregion

        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }

      
       

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                empLookup1.Focus();
                return;
                
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as StockRecvListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
               var _ListView = new StockRecvListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Stock Receive [View]";

            }
        }

     

        private void GrnIndex_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResetPage();
                NewRec();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "GRN Load");
                MessageBox.Show(ex.ToString());
            }
        }


        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                if (this.PrimaryKey == 0) return;

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\stock_receive_challan.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["challan"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Receive Challan";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Stock Receive Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "st print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ChallanModel>();
            this.Text = "Stock Receive";
           
           
            //empLookup1.SelectedValue = 1;
            //empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            
            barcodetextEdit.Text = "";
            empLookup1.Focus();
            this.grnTransDtoBindingSource1.DataSource = new List<GrnTransDto>();
            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
            
            barcodetextEdit.Text = string.Empty;
           
         
            empLookup1.SetEmpty();
           
            DelTrans = new List<GrnTransDto>();
            
        }
       
       
        

        

       

      


        #endregion
       
    }
}

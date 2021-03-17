using AutoMapper;
using DevExpress.XtraEditors;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;

using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Shared.Trans.Brokerage
{
    public partial class BrokerageIndex : KontoMetroForm
    {
        private List<BillModel> FilterView = new List<BillModel>();
        private List<BrokTransDto> DelTrans = new List<BrokTransDto>();
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private bool isImortOrSez = false;
        public BrokerageIndex()
        {
            InitializeComponent();
           
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            accLookup1.SelectedValueChanged += AccLookup1_SelectedValueChanged;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
          
            gridView1.MouseUp += GridView1_MouseUp;
          
          
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Gen_Expense_Index;
            this.GridLayoutFile = KontoFileLayout.Gen_Expense_Trans;
           
            this.invTypeLookUpEdit.EditValueChanged += InvTypeLookUpEdit_EditValueChanged;
            
            FillLookup();


            this.FirstActiveControl = invTypeLookUpEdit;

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            tdsPerTextEdit.EditValueChanged += TdsPerTextEdit_EditValueChanged;
            tdsAmtTextEdit.EditValueChanged += TdsAmtTextEdit_EditValueChanged;

        }

       
        private void TdsAmtTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }

        private void TdsPerTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }

        private void InvTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (invTypeLookUpEdit.EditValue.ToString() == "Import" || invTypeLookUpEdit.EditValue.ToString() == "Received from SEZ")
            {

                isImortOrSez = true;
            }
            else
                isImortOrSez = false;
        }

        

        #region UDF
       
      
      
        private void FinalTotal()
        {
            var Trans = grnTransDtoBindingSource1.DataSource as List<ExpTransDto>;
            var gross = Trans.Sum(x => x.Total) - Trans.Sum(x => x.Cgst) - Trans.Sum(x => x.Sgst) -
                Trans.Sum(x => x.Igst) - Trans.Sum(x => x.Cess);

            if (tdsPerTextEdit.Value > 0)
            {
                if(GenExpPara.TDS_RoundOff)
                    tdsAmtTextEdit.Value = decimal.Round((gross * tdsPerTextEdit.Value / 100)+ (decimal) 0.01);
                else
                    tdsAmtTextEdit.Value = decimal.Round( gross * tdsPerTextEdit.Value / 100,2);
            }

            gridView1.UpdateTotalSummary();
            var ntotal = Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue);

            var x1 = ntotal - Math.Truncate(ntotal);



            bool isEven = false;
            if (x1 == (decimal)0.5)
            {
                ntotal = ntotal + (decimal)0.01;
                isEven = true;
            }

            var round = decimal.Round(ntotal, 0) - ntotal;
            if (isEven)
            {
                round = (decimal)0.5;
                ntotal = ntotal + (decimal)0.49;
            }
            else
            {
                ntotal = ntotal + round;
            }

            roundoffSpinEdit.Value = round;
            billAmtSpinEdit.Value = ntotal;
            paybleTextEdit.Text = (ntotal - tdsAmtTextEdit.Value).ToString("F");

        }
     
        
        private void FillLookup()
        {

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Regular","Regular"),
                new ComboBoxPairs("Import", "Import"),
                new ComboBoxPairs("Received from SEZ","Received from SEZ"),

            };
            invTypeLookUpEdit.Properties.DataSource = cbp;
            List<ComboBoxPairs> rbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("NO", "NO"),
                new ComboBoxPairs("YES", "YES"),
            };
            rcmLookUpEdit.Properties.DataSource = rbp;
            
            List<ComboBoxPairs> ibp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Inputs", "Inputs"),
                new ComboBoxPairs("Capital Goods", "Capital Goods"),
                new ComboBoxPairs("Input Services", "Input Services"),
                new ComboBoxPairs("Ineligible","Ineligible")
            };
           itcLookUpEdit.Properties.DataSource = ibp;

           
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
          
            if ( string.IsNullOrEmpty(invTypeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Invoice Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                invTypeLookUpEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
            else if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Party", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                accLookup1.Focus();
                return false;
            }
            else if ( Convert.ToInt32(bookLookup.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Expense Ledger", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bookLookup.Focus();
                return false;
            }
            else if ( string.IsNullOrEmpty(billNoTextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Bill No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                billNoTextEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(rcmLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Rcm Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rcmLookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(itcLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Itc Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                itcLookUpEdit.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            //else if (string.IsNullOrEmpty(storeLookUpEdit.Text))
            //{
            //    MessageBoxAdv.Show(this, "Invalid Store", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    storeLookUpEdit.Focus();
            //    return false;
            //}
            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if( Convert.ToInt32(tdsAccLookup.SelectedValue) ==0 && tdsAmtTextEdit.Value > 0)
            { 
                MessageBoxAdv.Show(this, "Tds Account Must be Selected", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tdsAmtTextEdit.Focus();
                return false;
            }
            //check for duplicate bill no
            using(var db = new KontoContext())
            {
                var accid = Convert.ToInt32(accLookup1.SelectedValue);
                var find1 = db.Bills.FirstOrDefault(
               x => x.AccId == accid && !x.IsDeleted && x.BillNo == billNoTextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId 
               && x.YearId == KontoGlobals.YearId && x.Id!= this.PrimaryKey);

                if ( find1 != null)
                {
                    MessageBox.Show("Entered Bill No Already Exists for this Party");
                    billNoTextEdit.Focus();
                    return false;
                }

             

            }

            return true;
        }

        private void LoadData(BillModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            invTypeLookUpEdit.EditValue = model.BillType;
            rcmLookUpEdit.EditValue = model.Rcm;
            itcLookUpEdit.EditValue = model.Itc;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);

            bookLookup.SelectedValue = model.BookAcId;
            bookLookup.SetAcc(Convert.ToInt32(model.BookAcId));
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            voucherNoTextEdit.Text = model.VoucherNo;

            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc(model.AccId);
            challanNotextEdit.Text = model.RefNo;
            billNoTextEdit.Text = model.BillNo;
           
           
            if (Convert.ToInt32(model.EmpId) != 0)
            {
                empLookup1.SelectedValue = model.EmpId;
                empLookup1.SetGroup();
            }
            storeLookUpEdit.EditValue = model.StoreId;

            
            remarkTextEdit.Text = model.Remarks;

            if (Convert.ToInt32(model.HasteId) != 0)
            {
                tdsAccLookup.SelectedValue = model.HasteId;
                tdsAccLookup.SetAcc((int)model.HasteId);
            }

            tdsPerTextEdit.Value = model.TdsPer;
            tdsAmtTextEdit.Value = model.TdsAmt;
            
            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

            using (var _context = new KontoContext())
            {

                var _lst = (from bt in _context.BillTrans
                            join rb in _context.TaxMasters on bt.BatchId equals rb.Id into joinRb
                            from rb in joinRb.DefaultIfEmpty()
                            join um in _context.Uoms on bt.UomId equals um.Id
                            orderby bt.Id
                            where bt.BillId == model.Id
                            select new ExpTransDto
                            {
                                Id = bt.Id,
                                BillId = bt.BillId,
                                UomId = bt.UomId,
                                BatchId = bt.BatchId,
                                Cess = bt.Cess,
                                CessPer = bt.CessPer,
                                Cgst = bt.Cgst,
                                CgstPer = bt.CgstPer,
                                Disc = bt.Disc,
                                DiscAmt = bt.DiscAmt,
                                Freight = bt.Freight,
                                FreightRate = bt.FreightRate,
                                HsnCode = bt.HsnCode,
                                Igst = bt.Igst,
                                IgstPer = bt.IgstPer,
                                NetTotal = bt.NetTotal,
                                OtherAdd = bt.OtherAdd,
                                OtherLess = bt.OtherLess,
                                Qty = bt.Qty,
                                Rate = bt.Rate,
                                Remark = bt.Remark,
                                Sgst = bt.Sgst,
                                SgstPer = bt.SgstPer,
                                ToAccId = bt.ToAccId,
                                Total = bt.Total
                            }
                             ).ToList();

                grnTransDtoBindingSource1.DataSource = _lst;
            }

            FinalTotal();
            this.Text = "General Expense [View/Modify]";

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
        private void GridControl1_Enter(object sender, EventArgs e)
        {
            gridView1.FocusedColumn = gridView1.VisibleColumns[0];
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(gridView1.GetRow(e.RowHandle) is ExpTransDto er)) return;
           
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
                var row = view.GetRow(view.FocusedRowHandle) as BrokTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
                FinalTotal();
            }
            
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as ExpTransDto;
            rw.Id = -1 * gridView1.RowCount;
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
           ///     var dr = PreOpenLookup();
             //   if (dr == null) return;
                
                //else if (gridView1.FocusedColumn.FieldName == "Particular")
                //{

                //    if (e.KeyCode == Keys.Return)
                //    {
                //        if (dr.ToAccId == 0)
                //        {
                //            //OpenItemLookup(dr.ProductId, dr);
                //            // e.Handled = true;
                //        }
                //    }
                //    else if (e.KeyCode == Keys.F1)
                //    {
                //        //OpenItemLookup(dr.ProductId, dr);
                //        e.Handled = true;
                //    }
                //}
               
              
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Gen Expense GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

        }
       
      
      


        #endregion

        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }
       
        private void AccLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (accLookup1.LookupDto == null) return;
            
            if (accLookup1.LookupDto.TdsReq == "Yes")
            {
                tdsPerTextEdit.Value = accLookup1.LookupDto.TdsPer;
                if (Convert.ToInt32(accLookup1.LookupDto.TdsAccId) > 0)
                {
                    tdsAccLookup.SelectedValue = accLookup1.LookupDto.TdsAccId;
                    tdsAccLookup.SetAcc(Convert.ToInt32(accLookup1.LookupDto.TdsAccId));
                }
            }
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup1.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as BrokerageListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Brokrage Voucher [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new BrokerageListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Brokrage Voucher [View]";

            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Brokrage Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
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

                rpt.Load(new FileInfo("reg\\doc\\grn.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.Conn;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["Ord"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Purchase Order";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Order Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Grn print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<BillModel>();
            this.Text = "Gen Expense [Add New]";
            rcmLookUpEdit.EditValue = "NO";
            invTypeLookUpEdit.EditValue = "Regular";
            itcLookUpEdit.EditValue = "Inputs";
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
           
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            if (Convert.ToInt32(voucherLookup1.GroupDto.AccId) > 0)
            {
                bookLookup.SelectedValue = voucherLookup1.GroupDto.AccId;
                bookLookup.SetAcc(Convert.ToInt32(voucherLookup1.GroupDto.AccId));
            }

            DelTrans = new List<BrokTransDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<BrokTransDto>();
            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
            accLookup1.SetEmpty();
            bookLookup.SetEmpty();
            challanNotextEdit.Text = string.Empty;
            billNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
           
            voucherNoTextEdit.Text = string.Empty;
           
         
            empLookup1.SetEmpty();
          
            remarkTextEdit.Text = string.Empty;
            tdsAccLookup.SetEmpty();
            tdsPerTextEdit.Value = 0;
            tdsAmtTextEdit.Value = 0;
            paybleTextEdit.Text = "0";

            roundoffSpinEdit.Value = 0;
            billAmtSpinEdit.Value = 0;
            
            DelTrans = new List<BrokTransDto>();
            
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

          
            using (var db = new KontoContext())
            {
                var bill = db.Bills.Find(_key);
                LoadData(bill);
            }

        }
        public override void FirstRec()
        {
            base.FirstRec();
            var model = FilterView[RecordNo];
            LoadData(model);
        }
        public override void NextRec()
        {
            base.NextRec();

            LoadData(FilterView[this.RecordNo]);

        }
        public override void PrevRec()
        {
            base.PrevRec();

            LoadData(FilterView[this.RecordNo]);
        }
        public override void LastRec()
        {
            base.LastRec();
            LoadData(FilterView[this.RecordNo]);
        }

        public override void FindRec()
        {
            List<Filter> filter = new List<Filter>();
            

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup1.SelectedValue) });
            }
            if (!string.IsNullOrEmpty(billNoTextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "BillNo", Operation = Op.Equals, Value = billNoTextEdit.Text.Trim() });
            }
            if (Convert.ToInt32(accLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "AccId", Operation = Op.Equals, Value = Convert.ToInt32(accLookup1.SelectedValue) });
            }

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ChallanModel, GrnDto>();
            });

            using (var db = new KontoContext())
            {
                FilterView = db.Bills.Where(ExpressionBuilder.GetExpression<BillModel>(filter))
                    .OrderBy(x => x.VoucherDate).ThenBy(x => x.Id).ToList();
                   

                if (FilterView.Count == 0)
                {
                    MessageBoxAdv.Show(this, "No Record Found", "Find !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ResetPage();
                    return;
                }
                this.TotalRecord = FilterView.Count;
                this.RecordNo = 0;
                LoadData(this.FilterView[0]);

            }

        }

        public override void SaveDataAsync(bool newmode)
        {

            bool IsSaved = false;
            if (!ValidateData()) return;
          
            var _find = new BillModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ExpTransDto, BillTransModel>().ForMember(x => x.Id, p => p.Ignore());
               
            });
            
            var _translist = grnTransDtoBindingSource1.DataSource as List<ExpTransDto>;
            List<BillTransModel> Trans = new List<BillTransModel>();
            
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                        {
                            _find = db.Bills.Find(this.PrimaryKey);
                        }

                        UpdateBill(db, _find);
                        
                        var map = new Mapper(config);

                        foreach (var item in _translist)
                        {
                            var transid = item.Id;
                            item.BillId = _find.Id;
                            var tranModel = new BillTransModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.BillTrans.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (tranModel.Id <= 0)
                            {
                                db.BillTrans.Add(tranModel);
                                db.SaveChanges();

                            }
                            item.Id = tranModel.Id;
                            Trans.Add(tranModel);
                           
                        }
                        //delete item from  trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.BillTrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        //Bill Reference Update
                         LedgerEff.BillRefEntry("Credit",_find,0,db);       //Insert or update in Billref table

                        //Insert or update in LedgerTrans table
                        LedgerEff.LedgerTransEntry("Credit", _find, db, Trans);

                        // Insert in BtoB for BillAdjustment
                       //LedgerEff.BtoBEntry("Payment", KontoGlobals.UserName, Model.Id, Model, _db, BillList, KontoGlobals.CompanyId, KontoGlobals.FromDate, KontoGlobals.ToDate);

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Gen Expense" +" Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }
               

            
            if (IsSaved)
            {
               // NewRec();
               
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage +" Voucher No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    base.SaveDataAsync(newmode);
                    this.ResetPage();
                    this.NewRec();
                    voucherLookup1.buttonEdit1.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private bool UpdateBill(KontoContext db,BillModel model)
        {
           
            model.BillType = invTypeLookUpEdit.EditValue.ToString();
            model.Rcm = rcmLookUpEdit.EditValue.ToString();
            model.Itc = itcLookUpEdit.EditValue.ToString();

        
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.BookAcId = Convert.ToInt32(bookLookup.SelectedValue);
          
            model.VoucherNo = voucherNoTextEdit.Text.Trim();

          
            model.RefNo = challanNotextEdit.Text.Trim();
            model.BillNo = billNoTextEdit.Text.Trim();

            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remarks = remarkTextEdit.Text.Trim();
           
            model.TypeId = (int)VoucherTypeEnum.GenExpense;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.RoundOff = roundoffSpinEdit.Value;

            model.HasteId = Convert.ToInt32(tdsAccLookup.SelectedValue);
            model.TdsPer = tdsPerTextEdit.Value;
            model.TdsAmt = tdsAmtTextEdit.Value;
            var _translist = grnTransDtoBindingSource1.DataSource as List<ExpTransDto>;
            model.GrossAmount = _translist.Sum(x => x.Total);
            model.TotalAmount = billAmtSpinEdit.Value;
            model.TotalQty = _translist.Sum(x => x.Qty);
            model.TotalPcs = 0;
            model.RoundOff = roundoffSpinEdit.Value;
            model.IsActive = true;
           
          

            if (model.Id == 0)
            {
                model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);
                if (DbUtils.CheckExistVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                {
                    MessageBox.Show("Duplicate Voucher No Not Allowed");
                    return false;
                }
                db.Bills.Add(model);
                db.SaveChanges();
            }
            return true;

        }
      
        #endregion

    }
}

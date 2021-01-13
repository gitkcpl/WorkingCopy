using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Acc;
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

namespace Konto.Shared.Account.Jv
{
    public partial class JvIndex : KontoMetroForm
    {
        private List<BillModel> FilterView = new List<BillModel>();
        private List<BankTransDto> DelTrans = new List<BankTransDto>();
        private List<PendBillListDto> DelBill = new List<PendBillListDto>();
        private List<PendBillListDto> BillList = new List<PendBillListDto>();
        private List<PendBillListDto> AllBill = new List<PendBillListDto>();
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
       
        public JvIndex()
        {
            InitializeComponent();
           
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
          
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ValidateRow += GridView1_ValidateRow;
            gridView1.MouseUp += GridView1_MouseUp;
            gridView1.InvalidRowException += GridView1_InvalidRowException;
            accRepositoryItemButtonEdit.ButtonClick += AccRepositoryItemButtonEdit_ButtonClick;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Journal_Index;
            this.GridLayoutFile = KontoFileLayout.Journal_Trans;
            
            FillLookup();
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            gridView1.ValidatingEditor += GridView1_ValidatingEditor;
            gridView1.InvalidValueException += GridView1_InvalidValueException;
            billAdjustSimpleButton.Click += BillAdjustSimpleButton_Click;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }

        private void BillAdjustSimpleButton_Click(object sender, EventArgs e)
        {
            ShowBillDetails();
        }

        private void GridView1_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
        {
            MessageBox.Show(this, e.ErrorText, "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void GridView1_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if (gridView1.FocusedColumn.FieldName == "Particular" && (e.Value == null || string.IsNullOrEmpty(e.Value.ToString())))
            {
                e.Valid = false;
                e.ErrorText = "Particulars Cant Not Be Empty";
            }
            ColumnView view = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (column.FieldName != "Total" && column.FieldName != "NetTotal") return;
            decimal total, netTotal;
            if (column.FieldName == "Total")
            {
                total = Convert.ToDecimal(e.Value);
                netTotal = Convert.ToDecimal(gridView1.GetRowCellValue(view.FocusedRowHandle, colNetTotal));
            }
            else
            {
                netTotal = Convert.ToDecimal(e.Value);
                total = Convert.ToDecimal(gridView1.GetRowCellValue(view.FocusedRowHandle, colTotal));
            }

            if (total != 0 && netTotal != 0)
            {
                e.Value = false;
                e.ErrorText = "Please enter either credit or debit";
            }

        }

        private void AccRepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenAccLookup(dr.ToAccId, dr);
        }

        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            var descr = view.GetRowCellValue(e.RowHandle, colParticular);
            var rptype = view.GetRowCellValue(e.RowHandle, colRpType);
           // decimal amt = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colNetTotal));
          
            if (descr == null || string.IsNullOrEmpty(descr.ToString()))
            {
                e.Valid = false;
                view.SetColumnError(colParticular, "Particulars Cant Not Be Empty");
            }
            else if (rptype == null || string.IsNullOrEmpty(rptype.ToString()))
            {
                e.Valid = false;
                view.SetColumnError(colRpType, "Payment Type Can Not Be Empty");
            }

            //else if (amt == 0)
            //{
            //    e.Valid = false;
            //    view.SetColumnError(colNetTotal, "Invalid payment Amount");
            //}
        }


        #region UDF
        private void ShowBillDetails()
        {
            var dr = PreOpenLookup();
            if (dr == null) return;
            if (Convert.ToInt32(dr.ToAccId) == 0) return;
            var type = "";
            if (dr.Total > 0)
            {
                type = "DEBIT";
            }
            else if (dr.NetTotal > 0)
            {
                type = "CREDIT";
            }
            var frm = new PendingBillViewWindow("J",dr.ToAccId, (int)VoucherTypeEnum.JournalVoucher, type,
                Convert.ToInt32(dr.BillId),dr.Id, Convert.ToInt32(voucherLookup1.SelectedValue));
            
            
             
            frm.AllBill.AddRange(this.AllBill);
           
            frm.TotalAmount = dr.NetTotal;
            if (frm.ShowDialog() != DialogResult.OK) return;
            this.AllBill = frm.AllBill;
            this.DelBill.AddRange(frm.DelBillList);
          
            var plist = frm.BillList.Where(k => k.Amount > 0).ToList();
            var alist = frm.BillList.ToList();
         
            decimal amt = 0;
            dr.Remark = "";
            string bill = "";
            this.BillList.RemoveAll(x => x.RefTransId == dr.Id);
            foreach (var pro in plist)
            {
                    pro.RefTransId = dr.Id;
                    pro.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                    amt = amt + (decimal)pro.Amount;
                     this.BillList.Add(pro);

                    if (bill == "")
                    {
                        bill = pro.BillNo;
                    }
                    else
                    {
                        if (bill != null)
                        {
                            if (pro.BillNo != null)
                                if (!bill.Contains(pro.BillNo))
                                    bill = bill + "," + pro.BillNo;
                        }

                    }
                
            }

            if (dr.NetTotal == 0)
            {
                dr.NetTotal = amt;
            }
            dr.Remark = "BillNo : " + bill;
            gridView1.FocusedColumn = colChequeNo;
        }
        private BankTransDto PreOpenLookup()
        {
           
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (BankTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
        private void OpenAccLookup(int _selvalue, BankTransDto er)
        {
            var frm = new AccLkpWindow();
            frm.Tag = MenuId.Account;
            frm.SelectedValue = _selvalue;
            frm.VoucherType = VoucherTypeEnum.None;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ToAccId = frm.SelectedValue;
                er.Particular = frm.SelectedTex;
                
                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }
           
        }

        private void FillLookup()
        {

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("On Account", "Account"),
                new ComboBoxPairs("Advance", "Advance"),
                new ComboBoxPairs("Against Bill", "Bill")
                };
               payTypeRepositoryItemLookUpEdit.DataSource = cbp;
            
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            var list = this.bindingSource1.DataSource as List<BankTransDto>;
            if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
           
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (list.Any(x => x.ToAccId==0))
            {
                MessageBoxAdv.Show(this, "Invalid Particulars", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;

            }
            else if(list.Sum(x=>x.Total) != list.Sum(x => x.NetTotal))
            {
                MessageBoxAdv.Show(this, "Credit && Debit Total Not Equal", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
        
           
            return true;
        }

        private void LoadData(BillModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
         
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);

            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            voucherNoTextEdit.Text = model.VoucherNo;

          
            if (Convert.ToInt32(model.EmpId) != 0)
            {
                empLookup1.SelectedValue = model.EmpId;
                empLookup1.SetGroup();
            }
            storeLookUpEdit.EditValue = model.StoreId;

           
            remarkTextEdit.Text = model.Remarks;

           

          
            
            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdModel, GrnProdDto>();
            });

            using (var _context = new KontoContext())
            {

                var _list = (from bt in _context.BillTrans
                             join ac in _context.Accs on bt.ToAccId equals ac.Id into joinAc
                             from ac in joinAc.DefaultIfEmpty()
                             join rb in _context.RefBanks on bt.RefBankId equals rb.Id into joinRb
                             from rb in joinRb.DefaultIfEmpty()
                             orderby bt.Id
                             where bt.IsActive && bt.IsDeleted == false &&
                             bt.BillId == model.Id
                             select new BankTransDto
                             {
                                 BillId = bt.BillId,
                                 ChequeNo = bt.ChequeNo,
                                 Id = bt.Id,
                                 NetTotal = bt.NetTotal,
                                 Particular = ac.AccName,
                                 RefBank = rb.BankName,
                                 RefBankId = bt.RefBankId,
                                 Remark = bt.Remark,
                                 RpType = bt.RpType,
                                 ToAccId = (int)bt.ToAccId,Total= bt.Total
                             }
                             ).ToList();

                this.bindingSource1.DataSource = _list;
            }


           
            this.Text = "Journal Voucher [View/Modify]";

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
            if (!(gridView1.GetRow(e.RowHandle) is BankTransDto er)) return;
           
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
                var row = view.GetRow(view.FocusedRowHandle) as BankTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
            
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as BankTransDto;
            rw.Id = -1 * gridView1.RowCount;
            rw.RpType = "Account";
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Return && e.KeyCode != Keys.F1) return;
                
                var dr = PreOpenLookup();
               if (dr == null) return;

                if (gridView1.FocusedColumn.FieldName == "Particular")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ToAccId == 0)
                        {
                            OpenAccLookup(dr.ToAccId, dr);
                            // e.Handled = true;
                        }
                    }

                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenAccLookup(dr.ToAccId, dr);
                        e.Handled = true;
                    }
                }
                else if ((gridView1.FocusedColumn.FieldName == "RpType") &&
                    (gridView1.EditingValue!=null && gridView1.EditingValue.ToString()=="Bill") ||
                        gridView1.FocusedValue!=null && gridView1.FocusedValue.ToString()=="Bill")
                    
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        ShowBillDetails();
                    }
                }


                }
            catch (Exception ex)
            {
                Log.Error(ex, "Journal GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

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
                voucherLookup1.Focus();
                return;
            }
            if (tabControlAdv1.SelectedIndex==1 && tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as JvListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Journal Voucher [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new JvListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Journal Voucher [View]";

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

                Log.Error(ex, "Journal Save");
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

                rpt.Load(new FileInfo("reg\\doc\\JVPrint.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["Bill"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Journal Print";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Journal Print";
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
            this.Text = "Journal Voucher [Add New]";
           
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
           

            DelTrans = new List<BankTransDto>();
            this.bindingSource1.DataSource = new List<BankTransDto>();
            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();

            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
           
         
            challanNotextEdit.Text = string.Empty;
           
            voucherDateEdit.DateTime = DateTime.Now;
           
            voucherNoTextEdit.Text = string.Empty;
           
           
            empLookup1.SetEmpty();
          
            remarkTextEdit.Text = string.Empty;
          
            
            DelTrans = new List<BankTransDto>();
            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();
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
                cfg.CreateMap<BankTransDto, BillTransModel>().ForMember(x => x.Id, p => p.Ignore());
               
            });
            
            var _translist = bindingSource1.DataSource as List<BankTransDto>;
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

                       if(! UpdateBill(db, _find))
                        {
                            _tran.Rollback();
                            return;
                        }
                        
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

                            UpdateBtob(db, _find, transid, item);
                            
                            //DeleteDrCrIfExist(db, _find, item);
                        }
                        
                        foreach (var item in DelBill)
                        {
                            var billDel = db.BtoBs.Where(k => k.BillId == item.BillId && k.BillVoucherId == item.BillVoucherId && k.RefId == _find.Id && k.RefVoucherId == _find.VoucherId).ToList();
                            if (billDel.Count > 0)
                            {
                                db.BtoBs.RemoveRange(billDel);
                            }
                        }

                        //delete item from  trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.BillTrans.Find(item.Id);
                            _model.IsDeleted = true;

                            var billDel = db.BtoBs.Where(k => k.RefId == _find.Id && k.RefVoucherId == _find.VoucherId && k.RefTransId == item.Id && k.IsActive == true && k.IsDeleted == false).ToList();
                            if (billDel.Count > 0)
                            {
                                db.BtoBs.RemoveRange(billDel);
                            }

                            var drCr = db.BillTrans.Where(k => (k.RefId == _find.Id && k.RefTransId == item.Id && k.RefVoucherId == _find.VoucherId)).ToList();

                            db.BillTrans.RemoveRange(drCr);
                            foreach (var drc in drCr)
                            {
                                var drCrM = db.Bills.FirstOrDefault(k => k.Id == drc.BillId);
                                //Delete ledger from crder note
                                var st = db.Ledgers.Where(k => k.RefId == drCrM.RowId && k.IsActive && k.IsDeleted == false).ToList();
                                db.Ledgers.RemoveRange(st);
                                if (drCrM != null)
                                {
                                    db.Bills.Remove(drCrM);
                                }
                            }
                        }

                        LedgerEff.BillRefEntryJv(_find, Trans, DelTrans, db);
                        //Insert or update in LedgerTrans table
                        LedgerEff.LedgerTransEntryJv(KontoGlobals.UserName, _find, db, Trans);

                        //if (this.PrimaryKey == 0)
                        //    DbUtils.UsedSerial(_find.VoucherId, _SerialValue, db);

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Journal Voucher Save");
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
       
        private void UpdateBtob(KontoContext db,BillModel model,int transid,BankTransDto item)
        {


            var bList = BillList.Where(k => (k.RefTransId == transid && k.VoucherId == model.VoucherId)).ToList();
            if (transid > 0 && bList.Count > 0)
            {
                var billDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.RefTransId == item.Id && k.IsActive == true && k.IsDeleted == false).ToList();
                if (billDel.Count > 0)
                {
                    db.BtoBs.RemoveRange(billDel);
                }
            }

            foreach (var p in bList)
            {
                BtoBModel b = new BtoBModel();
                b.BillNo = p.BillNo;
                b.Amount = p.Amount;
                b.Adla1 = p.Adla1;
                b.Adlp1 = p.Adlp1;
                b.Adla2 = p.Adla2;
                b.Adlp2 = p.Adlp2;
                b.Adlp3 = p.Adlp3;
                b.Adla3 = p.Adla3;
                b.Adla4 = p.Adla4;
                b.Adlp4 = p.Adlp4;
                b.Adla5 = p.Adla5;
                b.Adlp5 = p.Adlp5;
                b.Adlp6 = p.Adlp6;
                b.Adla6 = p.Adla6;
                b.Adla7 = p.Adla7;
                b.Adlp7 = p.Adlp7;
                b.Adlp8 = p.Adlp8;
                b.Adla8 = p.Adla8;
                b.Adlp9 = p.Adlp9;
                b.Adla9 = p.Adla9;
                b.Adla10 = p.Adla10;
                b.Adlp10 = p.Adlp10;
                b.RefCode = p.RefCode;
                b.BillId = p.BillId;
                b.CompanyId = KontoGlobals.CompanyId;
                b.BillVoucherId = p.BillVoucherId;
                b.BillTransId = p.BillTransId;
                b.TransType = "Payment";
                b.RefId = model.Id;
                b.RefTransId = item.Id;
                b.RefVoucherId = model.VoucherId;
                db.BtoBs.Add(b);

                //UpdateDrCrForAdjustAmount(db, model, item, b);
            }
        }
        private bool UpdateBill(KontoContext db,BillModel model)
        {
           
            
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.VoucherNo = voucherNoTextEdit.Text.Trim();
          
            model.RefNo = challanNotextEdit.Text.Trim();
            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remarks = remarkTextEdit.Text.Trim();
         
            model.TypeId = (int)VoucherTypeEnum.JournalVoucher;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
          
            var _translist = bindingSource1.DataSource as List<BankTransDto>;
            model.TotalAmount = _translist.Sum(x => x.NetTotal);
            model.GrossAmount = 0;
            model.TotalQty = 0;
            model.TotalPcs = 0;
         
            model.IsActive = true;
           

            if (model.Id == 0)
            {
                 model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);

                if (DbUtils.CheckExistVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                {
                    MessageBox.Show("Duplicate Voucher No Not Allowed");
                    voucherNoTextEdit.Focus();
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

using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Acc;
using Konto.Shared.Masters.RefBank;
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

namespace Konto.Shared.Account.Receipt
{
    public partial class ReceiptIndex : KontoMetroForm
    {
        private List<BillModel> FilterView = new List<BillModel>();
        private List<BankTransDto> DelTrans = new List<BankTransDto>();
        private List<PendBillListDto> DelBill = new List<PendBillListDto>();
        private List<PendBillListDto> BillList = new List<PendBillListDto>();
        private List<PendBillListDto> AllBill = new List<PendBillListDto>();
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private int LastBookId;
        private int LastVoucherId;
        private DateTime LastVoucherDate = DateTime.Now;
        public ReceiptIndex()
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
            this.MainLayoutFile = KontoFileLayout.Receipt_Index;
            this.GridLayoutFile = KontoFileLayout.Receipt_Trans;
            
            FillLookup();
            SetParameter();
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            gridView1.ValidatingEditor += GridView1_ValidatingEditor;
            gridView1.InvalidValueException += GridView1_InvalidValueException;
            billAdjustSimpleButton.Click += BillAdjustSimpleButton_Click;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
            refBankRepositoryItemButtonEdit.ButtonClick += RefBankRepositoryItemButtonEdit_ButtonClick;
            this.FirstActiveControl = voucherLookup1;
            bookLookup.SelectedValueChanged += BookLookup_SelectedValueChanged;
            this.Shown += ReceiptIndex_Shown;
        }

        private void ReceiptIndex_Shown(object sender, EventArgs e)
        {
            colBalance.VisibleIndex = 1;
            colBalance.AppearanceCell.ForeColor = Color.FromArgb(227, 22, 91);
            colBalance.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;


            Root.AddItem(simpleLabelItem1, emptySpaceItem1, DevExpress.XtraLayout.Utils.InsertType.Left);
        }

        private void BookLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bookLookup.LookupDto == null) return;
            simpleLabelItem1.Text = bookLookup.LookupDto.Balance;
        }

        private void RefBankRepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenRefBank(Convert.ToInt32(dr.RefBankId), dr);
        }
        private void OpenRefBank(int _selvalue, BankTransDto er)
        {
            var frm = new RefBankLkpWindow();
            //frm.Tag = MenuId.ref;
            frm.SelectedValue = _selvalue;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.RefBankId = frm.SelectedValue;
                er.RefBank = frm.SelectedTex;
                gridView1.UpdateCurrentRow();
                gridView1.FocusedColumn = colParticular;
                gridView1.MoveNext();


            }

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
            else if (gridView1.FocusedColumn.FieldName == "Particular"
                && e.Value.ToString() == bookLookup.SelectedText)
            {
                e.ErrorText = "Bank/cash A/c & Particulars Can Not Same";
                e.Valid = false;
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
            decimal amt = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colNetTotal));
          
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

            else if (amt == 0)
            {
                e.Valid = false;
                view.SetColumnError(colNetTotal, "Invalid Receipt Amount");
            }
        }


        #region UDF
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "Receipt" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 221://Receipt
                            {
                                ReceiptPara.Auto_Bill_Adjust = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }
        }
        private void ShowBillDetails()
        {
            var dr = PreOpenLookup();
            if (dr == null) return;
            if (Convert.ToInt32(dr.ToAccId) == 0) return;
            var frm = new PendingBillViewWindow("R",dr.ToAccId, (int)VoucherTypeEnum.ReceiptVoucher,"DEBIT",
                Convert.ToInt32(dr.BillId),dr.Id, Convert.ToInt32(voucherLookup1.SelectedValue));
            
            
             
            frm.AllBill.AddRange(this.AllBill);
           
            frm.TotalAmount = dr.NetTotal;
            if (frm.ShowDialog() != DialogResult.OK) return;
            this.AllBill = frm.AllBill;
            this.DelBill.AddRange(frm.DelBillList);
          
            var plist = frm.BillList.Where(k => k.Amount > 0).ToList();
            var alist = frm.BillList.ToList();
            this.BillList.RemoveAll(x => x.RefTransId == dr.Id);
            decimal amt = 0;
            dr.Remark = "";
            string bill = "";
           
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
            if (Convert.ToInt32(bookLookup.SelectedValue) == 0) return null;
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
                var acc = frm.SelectedItem as AccLookupDto;

                er.ToAccId = frm.SelectedValue;
                er.Particular = frm.SelectedTex;

                if (acc.BToB == "Yes")
                {
                    er.RpType = "Bill";
                }

                if (acc != null)
                    er.Balance = acc.Balance;

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
          
            if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
          
            else if ( Convert.ToInt32(bookLookup.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Expense Ledger", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bookLookup.Focus();
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
        
           
            return true;
        }

        private void LoadData(BillModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
         
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);

            bookLookup.SetAcc(Convert.ToInt32(model.AccId));
            bookLookup.SelectedValue = model.AccId;
          
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
                             join acb in _context.AccBals on bt.ToAccId equals acb.AccId
                             orderby bt.Id
                             where bt.IsActive && bt.IsDeleted == false &&
                             bt.BillId == model.Id && acb.CompId == KontoGlobals.CompanyId & acb.YearId == KontoGlobals.YearId
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
                                 ToAccId = (int)bt.ToAccId,
                                 Balance = acb.Bal > 0 ? acb.Bal.ToString() + " Dr" : Math.Abs(acb.Bal).ToString() + " Cr"
                             }
                             ).ToList();

                this.bindingSource1.DataSource = _list;
                this.BillList = new List<PendBillListDto>();
                foreach (var _tr in _list)
                {
                    var _bl =  _context.Database.SqlQuery<PendBillListDto>(
                        "dbo.PendingBill @CompanyId={0},@AccountId={1},@VoucherTypeId={2},@BillType={3}" +
                        ",@RefId={4},@RefTransId={5},@RefVoucherId={6}",
                        KontoGlobals.CompanyId, _tr.ToAccId, 
                        (int)VoucherTypeEnum.ReceiptVoucher,"DEBIT", 
                        model.Id, _tr.Id,model.VoucherId).ToList();
                    this.BillList.AddRange(_bl);
                }
                
            }


           
            this.Text = "Receipt Voucher [View/Modify]";

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
                if (Convert.ToInt32(bookLookup.SelectedValue) == 0) return;
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

                else if ((gridView1.FocusedColumn.FieldName == "RefBank"))
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (Convert.ToInt32(dr.RefBankId) == 0)
                        {
                            OpenRefBank(Convert.ToInt32(dr.RefBankId), dr);
                            e.Handled = true;
                        }
                    }

                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenRefBank(Convert.ToInt32(dr.RefBankId), dr);
                        e.Handled = true;
                    }
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex, "Receipt GridControl KeyDown");
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
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as ReceiptListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Receipt Voucher [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new ReceiptListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Receipt Voucher [View]";

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

                Log.Error(ex, "General Expense Save");
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

                rpt.Load(new FileInfo("reg\\doc\\ReceiptVoucherPrint.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["reportid"].CurrentValue = 0;
                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["bill"].CurrentValue = "N";
                doc.Parameters["VTypeId"].CurrentValue = (int)VoucherTypeEnum.ReceiptVoucher;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Receipt Voucher Print";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Receipt Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Receipt print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<BillModel>();
            this.Text = "Receipt Voucher [Add New]";
           
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

            DelTrans = new List<BankTransDto>();
            this.bindingSource1.DataSource = new List<BankTransDto>();
            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();

            if (this.LastBookId > 0)
            {
                bookLookup.SelectedValue = LastBookId;
                bookLookup.SetAcc(this.LastBookId);
            }

            if (this.LastVoucherId > 0)
            {
                voucherLookup1.SelectedValue = LastVoucherId;
                voucherLookup1.SetGroup(this.LastVoucherId);
            }
            voucherDateEdit.DateTime = this.LastVoucherDate;

            voucherLookup1.buttonEdit1.Focus();

        }
        public override void ResetPage()
        {
            base.ResetPage();

            LastBookId = Convert.ToInt32(bookLookup.SelectedValue);
            LastVoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            
            if (voucherDateEdit.EditValue != null)
                LastVoucherDate = voucherDateEdit.DateTime;


            bookLookup.SetEmpty();
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
           
            if (Convert.ToInt32(bookLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "BookId", Operation = Op.Equals, Value = Convert.ToInt32(bookLookup.SelectedValue) });
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

                            UpdateBtob(db, _find, transid, tranModel);
                            
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
                            if (item.Id <= 0) continue;
                            
                            var _model = db.BillTrans.Find(item.Id);

                            _model.IsDeleted = true;

                            var billDel = db.BtoBs.Where(k => k.RefId == _find.Id && k.RefVoucherId == _find.VoucherId && k.RefTransId == item.Id && k.IsActive == true && k.IsDeleted == false).ToList();
                            if (billDel.Count > 0)
                            {
                                db.BtoBs.RemoveRange(billDel);
                            }

                            var drCr = db.BillTrans.FirstOrDefault(k => (k.RefId == _find.Id && k.RefTransId == item.Id
                            && k.RefVoucherId == _find.VoucherId && !k.IsDeleted && k.IsActive));
                            drCr.IsDeleted = true;

                            var drCrM = db.Bills.FirstOrDefault(k => k.Id == drCr.BillId && !k.IsDeleted && k.IsActive);
                            
                            drCrM.IsDeleted = true;
                            
                            // delete from ledger
                            var st = db.Ledgers.Where(k => k.RefId == drCrM.RowId && k.IsActive && k.IsDeleted == false).ToList();
                            db.Ledgers.RemoveRange(st);

                        }


                        LedgerEff.BillRefEntrypayrec("Credit", _find, Trans, DelTrans, db);
                        //Insert or update in LedgerTrans table
                        LedgerEff.LedgerTransEntryRecPay("Credit", _find, db, Trans, BillList.Where(x=>x.Amount > 0).ToList());

                       

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Receipt Voucher Save");
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
                   
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }
        private void DeleteDrCrIfExist(KontoContext _db,BillModel model,BankTransDto ord)
        {
            var drCr = _db.BillTrans.Where(k => (k.RefId == model.Id && k.RefTransId == ord.Id && k.RefVoucherId == model.VoucherId)).ToList();

            foreach (var item in drCr)
            {
                var drCrM = _db.Bills.FirstOrDefault(k => k.Id == item.BillId);
                //Delete ledger from crder note
                if (drCrM != null)
                {
                    var st = _db.Ledgers.Where(k => k.RefId == drCrM.RowId && k.IsActive && k.IsDeleted == false).ToList();
                    _db.Ledgers.RemoveRange(st);

                    _db.Bills.Remove(drCrM);
                    _db.SaveChanges();
                }

            }
        }
        private void UpdateBtob(KontoContext db,BillModel model,int transid,BillTransModel item)
        {


            var bList = BillList.Where(k => (k.RefTransId == transid && k.Amount > 0)).ToList();
           // if (transid > 0 && bList.Count > 0)
           // {
                var billDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.RefTransId == item.Id && k.IsActive == true && k.IsDeleted == false).ToList();
                if (billDel.Count > 0)
                {
                    db.BtoBs.RemoveRange(billDel);
                }
            //}

           
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
                p.RefTransId = item.Id;
                db.BtoBs.Add(b);

               
            }

            UpdateDrCrForAdjustAmount(db, model, item, bList);
        }
        private bool UpdateBill(KontoContext db,BillModel model)
        {
           
            
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

           
            model.AccId = Convert.ToInt32(bookLookup.SelectedValue);
           
            model.VoucherNo = voucherNoTextEdit.Text.Trim();

          
            model.RefNo = challanNotextEdit.Text.Trim();
        

            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remarks = remarkTextEdit.Text.Trim();
         
            model.TypeId = (int)VoucherTypeEnum.ReceiptVoucher;
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

        private void UpdateDrCrForAdjustAmount(KontoContext _db,BillModel model,BillTransModel ord, List<PendBillListDto> pbs)
        {
            
            var p = pbs.FirstOrDefault();

            string bills = "BNo: ";
            
            
            bills.Remove(bills.Length - 1, 1);

            //delete if row does not exist
            if (pbs.Count == 0)
            {
                var bill = _db.Bills.Where(x => x.RefId == ord.Id && x.RefVoucherId == model.VoucherId && 
                x.IsActive && !x.IsDeleted).ToList();

                foreach (var item in bill)
                {
                    item.IsActive = false;
                    var btrs = _db.BillTrans.Where(x => x.BillId == item.Id && x.IsActive && !x.IsDeleted).ToList();
                    foreach (var bt in btrs)
                    {
                        bt.IsActive = false;
                    }
                    LedgerEff.DeleLedgEffect(item, _db);
                }
                return;

            }

                var sm = (from p1 in pbs
                          group p1 by 1 into g
                          select new
                          {
                              amt1 = g.Sum(x => x.Adla1),
                              amt2 = g.Sum(x => x.Adla2),
                              amt3 = g.Sum(x => x.Adla3),
                              amt4 = g.Sum(x => x.Adla4),
                              amt5 = g.Sum(x => x.Adla5),
                              amt6 = g.Sum(x => x.Adla6),
                              amt7 = g.Sum(x => x.Adla7),
                              amt8 = g.Sum(x => x.Adla8),
                              amt9 = g.Sum(x => x.Adla9),
                              amt10 = g.Sum(x => x.Adla10),
                          }).FirstOrDefault();

                var summry = sm.amt1 + sm.amt2 + sm.amt3 + sm.amt3 + sm.amt4 + sm.amt5 + sm.amt6 + sm.amt7 + sm.amt8 + sm.amt8 + sm.amt9 + sm.amt10;
            

            if (summry == 0)
            {
                // cancel all generates debit credit note

                var bill = _db.Bills.Where(x => x.RefId == ord.Id && x.RefVoucherId == model.VoucherId
                && x.IsActive && !x.IsDeleted).ToList();

                foreach (var item in bill)
                {
                    item.IsActive = false;
                    var btrs = _db.BillTrans.Where(x => x.BillId == item.Id && x.IsActive && !x.IsDeleted).ToList();
                    foreach (var bt in btrs)
                    {
                        bt.IsActive = false;
                    }
                    LedgerEff.DeleLedgEffect(item, _db);
                }
                return;
            }
            foreach (var item in pbs)
            {
                bills = bills + item.BillNo + ",";
            }

            // main bill entry

            bool drcr = false;

            var acc = DbUtils.AccDetails((int)ord.ToAccId);
            if (acc == null) return;


            if (sm.amt1 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl1" && k.RecPay == "R" && 
                k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId== KontoGlobals.CompanyId
                && !k.IsDeleted);

             
                if (adl1 != null)
                {

                  drcr=  CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt1, bills, acc);

                }
            }

            if (sm.amt2 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl2" && k.RecPay == "R" 
                && k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);
                if (adl1 != null)
                {

                    drcr = CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt2, bills, acc);
                }
            }
            if (sm.amt3 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl3" && k.RecPay == "R" && 
                k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);
                if (adl1!=null)
                    drcr = CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt3, bills, acc);
            }
            if (sm.amt4 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl4" && k.RecPay == "R" && 
                k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);
                if (adl1 != null)
                    drcr = CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt4, bills, acc);
            }
            if (sm.amt5 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl5" && k.RecPay == "R" 
                && k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);
                if (adl1 != null)
                    drcr = CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt5, bills, acc);
            }
            if (sm.amt6 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl6" && k.RecPay == "R" 
                && k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);
                if (adl1 != null)
                    drcr = CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt6, bills, acc);
            }
            if (sm.amt7 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl7" && k.RecPay == "R" 
                && k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);
                if (adl1 != null)
                    drcr = CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt7, bills, acc);
            }
            if (sm.amt8 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl8" && k.RecPay == "R" 
                && k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);
                if (adl1 != null)
                    drcr=CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt8, bills, acc);
            }
            if (sm.amt9 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl9" && k.RecPay == "R" 
                && k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);
                if (adl1 != null)
                    drcr = CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt9, bills, acc);
            }
            if (sm.amt10 != 0)
            {
                var adl1 = _db.RPSets.FirstOrDefault(k => k.Field == "Adl10" && k.RecPay == "R" 
                && k.YearId == KontoGlobals.YearId && k.Drcr == "Y" && k.CompId == KontoGlobals.CompanyId
                && !k.IsDeleted);

                if (adl1 != null)
                    drcr = CreateOrUdpateDrCr(_db, adl1, model, ord, p, sm.amt10, bills, acc);
            }

            if (!drcr) // delete old entries
            {
                var bill = _db.Bills.Where(x => x.RefId == ord.Id && x.RefVoucherId == model.VoucherId
               && x.IsActive && !x.IsDeleted).ToList();

                foreach (var item in bill)
                {
                    item.IsActive = false;
                    var btrs = _db.BillTrans.Where(x => x.BillId == item.Id && x.IsActive && !x.IsDeleted).ToList();
                    foreach (var bt in btrs)
                    {
                        bt.IsActive = false;
                    }
                    LedgerEff.DeleLedgEffect(item, _db);
                }
            }
        }

        private bool CreateOrUdpateDrCr(KontoContext _db,RPSetModel adl1, BillModel model,BillTransModel ord, PendBillListDto p,
            decimal amt,string bills,AccLookupDto acc)
        {
            BillModel dr = new BillModel();
            dr = _db.Bills.SingleOrDefault(x => x.RefId == ord.Id && x.RefVoucherId == model.VoucherId &&
              x.BookAcId == adl1.AccountId && !x.IsDeleted && x.IsActive);

            if (dr == null)
                dr = new BillModel();

            dr.CompId = KontoGlobals.CompanyId;
            dr.YearId = KontoGlobals.YearId;

            var type = "Credit";
            var amount = amt;
            if (amount > 0)
            {
                dr.BillType = "CREDIT NOTE";

            }
            else
            {
                dr.BillType = "DEBIT NOTE";
                type = "Debit";
                amount = -1 * amount;
            }
            dr.Extra1 = "SALE";
            dr.AccId = (int)ord.ToAccId;
            dr.BookAcId = adl1.AccountId;
            dr.VoucherId = (int)adl1.VoucherId;
            dr.VoucherNo = DbUtils.NextSerialNo(dr.VoucherId, _db);
            dr.CreateUser = KontoGlobals.UserName;
            dr.CreateDate = DateTime.Now;
            dr.AgentId = 1;
            dr.VDate = model.VDate;
            dr.VoucherDate = model.VoucherDate;
            dr.BillNo = p.BillNo;
            dr.RcdDate = model.VDate;
            var tax = _db.TaxMasters.FirstOrDefault(k => k.Id == adl1.TaxId && k.IsDeleted == false);
            dr.GrossAmount = decimal.Round(Convert.ToDecimal(amount) * 100 / (100 + tax.Cgst + tax.Sgst), 2, MidpointRounding.AwayFromZero);
            dr.TotalQty = 1;
            dr.TotalAmount = Convert.ToDecimal(amount);
            dr.SpecialNotes = "07-Others";
            dr.RefId = ord.Id;
            dr.RefVoucherId = model.VoucherId;
            dr.Remarks = bills;
            dr.BranchId = KontoGlobals.BranchId;

            if (dr.Id == 0)
            {
                _db.Bills.Add(dr);
                _db.SaveChanges();
            }



            BillTransModel tr = null;

            tr = _db.BillTrans.FirstOrDefault(x => x.BillId == dr.Id && x.IsActive && !x.IsDeleted);
            if (tr == null)
                tr = new BillTransModel();

            tr.BillId = dr.Id;
            tr.Remark = adl1.Remark;
            tr.HsnCode = adl1.HsnCode;
            tr.UomId = 44;
            tr.Qty = 1;
            tr.Pcs = 1;
            tr.BatchId = adl1.TaxId;
            tr.RefId = model.Id;
            tr.RefVoucherId = model.VoucherId;
            tr.RefTransId = ord.Id;

            tr.UomId = 28;
            //  var tax = _db.TaxMasters.FirstOrDefault(k => k.Id == adl1.TaxId && k.IsDeleted == false);


            if (acc.IsGst)
            {
                tr.CgstPer = tax.Cgst;
                tr.SgstPer = tax.Sgst;
                tr.Total = decimal.Round(Convert.ToDecimal(amount) * 100 / (100 + tax.Cgst + tax.Sgst), 2);
                tr.Rate = tr.Total;
                tr.Cgst = decimal.Round(tr.Total * tr.CgstPer / 100, 2, MidpointRounding.AwayFromZero);
                tr.Sgst = decimal.Round(tr.Total * tr.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                tr.IgstPer = tax.Igst;
                tr.Total = decimal.Round(Convert.ToDecimal(amount) * 100 / (100 + tax.Igst), 2);
                tr.Rate = tr.Total;
                tr.Igst = decimal.Round(tr.Total * tr.IgstPer / 100, 2, MidpointRounding.AwayFromZero);
            }

            tr.NetTotal = tr.Total + tr.Cgst + tr.Sgst + tr.Igst;

            dr.RoundOff = dr.TotalAmount - tr.NetTotal;

            //   tr.Total = p.Adla1;

            if (tr.Id == 0)
                _db.BillTrans.Add(tr);

            _db.SaveChanges();
            var list = new List<BillTransModel>();
            list.Add(tr);
            //Insert or update in LedgerTrans table
            LedgerEff.LedgerTransEntry(type, dr, _db, list);
            return true;
        }
      
        #endregion

    }
}

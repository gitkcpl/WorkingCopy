﻿using AutoMapper;
using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Grade;
using Konto.Shared.Masters.Item;
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

namespace Konto.Shared.Trans.Request
{
    public partial class PrIndex : KontoMetroForm
    {
        private List<OrdDto> FilterView = new List<OrdDto>();
        private List<OrdTransDto> DelTrans = new List<OrdTransDto>(); 
        //private ProductDto _selectedProdudt;
        public PrIndex()
        {
            InitializeComponent();
            this.Load += PoIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            gradeRepositoryItemButtonEdit.ButtonClick += GradeRepositoryItemButtonEdit_ButtonClick;
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;
           
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ValidatingEditor += GridView1_ValidatingEditor;
            gridView1.InvalidValueException += GridView1_InvalidValueException;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            this.MainLayoutFile = KontoFileLayout.Indent_Index;
            this.GridLayoutFile = KontoFileLayout.Indent_Trans;
            this.Shown += PoIndex_Shown;
            FillLookup();
            SetParameter();
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;

            this.FirstActiveControl = voucherLookup1;
        }

        

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }

        private void GridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Qty")
            {
                decimal _qty = Convert.ToDecimal(e.Value);
                if (_qty <= 0)
                {
                    e.Value = false;
                    e.ErrorText = "Qty must be greater than zero";
                }
            }
            else if(view.FocusedColumn.FieldName=="ItemId")
            {
                int _value = Convert.ToInt32(e.Value);
                if (_value <= 0)
                {
                    e.Value = false;
                    e.ErrorText = "Invalid Item Name";
                }
            }
            else if (view.FocusedColumn.FieldName == "UomId")
            {
                int _value = Convert.ToInt32(e.Value);
                if (_value <= 0)
                {
                    e.Value = false;
                    e.ErrorText = "Invalid Unit Name";
                }
            }

        }
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = gridView1.GetFocusedRow() as OrdTransDto;
            if (itm == null)
                return;
            if(IndentPara.Default_Order_Status == "PENDING" && itm.OrdStatus != "PENDING")
            {
                e.Cancel = true;
            }
        }
        private void PoIndex_Shown(object sender, EventArgs e)
        {
            SetGridColumn();

            if (this.EditKey > 0)
                this.EditPage(this.EditKey);
        }




        #region UDF
        private void SetGridColumn()
        {
            colColorName.Visible = IndentPara.Color_Required;
            colDesignNo.Visible = IndentPara.Design_Required;
            colGradeName.Visible = IndentPara.Grade_Required;
           
           
        }
        private OrdTransDto PreOpenLookup()
        {
            
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (OrdTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
      
        public void GridCalculation(OrdTransDto er)
        {
            if (er.NoOfLot > 0 && er.Cut > 0)
                er.Qty = er.NoOfLot * er.Cut;


            er.Total = decimal.Round(er.Qty * er.Rate, 2, MidpointRounding.AwayFromZero);
            if (er.Disc > 0)
                er.DiscAmt = decimal.Round(er.Total * er.Disc / 100, 2, MidpointRounding.AwayFromZero);
            decimal gross = er.Total - er.DiscAmt;

            if (er.NoOfLot > 0 && er.LotPcs > 0 && er.Cut ==0)
                er.Qty = er.NoOfLot * er.LotPcs;

            er.SgstAmt = decimal.Round(gross * er.Sgst / 100, 2, MidpointRounding.AwayFromZero);
            er.CgstAmt = decimal.Round(gross * er.Cgst / 100, 2, MidpointRounding.AwayFromZero);
            er.IgstAmt = decimal.Round(gross * er.Igst / 100, 2, MidpointRounding.AwayFromZero);
            er.CessAmt = decimal.Round(er.Qty * er.Cess, 2, MidpointRounding.AwayFromZero);

            er.NetTotal = gross + er.SgstAmt + er.CgstAmt + er.IgstAmt + er.CessAmt;


        }
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "Purchase Indent" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 228: //indent
                            {
                                IndentPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 229:
                            {
                                IndentPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 230:
                            {
                                IndentPara.Grade_Required = (value == "Y") ? true : false;
                                break;
                            }
                       
                        case 231:
                            {
                                IndentPara.Default_Order_Status = value;
                                break;
                            }
                       
                    }
                }
            }

        }
        private void OpenItemLookup(int _selvalue, OrdTransDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;

            frm.VoucherType = VoucherTypeEnum.Indent;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UomId = model.UomId;
                er.Rate = model.DealerPrice;
               
               // er.Cess = model.Cess;
                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }
            GridCalculation(er);
        }
        private void OpenColorLookup(int _selvalue, OrdTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colColorName.VisibleIndex + 1);
            }

        }
        private void OpenGradeLookup(int _selvalue, OrdTransDto er)
        {
            var frm = new GradeLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Grade;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.GradeId = frm.SelectedValue;
                er.GradeName = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colGradeName.VisibleIndex + 1);
            }

        }
        private void OpenDesignLookup(int _selvalue, OrdTransDto er)
        {
            var frm = new DesignLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Design_Master;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.DesignId = frm.SelectedValue;
                er.DesignNo = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colDesignNo.VisibleIndex + 1);
            }

        }
        private void FillLookup()
        {
            Dictionary<int, string> statusEnums = Enum.GetValues(typeof(SaleOrderStatus))
                .Cast<SaleOrderStatus>().ToDictionary(x => (int)x, x => x.ToString());

           

            using (var db = new KontoContext())
            {

                var _divLists = (from p in db.Divisions
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.DivisionName,
                                     Id = p.Id
                                 }).ToList();

                var _uomlist = (from p in db.Uoms
                                where !p.IsDeleted & p.IsActive
                                orderby p.UnitName
                                select new BaseLookupDto()
                                {
                                    DisplayText = p.UnitName,
                                    Id = p.Id
                                }).ToList();

                var _warplist = (from p in db.WarpItems
                                where !p.IsDeleted & p.IsActive
                                orderby p.ItemName
                                select new BaseLookupDto()
                                {
                                    DisplayText = p.ItemName,
                                    Id = p.Id
                                }).ToList();
                warpItemRepositoryItemLookUpEdit.DataSource = _warplist;
                uomRepositoryItemLookUpEdit.DataSource = _uomlist;
                divLookUpEdit.Properties.DataSource = _divLists;
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
          
            var trans = ordTransDtoBindingSource1.DataSource as List<OrdTransDto>;

            if (string.IsNullOrEmpty(divLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
                return false;
            }

            else if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
           
           
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Request date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(empLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Employee Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                empLookup1.Focus();
                return false;
            }

            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            
            else if (trans.Sum(x => x.Qty) == 0)
            {
                MessageBoxAdv.Show(this, "Zero Qty Can Not be Accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }

            if (this.PrimaryKey != 0)
            {
                using(var db = new KontoContext())
                {
                    var vid = Convert.ToInt32(this.voucherLookup1.SelectedValue);
                    var exist = db.OrdTranses.Any(x => x.RefId == this.PrimaryKey && x.RefVoucherId == vid  && x.IsDeleted == false && x.IsActive == true);
                    if (exist)
                    {
                        MessageBoxAdv.Show("Request Exist In Order.. Can not Edit Order", "Access Denied !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }


            return true;
        }

        private void LoadData(OrdDto model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);
            voucherNoTextEdit.Text = model.VoucherNo;
            divLookUpEdit.EditValue = model.DivisionId;
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
           
            refNotextEdit.Text = model.RefNo;
            
            
            pgLookup1.SelectedValue = Convert.ToInt32( model.PGroupId);
            pgLookup1.SetGroup();

          
            if (model.EmpId != 0)
            {
                empLookup1.SelectedValue = model.EmpId;
                empLookup1.SetGroup();
            }
            
            remarkTextEdit.Text = model.Remarks;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

            using (var _context = new KontoContext())
            {
                var _list = (from ot in _context.OrdTranses
                             join pd in _context.Products on ot.ProductId equals pd.Id into join_pd
                             from pd in join_pd.DefaultIfEmpty()
                             join cl in _context.ColorModels on ot.ColorId equals cl.Id into join_cl
                             from cl in join_cl.DefaultIfEmpty()
                             join grd in _context.Grades on ot.GradeId equals grd.Id into join_grd
                             from grd in join_grd.DefaultIfEmpty()
                             join des in _context.Products on ot.DesignId equals des.Id into join_des
                             from des in join_des.DefaultIfEmpty()
                             orderby ot.Id
                             where ot.IsActive == true && ot.IsDeleted == false &&
                             ot.OrdId == model.Id
                             select new OrdTransDto()
                             {
                                 AvgWt = ot.AvgWt,
                                 Id = ot.Id,
                                 CancelReason=ot.CancelReason,Cess=ot.Cess,CessAmt=ot.CessAmt,Cgst=ot.Cgst,
                                 CgstAmt= ot.CgstAmt,ColorId=ot.ColorId,ColorName=cl.ColorName,CommDescr=ot.CommDescr,
                                 Cut=ot.Cut,DeptId=ot.DeptId,DesignId=ot.DesignId,DesignNo=des.ProductCode,
                                 Disc=ot.Disc,DiscAmt=ot.DiscAmt,DivisionId=ot.DivisionId,GradeId=ot.GradeId,
                                 GradeName=grd.GradeName,Igst=ot.Igst,IgstAmt=ot.IgstAmt,LotPcs=ot.LotPcs,
                                 NetTotal=ot.NetTotal,NoOfLot=ot.NoOfLot,OrdId=ot.OrdId,OrdStatus=ot.OrdStatus,
                                 Priority=ot.Priority,ProductId=ot.ProductId,ProductName=pd.ProductName,Qty=ot.Qty,
                                 Rate=ot.Rate,RefId=ot.RefId,RefVoucherId=ot.RefVoucherId, Remark=ot.Remark,
                                 Sgst=ot.Sgst,SgstAmt=ot.SgstAmt,Total=ot.Total,UomId=ot.UomId,WarpItemId=ot.WarpItemId,
                                 RequireDate= ot.RequireDate

                             }).ToList();

                this.ordTransDtoBindingSource1.DataSource = _list;

                if (DbUtils.CheckExistinChallan(model.Id, model.VoucherId, _context))
                {
                    okSimpleButton.Enabled = false;
                    this.UpdateMessage("Edit Restricted! Challan has Reference");

                }
            }

            this.Text = "Purchase Request [View/Modify]";

        }

        #endregion

        #region GridView

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
            gridView1.FocusedColumn = gridView1.Columns["ProductName"];
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            var er = gridView1.GetRow(e.RowHandle) as OrdTransDto;
            if (er == null) return;
            GridCalculation(er);
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
                var row = view.GetRow(view.FocusedRowHandle) as OrdTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
                else if (gridView1.FocusedColumn.FieldName == "DesignNo")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colDesignNo, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colDesignId, 0);
                }
            }
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as OrdTransDto;
            rw.Priority = "NORMAL";
            //if (KontoGlobals.PackageId == 1)
            //{
            //  //  rw.LotPcs = 12;
            //   // rw.Cut = 1200;
            //}
            rw.OrdStatus = IndentPara.Default_Order_Status;
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
               
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (gridView1.FocusedColumn.FieldName == "ProductName")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == 0)
                        {
                            OpenItemLookup(dr.ProductId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup(dr.ProductId, dr);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ColorId == 0)
                        {
                            OpenColorLookup(dr.ColorId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenColorLookup(dr.ProductId, dr);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "GradeName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.GradeId == 0)
                        {
                            OpenGradeLookup(dr.GradeId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenGradeLookup(dr.GradeId, dr);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "DesignNo")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.DesignId == 0)
                        {
                            OpenDesignLookup(dr.DesignId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenDesignLookup(dr.DesignId, dr);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup(dr.ProductId, dr);

        }
        private void ColorRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            var dr = PreOpenLookup();
            if (dr != null)
                OpenColorLookup(dr.ColorId, dr);
        }
        private void GradeRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenGradeLookup(dr.GradeId, dr);
        }
        private void DesignRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenDesignLookup(dr.DesignId, dr);
        }


        #endregion


       

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup1.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as PrListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new PrListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Purchase Request [View]";

            }
            if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.OrdPara.OrdParaMainView").Unwrap() as KontoForm;
                _frm.ReportFilterType = "PurchaseRequest";
                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.Location = new Point(tabPageAdv4.Location.X + tabPageAdv4.Width / 2 - _frm.Width / 2, tabPageAdv4.Location.Y + tabPageAdv4.Height / 2 - _frm.Height / 2);
                _frm.Show();// = true;

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

                Log.Error(ex, "Request Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void PoIndex_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResetPage();
                NewRec();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Purchase Request Load");
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

                rpt.Load(new FileInfo("reg\\doc\\ReqRep.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["Ord"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Purchase Request";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Request Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Purchase Request print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<OrdDto>();
            this.Text = "Purchase Request [Add New]";
           
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            divLookUpEdit.EditValue = 1;
            empLookup1.SetEmpty();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = divLookUpEdit;
            voucherLookup1.SetDefault();
            DelTrans = new List<OrdTransDto>();
            this.ordTransDtoBindingSource1.DataSource = new List<OrdTransDto>();

            if (this.Create_Permission)
                okSimpleButton.Enabled = true;

        }
        public override void ResetPage()
        {
            base.ResetPage();
            if (this.Modify_Permission)
                okSimpleButton.Enabled = true;
          
            refNotextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
           
            voucherNoTextEdit.Text = string.Empty;
            refNotextEdit.Text = string.Empty;
          
            pgLookup1.SetEmpty();
            empLookup1.SetEmpty();
           
            remarkTextEdit.Text = string.Empty;
           
            DelTrans = new List<OrdTransDto>();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrdModel, OrdDto>();
            });

            using (var db = new KontoContext())
            {
                var bill = db.Ords.Find(_key);
                var model = new OrdDto();
                var mapper = new Mapper(config);
                mapper.Map(bill, model);
                LoadData(model);
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
                cfg.CreateMap<OrdModel, OrdDto>();
            });

            using (var db = new KontoContext())
            {
                FilterView = db.Ords.Where(ExpressionBuilder.GetExpression<OrdModel>(filter))
                    .OrderBy(x => x.VoucherDate).ThenBy(x=>x.Id)
                    .ProjectToList<OrdDto>(config);

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
            OrdDto model = new OrdDto();
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
           
            model.VoucherNo = voucherNoTextEdit.Text.Trim();
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
         
            model.RefNo = refNotextEdit.Text.Trim();
            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
          
            model.Remarks = remarkTextEdit.Text.Trim();

            model.DivisionId = Convert.ToInt32(divLookUpEdit.EditValue);
            
            model.TypeId = (int)VoucherTypeEnum.Indent;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.PGroupId = Convert.ToInt32(pgLookup1.SelectedValue);

            var _find = new OrdModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrdDto, OrdModel>().ForMember(x => x.Id, p => p.Ignore()
                );
                cfg.CreateMap<OrdTransDto, OrdTransModel>().ForMember(x => x.Id, p => p.Ignore());
            });
            
            var _translist = ordTransDtoBindingSource1.DataSource as List<OrdTransDto>;

            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        string createuser = KontoGlobals.UserName;
                        DateTime createdate = DateTime.Now;
                        if (this.PrimaryKey != 0)
                        {
                            _find = db.Ords.Find(this.PrimaryKey);
                            createuser = _find.CreateUser;
                            createdate = Convert.ToDateTime( _find.CreateDate);
                        }

                        var map = new Mapper(config);
                        map.Map(model, _find);
                        _find.TotalQty = _translist.Sum(x => x.Qty);
                        _find.TotalPcs = _translist.Sum(x => x.LotPcs);
                        _find.TotalAmount = _translist.Sum(x => x.NetTotal);
                        _find.IsActive = true;
                        if (this.PrimaryKey == 0)
                        {
                            
                            _find.VoucherNo =  DbUtils.NextSerialNo(_find.VoucherId, db);
                           
                            model.VoucherNo = _find.VoucherNo;

                            if (DbUtils.CheckExistVoucherNo(_find.VoucherId, _find.VoucherNo, db, _find.Id))
                            {
                                MessageBox.Show("Duplicate Voucher No Not Allowed");
                                _tran.Rollback();
                                return;
                            }

                            _find.TypeId = (int)VoucherTypeEnum.Indent;
                            db.Ords.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }
                        
                        foreach (var item in _translist)
                        {
                            item.OrdId = _find.Id;
                            var tranModel = new OrdTransModel();
                            if(item.Id != 0)
                            {
                                tranModel = db.OrdTranses.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id == 0)
                            {
                                
                                db.OrdTranses.Add(tranModel);
                            }
                        }
                        //delete item from ord trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id <= 0) continue;
                            var _model = db.OrdTranses.Find(item.Id);
                            _model.IsDeleted = true;
                        }
                        
                        //if (this.PrimaryKey == 0)
                        //    DbUtils.UsedSerial(_find.VoucherId, _SerialValue, db);

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Po Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }
               

            
            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage +" Request No.: " + model.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (this.voucherLookup1.GroupDto.PrintAfterSave && MessageBox.Show("Print Challan ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.PrimaryKey = model.Id;
                    Print();
                    this.PrimaryKey = 0;
                }
                if (!this.OpenForLookup && newmode)
                {
                    this.ResetPage();
                    this.NewRec();
                    //svoucherLookup1.buttonEdit1.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }


        #endregion
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}

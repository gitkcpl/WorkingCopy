﻿using AutoMapper;
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
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;

using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Item;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;
using Filter = Konto.Core.Shared.Libs.Filter;

namespace Konto.Trading.OutJobChallan
{
    public partial class OJCIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<MrvTransDto> DelTrans = new List<MrvTransDto>();
        private List<ProdOutDto> prodDtos = new List<ProdOutDto>();
        private List<ProdOutDto> DelProd = new List<ProdOutDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private bool isImortOrSez = false;
        public OJCIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;
            accLookup1.SelectedValueChanged += AccLookup1_SelectedValueChanged;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.MouseUp += GridView1_MouseUp;
            lotNoRepositoryItemButtonEdit.ButtonClick += LotNoRepositoryItemButtonEdit_ButtonClick;
            challanNoRrepositoryItemButtonEdit.ButtonClick += ChallanNoRrepositoryItemButtonEdit_ButtonClick;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Ojc_Index;
            this.GridLayoutFile = KontoFileLayout.Ojc_Trans;
          
            FillLookup();
            SetParameter();
            SetGridColumn();

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            this.Shown += MrvIndex_Shown;
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

        private void MrvIndex_Shown(object sender, EventArgs e)
        {
            colShPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colShPer.DisplayFormat.FormatString = "F";
            
        }

       

        #region UDF
        private void SetGridColumn()
        {
            //colColorName.Visible = MillRecPara.Color_Required;
            //colDesignNo.Visible = MillRecPara.Design_Required;
            //colFreight.Visible = MillRecPara.Freight_Required;
            //colFreightRate.Visible = MillRecPara.Freight_Required;
            //colOtherAdd.Visible = MillRecPara.OtherAdd_Required;
            //colOtherLess.Visible = MillRecPara.OtherLess_Required;
           


        }
        private MrvTransDto PreOpenLookup()
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
            {
                MessageBox.Show("Party Name Not Selected");
                accLookup1.Focus();
                return null;
            }
            if (Convert.ToInt32(processLookup1.SelectedValue) == 0)
            {
                MessageBox.Show("Job Type Not Selected");
                processLookup1.Focus();
                return null;
            }
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (MrvTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }

        public void GridCalculation(MrvTransDto er)
        {



            var dr = uomRepositoryItemLookUpEdit.GetDataSourceRowByKeyValue(er.UomId) as UomLookupDto;
            if (dr == null) return;

            if (dr.RateOn == "N")
            {
                er.Gross = decimal.Round(er.Pcs * er.Rate, 2);
            }
            else
            {
                er.Gross = decimal.Round(er.Qty * er.Rate, 2);
            }

            if (er.DiscPer > 0)
                er.Disc = decimal.Round(er.Gross * er.DiscPer / 100, 2, MidpointRounding.AwayFromZero);
            decimal gross = er.Gross - er.Disc;

            if (er.FreightRate > 0)
                er.Freight = decimal.Round(er.Qty * er.FreightRate / 100, 2, MidpointRounding.AwayFromZero);

            gross = gross + er.Freight + er.OtherAdd - er.OtherLess;

            er.Sgst = decimal.Round(gross * er.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
            er.Cgst = decimal.Round(gross * er.CgstPer / 100, 2, MidpointRounding.AwayFromZero);
            er.Igst = decimal.Round(gross * er.IgstPer / 100, 2, MidpointRounding.AwayFromZero);

            //  er.Cess = decimal.Round(er.Qty * er.CessPer, 2, MidpointRounding.AwayFromZero);
            er.Total = gross + er.Sgst + er.Cgst + er.Igst;

            gridView1.UpdateCurrentRow();

            FinalTotal();
        }
        private void FinalTotal()
        {
            var Trans = grnTransDtoBindingSource1.DataSource as List<MrvTransDto>;
            var gross = Trans.Sum(x => x.Total) - Trans.Sum(x => x.Cgst) - Trans.Sum(x => x.Sgst) -
                Trans.Sum(x => x.Igst) - Trans.Sum(x => x.Cess);

           
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

            //roundoffSpinEdit.Value = round;
            billAmtSpinEdit.Value = ntotal;
           

        }
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "Mrv" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {

                        case 85:
                            {
                                MillRecPara.Tds_RoundOff = (value == "Y") ? true : false;
                                break;
                            }
                        case 86:
                            {
                                MillRecPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 87:
                            {
                                MillRecPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 88:
                            {
                                MillRecPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 89:
                            {
                                MillRecPara.OtherAdd_Required = (value == "Y") ? true : false;
                                break;
                            }

                        case 90:
                            {
                                MillRecPara.OtherLess_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 91:
                            {
                                MillRecPara.Taka_Detail_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 111:
                            {
                                MillRecPara.FinishMeter_more_than_GreyMeter = (value == "Y") ? true : false;
                                break;
                            }
                        case 171:
                            {
                                MillRecPara.Challan_Required = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }

        }
        private void OpenItemLookup(int _selvalue, MrvTransDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;
            frm.PTypeId = ProductTypeEnum.GREY;
            frm.VoucherType = VoucherTypeEnum.GreyOrder;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UomId = model.PurUomId;
                er.Rate = model.DealerPrice;
                if (accLookup1.LookupDto.IsGst)
                {
                    er.SgstPer = model.Sgst;
                    er.CgstPer = model.Cgst;
                    er.IgstPer = 0;
                    er.Igst = 0;
                }
                else
                {
                    er.SgstPer = 0;
                    er.Sgst = 0;
                    er.CgstPer = 0;
                    er.Cgst = 0;
                    er.IgstPer = model.Igst;
                }
                er.CessPer = model.Cess;
                gridView1.FocusedColumn = gridView1.VisibleColumns[gridView1.FocusedColumn.VisibleIndex + 1];
            }
            GridCalculation(er);

        }
        private void OpenColorLookup(int _selvalue, MrvTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.Tag = MenuId.Color;
            frm.SelectedValue = _selvalue;
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

        private void OpenDesignLookup(int _selvalue, MrvTransDto er)
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

          
            using (var db = new KontoContext())
            {

                //var TransTypeList = (from p in db.transTypes
                //                     where p.IsActive && !p.IsDeleted && (p.Category.ToUpper() == "OUTWARD" || p.Category == null)
                //                     select new BaseLookupDto()
                //                     {
                //                         DisplayText = p.TypeName,
                //                         Id = p.Id
                //                     }).ToList();

                var _divLists = (from p in db.Divisions
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.DivisionName,
                                     Id = p.Id
                                 }).ToList();

                var _storeLists = (from p in db.Stores
                                   where p.IsActive && !p.IsDeleted
                                   select new BaseLookupDto()
                                   {
                                       DisplayText = p.StoreName,
                                       Id = p.Id
                                   }).ToList();

                var _uomlist = (from p in db.Uoms
                                where !p.IsDeleted & p.IsActive
                                orderby p.UnitName
                                select new UomLookupDto()
                                {
                                    DisplayText = p.UnitName,
                                    Id = p.Id, RateOn = p.RateOn
                                }).ToList();



                uomRepositoryItemLookUpEdit.DataSource = _uomlist;
                divLookUpEdit.Properties.DataSource = _divLists;
                
                storeLookUpEdit.Properties.DataSource = _storeLists;
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            if (string.IsNullOrEmpty(divLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Division Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
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
            //else if (!MillRecPara.Challan_Required && Convert.ToInt32(bookLookup.SelectedValue) == 0)
            //{
            //    MessageBoxAdv.Show(this, "Invalid Job Book", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    bookLookup.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(challanNotextEdit.Text.Trim()))
            //{
            //    MessageBoxAdv.Show(this, "Invalid Challan No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    challanNotextEdit.Focus();
            //    return false;
            //}
            //else if (!MillRecPara.Challan_Required && string.IsNullOrEmpty(billNoTextEdit.Text.Trim()))
            //{
            //    MessageBoxAdv.Show(this, "Invalid Bill No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    billNoTextEdit.Focus();
            //    return false;
            //}
            //else if (!MillRecPara.Challan_Required && string.IsNullOrEmpty(rcmLookUpEdit.Text))
            //{
            //    MessageBoxAdv.Show(this, "Invalid Rcm Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    rcmLookUpEdit.Focus();
            //    return false;
            //}
            //else if (!MillRecPara.Challan_Required && string.IsNullOrEmpty(itcLookUpEdit.Text))
            //{
            //    MessageBoxAdv.Show(this, "Invalid Itc Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    itcLookUpEdit.Focus();
            //    return false;
            //}
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }

            else if (string.IsNullOrEmpty(storeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Store", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                storeLookUpEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(processLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Job Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                processLookup1.Focus();
                return false;
            }
            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
          

            var trans = grnTransDtoBindingSource1.DataSource as List<MrvTransDto>;
            if(trans.Any(x=>x.ProductId== 0))
            {
                MessageBoxAdv.Show(this, "Invalid Finish Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.FocusedColumn = colProductId;
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.Pcs == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Pcs/Taka", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.FocusedColumn = colPcs;
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x =>x.ChallanNo==null))
            {
                MessageBoxAdv.Show(this, "Invalid Challan No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.FocusedColumn = colChallanNo;
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.Qty == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Meters", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.FocusedColumn = colQty;
                gridView1.Focus();
                return false;
            }
            //else if (trans.Any(x => x.Rate == 0))
            //{
            //    MessageBoxAdv.Show(this, "Invalid Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    gridView1.FocusedColumn = colRate;
            //    gridView1.Focus();
            //    return false;
            //}

            //check for duplicate bill no
            //using (var db = new KontoContext())
            //{
            //   // var accid = Convert.ToInt32(accLookup1.SelectedValue);
            //   // var find1 = db.Challans.FirstOrDefault(
            //   //x => x.AccId == accid && !x.IsDeleted && x.BillNo == billNoTextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId
            //   //&& x.YearId == KontoGlobals.YearId && x.Id != this.PrimaryKey);

            //   // if (!MillRecPara.Challan_Required && find1 != null)
            //   // {
            //   //     MessageBox.Show("Entered Bill No Already Exists for this Party");
            //   //     billNoTextEdit.Focus();
            //   //     return false;
            //   // }

            //    find1 = db.Challans.FirstOrDefault(
            //  x => x.AccId == accid && !x.IsDeleted && x.ChallanNo == challanNotextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId
            //  && x.YearId == KontoGlobals.YearId && x.Id != this.PrimaryKey);

            //    if (find1 != null)
            //    {
            //        MessageBox.Show("Entered Challan No Already Exists for this Party");
            //        challanNotextEdit.Focus();
            //        return false;
            //    }

            //}

            return true;
        }

        private void LoadData(ChallanModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            divLookUpEdit.EditValue = model.DivId;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);

           // bookLookup.SelectedValue = model.BookAcId;
          //  bookLookup.SetAcc(Convert.ToInt32(model.BookAcId));
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            voucherNoTextEdit.Text = model.VoucherNo;

            accLookup1.SetAcc(model.AccId);
            accLookup1.SelectedValue = model.AccId;

            if (Convert.ToInt32(model.DelvAccId) != 0)
            {
                delvLookup.SetAcc(Convert.ToInt32(model.DelvAccId));
                delvLookup.SelectedValue = model.DelvAccId;
                
            }
            addressLookup1.SetValue(Convert.ToInt32(model.DelvAdrId));
            addressLookup1.SelectedValue = model.DelvAdrId;
            

            challanNotextEdit.Text = model.ChallanNo;
         //   billNoTextEdit.Text = model.BillNo;
         //   billDateEdit.EditValue = model.RcdDate;

            if (Convert.ToInt32(model.EmpId) != 0)
            {
                empLookup1.SelectedValue = model.EmpId;
                empLookup1.SetGroup();
            }
            if (Convert.ToInt32(model.ProcessId) != 0)
            {
                processLookup1.SelectedValue = model.ProcessId;
                processLookup1.SetValue();
            }
            storeLookUpEdit.EditValue = model.StoreId;

            if (Convert.ToInt32(model.TransId) != 0)
            {
                transportLookup.SelectedValue = model.TransId;
                transportLookup.SetAcc((int)model.TransId);
            }
            lrNotextEdit.Text = model.DocNo;
            lrDateEdit.EditValue = model.DocDate;
            remarkTextEdit.Text = model.Remark;

          

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdOutModel, ProdOutDto>();
            });

            using (var _context = new KontoContext())
            {
                var _list = (from ct in _context.ChallanTranses
                             join pd in _context.Products on ct.ProductId equals pd.Id into join_pd
                             from pd in join_pd.DefaultIfEmpty()
                             join cl in _context.ColorModels on ct.ColorId equals cl.Id into join_cl
                             from cl in join_cl.DefaultIfEmpty()
                             join dm in _context.Products on ct.DesignId equals dm.Id into join_dm
                             from dm in join_dm.DefaultIfEmpty()
                             join mit in _context.ChallanTranses on new {x1=(int) ct.RefId,x2= ct.MiscId } equals new {x1=mit.Id,x2=mit.ChallanId} into join_mi
                             from mit in join_mi.DefaultIfEmpty()
                             join gp in _context.Products on mit.ProductId equals gp.Id into join_gp
                             from gp in join_gp.DefaultIfEmpty()
                             join mi in _context.Challans on mit.ChallanId equals mi.Id into join_mi1
                             from mi in join_mi1.DefaultIfEmpty()
                             orderby ct.Id
                             where ct.IsActive == true && ct.IsDeleted == false &&
                             ct.ChallanId == model.Id
                             select new MrvTransDto()
                             {
                                 Id = ct.Id, Cess = ct.Cess, CessPer = ct.CessPer, Cgst = ct.Cgst, CgstPer = ct.CgstPer,
                                 ChallanId = ct.ChallanId, ColorId = ct.ColorId.HasValue ? (int)ct.ColorId : 1, ColorName = cl.ColorName,
                                 DesignId = ct.DesignId.HasValue ? (int)ct.DesignId : 1, DesignNo = dm.ProductCode, Disc = ct.Disc, DiscPer = ct.DiscPer, Freight = ct.Freight,
                                 FreightRate = ct.FreightRate, IssueQty = ct.IssueQty, IssuePcs = ct.IssuePcs,
                                 Gross = ct.Gross, Igst = ct.Igst,
                                 IgstPer = ct.IgstPer, LotNo = ct.LotNo, MiscId = ct.MiscId, OtherAdd = ct.OtherAdd, OtherLess = ct.OtherLess,
                                 Pcs = ct.Pcs, ProductId = (int)ct.ProductId, ProductName = pd.ProductName, Qty = ct.Qty, Rate = ct.Rate, RefId = ct.RefId,
                                 RefVoucherId = ct.RefVoucherId, Remark = ct.Remark, Sgst = ct.Sgst, SgstPer = ct.SgstPer, Total = ct.Total, UomId = (int)ct.UomId,
                                 ChallanNo = mi.VoucherNo==null ? "0": mi.VoucherNo ,GreyQuality = gp.ProductName,ShQty = ct.IssueQty- ct.Qty,
                                 ShPer = ct.IssueQty >0 ? ((ct.IssueQty-ct.Qty)/ct.IssueQty)*100 : 0
                             }).ToList();

               var  _prodDtos = _context.ProdOuts.Where(x => x.RefId == model.Id && !x.IsDeleted).ToList();

                var map = new Mapper(config);

                prodDtos = map.Map<List<ProdOutModel>, List<ProdOutDto>>(_prodDtos);

                this.grnTransDtoBindingSource1.DataSource = _list;
            }


            FinalTotal();
            this.Text = "Job Sales Challan [View/Modify]";

        }
        private void ShowPendingLot(MrvTransDto ct)
        {
            if (processLookup1.LookupDto == null) return;

            var frm = new PendingOJCView();
            frm.AccId = Convert.ToInt32(this.accLookup1.SelectedValue);

            if (frm.ShowDialog() != DialogResult.OK) return;

            var ord = frm.SelectedRow;
            if (ord == null) return;
            if (ct.RefId > 0) return;
            ct.GreyQuality = ord.GreyQuality;
            if (!string.IsNullOrEmpty(ord.FinishQuality))
            {
                ct.ProductId = Convert.ToInt32(ord.ProductId);
                ct.ProductName = ord.FinishQuality;
            }
            else
            {
                ct.ProductId =  Convert.ToInt32(ord.ProductId);
                ct.ProductName = ord.GreyQuality;
            }
            //if(ct.pr)
            ct.IssuePcs = Convert.ToInt32(ord.PendingPcs);
            ct.IssueQty = ord.PendingQty != 0 ? (decimal)ord.PendingQty : 0;
            ct.Pcs = 0;
            ct.Qty = 0;
            ct.LotNo = ord.LotNo;
            ct.RefNo = ord.ChallanNo;
            ct.Rate = ord.Rate != null ? (decimal)ord.Rate : 0;

            ct.Sgst = 0;
            // ct.SgstPer = ord.Sgst != null ? (decimal)ord.Sgst : 0;
            ct.Cgst = 0;
            //ct.CgstPer = ord.Cgst;
            ct.Igst = 0;
            // ct.IgstPer = ord.Igst;
            if (isImortOrSez)
            {
                ct.Sgst = 0;
                ct.SgstPer = 0;
                ct.Cgst = 0;
                ct.CgstPer = 0;
                ct.IgstPer = processLookup1.LookupDto.Igst;
                ct.Igst = 0;
            }
            else
            {
                ct.SgstPer = processLookup1.LookupDto.Sgst;
                ct.CgstPer = processLookup1.LookupDto.Cgst;
                ct.IgstPer = 0;
                ct.Igst = 0;
            }
            ct.Disc = 0;
            ct.DiscPer = 0;
            ct.Gross = 0;
            ct.UomId = Convert.ToInt32(ord.UomId);
            ct.DesignId = Convert.ToInt32(ord.DesignId);
            ct.ColorId = Convert.ToInt32(ord.ColorId);

            ct.ChallanNo = ord.VoucherNo;

            ct.RefId = ord.TransId;
            ct.MiscId = ord.Id;
            ct.RefVoucherId = ord.VoucherId;

            ct.ShQty = 0; //ct.IssueQty - ct.Qty;
            ct.ShPer = 0; //ct.ShQty / ct.IssueQty;



            //if (!MillRecPara.Taka_Detail_Required) return;

            var Prlist = new List<PendingMRProd>();

            using (var _db = new KontoContext())
            {
                var spcol1 = _db.SpCollections.FirstOrDefault(k => k.Id ==
                           (int)SpCollectionEnum.PendingOJCProd);
                if (spcol1 == null)
                {
                     Prlist =      _db.Database.SqlQuery<PendingMRProd>(
                                  "dbo.PendingOJCProd @CompanyId={0},@IsOk={1},@transid={2},@voucherid={3}",
                                  KontoGlobals.CompanyId, 1, ord.TransId, (int)VoucherTypeEnum.Inward).ToList();
                }
                else
                {
                    Prlist = _db.Database.SqlQuery<PendingMRProd>(
                             spcol1.Name + " @CompanyId={0},@IsOk={1},@transid={2},@voucherid={3}",
                             KontoGlobals.CompanyId, 1, ord.TransId, (int)VoucherTypeEnum.Inward).ToList();
                }
            }

            //check for existing item already added
            List<PendingMRProd> _tempdel = new List<PendingMRProd>();
            foreach (var item in Prlist)
            {
                var _exist = this.prodDtos.FirstOrDefault(x => x.ProdId == item.ProdId && x.FinMrt > 0);
                if (_exist != null)
                    _tempdel.Add(item);
            }

            Prlist = Prlist.Except(_tempdel).ToList();

            foreach (var prod in Prlist)
            {
                ProdOutDto pm = new ProdOutDto();

                pm.Id = 0;
                pm.SrNo = (int)prod.SrNo;
                pm.ProdId = prod.ProdId;
                pm.VoucherNo = prod.VoucherNo;
                pm.ColorId = prod.ColorId;
                pm.CompId = prod.CompId;
                pm.GradeId = prod.GradeId;
                pm.YearId = prod.YearId;
                pm.ProductId = prod.ProductId;
                pm.GrayMtr = prod.GrayMtr;
                pm.FinMrt = prod.FinMrt;
                pm.TP1 = 0;
                pm.TP2 = 0;
                pm.TP3 = 0;
                pm.TP4 = 0;
                pm.TP5 = 0;
                pm.IsOk = true;
                pm.ShMtr = prod.GrayMtr;
                pm.TransId = ct.Id;
                this.prodDtos.Add(pm);
            }


        }
        private void ShowItemDetail(MrvTransDto er)
        {

            var frm = new OJCTakaDetails();

            frm.prodOutModelBindingSource.DataSource = this.prodDtos.Where(x => x.TransId == er.Id).ToList();
            if (frm.ShowDialog() != DialogResult.OK) return;
            var tempprod = (frm.prodOutModelBindingSource.DataSource as List<ProdOutDto>).Where(x => x.FinMrt > 0).ToList();
            var _delprod = (frm.prodOutModelBindingSource.DataSource as List<ProdOutDto>).Where(x => x.FinMrt == 0 && x.Id > 0).ToList();
            ////remove existing entry
            //foreach (var po in tempprod)
            //{
            //    this.prodDtos.Remove(po);
            //}


            decimal GrayMtr = 0;
            decimal FinMtr = 0;
            int FinPcs = 0;
            int GryPcs = 0;
            decimal ShMrt = 0;

            foreach (var pro in tempprod)
            {
                pro.RefId = this.PrimaryKey;
                pro.TransId = er.Id;
                pro.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                pro.ProductId = er.ProductId;
                if (pro.TP1 > 0)
                    FinPcs = FinPcs + 1;
                if (pro.TP2 > 0)
                    FinPcs = FinPcs + 1;
                if (pro.TP3 > 0)
                    FinPcs = FinPcs + 1;
                if (pro.TP4 > 0)
                    FinPcs = FinPcs + 1;
                if (pro.TP5 > 0)
                    FinPcs = FinPcs + 1;
                decimal gm = pro.FinMrt == null || pro.FinMrt == 0 ? 0 : (decimal)pro.GrayMtr;
                decimal fm = pro.FinMrt == null || pro.FinMrt == 0 ? 0 : (decimal)pro.FinMrt;
                decimal sm = pro.ShMtr == null ? 0 : (decimal)pro.ShMtr;
                if (pro.FinMrt > 0)
                    GryPcs = GryPcs + 1;

                GrayMtr = GrayMtr + gm;
                FinMtr = FinMtr + fm;
                ShMrt = ShMrt + sm;

                //this.prodDtos.Add(pro);
            }

            DelProd.AddRange(_delprod);

            er.Qty = FinMtr;
            er.Pcs = FinPcs;
            er.IssuePcs = GryPcs;
            er.IssueQty = GrayMtr;
            er.ShQty = GrayMtr - FinMtr;
            er.ShPer = decimal.Round(er.ShQty * 100 / GrayMtr, 2, MidpointRounding.AwayFromZero);


            GridCalculation(er);
            gridView1.FocusedColumn = colRate;
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
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!"Pcs,Qty,IssuePcs,IssueQty".Contains(gridView1.FocusedColumn.FieldName)) return;
            if (!(gridView1.GetFocusedRow() is MrvTransDto itm)) return;
            if (this.prodDtos.Any(x => x.TransId == itm.Id))
                e.Cancel = true;

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
            if (!(gridView1.GetRow(e.RowHandle) is MrvTransDto er)) return;
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
                var row = view.GetRow(view.FocusedRowHandle) as MrvTransDto;
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
            var rw = gridView1.GetRow(e.RowHandle) as MrvTransDto;
            rw.Id = -1 * gridView1.RowCount;
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (gridView1.FocusedColumn.FieldName == "ChallanNo")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (Convert.ToInt32(dr.RefId) == 0)
                        {
                            ShowPendingLot(dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        ShowPendingLot(dr);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "ProductName")
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
                else if (gridView1.FocusedColumn.FieldName == "LotNo")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        ShowItemDetail(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Job Sales Challan GridControl KeyDown");
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

        private void DesignRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenDesignLookup(dr.DesignId, dr);
        }
        private void LotNoRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                ShowItemDetail(dr);
        }
        private void ChallanNoRrepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                ShowPendingLot(dr);
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
            if (this.PrimaryKey == 0 && Convert.ToInt32(this.accLookup1.SelectedValue) > 0)
            {
                this.delvLookup.SelectedValue = this.accLookup1.SelectedValue;
                this.delvLookup.buttonEdit1.Text = this.accLookup1.SelectedText;
                this.delvLookup.SelectedText = this.accLookup1.SelectedText;
                this.delvLookup.LookupDto = this.accLookup1.LookupDto;
                addressLookup1.SelectedValue = this.delvLookup.LookupDto.AddressId;
                addressLookup1.SelectedText = this.delvLookup.LookupDto.FullAddress;
                addressLookup1.buttonEdit1.Text = this.delvLookup.LookupDto.FullAddress;

            }

            if (accLookup1.LookupDto.IsGst)
            {
                colSgst.Visible = true;
                colSgstAmt.Visible = true;
                colCgst.Visible = true;
                colCgstAmt.Visible = true;
                colIgst.Visible = false;
                colIgstAmt.Visible = false;
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var rw = gridView1.GetRow(i) as MrvTransDto;
                    var gst = rw.Igst + rw.Cgst + rw.Sgst;
                    var gstPer = rw.IgstPer + rw.CgstPer + rw.SgstPer;
                    rw.Igst = 0;
                    rw.IgstPer = 0;
                    rw.Sgst = gst / 2;
                    rw.Cgst = gst / 2;
                    rw.SgstPer = gstPer / 2;
                    rw.CgstPer = gstPer / 2;
                }
            }
            if (accLookup1.LookupDto.IsIgst || isImortOrSez)
            {
                colSgst.Visible = false;
                colSgstAmt.Visible = false;
                colCgst.Visible = false;
                colCgstAmt.Visible = false;
                colIgst.Visible = true;
                colIgstAmt.Visible = true;

                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var rw = gridView1.GetRow(i) as MrvTransDto;
                    var gst = rw.Igst + rw.Cgst + rw.Sgst;
                    var gstPer = rw.IgstPer + rw.CgstPer + rw.SgstPer;
                    rw.IgstPer = gstPer;
                    rw.Igst = gst;
                    rw.Sgst = 0;
                    rw.Cgst = 0;
                    rw.SgstPer = 0;
                    rw.CgstPer = 0;
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
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                if (tabPageAdv2.Controls.Count > 0)
                {
                    var _list = tabPageAdv2.Controls[0] as OJCListView;
                    _list.ActiveControl = _list.KontoGrid;
                    this.Text = "Job Sales Challan [View]";
                    return;
                }
                var _ListView = new OJCListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Job Sales Challan [View]";

            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                //if (tabPageAdv4.Controls.Count > 0) return;
                //var _frm = new ChlParaMainView();
                //_frm.TopLevel = false;
                //_frm.Parent = tabPageAdv4;
                //_frm.ReportFilterType = "MillRec";
                //_frm.Location = new Point(tabPageAdv4.Location.X + tabPageAdv4.Width / 2 - _frm.Width / 2, tabPageAdv4.Location.Y + tabPageAdv4.Height / 2 - _frm.Height / 2);
                //_frm.Show();// = true;

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

                Log.Error(ex, "job sales Save");
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

                rpt.Load(new FileInfo("reg\\doc\\TakaJobSalesChallan.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString =
                    KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["challan"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Job Sales Challan";
                //var _tab = this.Parent.Parent as TabControlAdv;
                //if (_tab == null) return;
                //var pg1 = new TabPageAdv();
                // pg1.Text = "Challan Print";
                //_tab.TabPages.Add(pg1);
                // _tab.SelectedTab = pg1;
                // frm.TopLevel = false;
                //  frm.Parent = pg1;
                //  frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Job Sales Challan print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ChallanModel>();
            this.Text = "Job Sales Challan [Add New]";
           
            storeLookUpEdit.EditValue = 1;
            divLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
           // billDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            //if (Convert.ToInt32(voucherLookup1.GroupDto.AccId) > 0)
            //{
            //    bookLookup.SelectedValue = voucherLookup1.GroupDto.AccId;
            //    bookLookup.SetAcc(Convert.ToInt32(voucherLookup1.GroupDto.AccId));
            //}

            DelTrans = new List<MrvTransDto>();
            DelProd = new List<ProdOutDto>();
            prodDtos = new List<ProdOutDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<MrvTransDto>();
            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
            accLookup1.SetEmpty();
          //  bookLookup.SetEmpty();
            challanNotextEdit.Text = string.Empty;
          //  billNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
          //  billDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            delvLookup.SetEmpty();
            addressLookup1.SetEmpty();
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            processLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
           
            //roundoffSpinEdit.Value = 0;
            billAmtSpinEdit.Value = 0;
            
            DelTrans = new List<MrvTransDto>();
            DelProd = new List<ProdOutDto>();
            prodDtos = new List<ProdOutDto>();
            
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

          
            using (var db = new KontoContext())
            {
                var bill = db.Challans.Find(_key);
                if (bill != null)
                {
                    LoadData(bill);
                }
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
            if (!string.IsNullOrEmpty(challanNotextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "ChallanNo", Operation = Op.Equals, Value = challanNotextEdit.Text.Trim() });
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
                FilterView = db.Challans.Where(ExpressionBuilder.GetExpression<ChallanModel>(filter))
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
          
            var _find = new ChallanModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MrvTransDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<ProdOutDto, ProdOutModel>().ForMember(x => x.Id, p => p.Ignore());
            });
            
            var _translist = grnTransDtoBindingSource1.DataSource as List<MrvTransDto>;
            List<ChallanTransModel> Trans = new List<ChallanTransModel>();
            List<ProdModel> ProdList = new List<ProdModel>();
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
                            _find = db.Challans.Find(this.PrimaryKey);
                        }
                        _find.TotalQty = _translist.Sum(x => x.Qty);
                        _find.TotalPcs = _translist.Sum(x => x.Pcs);
                        _find.TotalAmount = _translist.Sum(x => x.Total);
                        
                        if(!UpdateChallan(db, _find))
                        {
                            _tran.Rollback();
                            return;
                        }
                  
                        var map = new Mapper(config);

                        string bill = string.Empty;
                        foreach (var item in _translist)
                        {
                            var transid = item.Id;
                            item.ChallanId = _find.Id;
                            var tranModel = new ChallanTransModel();
                            if(item.Id > 0)
                            {
                                tranModel = db.ChallanTranses.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);
                            
                            if (!bill.Contains(item.ChallanNo))
                            {
                                if (string.IsNullOrEmpty(bill))
                                    bill = item.ChallanNo;
                                else
                                    bill = bill + "," + item.ChallanNo;
                            }
                            if (tranModel.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();
                               
                            }
                            item.Id = tranModel.Id;
                            Trans.Add(tranModel);
                            // add subdetails item details
                            var prlist = prodDtos.Where(k => k.TransId == transid && k.FinMrt > 0).ToList();
                            var pUnUsedList = prodDtos.Where(k => k.FinMrt == 0 && k.Id > 0).ToList();

                            foreach (var item1 in pUnUsedList)
                            {
                                if (item1.Id > 0)
                                {
                                    item1.IsDeleted = true;
                                }
                            }

                            foreach (var p in prlist)
                            {
                                var pOut = new ProdOutModel();
                                if (p.Id > 0)
                                    pOut = db.ProdOuts.Find(p.Id);
                                map.Map(p, pOut);
                                pOut.IsActive = true;
                                if (p.Id==0)
                                {
                                    pOut.VoucherNo = _find.VoucherNo;
                                    pOut.TransId = tranModel.Id;
                                    pOut.VoucherId = _find.VoucherId;
                                    pOut.RefId = _find.Id;
                                    pOut.Qty = -1*p.FinMrt;
                                    
                                    db.ProdOuts.Add(pOut);
                                }
                                else
                                {
                                    pOut.Qty = -1* p.FinMrt;
                                }
                            }

                        }
                        if (!string.IsNullOrEmpty(bill))
                        {
                            if (bill.Length > 25)
                                _find.ChallanNo = bill.Substring(0, 25);
                            else
                                _find.ChallanNo = bill;
                        }
                        //delete item from ord trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;

                            var delprod = db.ProdOuts.Where(p => p.TransId == item.Id).ToList();
                            foreach (var item1 in delprod)
                            {
                                item1.IsDeleted = true;
                            }
                        }

                        // delete from item details
                        foreach (var p in DelProd)
                        {
                            if (p.Id == 0) continue;
                             var prd = db.ProdOuts.Find(p.Id);
                            if (prd != null)
                            {
                                prd.FinMrt = p.FinMrt;
                                prd.IsDeleted = true;
                            }
                        }

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        foreach (var item in Trans)
                        {
                            string TableName = "OJobChallan";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                            
                           // var prList = ProdList.Where(x => x.TransId == item.Id).ToList();
                            StockEffect.StockTransChlnEntry(_find, item,true, TableName, KontoGlobals.UserName, db);
                            
                        }

                       // UpdateBill(db,_find);

                         db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Job Sales Challan Save");
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
                    if (this.voucherLookup1.GroupDto.PrintAfterSave && MessageBox.Show("Print Challan ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.PrimaryKey = _find.Id;
                        Print();
                        this.PrimaryKey = 0;
                    }

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

        private bool UpdateChallan(KontoContext db,ChallanModel model)
        {
           
            model.DivId = Convert.ToInt32(divLookUpEdit.EditValue);
          

            model.ChallanType = (int)ChallanTypeEnum.SALES_JOB;
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.DelvAccId = Convert.ToInt32(delvLookup.SelectedValue);
            model.DelvAdrId = Convert.ToInt32(addressLookup1.SelectedValue);

            // model.BookAcId = Convert.ToInt32(bookLookup.SelectedValue);
            // model.RcdDate = billDateEdit.DateTime;
            model.VoucherNo = voucherNoTextEdit.Text.Trim();
            model.ProcessId = Convert.ToInt32(processLookup1.SelectedValue);
            model.AgentId = Convert.ToInt32(agentLookup.SelectedValue);
            model.ChallanNo = challanNotextEdit.Text.Trim();
          //  model.BillNo = billNoTextEdit.Text.Trim();

            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remark = remarkTextEdit.Text.Trim();
            model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
            model.DocNo = lrNotextEdit.Text.Trim();
            model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);
            model.TypeId = (int)VoucherTypeEnum.OutJobChallan;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
          //  model.RoundOff = roundoffSpinEdit.Value;

          
            var _translist = grnTransDtoBindingSource1.DataSource as List<MrvTransDto>;
            model.TotalAmount = billAmtSpinEdit.Value;
          
            model.TotalQty = _translist.Sum(x => x.Qty);
            model.TotalPcs = _translist.Sum(x => x.Pcs);
            model.IsActive = true;

            if (string.IsNullOrEmpty(model.ChallanNo)) model.ChallanNo = "NA";

            if (model.Id == 0)
            {
                model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);

                if (DbUtils.CheckExistVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                {
                    MessageBox.Show("Duplicate Voucher No Not Allowed");
                    return false;
                }

                db.Challans.Add(model);
                db.SaveChanges();
            }

            return true;
        }

        private bool UpdateBill(KontoContext db,ChallanModel model)
        {
            BillModel billModel = new BillModel();

            var _translist = grnTransDtoBindingSource1.DataSource as List<MrvTransDto>;

            if (this.PrimaryKey!=0)
            {
                billModel = db.Bills.FirstOrDefault(x => x.RefId == model.Id && x.RefVoucherId == model.VoucherId);
             
            }
            billModel.CompId = KontoGlobals.CompanyId;
            billModel.YearId = KontoGlobals.YearId;
            billModel.BranchId = KontoGlobals.BranchId;
            billModel.TypeId = (int)VoucherTypeEnum.OutJobChallan;
            billModel.BookAcId = model.BookAcId;
            billModel.AccId = model.AccId;
            billModel.VoucherDate = model.VoucherDate;
            billModel.BillNo = model.BillNo;
            billModel.RcdDate = model.RcdDate;
            billModel.GrossAmount = _translist.Sum(x => x.Gross);
            billModel.TotalAmount = billAmtSpinEdit.Value;
            billModel.TotalQty = _translist.Sum(x => x.Qty);
            billModel.TotalPcs = _translist.Sum(x => x.Pcs);
           // billModel.RoundOff = roundoffSpinEdit.Value;
           
            billModel.AgentId = model.AgentId;
            billModel.RefNo = model.ChallanNo;
            billModel.StoreId = model.StoreId;
            billModel.BillType = model.BillType;
            billModel.EmpId = model.EmpId;
            billModel.Rcm = model.Rcm;
            billModel.Itc = model.Itc;
            billModel.TransId = model.TransId;
            billModel.DocNo = model.DocNo;
            billModel.DocDate = model.DocDate;
            billModel.VehicleNo = model.VehicleNo;
            billModel.Remarks = model.Remark;
            billModel.DivisionId = model.DivId;
            billModel.HasteId = model.BillId; // tds amount
            billModel.TdsPer = model.TdsPer;
            billModel.TdsAmt = model.TdsAmt;
            billModel.RefId = model.Id;
            billModel.RefVoucherId = model.VoucherId;

            if (this.PrimaryKey == 0)
            {
                int vtypeid = (int)VoucherTypeEnum.OutJobChallan;
                var vouchr = db.Vouchers.FirstOrDefault(k => k.VTypeId == vtypeid);
                billModel.VoucherId = vouchr.Id;
                billModel.VoucherNo = DbUtils.NextSerialNo((int)billModel.VoucherId, db);

                if (DbUtils.CheckExistVoucherNo(billModel.VoucherId, billModel.VoucherNo, db, billModel.Id))
                {
                    MessageBox.Show("Duplicate Voucher No Not Allowed");
                    return false;
                }

                db.Bills.Add(billModel);
                db.SaveChanges();
            }
            else
            {
                var bt = db.BillTrans.Where(x => x.BillId == billModel.Id);
                db.BillTrans.RemoveRange(bt);
            }

            foreach (var ctrModel in _translist)
            {
                var btModel = new BillTransModel();
                btModel.ProductId = ctrModel.ProductId;
                btModel.ColorId = ctrModel.ColorId;
                btModel.GradeId = 1;
                btModel.DesignId = ctrModel.DesignId;
                btModel.UomId = ctrModel.UomId;
                btModel.LotNo = ctrModel.LotNo;
                btModel.Pcs = ctrModel.Pcs;
                btModel.Qty = ctrModel.Qty != 0 ? (decimal)ctrModel.Qty : 0;
                btModel.Rate = ctrModel.Rate != 0 ? (decimal)ctrModel.Rate : 0;
                btModel.Sgst = ctrModel.Sgst != 0 ? (decimal)ctrModel.Sgst : 0;
                btModel.SgstPer = ctrModel.SgstPer != 0 ? (decimal)ctrModel.SgstPer : 0;
                btModel.Cgst = ctrModel.Cgst != 0 ? (decimal)ctrModel.Cgst : 0;
                btModel.CgstPer = ctrModel.CgstPer != 0 ? (decimal)ctrModel.CgstPer : 0;
                btModel.Igst = ctrModel.Igst != 0 ? (decimal)ctrModel.Igst : 0;
                btModel.IgstPer = ctrModel.IgstPer != 0 ? (decimal)ctrModel.IgstPer : 0;
                btModel.Disc = ctrModel.DiscPer != 0 ? (decimal)ctrModel.DiscPer : 0;
                btModel.DiscAmt = ctrModel.Disc != 0 ? (decimal)ctrModel.Disc : 0;
               // btModel.ChallanNo = model.ChallanNo;
               // btModel.ChallanDate = voucherDateEdit.DateTime;

                btModel.RefId = model.Id;
                btModel.RefTransId = ctrModel.Id;
                btModel.RefVoucherId = model.VoucherId;

                btModel.FreightRate = ctrModel.FreightRate != 0 ? (decimal)ctrModel.FreightRate : 0;
                btModel.Freight = ctrModel.Freight != 0 ? (decimal)ctrModel.Freight : 0;
                btModel.OtherAdd = ctrModel.OtherAdd != 0 ? (decimal)ctrModel.OtherAdd : 0;
                btModel.OtherLess = ctrModel.OtherLess != 0 ? (decimal)ctrModel.OtherLess : 0;
                btModel.CessPer = ctrModel.CessPer != 0 ? (decimal)ctrModel.CessPer : 0;
                btModel.Cess = ctrModel.Cess != 0 ? (decimal)ctrModel.Cess : 0;
                
                btModel.Width = ctrModel.IssuePcs;
                btModel.AvgWt = ctrModel.IssueQty;

                btModel.Total = decimal.Round(btModel.Qty * btModel.Rate, 2);
                btModel.DiscAmt = btModel.Total * btModel.Disc / 100;
                decimal gross = btModel.Total - btModel.DiscAmt + btModel.Freight + btModel.OtherAdd - btModel.OtherLess;

                btModel.Sgst = decimal.Round(gross * btModel.SgstPer / 100, 2);
                btModel.Cgst = decimal.Round(gross * btModel.CgstPer / 100, 2); //, MidpointRounding.AwayFromZero);
                btModel.Igst = decimal.Round(gross * btModel.IgstPer / 100, 2); //, MidpointRounding.AwayFromZero);

                btModel.NetTotal = decimal.Round(gross + btModel.Sgst + btModel.Cgst + btModel.Igst, 0);
                btModel.BillId = billModel.Id;
                db.BillTrans.Add(btModel);
            }
            

             LedgerEff.BillRefEntry("Credit", billModel, _translist.FirstOrDefault().ProductId ,db);

            LedgerEff.LedgerTransEntry("Credit",  billModel, db,new List<BillTransModel>());

            return true;
        }

        #endregion

        
    }
}

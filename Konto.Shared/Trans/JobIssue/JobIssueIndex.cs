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
using Konto.Data.Models.Transaction.TradingDto;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Item;
using Konto.Shared.Trans.Common;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Shared.Trans.JobIssue
{
    public partial class JobIssueIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<MiTransDto> DelTrans = new List<MiTransDto>();
        private List<GrnProdDto> prodDtos = new List<GrnProdDto>();
        private List<GrnProdDto> DelProd = new List<GrnProdDto>();
        private List<JobIssueBarcodeDto> barcodeDtos = new List<JobIssueBarcodeDto>();
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private int OrderId;
        private int OrderVoucherId;
        private int OrderTransId;
        private decimal OrderQty;
        public JobIssueIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;

            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;


            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.MouseUp += GridView1_MouseUp;
            lotNoRepositoryItemButtonEdit.ButtonClick += LotNoRepositoryItemButtonEdit_ButtonClick;
           

            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Job_Issue_Index;
            this.GridLayoutFile = KontoFileLayout.Job_Issue_Trans;

            FillLookup();
            SetParameter();
            SetGridColumn();

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            accLookup1.SelectedValueChanged += AccLookup1_SelectedValueChanged;
            this.delvLookup.SelectedValueChanged += DelvLookup_SelectedValueChanged;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
            barcodeSimpleButton.Click += BarcodeSimpleButton_Click;
            RefNobuttonEdit.ButtonClick += RefNobuttonEdit_ButtonClick;
            RefNobuttonEdit.KeyDown += RefNobuttonEdit_KeyDown;
            this.Load += JobIssueIndex_Load;

            this.FirstActiveControl = grnTypeLookUpEdit;
        }


        #region UDF
        private void SetGridColumn()
        {
            colColorName.Visible = JobIssPara.Color_Required;
            colDesignNo.Visible = JobIssPara.Design_Required;
            colCut.Visible = JobIssPara.Cut_Required;
        }
        private MiTransDto PreOpenLookup()
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return null;
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (MiTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }

        public void GridCalculation(MiTransDto er)
        {


            var dr = uomRepositoryItemLookUpEdit.GetDataSourceRowByKeyValue(er.UomId) as UomLookupDto;

            if (dr != null && dr.RateOn == "N" && er.Qty > 0)
            {
                er.Gross = decimal.Round(er.Pcs * er.Rate, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                er.Gross = decimal.Round(er.Qty * er.Rate, 2, MidpointRounding.AwayFromZero);
            }

            if (er.Pcs > 0 && er.Cops > 0 && KontoGlobals.PackageId == 1)
                er.Qty = er.Pcs * er.Cops;

            if (er.DiscPer > 0)
                er.Disc = decimal.Round(er.Gross * er.DiscPer / 100, 2, MidpointRounding.AwayFromZero);
            decimal gross = er.Gross - er.Disc;

            if (er.FreightRate > 0)
                er.Freight = decimal.Round(er.Qty * er.FreightRate / 100, 2, MidpointRounding.AwayFromZero);

            gross = gross + er.Freight + er.OtherAdd - er.OtherLess;

            er.Sgst = decimal.Round(gross * er.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
            er.Cgst = decimal.Round(gross * er.CgstPer / 100, 2, MidpointRounding.AwayFromZero);
            er.Igst = decimal.Round(gross * er.IgstPer / 100, 2, MidpointRounding.AwayFromZero);

            er.Cess = decimal.Round(er.Qty * er.CessPer, 2, MidpointRounding.AwayFromZero);
            er.Total = gross + er.Sgst + er.Cgst + er.Igst;

            gridView1.UpdateCurrentRow();
        }
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "JobIssue" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 122://JOb Issue
                            {
                                JobIssPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 123:
                            {
                                JobIssPara.Batch_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 124:
                            {
                                JobIssPara.LotNo_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 126:
                            {
                                JobIssPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 125:
                            {
                                JobIssPara.Grade_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 127:
                            {
                                JobIssPara.Cut_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 128:
                            {
                                JobIssPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 129:
                            {
                                JobIssPara.Auto_Book_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 130:
                            {
                                JobIssPara.Auto_Voucher_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 135:
                            {
                                JobIssPara.Finish_Product_Requred = (value == "Y") ? true : false;
                                break;
                            }
                        case 177:
                            {
                                JobIssPara.Issue_By_Barcode = (value == "Y") ? true : false;
                                break;
                            }
                        case 206:
                            {
                                JobIssPara.Job_Issue_Against_Order = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }
        }
        private void OpenItemLookup(int _selvalue, MiTransDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
            frm.VoucherType = VoucherTypeEnum.JobIssue;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                if (gridView1.FocusedColumn.FieldName == "FinishQuality")
                {
                    er.NProductId = frm.SelectedValue;
                    er.FinishQuality = frm.SelectedTex;
                    gridView1.UpdateCurrentRow();
                    return;
                }
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                if (string.IsNullOrEmpty(er.FinishQuality))
                {
                    er.NProductId = frm.SelectedValue;
                    er.FinishQuality = frm.SelectedTex;
                }
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
                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }
            GridCalculation(er);
        }
        private void OpenColorLookup(int _selvalue, MiTransDto er)
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

        private void OpenDesignLookup(int _selvalue, MiTransDto er)
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
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("SEZ","SEZ"),
                new ComboBoxPairs("NON SEZ", "NON SEZ"),
            };
            jobTypeLookUpEdit1.Properties.DataSource = cbp;

            List<ComboBoxPairs> cbj = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Inputs", "Inputs"),
                new ComboBoxPairs("Capital Goods", "Capital Goods"),

            };
            goodsLookUpEdit.Properties.DataSource = cbj;

            using (var db = new KontoContext())
            {
                var TransTypeList = (from p in db.transTypes
                                     where p.IsActive && !p.IsDeleted && (p.Category.ToUpper() == "JOBISSUE" || p.Category == null)
                                     select new BaseLookupDto()
                                     {
                                         DisplayText = p.TypeName,
                                         Id = p.Id
                                     }).ToList();

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
                                    Id = p.Id,
                                    RateOn = p.RateOn
                                }).ToList();



                uomRepositoryItemLookUpEdit.DataSource = _uomlist;
                grnTypeLookUpEdit.Properties.DataSource = TransTypeList;
                divLookUpEdit.Properties.DataSource = _divLists;
                storeLookUpEdit.Properties.DataSource = _storeLists;
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

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
            else if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Party", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                accLookup1.Focus();
                return false;
            }

            else if (string.IsNullOrEmpty(grnTypeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Challan Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                grnTypeLookUpEdit.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Challan date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            var trans = grnTransDtoBindingSource1.DataSource as List<MiTransDto>;
            if (trans.Any(x => x.Pcs == 0) || trans.Any(x => x.Qty == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Pcs/qty", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            if (JobIssPara.Job_Issue_Against_Order && string.IsNullOrEmpty(RefNobuttonEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Ref No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefNobuttonEdit.Focus();
                return false;
            }
            //if (trans.Any(x => x.Rate == 0))
            //{
            //    MessageBoxAdv.Show(this, "Invalid Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    gridView1.Focus();
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

        private void LoadData(ChallanModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            divLookUpEdit.EditValue = model.DivId;
            grnTypeLookUpEdit.EditValue = model.ChallanType;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
           
            voucherNoTextEdit.Text = model.VoucherNo;
            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc(model.AccId);
            receiveDateEdit.EditValue = model.RcdDate;

            if (Convert.ToInt32(model.DelvAccId) != 0)
            {
                delvLookup.SelectedValue = model.DelvAccId;
                delvLookup.SetAcc(Convert.ToInt32(model.DelvAccId));
            }
            challanNotextEdit.Text = model.ChallanNo;

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
            addressLookup1.SelectedValue = model.DelvAdrId;
            addressLookup1.SetValue(Convert.ToInt32(model.DelvAdrId));

            if (Convert.ToInt32(model.TransId) != 0)
            {
                transportLookup.SelectedValue = model.TransId;
                transportLookup.SetAcc((int)model.TransId);
            }
            lrNotextEdit.Text = model.DocNo;
            lrDateEdit.EditValue = model.DocDate;
            vehicleTextEdit.Text = model.VehicleNo;
            driverTextEdit.Text = model.DName;


            remarkTextEdit.Text = model.Remark;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

            using (var _context = new KontoContext())
            {
                var _list = (from ct in _context.ChallanTranses
                             join pd in _context.Products on ct.ProductId equals pd.Id into join_pd
                             from pd in join_pd.DefaultIfEmpty()
                             join cl in _context.ColorModels on ct.ColorId equals cl.Id into join_cl
                             from cl in join_cl.DefaultIfEmpty()
                             join dm in _context.Products on ct.DesignId equals dm.Id into join_dm
                             from dm in join_dm.DefaultIfEmpty()
                             join np in _context.Products on ct.NProductId equals np.Id into join_np
                             from np in join_np.DefaultIfEmpty()
                             orderby ct.Id
                             where ct.IsActive == true && ct.IsDeleted == false &&
                             ct.ChallanId == model.Id
                             select new MiTransDto()
                             {
                                 Id = ct.Id,
                                 Cess = ct.Cess,
                                 CessPer = ct.CessPer,
                                 Cgst = ct.Cgst,
                                 CgstPer = ct.CgstPer,
                                 ChallanId = ct.ChallanId,
                                 ColorId = ct.ColorId.HasValue ? (int)ct.ColorId : 1,
                                 ColorName = cl.ColorName,
                                 DesignId = ct.DesignId.HasValue ? (int)ct.DesignId : 1,
                                 DesignNo = dm.ProductCode,
                                 Disc = ct.Disc,
                                 DiscPer = ct.DiscPer,
                                 Freight = ct.Freight,
                                 FreightRate = ct.FreightRate,
                                 Gross = ct.Gross,
                                 Igst = ct.Igst,
                                 IgstPer = ct.IgstPer,
                                 LotNo = ct.LotNo, 
                                 OtherAdd = ct.OtherAdd,
                                 OtherLess = ct.OtherLess,
                                 Pcs = ct.Pcs,
                                 ProductId = (int)ct.ProductId,
                                 ProductName = pd.ProductName,
                                 Qty = ct.Qty,
                                 Rate = ct.Rate,
                                 RefId = ct.RefId,
                                 MiscId = ct.MiscId,
                                 RefVoucherId = ct.RefVoucherId,
                                 Remark = ct.Remark,
                                 Sgst = ct.Sgst,
                                 SgstPer = ct.SgstPer,
                                 Total = ct.Total,
                                 UomId = (int)ct.UomId,
                                 FinishQuality = np.ProductName,
                                 NProductId = (int)ct.NProductId,
                                 RefNo = ct.RefNo,Cops = ct.Cops
                                 
                             }).ToList();

                //prodDtos = _context.ProdOuts.Where(x => x.RefId == model.Id && !x.IsDeleted)
                //                         .ProjectToList<GrnProdDto>(config);

                //var spcol = _context.SpCollections.FirstOrDefault(k => k.Id ==
                //                (int)SpCollectionEnum.OutwardprodList);
                //if (spcol == null)
                //{
                //    prodDtos = _context.Database.SqlQuery<GrnProdDto>(
                //                                "dbo.OutwardprodList @CompanyId={0},@VoucherId={1}," +
                //                                "@RefId={2}", KontoGlobals.CompanyId, (int)VoucherTypeEnum.MillIssue, model.Id).ToList();

                //}
                //else
                //{
                //    prodDtos = _context.Database.SqlQuery<GrnProdDto>(
                //     spcol.Name + " @CompanyId={0},@VoucherId={1}," +
                //                                "@RefId={2}", KontoGlobals.CompanyId, (int)VoucherTypeEnum.MillIssue, model.Id).ToList();
                //}
                var lst = (from po in _context.ProdOuts
                           join ct in _context.ChallanTranses on po.TransId equals ct.Id into join_ct
                           from ct in join_ct.DefaultIfEmpty()
                           join c in _context.Challans on ct.ChallanId equals c.Id into join_c
                           from c in join_c.DefaultIfEmpty()
                           join ac in _context.Accs on c.AccId equals ac.Id into join_ac
                           from ac in join_ac.DefaultIfEmpty()
                           join p in _context.Prods on po.ProdId equals p.Id into join_p
                           from p in join_p.DefaultIfEmpty()
                           orderby ct.Id
                           where ct.IsActive == true && ct.IsDeleted == false &&
                           po.RefId == model.Id && po.IsDeleted == false
                           select new GrnProdDto()
                           {
                               RefId = po.RefId,
                               TransId = po.TransId,
                               ProductId = po.ProductId,
                               ColorId = po.ColorId,
                               Id = (int)po.ProdId,
                               SrNo = p.SrNo,
                               ProdOutId = po.Id,
                               NetWt = (decimal)po.Qty *-1,
                               VoucherNo = po.VoucherNo,
                               Weaver = ac.AccName,
                               ChallanNo = c.VoucherNo,
                               VoucherDate = c.VoucherDate,
                               Extra1 = p.Extra1, 
                           }).ToList();

                this.prodDtos = lst;
                this.grnTransDtoBindingSource1.DataSource = _list;
                OrderQty = _list.Sum(k => k.Qty);
                RefNobuttonEdit.Text = _list.FirstOrDefault().RefNo;
            } 

           this.Text = "Job Issue Challan [View/Modify]";

        }
        private void ShowOrderPending()
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0 || this.PrimaryKey != 0) return;
                // if (grnTypeLookUpEdit.Text.ToUpper() != "PURCHASE") return;
                var ordfrm = new PendingSingleOrderView();
                ordfrm.VoucherType =(VoucherTypeEnum)voucherLookup1.SelectedValue;
                
                ordfrm.ShowDialog();
                if (ordfrm.DialogResult != DialogResult.OK) return;

                RefNobuttonEdit.Text = ordfrm.SelectedRow.VoucherNo;
                OrderId = ordfrm.SelectedRow.Id;
                OrderTransId = (int)ordfrm.SelectedRow.TransId; 
                OrderVoucherId = (int)ordfrm.SelectedRow.VoucherId;
                OrderQty = (decimal)ordfrm.SelectedRow.PendingQty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ShowItemDetail(MiTransDto er)
        {
            ProductModel prod = null;
            using (var db = new KontoContext())
            {
                prod = db.Products.Include("PType").SingleOrDefault(x => x.Id == er.ProductId);

            }
            if (prod == null || prod.SerialReq == "No") return;

            var frm = new IssueItemDetailView();
            frm.TypeEnum = (ProductTypeEnum)prod.PTypeId;

            if (prod.PType.TypeName.ToUpper() == "YARN" || prod.PType.TypeName.ToUpper() == "POY")
            {
                frm.GridLayoutFileName = KontoFileLayout.Job_Issue_Yarn_Item_Details;
                frm.Text = "Box Details";
            }
            else if (prod.PType.TypeName.ToUpper() == "GREY")
            {
                frm.GridLayoutFileName = KontoFileLayout.Job_Issue_Grey_Item_Details;
                frm.Text = "Taka Details";
            }
            else if (prod.PType.TypeName.ToUpper() == "BEAM")
            {
                frm.GridLayoutFileName = KontoFileLayout.Job_Issue_Beam_Item_Details;
                frm.Text = "Beam Details";
            }
            else
            {
                frm.Text = "Product Details";
                frm.GridLayoutFileName = KontoFileLayout.Job_Issue_Finish_Item_Details;
            }
            frm.Text = "Taka/Box Details";
            frm.TransId = Convert.ToInt32(er.Id);
            frm.ItemId = er.ProductId;
            frm.IsEditableQty = true;
            frm.prodDtos = new BindingList<GrnProdDto>(this.prodDtos.Where(x => x.TransId == er.Id || x.RefTransId == er.Id).ToList());
            if (frm.ShowDialog() != DialogResult.OK) return;
            var tempprod = frm.gridControl1.DataSource as BindingList<GrnProdDto>;

            //remove existing entry
            foreach (var po in tempprod)
            {
                this.prodDtos.Remove(po);
            }

            foreach (var pro in tempprod)
            {
                pro.RefId = this.PrimaryKey;
                pro.TransId = er.Id;
                pro.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                this.prodDtos.Add(pro);
            }
            foreach (var pro in frm.DelProd)
            {
                this.prodDtos.Remove(pro);
                this.DelProd.Add(pro);
            }
            er.Qty = tempprod.Sum(x => x.NetWt);
            var sumPcs = tempprod.Sum(x => x.Tops);
            if (sumPcs > 0)
            {
                er.Pcs = sumPcs;
            }
            else
            {
                er.Pcs = tempprod.Count();
            }

            GridCalculation(er);
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
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!"Pcs,Qty,ProductName".Contains(gridView1.FocusedColumn.FieldName)) return;
            var itm = gridView1.GetFocusedRow() as MiTransDto;
            if (itm == null) return;
            if ("Pcs,Qty".Contains(gridView1.FocusedColumn.FieldName) && this.prodDtos.Any(x => x.TransId == itm.Id))
                e.Cancel = true;
            else if (Convert.ToInt32(itm.RefId) > 0)
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
            gridView1.FocusedColumn = gridView1.Columns["ProductName"];
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            var er = gridView1.GetRow(e.RowHandle) as MiTransDto;
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
                var row = view.GetRow(view.FocusedRowHandle) as MiTransDto;
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
            var rw = gridView1.GetRow(e.RowHandle) as MiTransDto;
            rw.Id = -1 * gridView1.RowCount;
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
               // if (JobIssPara.Issue_By_Barcode) return;
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
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
                else if (gridView1.FocusedColumn.FieldName == "FinishQuality")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.NProductId == 0)
                        {
                            OpenItemLookup(dr.NProductId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup(dr.NProductId, dr);
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
                Log.Error(ex, "Job Issue GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
          //  if (JobIssPara.Issue_By_Barcode) return;
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (gridView1.FocusedColumn.FieldName == "ProductName")
                    OpenItemLookup(dr.ProductId, dr);
                else
                    OpenItemLookup(dr.NProductId, dr);
            }
        }
        private void ColorRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (JobIssPara.Issue_By_Barcode) return;
            var dr = PreOpenLookup();
            if (dr != null)
                OpenColorLookup(dr.ColorId, dr);
        }

        private void DesignRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (JobIssPara.Issue_By_Barcode) return;
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


        #endregion

        #region Event

        private void JobIssueIndex_Load(object sender, EventArgs e)
        {
            if (JobIssPara.Issue_By_Barcode)
            {
                barcodeLayoutControlItem.ContentVisible = true;
                //this.gridView1.OptionsBehavior.ReadOnly = true;
            }
            else
            {
                barcodeLayoutControlItem.ContentVisible = false;
               // this.gridView1.OptionsBehavior.ReadOnly = false;
            }
        }

        private void BarcodeSimpleButton_Click(object sender, EventArgs e)
        {
            if (JobIssPara.Job_Issue_Against_Order && string.IsNullOrEmpty(RefNobuttonEdit.Text))
            {
                MessageBox.Show("Select Reference No first!!!");
                return;
            }
            var _trans = this.grnTransDtoBindingSource1.DataSource as List<MiTransDto>;
            this.barcodeDtos = new List<JobIssueBarcodeDto>();
            foreach (var item in _trans)
            {
                var _takaList = this.prodDtos.Where(x => x.TransId == item.Id).ToList();
                foreach (var _taka in _takaList)
                {
                    var _bc = new JobIssueBarcodeDto()
                    {
                        Barcode = _taka.Extra1,
                        ChallanNo = _taka.ChallanNo,
                        ColorId = item.ColorId,
                        Color = item.ColorName,
                        Id = _taka.Id,
                        LotNo = _taka.LotNo,
                        Product = item.ProductName,
                        ProductId = item.ProductId,
                        Qty = _taka.NetWt,
                        RefId = _taka.RefId,
                        SrNo = _taka.SrNo,
                        TransId = _taka.TransId,
                        VoucherDate = _taka.VoucherDate,
                        VoucherNo = _taka.VoucherNo,
                        Weaver = _taka.Weaver,
                        ProdOutId = _taka.ProdOutId
                    };
                    barcodeDtos.Add(_bc);
                }
            }
            var frm = new JobIssueBarCode();
            frm.bindingSource1.DataSource = this.barcodeDtos;
            frm.OrderQty = this.OrderQty;
            frm.ShowDialog();
            this.barcodeDtos = frm.bindingSource1.DataSource as List<JobIssueBarcodeDto>;
            List<MiTransDto> _gridtrans = new List<MiTransDto>();

            if (prodDtos == null)
                this.prodDtos = new List<GrnProdDto>();

            var result = barcodeDtos.GroupBy(x => new { x.ProductId, x.Product, x.ColorId, x.Color })
                              .Select(g => new
                              {
                                  g.Key.ProductId,
                                  g.Key.Product,
                                  g.Key.Color,
                                  g.Key.ColorId,
                                  Qty = g.Sum(x => x.Qty),
                                  Pcs = g.Count()
                              }).ToList();
            int rowid = -1;

            foreach (var item in result)
            {
                var ct = _trans.Find(k => k.ProductId == item.ProductId && k.ColorId == item.ColorId);
                //var ct = _trans.Find(k => k.Id == item.TransId);

                if (ct == null)
                {
                    ct = new MiTransDto();
                    ct.ProductId = Convert.ToInt32(item.ProductId);
                    ct.ProductName = item.Product;
                    ct.ColorId = Convert.ToInt32(item.ColorId);
                    ct.ColorName = item.Color;
                    ct.Pcs = item.Pcs;
                    ct.Qty = item.Qty;
                    ct.ChallanId = 0;
                    ct.Id = rowid;
                    ct.UomId = 24;
                    _trans.Add(ct);
                    _gridtrans.Add(ct);
                }
                else
                {
                    ct.Pcs = item.Pcs;
                    ct.Qty = item.Qty;

                    rowid = ct.Id;

                    _gridtrans.Add(ct);
                }

                var _takalist = barcodeDtos.Where(x => x.ProductId == ct.ProductId && x.ColorId == ct.ColorId).ToList();
                foreach (var _taka in _takalist)
                {
                    var ptrans = prodDtos.Find(k => k.Id == _taka.Id);

                    if (ptrans == null)
                    {
                        ptrans = new GrnProdDto();
                        ptrans.RefId = 0;
                        ptrans.TransId = rowid;
                        ptrans.ProductId = _taka.ProductId;
                        ptrans.ColorId = _taka.ColorId;
                        ptrans.Id = _taka.Id;
                        ptrans.SrNo = _taka.SrNo;
                        ptrans.ProdOutId = (_taka.ProdOutId == null || _taka.ProdOutId <= 0) ? 0 : (int)_taka.ProdOutId;
                        ptrans.NetWt = Convert.ToDecimal(_taka.Qty);
                        ptrans.VoucherNo = _taka.VoucherNo;
                        ptrans.Weaver = _taka.Weaver;
                        ptrans.ChallanNo = _taka.ChallanNo;
                        ptrans.VoucherDate = Convert.ToInt32(_taka.VoucherDate);
                        ptrans.Extra1 = _taka.Barcode;
                        this.prodDtos.Add(ptrans);
                    }
                }
                rowid--;
            }
            foreach (var pro in frm.DelProdOut)
            {
                var pr = this.prodDtos.FirstOrDefault(k => k.Id == pro.Id);
                if (pr != null)
                {
                    this.DelProd.Add(pr);
                    this.prodDtos.Remove(pr);
                }
            }
            grnTransDtoBindingSource1.DataSource = _gridtrans;
            //grnTransDtoBindingSource1.DataSource = _trans;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }

        private void DelvLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (delvLookup.LookupDto != null)
            {
                addressLookup1.SelectedValue = this.delvLookup.LookupDto.AddressId;
                addressLookup1.buttonEdit1.Text = this.delvLookup.LookupDto.FullAddress;
            }
        }

        private void AccLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
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
        }

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
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                if (tabPageAdv2.Controls.Count > 0)
                {
                    var _list = tabPageAdv2.Controls[0] as JobIssueListView;
                    _list.ActiveControl = _list.KontoGrid;
                    this.Text = "Job Issue Challan [View]";
                    return;
                }
                var _ListView = new JobIssueListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Job Issue Challan [View]";

            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                //var _frm = new ChalParaMainView();
                //_frm.TopLevel = false;
                //_frm.Parent = tabPageAdv4;
                //_frm.ReportFilterType = "MillIssue";
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

                Log.Error(ex, "Job Issue Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void RefNobuttonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (!JobIssPara.Job_Issue_Against_Order) return;
            ShowOrderPending();
        }

        private void RefNobuttonEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (!JobIssPara.Job_Issue_Against_Order) return;
            if (string.IsNullOrEmpty(RefNobuttonEdit.Text))
            {
                if (e.KeyCode != Keys.Enter) return;
            }
            else
            {
                if (e.KeyCode != Keys.F1) return;
            }
            ShowOrderPending();
        }

        #endregion
        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                if (this.PrimaryKey == 0) return;

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\JobChallan.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["challan"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Job Issue Challan";
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
                Log.Error(ex, "Job Issue print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ChallanModel>();
            this.Text = "Job Issue Challan [Add New]";
            grnTypeLookUpEdit.EditValue = 8;
            jobTypeLookUpEdit1.EditValue = "NON SEZ";
            goodsLookUpEdit.EditValue = "Inputs";
            divLookUpEdit.EditValue = 1;
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            receiveDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            RefNobuttonEdit.Text = string.Empty;
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
           
            DelTrans = new List<MiTransDto>();
            DelProd = new List<GrnProdDto>();
            prodDtos = new List<GrnProdDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<MiTransDto>();
            //if(!this.ViewOnlyMode)
            // ShowPendingTaka();
        }
        private void ShowPendingTaka()
        {
            var frm = new PendingStockForJobView();
            if (frm.ShowDialog() != DialogResult.OK) return;
            var selpd = frm.list.Where(x => x.IsSelected).ToList();
            var result = selpd.GroupBy(x => new { x.ProductId, x.Rate, x.UomId, x.YarnName })
                              .Select(g => new
                              {
                                  g.Key.ProductId,
                                  g.Key.Rate,
                                  g.Key.UomId
                                ,
                                  g.Key.YarnName
                              }).ToList();

            int rowid = -1;
            List<MiTransDto> miTransDtos = new List<MiTransDto>();
            foreach (var item in result)
            {

                var _pid = Convert.ToInt32(item.ProductId);

                var _takalist = selpd.Where(x => x.ProductId == _pid).ToList();
                var taka = _takalist.FirstOrDefault();

                var trans = new MiTransDto();
                trans.ChallanId = 0;
                trans.Id = rowid;
                trans.ProductId = Convert.ToInt32(item.ProductId);
                trans.ProductName = item.YarnName;
                trans.UomId = Convert.ToInt32(item.UomId);
                trans.Rate = Convert.ToDecimal(item.Rate);
                trans.RefNo = taka.RefNo;
                trans.RefId = taka.TransId;
                trans.MiscId = taka.RefId;
                trans.RefVoucherId = taka.VoucherId;
                trans.Pcs = _takalist.Count;
                trans.Qty = Convert.ToDecimal(_takalist.Sum(x => x.Qty));
                foreach (var _taka in _takalist)
                {
                    var ptrans = new GrnProdDto();
                    ptrans.RefId = 0;
                    ptrans.TransId = rowid;
                    ptrans.ProductId = _taka.ProductId;
                    ptrans.ColorId = _taka.ColorId;
                    ptrans.GradeId = _taka.GradeId;
                    ptrans.Id = _taka.Id;
                    ptrans.SrNo = _taka.SrNo;
                    ptrans.ProdOutId = 0;
                    ptrans.NetWt = Convert.ToDecimal(_taka.Qty);
                    ptrans.VoucherNo = _taka.VoucherNo;
                    ptrans.Weaver = _taka.Weaver;
                    ptrans.ChallanNo = _taka.InwardNo;
                    ptrans.VoucherDate = Convert.ToInt32(_taka.VoucherDate);
                    this.prodDtos.Add(ptrans);
                }
                miTransDtos.Add(trans);
                rowid--;
            }
            this.grnTransDtoBindingSource1.DataSource = miTransDtos;
        }
        public override void ResetPage()
        {
            base.ResetPage();

            accLookup1.SetEmpty();
            challanNotextEdit.Text = string.Empty;

            voucherDateEdit.DateTime = DateTime.Now;
            receiveDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            RefNobuttonEdit.Text = string.Empty;
            delvLookup.SetEmpty();
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            DelTrans = new List<MiTransDto>();
            DelProd = new List<GrnProdDto>();
            prodDtos = new List<GrnProdDto>();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;
             
            using (var db = new KontoContext())
            {
                var model = db.Challans.Find(_key); 
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

            if (Convert.ToInt32(accLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "AccId", Operation = Op.Equals, Value = Convert.ToInt32(accLookup1.SelectedValue) });
            }

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


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
             
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MiTransDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<GrnProdDto, ProdOutModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _translist = grnTransDtoBindingSource1.DataSource as List<MiTransDto>;
            List<ChallanTransModel> Trans = new List<ChallanTransModel>();
            List<ProdOutModel> ProdList = new List<ProdOutModel>();
            ChallanModel model = new ChallanModel();
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                        {
                            model = db.Challans.Find(this.PrimaryKey);
                        }

                        model.DivId = Convert.ToInt32(divLookUpEdit.EditValue);
                        model.ChallanType = Convert.ToInt32(grnTypeLookUpEdit.EditValue);
                        model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                        model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                        model.DelvAccId = Convert.ToInt32(delvLookup.SelectedValue);
                        model.DelvAdrId = Convert.ToInt32(addressLookup1.SelectedValue);
                        model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
                        model.RcdDate = receiveDateEdit.DateTime;
                        model.VoucherNo = voucherNoTextEdit.Text.Trim();

                        model.ProcessId = Convert.ToInt32(processLookup1.SelectedValue);
                        model.AgentId = Convert.ToInt32(delvLookup.SelectedValue);
                        model.ChallanNo = "NA";

                        model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
                        model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

                        model.Remark = remarkTextEdit.Text.Trim();
                        model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
                        model.DocNo = lrNotextEdit.Text.Trim();
                        model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);
                        model.TypeId = (int)TypeEnum.JobIssue;
                        
                        model.CompId = KontoGlobals.CompanyId;
                        model.YearId = KontoGlobals.YearId;
                        model.BranchId = KontoGlobals.BranchId;
                        model.IsActive = true;

                        model.TotalQty = _translist.Sum(x => x.Qty);
                        model.TotalPcs = _translist.Sum(x => x.Pcs);
                        model.TotalAmount = _translist.Sum(x => x.Total);

                        if (this.PrimaryKey == 0)
                        {
                            model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db, 0);
                            // this._SerialValue = _srno.SerialValue;

                            if (DbUtils.CheckExistVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                            {
                                MessageBox.Show("Duplicate Voucher No Not Allowed");
                                //voucherNoTextEdit.Focus();
                                _tran.Rollback();
                                return;
                            }

                            db.Challans.Add(model);
                            db.SaveChanges();
                        }

                        foreach (var item in _translist)
                        {
                            item.ChallanId = model.Id;
                            var tranModel = new ChallanTransModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.ChallanTranses.Find(item.Id);
                            }
                            var map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                tranModel.RefId = OrderTransId;
                                tranModel.MiscId = OrderId;
                                tranModel.RefVoucherId = OrderVoucherId;
                                tranModel.RefNo = RefNobuttonEdit.Text;

                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();
                            }
                            Trans.Add(tranModel);
                            // add subdetails item details
                            var prlist = prodDtos.Where(k => k.TransId == item.Id).ToList();

                            foreach (var p in prlist)
                            {
                                ProdOutModel Out = db.ProdOuts.Find(p.ProdOutId);
                                ProdModel pm = new ProdModel();

                                pm = db.Prods.Find(p.Id);
                                pm.ProdStatus = "ISSUE";

                                if (Out == null)
                                    Out = new ProdOutModel();

                                Out.ProductId = p.ProductId;
                                Out.ColorId = p.ColorId;
                                Out.GradeId = p.GradeId;
                                Out.CompId = KontoGlobals.CompanyId;
                                Out.YearId = KontoGlobals.YearId;
                                Out.TakaStatus = "ISSUE";
                                Out.SrNo = p.SrNo;
                                Out.GrayMtr = (p.NetWt * -1);
                                Out.Qty = (p.NetWt * -1);
                                Out.ProdId = pm.Id;
                                Out.RefId = model.Id;
                                Out.VoucherId = model.VoucherId;
                                Out.VoucherNo = p.VoucherNo;
                                Out.TransId = tranModel.Id;

                                if (Out.Id <= 0)
                                {
                                    db.ProdOuts.Add(Out);
                                    db.SaveChanges();
                                }
                                ProdList.Add(Out);
                            }
                        }
                        //delete item fro trans table entry
                        foreach (var ditem in DelTrans)
                        {
                            if (ditem.Id <= 0) continue;
                            var _model = db.ChallanTranses.Find(ditem.Id);
                            _model.IsDeleted = true;

                            var delProdOut = prodDtos.Where(k => k.TransId == ditem.Id).ToList();
                            foreach (var poitem in delProdOut)
                            {
                                if (poitem.ProdOutId > 0)
                                {
                                    ProdOutModel pOut = db.ProdOuts.Find(poitem.ProdOutId);
                                    pOut.IsDeleted = true;

                                    ProdModel pitem = db.Prods.Find(poitem.Id);
                                    pitem.ProdStatus = "STOCK";
                                }
                            }
                        }

                        // delete from item details
                        foreach (var p in DelProd)
                        {
                            if (p.ProdOutId <= 0) continue;
                            var prd = db.Prods.Find(p.Id);
                            if (prd != null)
                            {
                                prd.ProdStatus = "STOCK";
                            }
                            var pout = db.ProdOuts.Find(p.ProdOutId);
                            pout.IsDeleted = true;
                        }

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == model.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        foreach (var item in Trans)
                        {
                            string TableName = "jobissue";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                            StockEffect.StockTransChlnEntry(model, item, true, TableName, KontoGlobals.UserName, db);

                        }

                       
                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Grn Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }



            if (IsSaved)
            {
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Voucher No.: " + model.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    voucherLookup1.buttonEdit1.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        #endregion
    }
}

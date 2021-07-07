using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Data.Models.Wvs;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Item;
using Konto.Shared.Trans.Common;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Windows.Forms;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;
using DevExpress.DataAccess.Sql;

namespace Konto.Trading.JobReceipt
{
    public partial class JrIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<MrvTransDto> DelTrans = new List<MrvTransDto>();
      
        private List<JobReceiptDTO> JrList = new List<JobReceiptDTO>();
        private List<JobReceiptDTO> DelJr = new List<JobReceiptDTO>();
        private List<GrnProdDto> prodDtos = new List<GrnProdDto>();
        private List<GrnProdDto> DelProd = new List<GrnProdDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private bool isImortOrSez = false;
        private string _barcodeDate;
        public JrIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;
            accLookup1.SelectedValueChanged += AccLookup1_SelectedValueChanged;
            accLookup1.ShownPopup += AccLookup1_ShownPopup;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.MouseUp += GridView1_MouseUp;
            lotNoRepositoryItemButtonEdit.ButtonClick += LotNoRepositoryItemButtonEdit_ButtonClick;
         //   challanNoRrepositoryItemButtonEdit.ButtonClick += ChallanNoRrepositoryItemButtonEdit_ButtonClick;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Jrv_Index;
            this.GridLayoutFile = KontoFileLayout.Jrv_Trans;
            this.challanNotextEdit.TextChanged += ChallanNotextEdit_TextChanged;
            this.invTypeLookUpEdit.EditValueChanged += InvTypeLookUpEdit_EditValueChanged;

            FillLookup();
            SetParameter();
            SetGridColumn();

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            tdsPerTextEdit.EditValueChanged += TdsPerTextEdit_EditValueChanged;
            tdsAmtTextEdit.EditValueChanged += TdsAmtTextEdit_EditValueChanged;
            consumeSimpleButton.Click += ConsumeSimpleButton_Click;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
            this.Load += JrIndex_Load;

            this.FirstActiveControl = voucherLookup1;
            jobcardButtonEdit.ButtonClick += JobcardButtonEdit_ButtonClick;
        }

        private void JobcardButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var frm = new JobcardPedingForJrView();
            if (frm.ShowDialog() != DialogResult.OK) return;
            var row = frm.gridView1.GetRow(frm.gridView1.FocusedRowHandle) as PendingJobDto;

            jobcardButtonEdit.Text = row.VoucherNo;
            var _db = new KontoContext();
            var jobcardlist = _db.jobCardTrans.Where(k => k.JobCardId == row.Id && k.IsDeleted == false && k.RefId > 0).ToList();
            var i = 0;
            var trans = grnTransDtoBindingSource1.DataSource as List<MrvTransDto>;

            foreach (var item in jobcardlist)
            {
                --i;
                MrvTransDto ct = new MrvTransDto();
                ct.Id = i;
                ct.ProductId = Convert.ToInt32( item.ItemId);
                var pd = _db.Products.FirstOrDefault(k => k.Id == item.ItemId);

                ct.ProductName = pd.ProductName;
                ct.DesignId = Convert.ToInt32(item.DesignId) ;
                
                var dm = _db.Products.Find(ct.DesignId);
                
                if(dm!=null)
                    ct.DesignNo = dm.ProductName;
                
                ct.ColorId = Convert.ToInt32( item.Ply);
                var cl = _db.ColorModels.Find(ct.ColorId);
                if (cl != null)
                    ct.ColorName = cl.ColorName;

                ct.Qty = (decimal)item.ConsumeQty;
                ct.UomId = 24;
                
               

                if (accLookup1.LookupDto.IsGst)
                {
                    ct.SgstPer = processLookup1.LookupDto.Sgst; 
                    ct.CgstPer = processLookup1.LookupDto.Cgst; 
                    ct.IgstPer = 0;
                    ct.Igst = 0;
                }
                else
                {
                    ct.Sgst = 0;
                    ct.SgstPer = 0;
                    ct.Cgst = 0;
                    ct.CgstPer = 0;
                    ct.IgstPer = processLookup1.LookupDto.Igst; 
                }
                trans.Add(ct);
            }

            gridControl1.RefreshDataSource();

            }

            private void LotNoRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                ShowItemDetail(dr);
        }
        private void ShowItemDetail(MrvTransDto er)
        {
            ProductModel prod = null;
            using (var db = new KontoContext())
            {
                prod = db.Products.Include("PType").SingleOrDefault(x => x.Id == er.ProductId);

            }
            if (prod == null || prod.SerialReq == "No") return;

            var frm = new ItemDetailView();
            frm.TypeEnum = (ProductTypeEnum)prod.PTypeId;

            if (prod.PType.TypeName.ToUpper() == "YARN" || prod.PType.TypeName.ToUpper() == "POY")
            {
                frm.GridLayoutFileName = KontoFileLayout.GRN_Yarn_Item_Details;
                frm.Text = "Box Details";
            }
            else if (prod.PType.TypeName.ToUpper() == "GREY")
            {
                frm.GridLayoutFileName = KontoFileLayout.GRN_Grey_Item_Details;
                frm.Text = "Taka Details";
            }
            else if (prod.PType.TypeName.ToUpper() == "BEAM")
            {
                frm.GridLayoutFileName = KontoFileLayout.GRN_Beam_Item_Details;
                frm.Text = "Beam Details";
            }
            else
            {
                frm.Text = "Product Details";
                frm.GridLayoutFileName = KontoFileLayout.GRN_Finish_Item_Details;
            }
            frm.prodDtos = new BindingList<GrnProdDto>(this.prodDtos.Where(x => x.TransId == er.Id).ToList());
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
        private void JrIndex_Load(object sender, EventArgs e)
        {
            colSgstAmt.OptionsColumn.AllowFocus = true;
            colSgstAmt.OptionsColumn.AllowEdit = true;
            colCgstAmt.OptionsColumn.AllowFocus = true;
            colCgstAmt.OptionsColumn.AllowEdit = true;
            colIgstAmt.OptionsColumn.AllowFocus = true;
            colIgstAmt.OptionsColumn.AllowEdit = true;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text =  DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
            if (voucherLookup1.GroupDto != null && voucherLookup1.GroupDto.ManualSeries)
            {
                voucherNoTextEdit.Enabled = true;
            }
            else
            {
                voucherNoTextEdit.Enabled = false;
            }
        }

        private void ConsumeSimpleButton_Click(object sender, EventArgs e)
        {
            
            ShowPendingDetails();
        }

        private void AccLookup1_ShownPopup(object sender, EventArgs e)
        {
            if (this.PrimaryKey != 0) return;
            ShowPendingDetails();
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

        private void ChallanNotextEdit_TextChanged(object sender, EventArgs e)
        {
            billNoTextEdit.Text = challanNotextEdit.Text.Trim();
        }

        #region UDF
        private void ShowPendingDetails()
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
            if (Convert.ToInt32(processLookup1.SelectedValue) == 0) return;
           
            var frm = new PendingJrViewWindow();
            frm.AddEdit = this.PrimaryKey;
            frm.EditKey = this.PrimaryKey;
            frm.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            frm.ProcessId = Convert.ToInt32(processLookup1.SelectedValue);
            frm.ShowDialog();
           
            this.JrList = frm.pendingMillReceiptSpBindingSource.DataSource as List<JobReceiptDTO>;
            this.JrList = this.JrList.Where(x => x.Qty > 0).ToList();
            if (this.PrimaryKey != 0) return;
            var id = -1;
            var db = new KontoContext();
            var JobTrans = new List<MrvTransDto>();


            foreach (var item in JrList)
            {

                
                    MrvTransDto ch = new MrvTransDto();
                    ch.Id = id;
                    ch.MiscId = item.RefId;
                    ch.RefId = item.TransId;
                    ch.RefVoucherId = item.VoucherId;
                    ch.ProductId = Convert.ToInt32(item.ProductId);
                    ch.ProductName = item.Product;
                   
                    ch.LotNo = item.Barcode;
                    ch.ColorId = Convert.ToInt32(item.ColorId);
                    ch.ColorName = item.Color;
                    ch.ColorId = Convert.ToInt32(item.ColorId);
                    ch.DesignId = Convert.ToInt32(item.DesignId);
                    ch.DesignNo = item.Design;
                    ch.IssuePcs = (int)item.Pcs;
                    ch.Pcs = (int)item.Pcs;
                    ch.Qty = (decimal)item.Qty;
                    ch.UomId = item.UnitId;
                    ch.Rate = 0;
                    ch.Cgst = 0;
                    ch.Igst = 0;
                    ch.Sgst = 0;
                    if (accLookup1.LookupDto.IsGst)
                    {
                        ch.CgstPer = processLookup1.LookupDto.Cgst;
                        ch.SgstPer = processLookup1.LookupDto.Sgst;
                        ch.IgstPer = 0;
                    }
                    else
                    {
                        ch.IgstPer = processLookup1.LookupDto.Igst;
                        ch.SgstPer = 0;
                        ch.CgstPer = 0;
                    }
                ch.SaleRate = item.SaleRate;
                ch.BulkRate = item.BulkRate;
                ch.SemiBulkRate = item.SemiBulkRate;
                    JobTrans.Add(ch);
                    id--;
                }
            
            this.grnTransDtoBindingSource1.DataSource = JobTrans;

        }
        private void SetGridColumn()
        {
            colColorName.Visible = JobRecPara.Color_Required;
            colDesignNo.Visible = JobRecPara.Design_Required;
            colFreight.Visible = JobRecPara.Freight_Required;
            colFreightRate.Visible = JobRecPara.Freight_Required;
            colOtherAdd.Visible = JobRecPara.OtherAdd_Required;
            colOtherLess.Visible = JobRecPara.OtherLess_Required;
            colGreyPcs.Visible = JobRecPara.ShortPcs_Required;
            colGreyMtrs.Visible = JobRecPara.ShortMtr_Required;
            if (JobRecPara.Challan_Required)
            {
                invTypeLayoutControlItem.ContentVisible = false;
                rcmLayoutControlItem.ContentVisible = false;
                itcLayoutControlItem.ContentVisible = false;
                jobBookLayoutControlItem.ContentVisible = false;
                tdsAcLayoutControlItem.ContentVisible = false;
                tdsAmtLayoutControlItem.ContentVisible = false;
                tdsPerLayoutControlItem.ContentVisible = false;
                paybleLayoutControlItem.ContentVisible = false;
            }

            if(KontoGlobals.PackageId!=(int)PackageType.POS)
            {
                colBarcode.Visible = false;
                colSaleRate.Visible = false;
                colSemiBulkRate.Visible = false;
                colBarcode.Visible = false;
            }

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

        public void GridCalculation(MrvTransDto er, bool isGstAmountChanged = false)
        {



            var dr = uomRepositoryItemLookUpEdit.GetDataSourceRowByKeyValue(er.UomId) as UomLookupDto;
            if (dr == null) return;
            if (dr.RateOn == "N")
            {
                er.Gross = decimal.Round(er.Pcs * er.Rate, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                er.Gross = decimal.Round(er.Qty * er.Rate, 2, MidpointRounding.AwayFromZero);
            }

            if (er.DiscPer > 0)
                er.Disc = decimal.Round(er.Gross * er.DiscPer / 100, 2, MidpointRounding.AwayFromZero);
            decimal gross = er.Gross - er.Disc;

            if (er.FreightRate > 0)
                er.Freight = decimal.Round(er.Qty * er.FreightRate / 100, 2, MidpointRounding.AwayFromZero);

            gross = gross + er.Freight + er.OtherAdd - er.OtherLess;

            if (!isGstAmountChanged)
            {
                er.Sgst = decimal.Round(gross * er.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
                er.Cgst = decimal.Round(gross * er.CgstPer / 100, 2, MidpointRounding.AwayFromZero);
                er.Igst = decimal.Round(gross * er.IgstPer / 100, 2, MidpointRounding.AwayFromZero);
            }
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

            if (tdsPerTextEdit.Value > 0)
            {
                if (JobRecPara.Tds_RoundOff)
                    tdsAmtTextEdit.Value = decimal.Round((gross * tdsPerTextEdit.Value / 100) + (decimal)0.01);
                else
                    tdsAmtTextEdit.Value = decimal.Round(gross * tdsPerTextEdit.Value / 100, 2);
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
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "JobRec" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {

                        case 96:
                            {
                                JobRecPara.Tds_RoundOff = (value == "Y") ? true : false;
                                break;
                            }
                        case 97:
                            {
                                JobRecPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 98:
                            {
                                JobRecPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 99:
                            {
                                JobRecPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 100:
                            {
                                JobRecPara.OtherAdd_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 101:
                            {
                                JobRecPara.OtherLess_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 102:
                            {
                                JobRecPara.Taka_Detail_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 103:
                            {
                                JobRecPara.ShortPcs_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 104:
                            {
                                JobRecPara.ShortMtr_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 105:
                            {
                                JobRecPara.Cut_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 106:
                            {
                                JobRecPara.JobReceipt_Against_Po = (value == "Y") ? true : false;
                                break;
                            }

                        case 173:
                            {
                                JobRecPara.Challan_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 223:
                            {
                                JobRecPara.Generate_Barcode = (value == "Y") ? true : false;
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
            frm.PTypeId = 0;
            frm.VoucherType = VoucherTypeEnum.JobReceipt;

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
                    er.SgstPer = processLookup1.LookupDto.Sgst;
                    er.CgstPer = processLookup1.LookupDto.Cgst;
                    er.IgstPer = 0;
                    er.Igst = 0;
                }
                else
                {
                    er.SgstPer = 0;
                    er.Sgst = 0;
                    er.CgstPer = 0;
                    er.Cgst = 0;
                    er.IgstPer = processLookup1.LookupDto.Igst;
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
                new ComboBoxPairs("Inputs Ineligible", "Inputs Ineligible"),
                new ComboBoxPairs("Capital Goods Ineligible", "Capital Goods Ineligible"),
                new ComboBoxPairs("Input Services Ineligible", "Input Services Ineligible"),
                new ComboBoxPairs("Ineligible","Ineligible")
            };
            itcLookUpEdit.Properties.DataSource = ibp;

            using (var db = new KontoContext())
            {


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

                // invTypeLookUpEdit.Properties.DataSource = _divLists;
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

            if (!JobRecPara.Challan_Required && string.IsNullOrEmpty(invTypeLookUpEdit.Text))
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
            else if (!JobRecPara.Challan_Required && Convert.ToInt32(bookLookup.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Job Book", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bookLookup.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(challanNotextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Challan No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                challanNotextEdit.Focus();
                return false;
            }
            else if (!JobRecPara.Challan_Required && string.IsNullOrEmpty(billNoTextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Bill No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                billNoTextEdit.Focus();
                return false;
            }
            else if (!JobRecPara.Challan_Required && string.IsNullOrEmpty(rcmLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Rcm Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rcmLookUpEdit.Focus();
                return false;
            }
            else if (!JobRecPara.Challan_Required && string.IsNullOrEmpty(itcLookUpEdit.Text))
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
            else if (!JobRecPara.Challan_Required && Convert.ToInt32(tdsAccLookup.SelectedValue) == 0 && tdsAmtTextEdit.Value != 0)
            {
                MessageBoxAdv.Show(this, "Tds Account Must be Selected", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tdsAmtTextEdit.Focus();
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

            else if (trans.Any(x => x.Qty == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Meters", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.FocusedColumn = colQty;
                gridView1.Focus();
                return false;
            }
            else if (!JobRecPara.Challan_Required && trans.Any(x => x.Rate == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.FocusedColumn = colRate;
                gridView1.Focus();
                return false;
            }

            //check for duplicate bill no
            using (var db = new KontoContext())
            {
                var accid = Convert.ToInt32(accLookup1.SelectedValue);
                var find1 = db.Challans.FirstOrDefault(
               x => x.AccId == accid && !x.IsDeleted && x.BillNo == billNoTextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId
               && x.YearId == KontoGlobals.YearId && x.Id != this.PrimaryKey
               && x.VoucherId == (int)voucherLookup1.SelectedValue);

                if (!JobRecPara.Challan_Required && find1 != null)
                {
                    MessageBox.Show("Entered Bill No Already Exists for this Party");
                    billNoTextEdit.Focus();
                    return false;
                }

              //  find1 = db.Challans.FirstOrDefault(
              //x => x.AccId == accid && !x.IsDeleted && x.ChallanNo == challanNotextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId
              //&& x.YearId == KontoGlobals.YearId && x.Id != this.PrimaryKey);

              //  if (find1 != null)
              //  {
              //      MessageBox.Show("Entered Challan No Already Exists for this Party");
              //      challanNotextEdit.Focus();
              //      return false;
              //  }

            }

            return true;
        }

        private void LoadData(ChallanModel model)
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
            challanNotextEdit.Text = model.ChallanNo;
            billNoTextEdit.Text = model.BillNo;
            billDateEdit.EditValue = model.RcdDate;

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

            if (Convert.ToInt32(model.BillId) != 0)
            {
                tdsAccLookup.SelectedValue = model.BillId;
                tdsAccLookup.SetAcc((int)model.BillId);
            }
            tdsPerTextEdit.Value = model.TdsPer;
            tdsAmtTextEdit.Value = model.TdsAmt;
            
            divLookUpEdit.EditValue = model.DivId != 0 ? model.DivId : 1;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdModel, GrnProdDto>();
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
                             join pr in _context.Prices on pd.Id equals pr.ProductId
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
                                 Barcode = pd.BarCode,SaleRate= pr.SaleRate,BulkRate = pr.Rate1,SemiBulkRate= pr.Rate2
                             }).ToList();

                //prodDtos = _context.ProdOuts.Where(x => x.RefId == model.Id && !x.IsDeleted).ToList();
                prodDtos = _context.Prods.Where(x => x.RefId == model.Id && !x.IsDeleted)
                                      .ProjectToList<GrnProdDto>(config);


                this.grnTransDtoBindingSource1.DataSource = _list;
            }


            FinalTotal();
            this.Text = "Job Receipt [View/Modify]";

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

            if (e.Column == colBarcode && e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
            {
                var pos = DbUtils.GetProductDetails(e.Value.ToString());

                if (pos == null)
                {
                    MessageBox.Show("Barcode Not Found..");

                    if (gridView1.ActiveEditor.OldEditValue != null)
                        er.Barcode = gridView1.ActiveEditor.OldEditValue.ToString();
                    else
                        er.Barcode = string.Empty;

                    BeginInvoke(new MethodInvoker(() =>
                    {
                        gridView1.FocusedColumn = colBarcode;
                    }));

                    return;
                }
                er.ProductId = pos.Id;
                er.Barcode = pos.BarCode;
                er.ColorId = pos.ColorId;
                er.ColorName = pos.ColorName;
                er.ProductName = pos.ProductName;
                er.UomId = pos.PurUomId;
                er.SaleRate = pos.SaleRate;
                er.BulkRate = pos.Rate1;
                er.SemiBulkRate = pos.Rate2;
                GridFocus();
            }

            if (e.Column == colSgstAmt || e.Column == colCgstAmt || e.Column == colIgstAmt)
                GridCalculation(er, true);
            else
                GridCalculation(er, false);
        }
        private void GridFocus() // for focus setting in gridview
        {

            gridView1.FocusedColumn = gridView1.GetVisibleColumn(colProductName.VisibleIndex);

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
                FinalTotal();
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
                Log.Error(ex, "Job Receipt GridControl KeyDown");
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
       


        #endregion

        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }

        private void AccLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (accLookup1.LookupDto == null) return;
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
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                if (tabPageAdv2.Controls.Count > 0)
                {
                    var _list = tabPageAdv2.Controls[0] as JobReceiptListView;
                    _list.ActiveControl = _list.KontoGrid;
                    this.Text = "Job Receipt [View]";
                    return;
                }
                var _ListView = new JobReceiptListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Job Receipt [View]";

            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                // var _frm = new Konto.Reporting.Para.ChlPara.ChlParaMainView();
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.ChlPara.ChlParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.ReportFilterType = "JobRec";
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

                Log.Error(ex, "Job Receipt Save");
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

                if (JobRecPara.Generate_Barcode)
                {

                    var xrep = new XtraReport();
                    xrep.LoadLayout("reg\\doc\\grn_barcode.repx");

                    var sqlDataSource = (xrep.DataSource as SqlDataSource);
                    sqlDataSource.ConnectionParameters = new CustomStringConnectionParameters(KontoGlobals.sqlConnectionString.ConnectionString);


                    xrep.Parameters["id"].Value = this.PrimaryKey;
                    var frm = new RepXViewer()
                    {
                        Text = "Barcode Printing",
                        RepSource = xrep
                    };

                    
                    frm.Show();

                    //PageReport rpt = new PageReport();

                    //rpt.Load(new FileInfo("reg\\doc\\GRNBarcode.rdlx"));

                    //rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                    //GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                    //doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                    //doc.Parameters["challan"].CurrentValue = "N";
                    //doc.Parameters["reportid"].CurrentValue = 0;
                    //var frm = new KontoRepViewer(doc);
                    //frm.Text = "Barcode";
                    //var _tab = this.Parent.Parent as TabControlAdv;
                    //if (_tab == null) return;
                    //var pg1 = new TabPageAdv();
                    //pg1.Text = "Barcode Print";
                    //_tab.TabPages.Add(pg1);
                    //_tab.SelectedTab = pg1;
                    //frm.TopLevel = false;
                    //frm.Parent = pg1;
                    //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                    //frm.Show();// = true;
                }
                PageReport rpt1 = new PageReport();

                rpt1.Load(new FileInfo("reg\\doc\\JobReceiptChallan.rdlx"));

                rpt1.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc1 = new GrapeCity.ActiveReports.Document.PageDocument(rpt1);

                doc1.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc1.Parameters["challan"].CurrentValue = "N";
                doc1.Parameters["reportid"].CurrentValue = 0;
                var frm1 = new KontoRepViewer(doc1);
                frm1.Text = "Job Rec Print";
                var _tab1 = this.Parent.Parent as TabControlAdv;
                if (_tab1 == null) return;
                var pg2 = new TabPageAdv();
                pg2.Text = "Job Rec Print";
                _tab1.TabPages.Add(pg2);
                _tab1.SelectedTab = pg2;
                frm1.TopLevel = false;
                frm1.Parent = pg2;
                frm1.Location = new Point(pg2.Location.X + pg2.Width / 2 - frm1.Width / 2, pg2.Location.Y + pg2.Height / 2 - frm1.Height / 2);
                frm1.Show();// = true;

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
            this.FilterView = new List<ChallanModel>();
            this.Text = "Job Receipt [Add New]";
            rcmLookUpEdit.EditValue = "NO";
            invTypeLookUpEdit.EditValue = "Regular";
            itcLookUpEdit.EditValue = "Inputs";
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            billDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            divLookUpEdit.EditValue = 1;
            voucherLookup1.SetDefault();
            if (Convert.ToInt32(voucherLookup1.GroupDto.AccId) > 0)
            {
                bookLookup.SelectedValue = voucherLookup1.GroupDto.AccId;
                bookLookup.SetAcc(Convert.ToInt32(voucherLookup1.GroupDto.AccId));
            }

            DelTrans = new List<MrvTransDto>();
            DelProd = new List<GrnProdDto>();
            prodDtos = new List<GrnProdDto>();
            this.JrList = new List<JobReceiptDTO>();
            this.DelJr = new List<JobReceiptDTO>();
            this.grnTransDtoBindingSource1.DataSource = new List<MrvTransDto>();
            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
            accLookup1.SetEmpty();
            bookLookup.SetEmpty();
            challanNotextEdit.Text = string.Empty;
            billNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
            billDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
           
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            processLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            tdsAccLookup.SetEmpty();
            tdsPerTextEdit.Value = 0;
            tdsAmtTextEdit.Value = 0;
            paybleTextEdit.Text = "0";

            roundoffSpinEdit.Value = 0;
            billAmtSpinEdit.Value = 0;
            
            DelTrans = new List<MrvTransDto>();
            DelProd = new List<GrnProdDto>();
            prodDtos = new List<GrnProdDto>();
            
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;


            using (var db = new KontoContext())
            {
                ChallanModel bill = null;
                if (this.IsOpenFromLedger)
                {
                    var ch = db.Bills.Find(_key);
                    if (ch != null)
                        bill = db.Challans.Find(ch.RefId);
                }
                else
                {
                    bill = db.Challans.Find(_key);
                }
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
            if (!string.IsNullOrEmpty(challanNotextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "ChallanNo", Operation = Op.Equals, Value = challanNotextEdit.Text.Trim() });
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
                cfg.CreateMap<GrnProdDto, ProdModel>().ForMember(x => x.Id, p => p.Ignore());
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


                        foreach (var item in _translist)
                        {
                            var transid = item.Id;
                            item.ChallanId = _find.Id;
                            var tranModel = new ChallanTransModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.ChallanTranses.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (tranModel.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();

                            }
                            item.Id = tranModel.Id;
                            Trans.Add(tranModel);

                            // taka/box/etc
                            var prlist = prodDtos.Where(k => (k.TransId == transid && k.VoucherId == _find.VoucherId)).ToList();

                            var _time = DateTime.Now.ToString("ddMMyyHHmmss");
                            _barcodeDate = string.Empty;
                            if (prlist.Any(x => x.Extra1 != null && x.Extra1.Length > 5))
                            {
                                _barcodeDate = prlist.FirstOrDefault(x => x.Extra1 != null && x.Extra1.Length > 5).Extra1.Substring(1, 12);
                                _time = _barcodeDate;
                            }
                            foreach (var p in prlist)
                            {
                                p.ProdStatus = "STOCK";
                                p.RefId = _find.Id;
                                p.TransId = tranModel.Id;

                                if (string.IsNullOrEmpty(p.LotNo))
                                    p.LotNo = tranModel.LotNo;

                                p.YearId = (int)KontoGlobals.YearId;
                                p.CompId = KontoGlobals.CompanyId;
                                p.BranchId = KontoGlobals.BranchId;
                                p.ProductId = tranModel.ProductId;
                                p.VoucherDate = _find.VoucherDate;
                                p.IssueRefVoucherId = tranModel.Id;

                                if (tranModel.ColorId != null && tranModel.ColorId != 0)
                                    p.ColorId = tranModel.ColorId;
                                else if (p.ColorId == 0)
                                    p.ColorId = 1;

                                if (tranModel.GradeId != null && tranModel.GradeId != 0)
                                    p.GradeId = tranModel.GradeId;
                                else if (p.GradeId == 0)
                                    p.GradeId = 1;

                                if (tranModel.DesignId != null && tranModel.DesignId != 0)
                                    p.PlyProductId = tranModel.DesignId;
                                else if (p.PlyProductId == 0)
                                    p.PlyProductId = 1;

                                ProdModel prodModel = new ProdModel();
                                if (p.Id > 0)
                                {
                                    prodModel = db.Prods.Find(p.Id);
                                }
                                map.Map(p, prodModel);

                                if (prodModel.CProductId == null || prodModel.CProductId==0)
                                    prodModel.CProductId = tranModel.ProductId;

                                if ( JobRecPara.Generate_Barcode && string.IsNullOrEmpty(prodModel.Extra1))
                                {
                                    prodModel.Extra1 = _time + p.SrNo.ToString().PadLeft(2, '0');
                                }
                                if (p.Id <= 0)
                                {
                                  
                                    
                                    db.Prods.Add(prodModel);
                                    db.SaveChanges();
                                }
                                ProdList.Add(prodModel);
                            }

                        }
                        //delete item from ord trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;
                        }


                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        foreach (var item in Trans)
                        {
                            string TableName = "Job Receipt Voucher";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                            StockEffect.StockTransChlnEntry(_find, item,false, TableName, KontoGlobals.UserName, db);
                            
                        }

                        UpdateJobReceipt(db, _find);

                        if(KontoGlobals.PackageId == (int)PackageType.POS)
                        {
                            foreach (var item in _translist)
                            {
                                var itm = db.Products.Find(item.ProductId);
                                if(itm!=null && item.ColorId!=0)
                                {
                                    itm.ColorId = item.ColorId;
                                }
                                var pm = db.Prices.SingleOrDefault(x => x.ProductId == item.ProductId);
                                if (pm != null)
                                {
                                    pm.SaleRate = item.SaleRate;
                                    pm.Rate1 = item.BulkRate;
                                    pm.Rate2 = item.SemiBulkRate;
                                }
                                var btc = db.ItemBatches.SingleOrDefault(x => x.ProductId == item.ProductId);
                                if (btc != null)
                                {
                                    btc.SaleRate = item.SaleRate;
                                    btc.BulkRate = item.BulkRate;
                                    btc.SemiBulkRate = item.SemiBulkRate;
                                }
                            }
                        }

                        if (!JobRecPara.Challan_Required)
                        {
                            if(!UpdateBill(db, _find))
                            {
                                _tran.Rollback();
                                return;
                            }
                        }
                        

                         db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Job Receipt Save");
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
                    if (GRNPara.Generate_Barcode && MessageBox.Show("Print Barcode ??..", "Barcode", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
        private void UpdateJobReceipt(KontoContext db,ChallanModel model)
        {
            foreach (var Jritem in this.JrList)
            {
                JobReceiptModel jr = new JobReceiptModel();
                if (Jritem.Id > 0 )
                     jr = db.JobReceipts.Find(Jritem.Id);

                        jr.ChallanId = model.Id;
                        jr.IssuePcs = Jritem.IssuePcs;
                        jr.IssueQty = Jritem.IssueQty;
                        jr.Pcs = Jritem.Pcs;
                        jr.Qty = Jritem.Qty;
                        jr.PendingPcs = Jritem.PendingPcs;
                        jr.PendingQty = Jritem.PendingQty;
                        jr.RefTransId = Jritem.TransId;
                        jr.RefId = Jritem.RefId;
                        jr.VoucherId = model.VoucherId;
                        jr.ProductId = Jritem.ProductId;
                        jr.ColorId = Jritem.ColorId;
                        jr.IsClear = Jritem.IsClear;
                if(jr.Id == 0)
                {
                    db.JobReceipts.Add(jr);
                }
            }
        }
        private bool UpdateChallan(KontoContext db,ChallanModel model)
        {
           
            model.DivId = 1;
            model.BillType = invTypeLookUpEdit.EditValue.ToString();
            model.Rcm = rcmLookUpEdit.EditValue.ToString();
            model.Itc = itcLookUpEdit.EditValue.ToString();

            model.ChallanType = (int)ChallanTypeEnum.INWARD_FROM_JOB;
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.BookAcId = Convert.ToInt32(bookLookup.SelectedValue);
            model.RcdDate = billDateEdit.DateTime;
            model.VoucherNo = voucherNoTextEdit.Text.Trim();
            model.ProcessId = Convert.ToInt32(processLookup1.SelectedValue);
          
            model.ChallanNo = challanNotextEdit.Text.Trim();
            model.BillNo = billNoTextEdit.Text.Trim();

            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remark = remarkTextEdit.Text.Trim();
            model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
            model.DocNo = lrNotextEdit.Text.Trim();
            if (lrDateEdit.EditValue == null)
                model.DocDate = DateTime.Now;
            else
            model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);

            model.TypeId = (int)VoucherTypeEnum.JobReceipt;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.RoundOff = roundoffSpinEdit.Value;

            model.BillId = Convert.ToInt32(tdsAccLookup.SelectedValue);
            model.TdsPer = tdsPerTextEdit.Value;
            model.TdsAmt = tdsAmtTextEdit.Value;
            var _translist = grnTransDtoBindingSource1.DataSource as List<MrvTransDto>;
            model.TotalAmount = billAmtSpinEdit.Value;
          
            model.TotalQty = _translist.Sum(x => x.Qty);
            model.TotalPcs = _translist.Sum(x => x.Pcs);
            model.IsActive = true;
            model.DivId = Convert.ToInt32(divLookUpEdit.EditValue);
           
            if (model.Id == 0)
            {
                if(!voucherLookup1.GroupDto.ManualSeries)
                    model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);
                if (DbUtils.CheckExistChllanVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
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
            billModel.TypeId = (int)VoucherTypeEnum.JobReceiptVoucher;
            billModel.BookAcId = model.BookAcId;
            billModel.AccId = model.AccId;
            billModel.VoucherDate = model.VoucherDate;
            billModel.BillNo = model.BillNo;
            billModel.RcdDate = model.RcdDate;
            billModel.GrossAmount = _translist.Sum(x => x.Gross);
            billModel.TotalAmount = billAmtSpinEdit.Value;
            billModel.TotalQty = _translist.Sum(x => x.Qty);
            billModel.TotalPcs = _translist.Sum(x => x.Pcs);
            billModel.RoundOff = roundoffSpinEdit.Value;
           
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
            billModel.DivisionId = model.DivId;
            int vtypeid = (int)VoucherTypeEnum.JobReceiptVoucher;
            var vouchr = db.Vouchers.FirstOrDefault(k => k.VTypeId == vtypeid);
            billModel.VoucherId = vouchr.Id;
            
            if (this.PrimaryKey == 0)
            {

                billModel.VoucherNo = model.VoucherNo;  //DbUtils.NextSerialNo((int)billModel.VoucherId, db);
                //if (DbUtils.CheckExistVoucherNo(billModel.VoucherId, billModel.VoucherNo, db, billModel.Id))
                //{
                //    MessageBox.Show("Duplicate Voucher No Not Allowed");
                //    return false;
                //}
                db.Bills.Add(billModel);
                db.SaveChanges();
            }
            else
            {
                var bt = db.BillTrans.Where(x => x.BillId == billModel.Id);
                db.BillTrans.RemoveRange(bt);
            }

            var _btList = new List<BillTransModel>();

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

                btModel.Total = ctrModel.Gross;
               // btModel.DiscAmt = btModel.Total * btModel.Disc / 100;
                decimal gross = btModel.Total - btModel.DiscAmt + btModel.Freight + btModel.OtherAdd - btModel.OtherLess;

                // btModel.Sgst = decimal.Round(gross * btModel.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
                // btModel.Cgst = decimal.Round(gross * btModel.CgstPer / 100, 2, MidpointRounding.AwayFromZero); //, MidpointRounding.AwayFromZero);
                // btModel.Igst = decimal.Round(gross * btModel.IgstPer / 100, 2, MidpointRounding.AwayFromZero); //, MidpointRounding.AwayFromZero);

                btModel.NetTotal = ctrModel.Total;
                btModel.BillId = billModel.Id;
                _btList.Add(btModel);
                db.BillTrans.Add(btModel);
            }
            

             LedgerEff.BillRefEntry("Credit", billModel, _translist.FirstOrDefault().ProductId ,db);

            LedgerEff.LedgerTransEntry("Credit",  billModel, db,_btList);

            return true;
        }

        #endregion

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void roundoffSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!roundoffSpinEdit.ContainsFocus) return;
            gridView1.UpdateTotalSummary();
            var ntotal = Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue);

            ntotal = ntotal + roundoffSpinEdit.Value;

            billAmtSpinEdit.Value = ntotal;
            paybleTextEdit.Text = (ntotal - tdsAmtTextEdit.Value).ToString("F");
        }
    }
}

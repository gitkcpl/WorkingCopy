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

namespace Konto.Trading.MillIssue
{
    public partial class MillIssueIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<MiTransDto> DelTrans = new List<MiTransDto>();
        private List<GrnProdDto> prodDtos = new List<GrnProdDto>();
        private List<GrnProdDto> DelProd = new List<GrnProdDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;

        public MillIssueIndex()
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
            this.MainLayoutFile = KontoFileLayout.Mi_Index;
            this.GridLayoutFile = KontoFileLayout.Mi_Trans;
            
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

            this.FirstActiveControl = voucherLookup1;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
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
           if(this.PrimaryKey==0 && Convert.ToInt32(this.accLookup1.SelectedValue) >0 )
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



        #region UDF
        private void SetGridColumn()
        {
            colColorName.Visible = GRNPara.Color_Required;
            colDesignNo.Visible = GRNPara.Design_Required;
            this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
        }
        private MiTransDto PreOpenLookup()
        {
            //if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return null;
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
                              .Where(x => x.SysPara.Category == "MillIssue" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 112://Mill Issue
                            {
                                MillIssPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 113:
                            {
                                MillIssPara.Batch_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 114:
                            {
                                MillIssPara.LotNo_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 116:
                            {
                                MillIssPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 115:
                            {
                                MillIssPara.Grade_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 117:
                            {
                                MillIssPara.Cut_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 118:
                            {
                                MillIssPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 119:
                            {
                                MillIssPara.Auto_Book_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 120:
                            {
                                MillIssPara.Auto_Voucher_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 121:
                            {
                                MillIssPara.Taka_From_Stock = (value == "Y") ? true : false;
                                break;
                            }
                        case 132:
                            {
                                MillIssPara.JobCard_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 146:
                            {
                                MillIssPara.Pending_Mill_Issue_Open_on_Load = (value == "Y") ? true : false;
                                break;
                            }
                        case 220:
                            {
                                MillIssPara.Editable_Grey_Meters = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }

        }
        private void OpenItemLookup(int _selvalue, MiTransDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;

            frm.VoucherType = VoucherTypeEnum.MillIssue;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                if (gridView1.FocusedColumn.FieldName == "FinishQuality")
                {
                    er.NProductId = frm.SelectedValue;
                    er.FinishQuality = frm.SelectedTex;
                    return;
                }
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
                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }
            GridCalculation(er);
        }
        private void OpenColorLookup(int _selvalue, MiTransDto er)
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
       
        private void OpenDesignLookup(int _selvalue, MiTransDto er)
        {
            var frm = new DesignLkpWindow();
            frm.Tag = MenuId.Design_Master;
            frm.SelectedValue = _selvalue;
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
               var  TransTypeList = (from p in  db.transTypes
                                        where p.IsActive && !p.IsDeleted && (p.Category.ToUpper() == "MILLISSUE" || p.Category == null)
                                     select new BaseLookupDto()
                                     {
                                         DisplayText = p.TypeName,Id = p.Id
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
                                    Id = p.Id,RateOn = p.RateOn
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
                MessageBoxAdv.Show(this, "Invalid Inward Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            masterLookup.SelectedValue = model.MasterId;
            masterLookup.SetValue();
            pvtMarkatextEdit.Text = model.Extra4;
            shortageTextEdit.Text = model.Extra3;
            foldingTextEdit.Text = model.Extra2;
            

            remarkTextEdit.Text = model.Remark;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

            

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
                                  Id = ct.Id,Cess=ct.Cess,CessPer=ct.CessPer,Cgst=ct.Cgst,CgstPer=ct.CgstPer,
                                  ChallanId=ct.ChallanId,ColorId=ct.ColorId.HasValue ? (int)ct.ColorId: 1,ColorName=cl.ColorName,
                                  DesignId= ct.DesignId.HasValue? (int)ct.DesignId:1,DesignNo=dm.ProductCode,Disc=ct.Disc,DiscPer=ct.DiscPer,Freight=ct.Freight,
                                  FreightRate=ct.FreightRate,
                                  Gross=ct.Gross,Igst=ct.Igst,
                                  IgstPer=ct.IgstPer,LotNo=ct.LotNo,MiscId=ct.MiscId,OtherAdd=ct.OtherAdd,OtherLess=ct.OtherLess,
                                  Pcs=ct.Pcs,ProductId=(int)ct.ProductId,ProductName=pd.ProductName,Qty=ct.Qty,Rate=ct.Rate,RefId=ct.RefId,
                                  RefVoucherId=ct.RefVoucherId,Remark=ct.Remark,Sgst=ct.Sgst,SgstPer=ct.SgstPer,Total=ct.Total,UomId=(int)ct.UomId,
                                    FinishQuality = np.ProductName,NProductId = (int)ct.NProductId,RefNo = ct.RefNo,Cops=ct.Cops                                 
                                  
                              }).ToList();

                //prodDtos = _context.ProdOuts.Where(x => x.RefId == model.Id && !x.IsDeleted)
                //                         .ProjectToList<GrnProdDto>(config);

                var spcol = _context.SpCollections.FirstOrDefault(k => k.Id ==
                                (int)SpCollectionEnum.OutwardprodList);
                if (spcol == null)
                {
                    prodDtos = _context.Database.SqlQuery<GrnProdDto>(
                                                "dbo.OutwardprodList @CompanyId={0},@VoucherId={1}," +
                                                "@RefId={2}", KontoGlobals.CompanyId, (int)VoucherTypeEnum.MillIssue, model.Id).ToList();

                }
                else
                {
                    prodDtos = _context.Database.SqlQuery<GrnProdDto>(
                     spcol.Name + " @CompanyId={0},@VoucherId={1}," +
                                                "@RefId={2}", KontoGlobals.CompanyId, (int)VoucherTypeEnum.MillIssue, model.Id).ToList();
                }

                this.grnTransDtoBindingSource1.DataSource = _list;

                //check for mill receipt entry
                if (KontoGlobals.UserName.ToUpper()!="KEYSOFT" && DbUtils.CheckExistinChallan(model.Id, model.VoucherId, _context))
                {
                    this.okSimpleButton.Visible = false;
                }
                else
                {
                    this.okSimpleButton.Visible = true; 
                }
            }


           
            this.Text = "Mill Issue [View/Modify]";

        }

        private void ShowItemDetail(MiTransDto er)
        {
            ProductModel prod = null;
            using (var db = new KontoContext())
            {
                prod = db.Products.Include("PType").SingleOrDefault(x => x.Id == er.ProductId);

            }
            if (prod == null) return;

            var frm = new ItemDetailView();
            frm.EditableNetWeight = MillIssPara.Editable_Grey_Meters;
            frm.IsReadOnly = true;
            frm.TypeEnum = (ProductTypeEnum)prod.PTypeId;

                frm.GridLayoutFileName = KontoFileLayout.Mill_Issue_Grey_Item_Details;
                frm.Text = "Taka Details";
           
            frm.prodDtos = new BindingList<GrnProdDto>(this.prodDtos.Where(x=>x.TransId == er.Id || x.RefTransId == er.Id ).ToList());
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
                pro.ProductId = er.ProductId;
                this.prodDtos.Add(pro);
            }
            foreach (var pro in frm.DelProd)
            {
                this.prodDtos.Remove(pro);
                this.DelProd.Add(pro);
            }
            er.Qty =tempprod.Sum(x => x.NetWt);
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
            if ("Pcs,Qty".Contains(gridView1.FocusedColumn.FieldName)  && this.prodDtos.Any(x => x.TransId == itm.Id))
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
          


            CancelEventArgs args = new CancelEventArgs(false);
            GridView1_ShowingEditor(sender, args);
            if (e.KeyData == Keys.Enter && args.Cancel)
            {
                gridView1.FocusedColumn = gridView1.VisibleColumns[gridView1.FocusedColumn.VisibleIndex + 1];
                return;
            }
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
                else if(gridView1.FocusedColumn.FieldName == "LotNo")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        ShowItemDetail(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Mill Issue GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if(gridView1.FocusedColumn.FieldName=="ProductName")
                    OpenItemLookup(dr.ProductId, dr);
                else
                    OpenItemLookup(dr.NProductId, dr);
            }
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
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                if(tabPageAdv2.Controls.Count > 0)
                {
                    var _list = tabPageAdv2.Controls[0] as MillIssueListView;
                    _list.ActiveControl = _list.KontoGrid;
                    this.Text = "Mill Issue [View]";
                    return;
                }
                var _ListView = new MillIssueListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Mill Issue [View]";

            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                //  var _frm = new ChlParaMainView();
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.ChlPara.ChlParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.ReportFilterType = "MillIssue";
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

                Log.Error(ex, "Mill Issue Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        

        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                if ( this.PrimaryKey == 0) return;

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\GrayIssueToMillChallan.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["challan"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Mill Issue Challan";
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
                Log.Error(ex, "Mill Issue print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ChallanModel>();
            this.Text = "Mill Issue [Add New]";
            grnTypeLookUpEdit.EditValue = 7;
            okSimpleButton.Visible = true;
            divLookUpEdit.EditValue = 1;
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            receiveDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            DelTrans = new List<MiTransDto>();
            DelProd = new List<GrnProdDto>();
            this.prodDtos = new List<GrnProdDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<MiTransDto>();
            if(!this.ViewOnlyMode && !this.OpenForLookup)
            ShowPendingTaka(0);
        }
        private void ShowPendingTaka(int productid)
        {
            var frm = new PendingGreyForMillView();
            if (Convert.ToInt32(grnTypeLookUpEdit.EditValue) == (int)ChallanTypeEnum.REFINISH_ISSUE)
                frm.IssueType = "Refinish";
            frm.ProductId = productid;
            if (frm.ShowDialog() != DialogResult.OK) return;
            var selpd = frm.list.Where(x => x.IsSelected).ToList();
            //if(frm._LotMeters > 0)
            //{
            //    DivideAverageMeters(selpd,frm._LotMeters);
            //}
            var result = selpd.GroupBy(x => new { x.ProductId, x.Rate,  x.UomId,x.YarnName })
                              .Select(g => new { g.Key.ProductId, g.Key.Rate, g.Key.UomId
                                ,g.Key.YarnName }).ToList();

            int rowid = -1;

            var miTransDtos = this.grnTransDtoBindingSource1.DataSource as List<MiTransDto>;

           

            if  (miTransDtos==null)
                miTransDtos = new List<MiTransDto>();


            foreach (var item in result)
            {
                var _pid = Convert.ToInt32(item.ProductId);

                var _takalist = selpd.Where(x => x.ProductId == _pid).ToList();
                var taka = _takalist.FirstOrDefault();

                if (miTransDtos.Count > 0)
                {
                    var mit = gridView1.GetRow(gridView1.FocusedRowHandle) as MiTransDto; //  miTransDtos.FirstOrDefault();

                   
                    // if(mit.ProductId == item.ProductId)
                    // {
                    int _pcs = 0;
                        decimal _qty = 0;
                        foreach (var _taka in _takalist)
                        {
                            if (prodDtos.FirstOrDefault(x => x.Id == _taka.Id) != null)
                                continue;

                            var ptrans = new GrnProdDto();
                            ptrans.RefId = this.PrimaryKey;
                           
                                ptrans.TransId = mit.Id;
                            

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
                            _pcs = _pcs + 1;
                            _qty = _qty + Convert.ToDecimal(_taka.Qty);
                            this.prodDtos.Add(ptrans);
                        }

                        mit.Pcs = mit.Pcs + _pcs;
                        mit.Qty = mit.Qty + _qty;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Product Not match");
                    //}
                    break;
                }

              
               
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
                trans.Qty =  Convert.ToDecimal( _takalist.Sum(x => x.Qty));
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
                    ptrans.VoucherDate =Convert.ToInt32(_taka.VoucherDate);
                    this.prodDtos.Add(ptrans);
                }
                miTransDtos.Add(trans);
                rowid--;
            }
            //this.grnTransDtoBindingSource1.DataSource = miTransDtos;
            //this.gridControl1.DataSource = this.grnTransDtoBindingSource1;
            this.gridControl1.RefreshDataSource();
            
        }

        private void DivideAverageMeters(List<DetailStockDto> selpd,decimal _mtrs)
        {
            if (_mtrs == 0) return;
            selpd = selpd.OrderByDescending(x => x.NetWt).ToList();
            var _lot = new List<DetailStockDto>();
            var _mainLot = new List<DetailStockDto>();
            int i = 1;
            foreach (var item in selpd)
            {
                if (_lot.Sum(x => x.NetWt) == _mtrs)
                {
                    _mainLot.AddRange(_lot);
                    _lot = new List<DetailStockDto>();
                    i++;
                }
                else if(!_lot.Contains(item))
                {
                    item.LotNo = i.ToString();
                    _lot.Add(item);
                }

            }

        }

        public override void ResetPage()
        {
            base.ResetPage();
            
            accLookup1.SetEmpty();
            challanNotextEdit.Text = string.Empty;
            
            voucherDateEdit.DateTime = DateTime.Now;
            receiveDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            delvLookup.SetEmpty();
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            DelTrans = new List<MiTransDto>();
            DelProd = new List<GrnProdDto>();
            this.prodDtos = new List<GrnProdDto>();
            
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


                        //model.AgentId = Convert.ToInt32(agen.SelectedValue);
                        model.ChallanNo = "NA";

                        model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
                        model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

                        model.Remark = remarkTextEdit.Text.Trim();
                        model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
                        model.DocNo = lrNotextEdit.Text.Trim();
                        model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);
                        model.TypeId = (int)TypeEnum.MillIssue;
                        model.MasterId = Convert.ToInt32(masterLookup.SelectedValue);
                        model.CompId = KontoGlobals.CompanyId;
                        model.YearId = KontoGlobals.YearId;
                        model.BranchId = KontoGlobals.BranchId;
                        model.IsActive = true;

                        model.TotalQty = _translist.Sum(x => x.Qty);
                        model.TotalPcs = _translist.Sum(x => x.Pcs);
                        model.TotalAmount = _translist.Sum(x => x.Total);
                        model.Extra4 = pvtMarkatextEdit.Text;
                        model.Extra3 = shortageTextEdit.Text;
                        model.Extra2 = foldingTextEdit.Text;
                        
                        model.VehicleNo = vehicleTextEdit.Text.Trim();
                        model.DName = driverTextEdit.Text.Trim();
                        if (model.Id==0)
                        {
                            if (!voucherLookup1.GroupDto.ManualSeries)
                                model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);

                            if (DbUtils.CheckExistChllanVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                            {
                                MessageBox.Show("Duplicate Challan/Voucher No Not Allowed");
                                voucherNoTextEdit.Focus();
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
                            if(item.Id > 0)
                            {
                                tranModel = db.ChallanTranses.Find(item.Id);
                            }
                           var map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
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
                               
                                    if(p.Id <=0){
                                        
                                        pm.ProductId = p.ProductId;
                                        
                                        if(model.ChallanType == (int) ChallanTypeEnum.REFINISH_ISSUE)
                                            pm.ProdStatus = "REFISSUE";
                                        else
                                            pm.ProdStatus = "ISSUE";    

                                        pm.RefId = model.Id;
                                        p.TransId = tranModel.Id;
                                        pm.LotNo = tranModel.RefNo;
                                        pm.NetWt = p.NetWt;
                                        pm.Tops = p.Tops;
                                        pm.YearId = KontoGlobals.YearId;
                                        pm.CompId = KontoGlobals.CompanyId;
                                        pm.BranchId = model.BranchId;
                                        pm.VoucherDate = model.VoucherDate;
                                        pm.VoucherNo = p.VoucherNo;
                                        pm.VoucherId = p.VoucherId;
                                        pm.TransId = p.TransId;
                                        pm.SrNo = p.SrNo;
                                        pm.CurrQty = p.NetWt;

                                        if (p.ColorId != null && p.ColorId != 0)
                                            pm.ColorId = p.ColorId;
                                        else if (p.ColorId == 0)
                                            p.ColorId = 1;

                                        if (p.GradeId != null && p.GradeId != 0)
                                            pm.GradeId = p.GradeId;
                                        else if (p.GradeId == 0)
                                            p.GradeId = 1;

                                        if (tranModel.DesignId != null && tranModel.DesignId != 0)
                                            pm.PlyProductId = tranModel.DesignId;
                                        else if (pm.PlyProductId == 0)
                                            pm.PlyProductId = 1;
                                        pm.IsOk = true;
                                        db.Prods.Add(pm);
                                        db.SaveChanges();
                                    }
                                
                                else
                                {
                                   pm = db.Prods.Find(p.Id);
                                   pm.ProdStatus = "ISSUE";
                                }

                              

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
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;

                            var delProdOut = prodDtos.Where(k => k.TransId == item.Id).ToList();
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
                            if (p.Id == 0) continue;
                             var prd = db.Prods.Find(p.Id);
                            if (prd != null && MillIssPara.Taka_From_Stock)
                            {
                                prd.ProdStatus = "STOCK";
                            }
                            else
                            {
                                prd.IsDeleted = true;
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
                            string TableName = "mi";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                            
                            //var prList = ProdList.Where(x => x.TransId == item.Id).ToList();
                            //if (prList.Count > 0)
                            //{
                            //    foreach (var prod in prList)
                            //    {
                            //       StockEffect.StockTransChlnProdOutEntry(model, item,true, TableName, db, prod,false);
                            //    }
                            //}
                            //else
                            //{
                                StockEffect.StockTransChlnEntry(model, item,true, TableName, KontoGlobals.UserName, db);
                            //}
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
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage +" Voucher No.: " + model.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if(this.voucherLookup1.GroupDto.PrintAfterSave && MessageBox.Show("Print Challan ?","Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void consumeSimpleButton_Click(object sender, EventArgs e)
        {
            if (!this.ViewOnlyMode)
            {
                int pid = 0;
                if(gridView1.FocusedRowHandle >= 0)
                {
                    var rw = gridView1.GetRow(gridView1.FocusedRowHandle) as MiTransDto;
                    if (rw != null)
                        pid = rw.ProductId;
                }
                ShowPendingTaka(pid);
            }
            
        }
    }
}

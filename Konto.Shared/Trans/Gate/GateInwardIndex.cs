using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Shared.Trans.Gate
{
    public partial class GateInwardIndex : KontoMetroForm
    {
        private List<OrdDto> FilterView = new List<OrdDto>();
     
        //private ProductDto _selectedProdudt;
        public GateInwardIndex()
        {
            InitializeComponent();
            this.Load += PoIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            
           
            this.MainLayoutFile = KontoFileLayout.Gate_Inward_Index;
            this.Shown += PoIndex_Shown;
         
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

      

        private void PoIndex_Shown(object sender, EventArgs e)
        {
           

            if (this.EditKey > 0)
                this.EditPage(this.EditKey);
        }




        #region UDF
        
       
     

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
           
            var trans = ordTransDtoBindingSource1.DataSource as List<OrdTransDto>;

            if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Party", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                accLookup1.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Order date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
           

            if (this.PrimaryKey != 0)
            {
                using(var db = new KontoContext())
                {
                    var vid = Convert.ToInt32(this.voucherLookup1.SelectedValue);
                    var exist = db.ChallanTranses.Any(x => x.MiscId == this.PrimaryKey && x.RefVoucherId == vid  && x.IsDeleted == false && x.IsActive == true);
                    if (exist)
                    {
                        MessageBoxAdv.Show("Gate Entry Exist In Challan.. Can not Edit Order", "Access Denied !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
         
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc((int) model.AccId);
            refNotextEdit.Text = model.RefNo;
            vehicleNotextEdit.Text = model.Extra1;
            
            specialtextEdit.Text = model.SpecialNotes;
            remarkTextEdit.Text = model.Remarks;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

          
            this.Text = "Gate Inward [View/Modify]";

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
                var _list = tabPageAdv2.Controls[0] as GateInwardListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new GateInwardListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Gate Inward [View]";

            }
            if (tabControlAdv1.SelectedIndex == 3)
            {
                //if (tabPageAdv4.Controls.Count > 0) return;
                //var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.OrdPara.OrdParaMainView").Unwrap() as KontoForm;
                //_frm.ReportFilterType = "PurchaseOrder";
                //_frm.TopLevel = false;
                //_frm.Parent = tabPageAdv4;
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

                Log.Error(ex, "Gate Inward Save");
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

                Log.Error(ex, "Gate Inward Load");
                MessageBox.Show(ex.ToString());
            }
        }


        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                //if (this.PrimaryKey == 0) return;

                //PageReport rpt = new PageReport();

                //rpt.Load(new FileInfo("reg\\doc\\PoRep.rdlx"));

                //rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                //GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                //doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                //doc.Parameters["Ord"].CurrentValue = "N";
                //doc.Parameters["reportid"].CurrentValue = 0;
                //var frm = new KontoRepViewer(doc);
                //frm.Text = "Purchase Order";
                //var _tab = this.Parent.Parent as TabControlAdv;
                //if (_tab == null) return;
                //var pg1 = new TabPageAdv();
                //pg1.Text = "Order Print";
                //_tab.TabPages.Add(pg1);
                //_tab.SelectedTab = pg1;
                //frm.TopLevel = false;
                //frm.Parent = pg1;
                //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                //frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Purchase Order print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<OrdDto>();
            this.Text = "Gate Inward [Add New]";
          
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
          
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
          
          

            if (this.Create_Permission)
                okSimpleButton.Enabled = true;

        }
        public override void ResetPage()
        {
            base.ResetPage();
            if (this.Modify_Permission)
                okSimpleButton.Enabled = true;
            accLookup1.SetEmpty();
            refNotextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
           
            voucherNoTextEdit.Text = string.Empty;
            refNotextEdit.Text = string.Empty;
            vehicleNotextEdit.Text = string.Empty;
           
           
            remarkTextEdit.Text = string.Empty;
            specialtextEdit.Text = string.Empty;
            
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
          
            if (Convert.ToInt32(accLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "AccId", Operation = Op.Equals, Value = Convert.ToInt32(accLookup1.SelectedValue) });
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
         
            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.RefNo = refNotextEdit.Text.Trim();
          
            model.SpecialNotes = specialtextEdit.Text.Trim();
            model.Remarks = remarkTextEdit.Text.Trim();

            model.Extra1 = vehicleNotextEdit.Text.Trim();
            model.EmpId = 1;
          
            model.TypeId = (int)VoucherTypeEnum.GateInward;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
        

            var _find = new OrdModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrdDto, OrdModel>().ForMember(x => x.Id, p => p.Ignore()
                );
                cfg.CreateMap<OrdTransDto, OrdTransModel>().ForMember(x => x.Id, p => p.Ignore());
            });
            
           

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

                            _find.TypeId = (int)VoucherTypeEnum.PurchaseOrder;
                            db.Ords.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
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
                        Log.Error(ex, "Gate Inward Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }
               

            
            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage +" Sr No.: " + model.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if (this.voucherLookup1.GroupDto.PrintAfterSave && MessageBox.Show("Print Order ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    this.PrimaryKey = model.Id;
                //    Print();
                //    this.PrimaryKey = 0;
                //}
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

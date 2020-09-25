using AutoMapper;
using GrapeCity.Viewer.Common.Model;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Comp
{
    public partial class CompIndex : KontoMetroForm
    {
        private List<CompModel> FilterView = new List<CompModel>();
        public CompIndex()
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            stateIdComboBox.DisplayMember = "StateName";
            stateIdComboBox.ValueMember = "Id";
            facStateIdComboBoxEx.DisplayMember = "StateName";
            facStateIdComboBoxEx.ValueMember = "Id";
            this.MainLayoutFile = KontoFileLayout.CompanyMaster_List_Layout;
            FillState();
        }
        private void FillState()
        {
            using (var db = new KontoContext())
            {
                var cnt = db.States.Where(x => !x.IsDeleted && x.IsActive).OrderBy(x => x.StateName).ToList();
                stateIdComboBox.DataSource = cnt;
                facStateIdComboBoxEx.DataSource = cnt;

                var TransTypeList = (from p in db.Nobs
                                     select new BaseLookupDto()
                                     {
                                         DisplayText = p.BusinessType,
                                         Id = p.Id
                                     }).ToList();
                nobLookUpEdit.Properties.DataSource = TransTypeList;
            }
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                companyTextBoxExt.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _modellist = tabPageAdv2.Controls[0] as CompListView;
                _modellist.ActiveControl = _modellist.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new CompListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);

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

                Log.Error(ex, "company Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            companyTextBoxExt.Text = string.Empty;
            printTextBoxExt.Text= string.Empty;
            address1TextBoxExt.Text= string.Empty;
            address2TextBoxExt.Text= string.Empty;
            cityLookup1.SetEmpty();
            pinCodeTextBoxExt.Text = string.Empty;

            facAddress1TextBoxExt.Text = string.Empty;
            facAddress2TextBoxExt.Text = string.Empty;
            facCityLookup.SetEmpty();
            facPinCodeTextBoxExt.Text = string.Empty;

            mobileTextBoxExt.Text= string.Empty;
            phoneTextBoxExt.Text= string.Empty;
            emailTextBoxExt.Text= string.Empty;
            websiteTextBoxExt.Text= string.Empty;

            gstinTextBoxExt.Text= string.Empty;
            panTextBoxExt.Text= string.Empty; ;
            aadharTextBoxExt1.Text= string.Empty; 
            tanTextBoxExt.Text= string.Empty;

            bankNameTextBoxExt.Text= string.Empty;
            acNoTextBoxExt.Text= string.Empty;
            ifscTextBoxExt.Text= string.Empty;
            insuranceTextBoxExt.Text= string.Empty;

            sendFromTextBoxExt.Text= string.Empty;
            sendPassTextBoxExt.Text= string.Empty;
            paraTextBoxExt.Text= string.Empty;
            holyWordTextBoxExt.Text= string.Empty;
            othersTextBoxExt.Text= string.Empty;
            smtpTextBoxExt.Clear();
            portTextBoxExt.Clear();
            pictureEdit1.EditValue = DBNull.Value;
            buttonEdit1.Text = string.Empty;
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(companyTextBoxExt.Text) || companyTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Company Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                companyTextBoxExt.Clear();
                companyTextBoxExt.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(printTextBoxExt.Text) || printTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Company Print/Alias Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                printTextBoxExt.Clear();
                printTextBoxExt.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(stateIdComboBox.Text))
            {
                MessageBoxAdv.Show(this, "Invalid State", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                stateIdComboBox.Focus();
                return false;
            }
            tabbedControlGroup1.SelectedTabPageIndex = 1;
            if (string.IsNullOrEmpty(nobLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Bussiness Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nobLookUpEdit.Focus();
                return false;
            }
            tabbedControlGroup1.SelectedTabPageIndex = 0;
            using (var db = new KontoContext())
            {
                var find = db.Companies.FirstOrDefault(
                   x => x.CompName == companyTextBoxExt.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Company Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    companyTextBoxExt.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<CompModel>();
            this.Text = "Company Master [Add New]";
            nobLookUpEdit.EditValue = 1;
            this.ResetPage();
        }

        private void CompIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();
                tabbedControlGroup1.SelectedTabPage = layoutControlGroup6;
                this.ActiveControl = companyTextBoxExt;
            }
            catch (Exception ex)
            {

                Log.Error(ex, "company Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Companies.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(CompModel model)
        {

            this.ResetPage();
            this.PrimaryKey = model.Id;
           
            this.Text = "Company Master [View/Modify]";

            companyTextBoxExt.Text = model.CompName;
            printTextBoxExt.Text = model.PrintName;
            address1TextBoxExt.Text = model.Address1;
            address2TextBoxExt.Text = model.Address2;
            cityLookup1.SelectedValue = model.CityId;
            cityLookup1.SetCity();
            stateIdComboBox.SelectedValue = model.StateId;
            pinCodeTextBoxExt.Text = model.Pincode;

            facAddress1TextBoxExt.Text = model.FAddress1;
            facAddress2TextBoxExt.Text = model.FAddress2;
            if(model.FStateId!=null)
                facStateIdComboBoxEx.SelectedValue = model.FStateId;

            facCityLookup.SelectedValue = model.FCityId;
            facCityLookup.SetCity();
            facPinCodeTextBoxExt.Text = model.FPincode;

            mobileTextBoxExt.Text = model.Mobile;
            phoneTextBoxExt.Text = model.Phone;
            emailTextBoxExt.Text = model.Email; 
            websiteTextBoxExt.Text = model.Website;

            gstinTextBoxExt.Text = model.GstIn;
            panTextBoxExt.Text = model.PanNo; ;
            aadharTextBoxExt1.Text = model.AadharNo;
            tanTextBoxExt.Text = model.TdsAcNo;

            bankNameTextBoxExt.Text = model.BankName;
            acNoTextBoxExt.Text = model.AcNo;
            ifscTextBoxExt.Text = model.IfsCode; 
            insuranceTextBoxExt.Text = model.Insurance;

            sendFromTextBoxExt.Text = model.SendFrom;
            sendPassTextBoxExt.Text = model.SendPass;
            smtpTextBoxExt.Text = model.EmailPass;
            portTextBoxExt.Text = model.SortName;
            paraTextBoxExt.Text = model.Para;
            holyWordTextBoxExt.Text = model.HolyWorld;
            othersTextBoxExt.Text = model.Remark;
            nobLookUpEdit.EditValue = model.NobId;
            if (!string.IsNullOrEmpty(model.LogoPath))
            {
                pictureEdit1.Image = Image.FromFile(model.LogoPath);
                buttonEdit1.Text = model.LogoPath;
            }
            else
            {
                buttonEdit1.Text = string.Empty;
                pictureEdit1.Image = null;
            }
        }

        public override void FirstRec()
        {
            base.FirstRec();
            LoadData(FilterView[RecordNo]);
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
            CompModel _find = new CompModel();
            if (!string.IsNullOrWhiteSpace(companyTextBoxExt.Text.Trim()))
                filter.Add(new Filter { PropertyName = "CompName", Operation = Op.Contains, Value = companyTextBoxExt.Text.Trim() });


            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.Companies.Where(ExpressionBuilder.GetExpression<CompModel>(filter)).ToList();
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
            CompModel model = new CompModel();
            
            model.CompName = companyTextBoxExt.Text.Trim();
            model.PrintName = printTextBoxExt.Text.Trim();
            model.Address1 = address1TextBoxExt.Text.Trim();
            model.Address2 = address2TextBoxExt.Text.Trim();
            model.StateId = Convert.ToInt32(stateIdComboBox.SelectedValue);
            if (cityLookup1.SelectedValue != null)
                model.CityId = Convert.ToInt32(cityLookup1.SelectedValue);
            else
                model.CityId = null;
            model.Pincode = pinCodeTextBoxExt.Text.Trim();

            model.FAddress1 = facAddress1TextBoxExt.Text.Trim();
            model.FAddress2 = facAddress2TextBoxExt.Text.Trim();
            if (facCityLookup.SelectedValue != null)
                model.FCityId = Convert.ToInt32(facCityLookup.SelectedValue);
            else
                model.FCityId = null;
            model.Pincode = pinCodeTextBoxExt.Text.Trim();
            model.FStateId = Convert.ToInt32(facStateIdComboBoxEx.SelectedValue);

            model.Mobile = mobileTextBoxExt.Text.Trim();
            model.Phone = phoneTextBoxExt.Text.Trim();
            model.Email = emailTextBoxExt.Text.Trim();
            model.Website = websiteTextBoxExt.Text.Trim();

            model.GstIn = gstinTextBoxExt.Text.Trim();
            model.PanNo = panTextBoxExt.Text.Trim();
            model.AadharNo = aadharTextBoxExt1.Text.Trim();
            model.TdsAcNo = tanTextBoxExt.Text.Trim();

            model.BankName = bankNameTextBoxExt.Text.Trim();
            model.AcNo=acNoTextBoxExt.Text.Trim();
            model.IfsCode=ifscTextBoxExt.Text.Trim();
            model.Insurance=insuranceTextBoxExt.Text.Trim();

            model.HolyWorld = holyWordTextBoxExt.Text.Trim();
            model.SendFrom=sendFromTextBoxExt.Text.Trim();
            model.SendPass=sendPassTextBoxExt.Text.Trim();
            model.EmailPass = smtpTextBoxExt.Text.Trim(); //smtp
            model.SortName = portTextBoxExt.Text.Trim();//port
            model.Para = paraTextBoxExt.Text.Trim();
            model.Remark = othersTextBoxExt.Text.Trim();
            model.NobId = Convert.ToInt32(nobLookUpEdit.EditValue);
            

            if (pictureEdit1.EditValue != null)
                model.LogoPath = buttonEdit1.Text;
            else
                model.LogoPath = null;

            using (var db = new KontoContext())
            {
                using (var _trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey == 0)
                        {
                            db.Companies.Add(model);
                            db.SaveChanges(); // generate id for company


                            // add voucher setup for created company
                            var vouchr = db.Vouchers.Where(k => k.IsActive && !k.IsDeleted).ToList();
                            VchSetupModel VchModel;
                            var _vList = new List<VchSetupModel>();
                            foreach (var Vou in vouchr)
                            {
                                VchModel = new VchSetupModel();

                                VchModel.RowId = Guid.NewGuid();
                                VchModel.CompId = model.Id;
                                VchModel.VoucherId = Vou.Id;
                                VchModel.InvoiceHeading = Vou.SortName;
                                VchModel.VchWidth = 0;
                                VchModel.PreFillZero = false;
                                VchModel.StartFrom = 1;
                                VchModel.Increment = 1;
                                VchModel.Serial_Mask = "{#}/{YY}";
                                VchModel.Last_Serial = 0;
                                VchModel.FyReset = false;
                                VchModel.PrintAfterSave = false;
                                VchModel.EmailAfterSave = false;
                                VchModel.BookFix = false;
                                VchModel.AccId = 0;
                                VchModel.IsActive = true;
                                VchModel.IsDeleted = false;
                                VchModel.CreateDate = DateTime.Now;
                                VchModel.CreateUser = KontoGlobals.UserName;
                                VchModel.IpAddress = "NA";
                                _vList.Add(VchModel);
                                //db.VchSetups.Add(VchModel);
                            }
                            db.VchSetups.AddRange(_vList);

                            // add account master for created company
                            var Acc = db.Accs.Where(k => k.IsActive && !k.IsDeleted).ToList();
                           // var cmp = db.Companies.FirstOrDefault(x => x.Id == KontoGlobals.CompanyId);

                            AccBalModel acbalModel;
                            var _aList = new List<AccBalModel>();
                            foreach (var ac in Acc)
                            {
                                acbalModel = new AccBalModel();
                                var acaddress = db.AccAddresses.FirstOrDefault(k => k.AccId == ac.Id
                                        && k.IsDeleted == false);

                                if (acbalModel.Id == 0)
                                {
                                    acbalModel.RowId = Guid.NewGuid();
                                    acbalModel.CompId = model.Id;
                                    acbalModel.AccId = ac.Id;
                                    acbalModel.AccRowId = ac.RowId;

                                    var _bal = db.AccBals.FirstOrDefault(x => x.CompId == KontoGlobals.CompanyId && x.YearId == KontoGlobals.YearId && x.AccId == ac.Id);
                                    
                                    if(_bal!=null)
                                        acbalModel.GroupId = Convert.ToInt32(_bal.GroupId);
                                    else
                                        acbalModel.GroupId = Convert.ToInt32(ac.GroupId);

                                    if (acbalModel.GroupId == 0)
                                        acbalModel.GroupId = 1;

                                    if (acaddress != null)
                                    {
                                        acbalModel.Address1 = acaddress.Address1;
                                        acbalModel.Address2 = acaddress.Address2;
                                    }
                                    acbalModel.AddressId = ac.AddressId;
                                    acbalModel.Bal = 0;
                                    acbalModel.CityId = model.CityId;
                                    acbalModel.YearId = KontoGlobals.YearId;
                                    _aList.Add(acbalModel);
                                    //db.AccBals.Add(acbalModel);
                                }
                            }

                            db.AccBals.AddRange(_aList);
                            // add product for created company
                            var Prod = db.Products.Where(k => k.IsActive && !k.IsDeleted).ToList();

                            StockBalModel SBModel;
                            var _sList = new List<StockBalModel>();
                            foreach (var prod in Prod)
                            {
                                SBModel = new StockBalModel();
                                if (SBModel.Id == 0)
                                {
                                    SBModel.RowId = Guid.NewGuid();
                                    SBModel.CompanyId = model.Id;
                                    SBModel.ProductId = prod.Id;
                                    SBModel.BranchId = 1;
                                    SBModel.GodownId = 1;
                                    SBModel.OpNos = 0;
                                    SBModel.OpQty = 0;
                                    SBModel.Rate = 0;
                                    SBModel.StockValue = 0;
                                    SBModel.YearId = KontoGlobals.YearId;
                                    SBModel.RcptQty = 0;
                                    SBModel.RcptNos = 0;
                                    SBModel.IssueQty = 0;
                                    SBModel.IssueNo = 0;
                                    _sList.Add(SBModel);
                                    //db.StockBals.Add(SBModel);
                                }
                            }
                            db.StockBals.AddRange(_sList);
                        }
                        else
                        {
                            CompModel modelfind = db.Companies.Find(this.PrimaryKey);
                            var config = new MapperConfiguration(cfg =>
                            {
                                cfg.CreateMap<CompModel, CompModel>().ForMember(x => x.Id, p => p.Ignore()
                                ).ForMember(x => x.Branches, p => p.Ignore())
                                .ForMember(x=>x.RowId,p=>p.Ignore());
                            });
                            var mapper = config.CreateMapper();
                            mapper.Map<CompModel, CompModel>(model, modelfind);
                            
                        }
                        db.SaveChanges();
                        _trans.Commit();
                        IsSaved = true;
                    }
                    catch(Exception ex)
                    {
                        _trans.Rollback();
                        Log.Error(ex, "company Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }
                }
            }
            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    this.ResetPage();
                    companyTextBoxExt.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            //Open the Pop-Up Window to select the file 
            if (dlg.ShowDialog()== DialogResult.OK)
            {
                buttonEdit1.Text = dlg.FileName;
                pictureEdit1.Image = Image.FromFile(dlg.FileName);
            }
        }
    }
}

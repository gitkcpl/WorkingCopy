using AutoMapper;
using DevExpress.XtraLayout.Utils;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TaxProGST.API;
using TaxProGST.JsonModels;

namespace Konto.Shared.Masters.Acc
{
    public partial class AccIndex : KontoMetroForm
    {
        private List<AccModel> FilterView = new List<AccModel>();
        public int GroupId { get; set; }
        public AccIndex()
        {
            InitializeComponent();

            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += AccIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            tdsComboBoxEx.SelectedIndexChanged += TdsComboBoxEx_SelectedIndexChanged;
            tcsComboBoxEx.SelectedIndexChanged += TcsComboBoxEx_SelectedIndexChanged;

            this.MainLayoutFile = KontoFileLayout.AccountMaster_Layout;
            
            FillAllList();
        }
        private void FillAllList()
        {
            dedComboBoxEx.DisplayMember = "Descr";
            dedComboBoxEx.ValueMember = "Id";
            nopComboBoxEx.DisplayMember = "Descr";
            nopComboBoxEx.ValueMember = "Id";
            iokontoComboBoxEx.DisplayMember = "_Value";
            iokontoComboBoxEx.ValueMember = "_Key";
            using(var db = new KontoContext())
            {
                var _deductee = db.Deductees.OrderBy(x => x.Descr).ToList();
                dedComboBoxEx.DataSource = _deductee;
                var _nop = db.Nops.OrderBy(x => x.Descr).ToList();
                nopComboBoxEx.DataSource = _nop;
            }
            //grade
            List<string> grade = new List<string>();
            grade.Add("A");
            grade.Add("B");
            grade.Add("C");
            grade.Add("D");
            grade.Add("E");
            gradeComboBoxEx.DataSource = grade;

            List<string> days = new List<string>();
            days.Add("Monday");
            days.Add("Tuesday");
            days.Add("Wednesday");
            days.Add("Thursday");
            days.Add("Friday");
            days.Add("Saturday");
            days.Add("Sunday");
            daysComboBoxEx.DataSource = days;

            List<string> yesno = new List<string>();
            yesno.Add("No");
            yesno.Add("Yes");
            tdsComboBoxEx.DataSource = yesno;
            tcsComboBoxEx.DataSource = yesno;
            btobComboBoxEx.DataSource = yesno;

           var  cbp1 = new List<ComboBoxPairs>
                {
                    new ComboBoxPairs("NA", "NA")
                    //new ComboBoxPairs("Consumer", "CON"),

                };

            taxTypeComboBox.DisplayMember = "_Key";
            taxTypeComboBox.ValueMember = "_Value";
            taxTypeComboBox.DataSource = cbp1;

            List<ComboBoxPairs> cbpio = new List<ComboBoxPairs>
            {
                 new ComboBoxPairs("NA", "NA"),
                new ComboBoxPairs("Input", "Input"),
                new ComboBoxPairs("Output", "Output"),
               
            };
            iokontoComboBoxEx.DataSource = cbpio;
        }
        private void TcsComboBoxEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcsComboBoxEx.SelectedIndex == 1 || tdsComboBoxEx.SelectedIndex == 1)
            {
                deducteelayoutControlItem.Visibility = LayoutVisibility.Always;
                noplayoutControlItem.Visibility = LayoutVisibility.Always;
            }
            else
            {
                deducteelayoutControlItem.Visibility = LayoutVisibility.Never;
                noplayoutControlItem.Visibility = LayoutVisibility.Never;
            }
        }

        private void TdsComboBoxEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcsComboBoxEx.SelectedIndex == 1 || tdsComboBoxEx.SelectedIndex == 1)
            {
                deducteelayoutControlItem.ContentVisible = true;
                noplayoutControlItem.ContentVisible = true;
            }
            else
            {
                deducteelayoutControlItem.ContentVisible = false;
                noplayoutControlItem.ContentVisible = false;
            }
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                ledgerGroupLookup1.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as AccListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new AccListView();
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

                Log.Error(ex, "Acc Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void AccIndex_Load(object sender, EventArgs e)
        {
            try
            {
               // NewRec();

                this.ActiveControl = ledgerGroupLookup1.buttonEdit1;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Emp Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || nameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Account Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(printNameTextBoxExt.Text) || printNameTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Account Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                printNameTextBoxExt.Focus();
                return false;
            }
            else if (Convert.ToInt32(ledgerGroupLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Ledger Group", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ledgerGroupLookup1.buttonEdit1.Focus();
                return false;
            }
            if(taxTypeComboBox.SelectedIndex < 0)
            {
                MessageBoxAdv.Show(this, "Invalid Tax Type Selection", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                taxTypeComboBox.Focus();
                return false;
            }
            else if (ledgerGroupLookup1.GroupDto.AddressReq && Convert.ToInt32(cityLookup1.SelectedValue)==0)
            {
                MessageBox.Show("Please Select City");
                cityLookup1.Focus();
                return false;
            }

            //if (!string.IsNullOrEmpty(gstInTextBoxExt.Text.Trim()))
            //{
            //    try
            //    {
            //        if (!GSTINValidator.IsValid(gstInTextBoxExt.Text.Trim()))
            //        {
            //            MessageBox.Show("Invalid GSTIN");
            //            gstInTextBoxExt.Focus();
            //            return false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        gstInTextBoxExt.Focus();
            //        return false;
            //    }
            //}

            using (var db = new KontoContext())
            {
                var find = db.Emps.FirstOrDefault(
                   x => x.EmpName == nameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Account Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<AccModel>();
            this.Text = "Account Master [Add New]";
            this.ActiveControl = ledgerGroupLookup1.buttonEdit1;
            gstdateEdit.EditValue = DateTime.Now;
            tdsComboBoxEx.SelectedIndex = 0;
            tcsComboBoxEx.SelectedIndex = 0;
            btobComboBoxEx.SelectedIndex = 1;
            taxTypeComboBox.SelectedIndex = 0;
            iokontoComboBoxEx.SelectedIndex = 0;
            gradeComboBoxEx.SelectedIndex = 0;
            daysComboBoxEx.SelectedIndex = 0;
            dedComboBoxEx.SelectedValue = 1;
            nopComboBoxEx.SelectedValue = 1;
            areaLookup1.SelectedValue = 1;
            areaLookup1.SetArea();
            cityLookup1.SelectedValue = 1;
            cityLookup1.SetCity();
            pgLookup1.SelectedValue = 1;
            pgLookup1.SetGroup();
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            agentLookup1.SelectedValue = 1;
            agentLookup1.SetAcc(1);
            transportLookup2.SelectedValue = 1;
            transportLookup2.SetAcc(1);

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            if(this.GroupId > 0)
            {
                this.ledgerGroupLookup1.SetGroup(this.GroupId);
                this.ledgerGroupLookup1.SelectedValue = this.GroupId;
            }

        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextBox.Clear();
            gstInTextBoxExt.Clear();
            ledgerGroupLookup1.SetEmpty();
            printNameTextBoxExt.Clear();
            address1TextBoxExt.Clear();
            address2TextBoxExt.Clear();
            aadharTextBoxExt.Clear();
            panTextBoxExt.Clear();
            address1TextBoxExt.Clear();
            address2TextBoxExt.Clear();
            cityLookup1.SetEmpty();
            areaLookup1.SetEmpty();
            pinCodeTextBoxExt.Clear();
            mobileTextBoxExt.Clear();
            emailTextBoxExt.Clear();
            pgLookup1.SetEmpty();
            empLookup1.SetEmpty();
            daysKontoTextBox.Clear();
            crLimitKontoTextBox.Clear();
            discPerKontoTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Accs.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(AccModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;

            AccBalModel balmodel = null;
            using (var db = new KontoContext())
            {
                balmodel = db.AccBals.FirstOrDefault(x => x.AccId == model.Id &&
                    x.CompId == KontoGlobals.CompanyId && x.YearId == KontoGlobals.YearId);

            }
            if (balmodel != null)
            {
                address1TextBoxExt.Text = balmodel.Address1;
                address2TextBoxExt.Text = balmodel.Address2;
                cityLookup1.SelectedValue = balmodel.CityId;
                cityLookup1.SetCity();
                areaLookup1.SelectedValue = balmodel.AreaId;
                areaLookup1.SetArea();
                pinCodeTextBoxExt.Text = balmodel.PinCode;
                mobileTextBoxExt.Text = balmodel.MobileNo;
                emailTextBoxExt.Text = balmodel.Email;
                ledgerGroupLookup1.SetGroup(balmodel.GroupId);
                ledgerGroupLookup1.SelectedValue = balmodel.GroupId;
                
            }

            nameTextBox.Text = model.AccName;
            printNameTextBoxExt.Text = model.PrintName;
            gstInTextBoxExt.Text = model.GstIn;
            gstdateEdit.EditValue = model.GSTDate;
            taxTypeComboBox.SelectedValue = model.VatTds;
            iokontoComboBoxEx.SelectedValue = model.IoTax;
            tdsComboBoxEx.SelectedValue = model.TdsReq;
            tcsComboBoxEx.SelectedValue = model.TcsReq;
            gradeComboBoxEx.SelectedValue = model.Grade;
            daysComboBoxEx.SelectedValue = model.CollDay;
            discPerKontoTextBox.DoubleValue = (double)model.DiscPer;
            if (model.DeducteeId == 0)
                dedComboBoxEx.SelectedIndex = -1;
            else
                dedComboBoxEx.SelectedValue = model.DeducteeId;

            if (model.NopId == 0)
                nopComboBoxEx.SelectedIndex = -1;
            else
                nopComboBoxEx.SelectedValue = model.NopId;
            aadharTextBoxExt.Text = model.AadharNo;
            panTextBoxExt.Text = model.PanNo;
            btobComboBoxEx.SelectedValue = model.BToB;
            pgLookup1.SelectedValue = model.PGroupId;
            pgLookup1.SetGroup();
            empLookup1.SelectedValue = model.EmpId;
            empLookup1.SetGroup();
            agentLookup1.SelectedValue = model.AgentId;
            agentLookup1.SetAcc(model.AgentId);
            transportLookup2.SelectedValue = model.TransportId;
            transportLookup2.SetAcc(model.TransportId);
            crLimitKontoTextBox.DoubleValue = (double) model.CrLimit;
            daysKontoTextBox.DoubleValue = (double)model.CrDays;
       
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;



            ledgerGroupLookup1.buttonEdit1.Focus();
            this.Text = "Account Master [View/Modify]";

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";
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
            EmpModel _find = new EmpModel();

            if (!string.IsNullOrWhiteSpace(nameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "AccName", Operation = Op.Contains, Value = nameTextBox.Text.Trim() });


            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.Accs.Where(ExpressionBuilder.GetExpression<AccModel>(filter))
                    .OrderBy(x => x.AccName).ToList();
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
            AccDto model = new AccDto();
            model.AccName = nameTextBox.Text.Trim();
            model.PrintName = printNameTextBoxExt.Text.Trim();
            model.GstIn = gstInTextBoxExt.Text.Trim();
            if (gstdateEdit.EditValue == null)
                model.GSTDate = DateTime.Now;
            else
                model.GSTDate = Convert.ToDateTime(gstdateEdit.EditValue);
            model.VatTds = taxTypeComboBox.SelectedValue.ToString();
            model.IoTax = iokontoComboBoxEx.SelectedValue.ToString();
            model.DeducteeId = Convert.ToInt32(dedComboBoxEx.SelectedValue);
            model.NopId = Convert.ToInt32(nopComboBoxEx.SelectedValue);
            model.Grade = gradeComboBoxEx.Text;
            model.CollDay = daysComboBoxEx.Text;
            model.TdsReq = tdsComboBoxEx.Text;
            model.TcsReq = tcsComboBoxEx.Text;
            model.PanNo = panTextBoxExt.Text.Trim();
            model.AadharNo = aadharTextBoxExt.Text.Trim();
            model.BToB = btobComboBoxEx.Text;
            model.PGroupId = Convert.ToInt32(pgLookup1.SelectedValue);
            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.CrDays = (int) daysKontoTextBox.DoubleValue;
            model.CrLimit = (decimal)crLimitKontoTextBox.DoubleValue;
            model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);
            model.IoTax = "NA";
            model.AgentId = Convert.ToInt32(agentLookup1.SelectedValue);
            model.TransportId = Convert.ToInt32(transportLookup2.SelectedValue);
            model.DiscPer = (decimal) discPerKontoTextBox.DoubleValue;
            var balmodel = new AccBalDto();
            balmodel.Address1 = address1TextBoxExt.Text.Trim();
            balmodel.Address2 = address2TextBoxExt.Text.Trim();
            if (Convert.ToInt32(cityLookup1.SelectedValue) == 0)
                balmodel.CityId = null;
            else
                balmodel.CityId = Convert.ToInt32(cityLookup1.SelectedValue);

           
            if (Convert.ToInt32(areaLookup1.SelectedValue) == 0)
                balmodel.AreaId = null;
            else
                balmodel.AreaId = Convert.ToInt32(areaLookup1.SelectedValue);
            balmodel.GroupId = Convert.ToInt32(ledgerGroupLookup1.SelectedValue);
            balmodel.PinCode = pinCodeTextBoxExt.Text.Trim();
            balmodel.MobileNo = mobileTextBoxExt.Text.Trim();
            balmodel.Email = emailTextBoxExt.Text.Trim();
            balmodel.CompId = KontoGlobals.CompanyId;
            balmodel.YearId = KontoGlobals.YearId;
           
            var AddrModel = new AddressDto();

          
            AddrModel.Address1 = balmodel.Address1;
            AddrModel.Address2 = balmodel.Address2;
            AddrModel.AddressType = "Mailing Address";
            AddrModel.AreaId = balmodel.AreaId;
            AddrModel.CityId = balmodel.CityId;
            AddrModel.Email = balmodel.Email;
            AddrModel.MobileNo = balmodel.MobileNo;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccDto, AccModel>().ForMember(x => x.Id, p => p.Ignore()
                );
                cfg.CreateMap<AddressDto, AccAddressModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<AccBalDto, AccBalModel>().ForMember(x => x.Id, p => p.Ignore());

            });

            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey == 0)
                        {
                            var mapper = new Mapper(config);
                            var _acc = new AccModel();
                            mapper.Map<AccDto, AccModel>(model,_acc);
                            db.Accs.Add(_acc);
                            db.SaveChanges();

                            var _addr = new AccAddressModel();
                            mapper.Map<AddressDto, AccAddressModel>(AddrModel, _addr);
                            _addr.AccId = _acc.Id;
                            db.AccAddresses.Add(_addr);
                            db.SaveChanges();
                            
                            var yearlist = db.FinYears.ToList();
                            var complist = db.Companies.ToList();
                            foreach (var yr in yearlist)
                            {
                                foreach (var comp in complist)
                                {
                                    var _model = new AccBalModel
                                    {
                                        AccId = _acc.Id,
                                        AccRowId = _acc.RowId,
                                        AddressId = _addr.Id,
                                        Bal = 0,
                                        GroupId = balmodel.GroupId,
                                        CompId = comp.Id,
                                        YearId = yr.Id,
                                        OpBal = 0,
                                        Address1 = balmodel.Address1,
                                        Address2 = balmodel.Address2,
                                        AreaId = balmodel.AreaId,
                                        CityId = balmodel.CityId,
                                        Email = balmodel.Email,
                                        MobileNo = balmodel.MobileNo,
                                        PinCode = balmodel.PinCode,
                                    };
                                    db.AccBals.Add(_model);
                                }
                            }

                        }
                        else
                        {
                            var _accmodel = db.Accs.Find(this.PrimaryKey);
                            var _existbalmodel = db.AccBals.FirstOrDefault(x => x.AccId == this.PrimaryKey && x.CompId == KontoGlobals.CompanyId
                                && x.YearId == KontoGlobals.YearId);
                            var _existaddrs = db.AccAddresses.Find(_existbalmodel.AddressId);
                            if (_existaddrs == null)
                            {
                                _existaddrs = db.AccAddresses.FirstOrDefault(x => x.AccId == _accmodel.Id);
                            }
                                var mapper = config.CreateMapper();
                            mapper.Map<AccDto, AccModel>(model, _accmodel);
                            mapper.Map<AccBalDto, AccBalModel>(balmodel, _existbalmodel);


                            if (_existaddrs != null)
                            {
                                _existbalmodel.AddressId = _existaddrs.Id;
                                AddrModel.AccId = _accmodel.Id;
                                _existaddrs.AccId = _accmodel.Id;
                                mapper.Map<AddressDto, AccAddressModel>(AddrModel, _existaddrs);
                            }

                            _existbalmodel.AccId = _accmodel.Id;
                            
                           
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "acc Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }
                    
                }
            }
            if (IsSaved)
            {
               
               
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    this.ResetPage();
                    NewRec();
                    ledgerGroupLookup1.buttonEdit1.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void ledgerGroupLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            List<ComboBoxPairs> cbp1 = null;
            ioLayoutControlItem.ContentVisible = false;

            if (ledgerGroupLookup1.SelectedValue == null) return;
            if (ledgerGroupLookup1.GroupDto != null)
            {
                gstinLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.TaxationReq;
                gstDatalayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.TaxationReq;
                panlayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.TaxationReq;
                aadharLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.TaxationReq;

                gradeLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.GradeReq;
                collDaysLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.CollDaysReq;

                tdslayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.TdsReq;
                tcslayoutControlItem.ContentVisible =ledgerGroupLookup1.GroupDto.TcsReq;
                panlayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.TaxationReq;
                aadharLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.TaxationReq;

                addressLayoutControlGroup.Visibility = GetLayoutVisibility(ledgerGroupLookup1.GroupDto.AddressReq);
                partyGroupLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.PartyGroupReq;
                salesmanLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.SalesmanReq;
                creditLayoutControlGroup.Visibility =GetLayoutVisibility(ledgerGroupLookup1.GroupDto.CrLimitReq);
                agentLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.AgentReq;
                transportLayoutControlItem.ContentVisible = ledgerGroupLookup1.GroupDto.TransportReq;

            }

            if (ledgerGroupLookup1.SelectedValue.ToString() == "19")
            {
                cbp1 = new List<ComboBoxPairs>
                {
                new ComboBoxPairs("State/Ut Tax", "SGST"),
                new ComboBoxPairs("Central Tax", "CGST"),
                new ComboBoxPairs("Cess", "CESS"),
                new ComboBoxPairs("Integrated Tax", "IGST"),
                new ComboBoxPairs("Tds", "TDS"),
                new ComboBoxPairs("Tcs", "TCS"),
                new ComboBoxPairs("Non-GST","NGST"),
                new ComboBoxPairs("Other","OTH"),
                new ComboBoxPairs("Provisional","PROV"),
                new ComboBoxPairs("NA","NA")

                };
                taxTypeComboBox.DataSource = cbp1;
                ioLayoutControlItem.ContentVisible = true;
            }
            else if (ledgerGroupLookup1.GroupDto.Nature == "LIABILITIES" ||
               ledgerGroupLookup1.GroupDto.Nature == "ASSETS")
            {
                cbp1 = new List<ComboBoxPairs>
                {
                    new ComboBoxPairs("Regular", "REG"),
                    new ComboBoxPairs("Consumer", "CON"),
                    new ComboBoxPairs("Un-Register", "URD"),
                    new ComboBoxPairs("Composition", "CMP"),
                    new ComboBoxPairs("Ecommerce-Op", "ECOM"),
                    new ComboBoxPairs("NA", "NA"),
                };
                taxTypeComboBox.DataSource = cbp1;
            }
           
           

        }

        private LayoutVisibility GetLayoutVisibility(bool _value)
        {
            if (_value)
                return LayoutVisibility.Always;

            return LayoutVisibility.Never;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if(this.PrimaryKey == 0)
            {
                printNameTextBoxExt.Text = nameTextBox.Text;
            }
        }

        private async void gstinLayoutControlItem_DoubleClick(object sender, EventArgs e)
        {
            
            if (KontoGlobals.GstIn.Length !=15)
            {
                MessageBox.Show("Please Enter GstNo in Company Master!");
                return;
            }
            if (gstInTextBoxExt.Text.Length != 15) return;
            TxnRespWithObj<SearchTaxpayerJson> txnResp = await PublicAPI.SearchTaxPayerAsync("1602351118", "taxpro*199", KontoGlobals.GstIn, gstInTextBoxExt.Text.Trim());
            taxTypeComboBox.Text = txnResp.RespObj.dty.ToUpper();
            nameTextBox.Text = txnResp.RespObj.tradeNam;
            printNameTextBoxExt.Text = txnResp.RespObj.tradeNam;
            address1TextBoxExt.Text = txnResp.RespObj.pradr.addr.bno + "-" + txnResp.RespObj.pradr.addr.bnm + " " + txnResp.RespObj.pradr.addr.st + " "
                + txnResp.RespObj.pradr.addr.loc + " " + txnResp.RespObj.pradr.addr.stcd;
            pinCodeTextBoxExt.Text = txnResp.RespObj.pradr.addr.pncd;
            gstdateEdit.Text = txnResp.RespObj.rgdt;
           



            using (var db = new KontoContext())
            {
                try
                {
                    var stname = txnResp.RespObj.pradr.addr.stcd;
                    var st = db.States.FirstOrDefault(x => x.StateName == stname);
                    if (st != null)
                    {
                        var ctname = txnResp.RespObj.pradr.addr.dst;
                        var ct = db.Cities.FirstOrDefault(x => x.CityName == ctname);
                        if (ct == null && ctname != "")
                        {
                            ct = new Konto.Data.Models.Masters.CityModel();
                            ct.CityName = ctname;
                            ct.StateId = st.Id;
                            ct.CreateUser = KontoGlobals.UserName;
                            ct.CreateDate = DateTime.Now;
                            ct.IpAddress = "NA";
                            db.Cities.Add(ct);
                            db.SaveChanges();
                        }
                        if (ct != null)
                        {
                            cityLookup1.SelectedValue = ct.Id;
                            cityLookup1.SetCity();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //throw;
                }
            }
        }

        private void gstInTextBoxExt_TextChanged(object sender, EventArgs e)
        {
            if(gstInTextBoxExt.Text.Length==15)
                panTextBoxExt.Text = gstInTextBoxExt.Text.Substring(2, 10);
        }
    }
}

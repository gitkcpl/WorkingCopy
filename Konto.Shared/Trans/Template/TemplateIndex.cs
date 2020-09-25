using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Konto.Core.Shared.Libs;

namespace Konto.Shared.Trans.Template
{
    public partial class TemplateIndex : KontoMetroForm
    {
        private List<TemplateModel> FilterView = new List<TemplateModel>();
        private List<TempTransDto> DelTrans = new List<TempTransDto>();
        public TemplateIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            this.Load += TemplateIndex_Load;
            FillLookup();
        }

        private void TemplateIndex_Load(object sender, EventArgs e)
        {
            btnGet.Enabled = false;
            btnGet.AllowFocus = false;

            this.NewRec();
        }

        #region UDF

        private void FillLookup()
        {
            using (var db = new KontoContext())
            {
                var TemplateAsList = (from p in db.Templates
                                      select new BaseLookupDto()
                                      {
                                          DisplayText = p.Descriptions,
                                          Id = p.Id
                                      }).ToList();

                var vtype = (from p in db.VoucherTypes
                             where !p.IsDeleted && p.IsActive
                             orderby p.TypeName
                             select new BaseLookupDto()
                             {
                                 DisplayText = p.TypeName,
                                 Id = p.Id
                             }).ToList();

                TemplateAsLookUpEdit.Properties.DataSource = TemplateAsList;
                typeLookUpEdit.Properties.DataSource = vtype;

                // var fieldLists =
                //(from fl in db.TempFields
                // //orderby fl.FieldName
                // select new TempModelDto
                // {
                //     TempFieldId = fl.Id,
                //     FieldName = fl.FieldName,
                //     ColNo = 0,
                //     Id= fl.Id*-1
                // }).ToList();

                // this.bindingSource1.DataSource = fieldLists;
            }
        }
        public override void SaveDataAsync(bool newmode)
        {
            bool IsSaved = false;
            if (!ValidateData()) return;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TempTransDto, TemplateTrans>().ForMember(x => x.Id, p => p.Ignore());
            });
            using (KontoContext _db = new KontoContext())
            {
                using (var transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        TemplateModel temp;

                        var fieldLists = bindingSource1.DataSource as List<TempTransDto>;
                        //List<TempTransDto> fieldLists = (list.Where(k => k.ColNo > 0).ToList());
                        var fieldUpdatedZero = _db.Templatetrans.Where(k => k.TemplateId == this.PrimaryKey).ToList();

                        var excludedIDs = new HashSet<int?>(fieldLists.Select(p => p.TempFieldId));
                        var result = fieldUpdatedZero.Where(p => !excludedIDs.Contains((int)p.TempFieldId)).ToList();

                        foreach (var item in result)
                        {
                            item.IsDeleted = true;
                            if (item.Id > 0)
                            {
                                item.ModifyDate = DateTime.Now;
                                item.ModifyUser = KontoGlobals.UserName;
                            }
                        }
                        int vtypeId = Convert.ToInt32(typeLookUpEdit.EditValue);

                        temp = _db.Templates.Find(this.PrimaryKey);

                        if (temp == null)
                            temp = new TemplateModel();

                        temp.Descriptions = tempName.Text;
                        temp.StartRowNo = Convert.ToInt32(StartRowNospinEdit.Value);
                        temp.VTypeId = vtypeId;
                        if (accLookup.SelectedValue != null)
                            temp.AccId = Convert.ToInt32(accLookup.SelectedValue);

                        if (voucherLookup11.SelectedValue != null)
                            temp.VoucherId = Convert.ToInt32(voucherLookup11.SelectedValue);

                        temp.IsActive = true;
                        if (temp.Id == 0)
                        {
                            temp.CreateUser = KontoGlobals.UserName;
                            temp.CreateDate = DateTime.Now;
                            _db.Templates.Add(temp);
                        }
                        else
                        {
                            temp.ModifyDate = DateTime.Now;
                            temp.ModifyUser = KontoGlobals.UserName;
                        }
                        var map = new Mapper(config);
                        foreach (var item in fieldLists)
                        {
                            item.TemplateId = temp.Id;
                            var tranModel = new TemplateTrans();
                            if (item.Id > 0)
                            {
                                tranModel = _db.Templatetrans.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);
                            if (item.Id <= 0)
                            {
                                tranModel.IsActive = true;
                                tranModel.CreateUser = KontoGlobals.UserName;
                                tranModel.CreateDate = DateTime.Now;
                                _db.Templatetrans.Add(tranModel);
                            }
                        }
                        //delete item from ord trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = _db.Templatetrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        _db.SaveChanges();
                        transaction.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Template Save Error");
                        transaction.Rollback();
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
                    this.NewRec();
                    tempName.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(tempName.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Template Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tempName.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(tempName.Text))
            {
                var _db = new KontoContext();

                int vtypeId = Convert.ToInt32(typeLookUpEdit.EditValue);
                int VoucherId = Convert.ToInt32(voucherLookup11.SelectedValue);

                var find = _db.Templates.FirstOrDefault(x => x.Descriptions == tempName.Text
                            && x.VTypeId == vtypeId && x.VoucherId == VoucherId
                            && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBox.Show("Entered Template Name is Already Exists");
                    return false;
                }
            }
            if (Convert.ToInt32(typeLookUpEdit.EditValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                typeLookUpEdit.Focus();
                return false;
            }

            var list = bindingSource1.DataSource as List<TempTransDto>;
            var fieldLists = (list.Where(k => k.ColNo > 0).ToList());
            if (fieldLists.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Add Proper Field number", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView11.Focus();
                return false;
            }

            return true;
        }
        private void LoadData(TemplateModel temp)
        {
            this.ResetPage();
            this.PrimaryKey = temp.Id;

            tempName.Text = temp.Descriptions;
            StartRowNospinEdit.Value = Convert.ToDecimal(temp.StartRowNo);
            typeLookUpEdit.EditValue = temp.VTypeId;

            if (temp.AccId != null)
            {
                accLookup.SelectedValue = temp.AccId;
                accLookup.SetAcc((int)temp.AccId);
            }
            if (temp.VoucherId != null)
            {
                voucherLookup11.SelectedValue = temp.VoucherId;
                voucherLookup11.SetGroup((int)temp.VoucherId);
            }
            using (var db = new KontoContext())
            {
                var fieldLists =
             (from fl in db.TempFields
              orderby fl.FieldName
              select new TempTransDto
              {
                  TempFieldId = fl.Id,
                  FieldName = fl.FieldName,
                  ColNo = 0,
                  Id = fl.Id * -1
              }).ToList();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TemplateTrans, TempTransDto>().ForMember(x => x.Id, p => p.Ignore());
                });
                var map = new Mapper(config);
                string fname = "";
                foreach (var item in fieldLists)
                {
                    var data = db.Templatetrans.FirstOrDefault(k => k.TemplateId == temp.Id && k.TempFieldId == item.TempFieldId && !k.IsDeleted);
                    if (data != null)
                    {
                        fname = item.FieldName;
                        map = new Mapper(config);
                        map.Map(data, item);
                        item.FieldName = fname;
                        item.Id = data.Id;
                    }
                }
                this.bindingSource1.DataSource = fieldLists;
            }
        }

        #endregion

        #region Events

        private void btnGet_Click(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {
                List<TempTransDto> fieldLists = bindingSource1.DataSource as List<TempTransDto>;
                int tempId = Convert.ToInt32(TemplateAsLookUpEdit.EditValue);
                foreach (var item in fieldLists)
                {
                    var data = db.Templatetrans.FirstOrDefault(k => k.TemplateId == tempId
                                            && k.TempFieldId == item.TempFieldId
                                            && !k.IsDeleted);
                    if (data != null)
                        if (data.ColNo != null)
                            item.ColNo = (int)data.ColNo;
                }
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
                Log.Error(ex, "Template Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }
        private void typeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            int vid = Convert.ToInt32(typeLookUpEdit.EditValue);
            voucherLookup11.VTypeId = (VoucherTypeEnum)vid;
        }
        private void TemplateAsLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (TemplateAsLookUpEdit.EditValue != DBNull.Value)
            {
                int tempAsId = Convert.ToInt32(TemplateAsLookUpEdit.EditValue);
                if (tempAsId != 0 || tempAsId != null)
                    btnGet.Enabled = true;
                btnGet.AllowFocus = true;
            }
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                tempName.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as TemplateListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new TemplateListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Template [View]";

            }
        }

        #endregion

        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Template print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<TemplateModel>();
            this.Text = "Template [Add New]";
            typeLookUpEdit.EditValue = 0;
            //this.ActiveControl = voucherLookup11.buttonEdit1;
            //voucherLookup11.SetDefault();

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            DelTrans = new List<TempTransDto>();

            this.bindingSource1.DataSource = new List<TempTransDto>();
            var db = new KontoContext();
            var fieldLists =
              (from fl in db.TempFields
               orderby fl.FieldName
               select new TempTransDto
               {
                   TempFieldId = fl.Id,
                   FieldName = fl.FieldName,
                   ColNo = 0,
                   Id = fl.Id * -1
               }).ToList();

            this.bindingSource1.DataSource = fieldLists;

            tempName.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();

            accLookup.SetEmpty();
            voucherLookup11.SetEmpty();
            tempName.Text = string.Empty;
            StartRowNospinEdit.EditValue = 0;
            TemplateAsLookUpEdit.EditValue = DBNull.Value;

            DelTrans = new List<TempTransDto>();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                TemplateModel template = db.Templates.Find(_key);
                LoadData(template);
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


            if (Convert.ToInt32(voucherLookup11.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup11.SelectedValue) });
            }

            if (Convert.ToInt32(accLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "AccId", Operation = Op.Equals, Value = Convert.ToInt32(accLookup.SelectedValue) });
            }

            //  filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            //  filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.Templates.Where(ExpressionBuilder.GetExpression<TemplateModel>(filter))
                    .OrderBy(x => x.Id).ToList();

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

        #endregion

    }
}

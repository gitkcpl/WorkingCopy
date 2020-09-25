using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Export.Html;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.VType
{
    public partial class VTypeIndex : KontoMetroForm
    {
        private List<VoucherTypeModel> FilterView = new List<VoucherTypeModel>();
        private BindingList<VoucherBookModel> BookList { get; set; }
        private List<VoucherBookModel> DelBookList { get; set; }
        private BindingList<VoucherPartyModel> PartyList { get; set; }
        private List<VoucherPartyModel> DelPartyList { get; set; }
        
        private BindingList<VoucherItemModel> ItemList { get; set; }
        private List<VoucherItemModel> DelItemList { get; set; }

        public VTypeIndex()
        {
            InitializeComponent();
            
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += ColorIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            bookGridView.KeyDown += BookGridView_KeyDown;
            partyGridView.KeyDown += PartyGridView_KeyDown;
            itemGridView.KeyDown += ItemGridView_KeyDown;
            this.MainLayoutFile = KontoFileLayout.Voucher_Type;
            SetGridData();
        }

        private void ItemGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as VoucherItemModel;
                view.DeleteRow(view.FocusedRowHandle);
                DelItemList.Add(row);
            }
        }

        private void PartyGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as VoucherPartyModel;
                view.DeleteRow(view.FocusedRowHandle);
                DelPartyList.Add(row);
            }
        }

        private void BookGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as VoucherBookModel;
                view.DeleteRow(view.FocusedRowHandle);
                DelBookList.Add(row);
            }
        }

        private void SetGridData()
        {
            PartyList = new BindingList<VoucherPartyModel>();
            BookList = new BindingList<VoucherBookModel>();
            ItemList = new BindingList<VoucherItemModel>();
            DelBookList = new List<VoucherBookModel>();
            DelPartyList = new List<VoucherPartyModel>();
            DelItemList = new List<VoucherItemModel>();
            booksGridControl.DataSource = BookList;
            partyGridControl.DataSource = PartyList;
            itemGridControl.DataSource = ItemList;

            gridColumn1.ColumnEdit = bookRepositoryItemLookUpEdit;

            using(var db = new KontoContext())
            {
                var grplist = (from p in db.AcGroupModels
                               where !p.IsDeleted && p.IsActive
                               orderby p.GroupName
                               select new BaseLookupDto()
                               {
                                   DisplayText = p.GroupName,
                                   Id = p.Id
                               }).ToList();

                var item_list = (from p in db.ProductTypes
                                where !p.IsDeleted && p.IsActive
                                orderby p.TypeName
                                select new BaseLookupDto()
                                {
                                    DisplayText = p.TypeName,
                                    Id = p.Id
                                }).ToList();

                bookRepositoryItemLookUpEdit.DataSource = grplist;
                typeRepositoryItemLookUpEdit.DataSource = item_list;
            }
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                nameTextEdit.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as VTypeListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new VTypeListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Color Master [View]";

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

                Log.Error(ex, "Voucher Type Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void ColorIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = nameTextEdit;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Size Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Voucher Type Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextEdit.Focus();
                return false;
            }
           


            using (var db = new KontoContext())
            {
                var find = db.VoucherTypes.FirstOrDefault(
                   x => x.TypeName == nameTextEdit.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Voucher Type Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextEdit.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<VoucherTypeModel>();
            this.Text = "Color Master [Add New]";
            this.ActiveControl = nameTextEdit;
            SetGridData();
            tabbedControlGroup1.SelectedTabPageIndex = 0;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextEdit.Text = string.Empty;
            smsTempTextEdit.Text = string.Empty;
            partySmsCheckEdit.Checked = false;
            brokerSmsCheckEdit.Checked = false;
            userSmscheckEdit.Checked = false;
            subjectTextEdit.Text = string.Empty;
            partyEmailcheckEdit.Checked = false;
            brokerEmailcheckEdit.Checked = false;
            userEmailcheckEdit.Checked = false;
            othersEmailTextEdit.Text = string.Empty;
            bodyRichEditControl.Text = string.Empty;
            termsRichEditControl.Text = string.Empty;
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.VoucherTypes.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(VoucherTypeModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextEdit.Text = model.TypeName;
            termsRichEditControl.HtmlText = model.Terms;
            smsTempTextEdit.Text = model.SmsTemplates;
            partySmsCheckEdit.Checked = Convert.ToBoolean(model.SmsToParty);
            brokerSmsCheckEdit.Checked = Convert.ToBoolean(model.SmsToAgent);
            userSmscheckEdit.Checked = Convert.ToBoolean(model.SmsToUser);
            smsOtherstextEdit.Text = model.OtherMobile;

            subjectTextEdit.Text = model.EmailSub;
            partyEmailcheckEdit.Checked= Convert.ToBoolean(model.EmailToParty);
            brokerEmailcheckEdit.Checked = Convert.ToBoolean(model.EmailToAgent);
            userEmailcheckEdit.Checked = Convert.ToBoolean(model.EmailToUser);
            bodyRichEditControl.HtmlText = model.EmailBody;
            othersEmailTextEdit.Text = model.OtherEmail;

            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            this.Text = "Voucher Type Master [View/Modify]";

            using(var db = new KontoContext())
            {
                ItemList = new BindingList<VoucherItemModel>(db.VoucherItems.Where(x => !x.IsDeleted && x.VoucherTypeId == model.Id).ToList());
                PartyList = new BindingList<VoucherPartyModel>(db.VoucherParties.Where(x => !x.IsDeleted && x.VoucherTypeId == model.Id).ToList());
                BookList = new BindingList<VoucherBookModel>(db.VoucherBooks.Where(x => !x.IsDeleted && x.VoucherTypeId == model.Id).ToList());
            }
            
            booksGridControl.DataSource = BookList;
            partyGridControl.DataSource = PartyList;
            itemGridControl.DataSource = ItemList;

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
            VoucherTypeModel _find = new VoucherTypeModel();

            if (!string.IsNullOrWhiteSpace(nameTextEdit.Text.Trim()))
                filter.Add(new Filter { PropertyName = "TypeName", Operation = Op.Contains, Value = nameTextEdit.Text.Trim() });

           

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.VoucherTypes.Where(ExpressionBuilder.GetExpression<VoucherTypeModel>(filter))
                    .OrderBy(x => x.TypeName).ToList();
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
            VoucherTypeModel model = new VoucherTypeModel();

            using (var db = new KontoContext())
            {
                using(var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                            model = db.VoucherTypes.Find(this.PrimaryKey);

                        model.TypeName = nameTextEdit.Text.Trim();
                        
                        HtmlDocumentExporterOptions options = new HtmlDocumentExporterOptions();
                        options.ExportRootTag = ExportRootTag.Body;
                        options.CssPropertiesExportType = CssPropertiesExportType.Inline;
                        HtmlExporter exporter = new HtmlExporter(this.termsRichEditControl.Model, options);
                        string stringHtml = exporter.Export();

                        model.Terms = stringHtml;
                        model.SmsTemplates = smsTempTextEdit.Text.Trim();
                        model.SmsToParty = partySmsCheckEdit.Checked;
                        model.SmsToAgent = brokerSmsCheckEdit.Checked;
                        model.SmsToUser = userSmscheckEdit.Checked;
                        model.OtherMobile = smsOtherstextEdit.Text.Trim();

                        model.EmailSub = subjectTextEdit.Text.Trim();
                        model.EmailToParty = partyEmailcheckEdit.Checked;
                        model.EmailToAgent = brokerEmailcheckEdit.Checked;
                        model.SmsToUser = userEmailcheckEdit.Checked;
                        model.EmailBody = new HtmlExporter(this.bodyRichEditControl.Model, options).Export(); //bodyRichEditControl.HtmlText;
                        model.OtherEmail = othersEmailTextEdit.Text.Trim();

                        model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                        if (this.PrimaryKey == 0)
                        {
                            db.VoucherTypes.Add(model);
                            db.SaveChanges();
                        }

                       // voucher item type

                        foreach (var item in ItemList)
                        {
                            item.VoucherTypeId = model.Id;
                            
                            if (item.Id <= 0)
                            {
                                db.VoucherItems.Add(item);
                            }
                            else
                            {
                                var _item = db.VoucherItems.Find(item.Id);
                                _item.PTypeId = item.PTypeId;
                            }
                        }

                        // voucher books
                        foreach (var Book in BookList)
                        {
                            Book.VoucherTypeId = model.Id;
                            if (Book.Id <= 0)
                            {
                                db.VoucherBooks.Add(Book);
                            }
                            else
                            {
                                var _item = db.VoucherBooks.Find(Book.Id);
                                _item.GroupId = Book.GroupId;
                            }
                        }
                        // voucher party group
                        foreach (var patry in PartyList)
                        {
                            patry.VoucherTypeId = model.Id;
                            if (patry.Id <= 0)
                            {
                                db.VoucherParties.Add(patry);                                
                            }
                            else
                            {
                                var _item = db.VoucherParties.Find(patry.Id);
                                _item.GroupId = patry.GroupId;
                            }
                        }

                        foreach (var item in DelItemList)
                        {
                            var itm = db.VoucherItems.Find(item.Id);
                            if (itm != null)
                            {
                                itm.IsDeleted = true;
                            }
                        }
                        foreach (var Book in DelBookList)
                        {
                            var book = db.VoucherBooks.Find(Book.Id);
                            if (book != null)
                            {
                                book.IsDeleted = true;
                            }
                        }
                        foreach (var patry in DelPartyList)
                        {
                            var prty = db.VoucherParties.Find(patry.Id);
                            if (prty != null)
                            {
                                prty.IsDeleted = true;
                            }
                        }

                        db.SaveChanges();

                        _tran.Commit();
                        IsSaved = true;

                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "voucher Save");
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
                    nameTextEdit.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void partyGridControl_Click(object sender, EventArgs e)
        {

        }
    }
}

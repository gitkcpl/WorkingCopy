using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared;
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

namespace Konto.Shared.Masters.Acc
{
    public partial class AddressView : KontoForm
    {
        private string GridLayoutFileName { get; set; }
        private BindingList<AccAddressModel> AddressList { get; set; }
        private List<AccAddressModel> DelList { get; set; } 

        private int RowNo { get; set; }
        
        public int AccId { get; set; }
        private KontoContext db;
        public AddressView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Acc_Address_List_Layout;
            this.Load += AddressView_Load;
            this.customGridView1.RowClick += CustomGridView1_RowClick;
            this.addSimpleButton.Click += AddSimpleButton_Click;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.customGridView1.KeyDown += CustomGridView1_KeyDown;
            db = new KontoContext();
            DelList = new List<AccAddressModel>();
            this.RowNo = -1;
            this.addressTypeComboBoxEdit.SelectedIndex = 0;
            
        }

        private void CustomGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var rw = view.GetRow(view.FocusedRowHandle) as AccAddressModel;
                view.DeleteRow(view.FocusedRowHandle);
                DelList.Add(rw);
            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in AddressList)
                {
                    if (item.Id == 0)
                    {
                        item.AccId = this.AccId;
                        db.AccAddresses.Add(item);
                    }
                    else
                    {
                        var model = db.AccAddresses.Find(item.Id);
                        model.ContactPerson = item.ContactPerson;
                        model.Address1 = item.Address1;
                        model.Address2 = item.Address2;
                        model.PinCode = item.PinCode;
                        model.CityId = item.CityId;
                        model.AreaId = item.AreaId;
                        model.MobileNo = item.MobileNo;
                        model.Email = item.Email;
                        model.Website = item.Website;
                        model.Others = item.Others;
                        model.AddressType = item.AddressType;
                        model.IsDefault = item.IsDefault;
                    }
                }

                foreach (var item in DelList)
                {
                    if (item.Id == 0) continue;
                    var model = db.AccAddresses.Find(item.Id);
                    model.IsDeleted = true;
                }
                db.SaveChanges();

                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Address Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
           
        }

        private void AddSimpleButton_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;

            var model = new AccAddressModel();
            if(this.RowNo>=0)
            {
                model = customGridView1.GetRow(RowNo) as AccAddressModel;
            }
            if (model == null) return;
            model.ContactPerson = nameTextBox.Text.Trim();
            model.Address1 = address1TextBoxExt.Text.Trim();
            model.Address2 = address2TextBoxExt.Text.Trim();
            
            if(Convert.ToInt32(cityLookup1.SelectedValue)!=0)
                model.CityId = Convert.ToInt32(cityLookup1.SelectedValue);
            else
                model.CityId = 1;

            if (Convert.ToInt32(areaLookup1.SelectedValue) != 0)
                model.AreaId = Convert.ToInt32(areaLookup1.SelectedValue);
            else
                model.AreaId = 1;

            model.PinCode = pinCodeTextBoxExt.Text.Trim();
            model.MobileNo = mobileTextBoxExt.Text.Trim();
            model.Email = emailTextBoxExt.Text.Trim();
            model.Website = webSiteTextBoxExt.Text.Trim();
            model.AddressType = addressTypeComboBoxEdit.Text;
            model.Others = othersTextBoxExt.Text.Trim();
            model.IsDefault = checkBox1.Checked;
            if (this.RowNo < 0)
            {
                model.RowId = Guid.NewGuid();
                this.AddressList.Add(model);
            }
            this.customGridControl1.RefreshDataSource();
            this.RowNo = -1;

            nameTextBox.Clear();
            address1TextBoxExt.Clear();
            address2TextBoxExt.Clear();
            cityLookup1.SelectedValue = 1;
            areaLookup1.SelectedValue = 1;
            cityLookup1.SetCity();
            areaLookup1.SetArea();
            pinCodeTextBoxExt.Clear();
            mobileTextBoxExt.Clear();
            emailTextBoxExt.Clear();
            webSiteTextBoxExt.Clear();
            addressTypeComboBoxEdit.SelectedIndex = 0;
            othersTextBoxExt.Clear();
            checkBox1.Checked = false;
            nameTextBox.Focus(); 
        }

        private void CustomGridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            var vw = sender as GridView;
            var model = vw.GetRow(e.RowHandle) as AccAddressModel;
          //  this.PrimaryKey = model.Id;
            this.RowNo = e.RowHandle;
            nameTextBox.Text = model.ContactPerson;
            address2TextBoxExt.Text = model.Address2;
            address1TextBoxExt.Text = model.Address1;
            cityLookup1.SelectedValue = model.CityId;
            cityLookup1.SetCity();
            areaLookup1.SelectedValue = model.AreaId;
            areaLookup1.SetArea();
            pinCodeTextBoxExt.Text = model.PinCode;
            mobileTextBoxExt.Text = model.MobileNo;
            emailTextBoxExt.Text = model.Email;
            webSiteTextBoxExt.Text = model.Website;
            addressTypeComboBoxEdit.Text = model.AddressType;
            othersTextBoxExt.Text = model.Others;
            checkBox1.Checked = model.IsDefault;

        }

        private void AddressView_Load(object sender, EventArgs e)
        {
           
                AddressList = new BindingList<AccAddressModel>( db.AccAddresses.Where(x => x.AccId == this.AccId && !x.IsDeleted).ToList());
            
            this.customGridControl1.DataSource = AddressList;

            if (string.IsNullOrEmpty(this.GridLayoutFileName)) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.customGridView1);

            this.ActiveControl = nameTextBox;

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
             if (keyData == (Keys.S | Keys.Shift))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.customGridView1);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private bool ValidateData()
        {
            if(Convert.ToInt32(cityLookup1.SelectedValue) ==0)
            {
                MessageBox.Show("Invalid City");
                cityLookup1.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(addressTypeComboBoxEdit.Text))
            {
                MessageBox.Show("Invalid Address");
                addressTypeComboBoxEdit.Focus();
                return false;
            }

            return true;
        }
    }
}

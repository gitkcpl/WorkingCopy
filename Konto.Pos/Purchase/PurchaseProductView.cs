using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Pos.Dtos;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Pos.Purchase
{
    public partial class PurchaseProductView : KontoForm
    {
        public List<PosPurTransDto> ItemDetails = new List<PosPurTransDto>();
        public bool IsGst { get; set; }

        public string GridLayoutFileName = "pos\\pur_barcode_grid.xml";
        public string MainLayoutFileName = "pos\\pur_barcode.xml";
        public PurchaseProductView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            genItemSimpleButton.Click += GenItemSimpleButton_Click;
            
            profitKontoSpinEdit.EditValueChanged += ProfitSpinEdit_EditValueChanged;

            saleRateKontoSpinEdit.EditValueChanged += SaleRatespinEdit_EditValueChanged;

            nameTextBoxExt.TextChanged += NameTextBoxExt_TextChanged;

            taxTypelookUpEdit.Properties.DisplayMember = "TaxName";
            taxTypelookUpEdit.Properties.ValueMember = "Id";

            purUnitlookUpEdit.Properties.DisplayMember = "DisplayText";
            purUnitlookUpEdit.Properties.ValueMember = "Id";

            unitLookUpEdit.Properties.DisplayMember = "DisplayText";
            unitLookUpEdit.Properties.ValueMember = "Id";

            sizeCheckedComboBoxEdit.Properties.DisplayMember = "DisplayText";
            sizeCheckedComboBoxEdit.Properties.ValueMember = "Id";

            qtyKontoSpinEdit.EditValueChanged += QtyKontoSpinEdit_EditValueChanged;
            
           

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1 | Keys.Shift))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
                KontoUtils.SaveMainFormLayout(this.MainLayoutFileName, layoutControl1);
                return true;
            }
            else if (keyData == (Keys.F2 | Keys.Shift))
            {

                var frm = new GridPropertView();
                frm.gridControl1.DataSource = this.gridControl1.DataSource;
                frm.gridView1.Assign(this.gridView1, false);
                if (frm.ShowDialog() != DialogResult.OK) return true;
                this.gridView1.Assign(frm.gridView1, false);
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void QtyKontoSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            pcsKontoSpinEdit.Value = Convert.ToInt32(qtyKontoSpinEdit.Value);
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NameTextBoxExt_TextChanged(object sender, EventArgs e)
        {
            descTextBoxExt.Text = nameTextBoxExt.Text;
        }

        private void SaleRatespinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if(saleRateKontoSpinEdit.Value  >0)
            {
                mrpKontoSpinEdit.Value = saleRateKontoSpinEdit.Value;
                if (purRateKontoSpinEdit.Value == 0)
                {
                    purRateKontoSpinEdit.Value = Math.Round((saleRateKontoSpinEdit.Value * (100 / (100 + profitKontoSpinEdit.Value))), 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        private void ProfitSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            var tx = (TaxModel) taxTypelookUpEdit.Properties.GetDataSourceRowByKeyValue(taxTypelookUpEdit.EditValue);

            if (mrpKontoSpinEdit.Value > 0 && purRateKontoSpinEdit.Value == 0)
                saleRateKontoSpinEdit.Value = Math.Round(mrpKontoSpinEdit.Value * (100 / (100 + profitKontoSpinEdit.Value)), 2, MidpointRounding.AwayFromZero);
            else
            {
                if (tx != null)
                {
                    saleRateKontoSpinEdit.Value = Math.Round(purRateKontoSpinEdit.Value + (purRateKontoSpinEdit.Value * tx.Igst / 100) + (purRateKontoSpinEdit.Value * profitKontoSpinEdit.Value) / 100, 2, MidpointRounding.AwayFromZero);
                }
            }



        }

        private void GenItemSimpleButton_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;

            string sizez = "1";

            if (!string.IsNullOrEmpty(sizeCheckedComboBoxEdit.Text))
            {
                sizez = sizeCheckedComboBoxEdit.Properties.GetCheckedItems().ToString();
                
            }

            var arsz = sizez.Split(',');

            if (arsz.Length == 0)
            {
                arsz = new string[1];
                arsz[0] = "1";
            }
            var db = new KontoContext();

            var tx = db.TaxMasters.Find(Convert.ToInt32(taxTypelookUpEdit.EditValue));
            if (tx == null) return;

            
            decimal _Qty = qtyKontoSpinEdit.Value;
            int _TQty = 1;

            if (uniqueCheckEdit.Checked)
            {
                _TQty = Convert.ToInt32( _Qty);
                _Qty = 1;
            }
            
            try
            {
                foreach (var item in arsz)
                {
                    for (int i = 0; i < _TQty; i++)
                    {


                        var model = new PosPurTransDto();
                        model.Barcode = barcodeTextBoxExt.Text;

                        if (this.IsGst)
                        {
                            model.CgstPer = tx.Cgst;
                            model.SgstPer = tx.Sgst;
                        }
                        else
                        {
                            model.IgstPer = tx.Igst;
                        }
                        model.ItemCode = codeTextBoxExt.Text.Trim();
                        model.ProductName = nameTextBoxExt.Text.Trim();
                        model.Description = descTextBoxExt.Text.Trim();
                        model.StyleNo = styleNoTextEdit.Text.Trim();

                        model.GroupId = Convert.ToInt32(groupLookup1.SelectedValue);
                        model.GroupName = groupLookup1.SelectedText;

                        model.SubGroupId = Convert.ToInt32(subGroupLookup1.SelectedValue);
                        model.SubGroupName = subGroupLookup1.SelectedText;

                        model.BrandId = Convert.ToInt32(brandLookup1.SelectedValue);
                        model.Brand = brandLookup1.SelectedText;

                        model.CategoryId = Convert.ToInt32(categoryLookup1.SelectedValue);
                        model.Category = categoryLookup1.SelectedText;

                        model.ColorId = Convert.ToInt32(colorLookup1.SelectedValue);
                        model.ColorName = colorLookup1.SelectedText;

                        model.SizeId = Convert.ToInt32(item);
                        model.Size = db.SizeModels.Find(model.SizeId).SizeName;


                        model.HsnCode = hsnTextBoxExt.Text.Trim();
                        model.TaxId = Convert.ToInt32(taxTypelookUpEdit.EditValue);

                        model.PurUomId = Convert.ToInt32(purUnitlookUpEdit.EditValue);
                        model.UomId = Convert.ToInt32(unitLookUpEdit.EditValue);

                        model.ChkNegative = negativeCheckEdit.Checked;
                        model.SaleRateTaxInc = taxIncCheckEdit.Checked;

                        model.Rate = purRateKontoSpinEdit.Value;
                        model.Disc = purDiscKontoSpinEdit.Value;
                        model.SellingPrice = saleRateKontoSpinEdit.Value;
                        model.SaleDisc = saleDiscKontoSpinEdit.Value;
                        model.ProfitPer = profitKontoSpinEdit.Value;
                        model.Mrp = mrpKontoSpinEdit.Value;

                        model.BulkRate = bulkRateKontoSpinEdit.Value;
                        model.SemiBulkRate = semBulkRateKontoSpinEdit.Value;
                        model.BulkQty = bulkQtyKontoSpinEdit.Value;
                        model.Pcs = Convert.ToInt32( pcsKontoSpinEdit.Value);
                        model.AvgWt = wtKontoSpinEdit1.Value;

                        if (uniqueCheckEdit.Checked)
                            model.Qty = 1;
                        else
                            model.Qty = qtyKontoSpinEdit.Value;

                        ItemDetails.Add(model);
                    }
                }

                posPurTransDtoBindingSource.DataSource = ItemDetails;
                gridControl1.RefreshDataSource();

                styleNoTextEdit.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PurchaseProductView_Load(object sender, EventArgs e)
        {
            using(var db = new KontoContext())
            {
                var model = db.TaxMasters
                            .Where(p => !p.IsDeleted && p.IsActive)
                            .OrderBy(p => p.TaxName).ToList();

                taxTypelookUpEdit.Properties.DataSource = model;

                var uom = (from p in db.Uoms
                           where !p.IsDeleted && p.IsActive
                           orderby p.UnitName
                           select new BaseLookupDto
                           {
                               DisplayText = p.UnitName,
                               Id = p.Id
                           }).ToList();

                var szs = (from p in db.SizeModels
                           where !p.IsDeleted && p.IsActive
                           orderby p.SizeName
                           select new BaseLookupDto
                           {
                               DisplayText = p.SizeName,
                               Id = p.Id
                           }).ToList();

                purUnitlookUpEdit.Properties.DataSource = uom;
                unitLookUpEdit.Properties.DataSource = uom;

                sizeCheckedComboBoxEdit.Properties.DataSource = szs;
            }

            taxIncCheckEdit.Checked = true;
            negativeCheckEdit.Checked = true;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, gridView1);
            KontoUtils.RestoreMainFormLayout(this.MainLayoutFileName, layoutControl1);

            this.ActiveControl = barcodeTextBoxExt;
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextBoxExt.Text) || nameTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Product Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                nameTextBoxExt.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(descTextBoxExt.Text) || descTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Product Description", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                descTextBoxExt.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(hsnTextBoxExt.Text) || hsnTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid hsn Code", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                hsnTextBoxExt.Focus();
                return false;
            }

            else if (string.IsNullOrEmpty(taxTypelookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Tax Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                taxTypelookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(purUnitlookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Unit", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              
                purUnitlookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(unitLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Sale Unit", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              
                unitLookUpEdit.Focus();
                return false;
            }
            else if (purRateKontoSpinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Enter Purchase Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                purRateKontoSpinEdit.Focus();
                return false;
            }

            else if (saleRateKontoSpinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Enter Sale Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                saleRateKontoSpinEdit.Focus();
                return false;
            }
            else if(saleRateKontoSpinEdit.Value< purRateKontoSpinEdit.Value)
            {
                MessageBoxAdv.Show(this, "Sale Rate Can not be less than pruchase rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                saleRateKontoSpinEdit.Focus();
                return false;
            }

            else if (qtyKontoSpinEdit.Value == 0)
            {
                MessageBoxAdv.Show(this, "Enter Purchase Qty", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                qtyKontoSpinEdit.Focus();
                return false;
            }

            return true;
        }

        private void purUnitlookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            unitLookUpEdit.EditValue = purUnitlookUpEdit.EditValue;
        }
    }
}

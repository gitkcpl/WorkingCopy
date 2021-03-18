﻿using Konto.Core.Shared;
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

        
        public PurchaseProductView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            genItemSimpleButton.Click += GenItemSimpleButton_Click;

            profitSpinEdit.EditValueChanged += ProfitSpinEdit_EditValueChanged;

            saleRatespinEdit.EditValueChanged += SaleRatespinEdit_EditValueChanged;

            nameTextBoxExt.TextChanged += NameTextBoxExt_TextChanged;

            taxTypelookUpEdit.Properties.DisplayMember = "TaxName";
            taxTypelookUpEdit.Properties.ValueMember = "Id";

            purUnitlookUpEdit.Properties.DisplayMember = "DisplayText";
            purUnitlookUpEdit.Properties.ValueMember = "Id";

            unitLookUpEdit.Properties.DisplayMember = "DisplayText";
            unitLookUpEdit.Properties.ValueMember = "Id";

            sizeCheckedComboBoxEdit.Properties.DisplayMember = "DisplayText";
            sizeCheckedComboBoxEdit.Properties.ValueMember = "Id";

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
            if(saleRatespinEdit.Value  >0)
            {
                mrpSpinEdit.Value = saleRatespinEdit.Value;
                if (purRatespinEdit.Value == 0)
                {
                    purRatespinEdit.Value = Math.Round((saleRatespinEdit.Value * (100 / (100 + profitSpinEdit.Value))), 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        private void ProfitSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            var tx = (TaxModel) taxTypelookUpEdit.Properties.GetDataSourceRowByKeyValue(taxTypelookUpEdit.EditValue);

            if (mrpSpinEdit.Value > 0 && purRatespinEdit.Value == 0)
                saleRatespinEdit.Value = Math.Round(mrpSpinEdit.Value * (100 / (100 + profitSpinEdit.Value)), 2, MidpointRounding.AwayFromZero);
            else
            {
                if (tx != null)
                {
                    saleRatespinEdit.Value = Math.Round(purRatespinEdit.Value + (purRatespinEdit.Value * tx.Igst / 100) + (purRatespinEdit.Value * profitSpinEdit.Value) / 100, 2, MidpointRounding.AwayFromZero);
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

            
            int _Qty = Convert.ToInt32(qtySpinEdit.Value);
            int _TQty = 1;

            if (uniqueCheckEdit.Checked)
            {
                _TQty = _Qty;
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

                        model.CheckNegative = negativeCheckEdit.Checked;
                        model.SaleRateTaxInc = taxIncCheckEdit.Checked;

                        model.Rate = purRatespinEdit.Value;
                        model.Disc = purDiscspinEdit.Value;
                        model.SellingPrice = saleRatespinEdit.Value;
                        model.SaleDisc = saleDiscSpinEdit.Value;
                        model.ProfitPer = profitSpinEdit.Value;
                        model.Mrp = mrpSpinEdit.Value;
                        model.Qty = _Qty;

                        ItemDetails.Add(model);
                    }
                }

                posPurTransDtoBindingSource.DataSource = ItemDetails;
                gridControl1.RefreshDataSource();
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
                MessageBoxAdv.Show(this, "Invalid Tax Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              
                unitLookUpEdit.Focus();
                return false;
            }
            else if (purRatespinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Enter Purchase Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                purRatespinEdit.Focus();
                return false;
            }

            else if (saleRatespinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Enter Purchase Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                saleRatespinEdit.Focus();
                return false;
            }

            else if (qtySpinEdit.Value == 0)
            {
                MessageBoxAdv.Show(this, "Enter Purchase Qty", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                qtySpinEdit.Focus();
                return false;
            }

            return true;
        }
    }
}
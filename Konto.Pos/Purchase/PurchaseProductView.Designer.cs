
using Konto.Core.Shared.Libs;

namespace Konto.Pos.Purchase
{
    partial class PurchaseProductView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseProductView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.wtKontoSpinEdit1 = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.qtyKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.genItemSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.sizeCheckedComboBoxEdit = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.negativeCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.posPurTransDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colItemCode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colStyleNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDescription = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGroupId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBrand = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAvgWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCategory = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSellingPrice = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBulkQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBulkRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSubGroupId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGroupName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSize = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSubGroupName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSizeId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCategoryId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBrandId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPurUomId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPurDisc = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSaleDisc = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colMrp = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProfitPer = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSemiBulkRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCheckNegative = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSaleRateTaxInc = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTaxId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDesignName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGradeName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLotNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCut = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colUomId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTotal = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDisc = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDiscAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFreightRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFreight = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOtherAdd = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOtherLess = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSgstPer = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCgstPer = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIgstPer = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCessPer = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCess = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colNetTotal = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRemark = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOrderNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOrderDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBillId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBatchId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDesignId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGradeId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRefId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRefTransId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRefVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOrdId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOrdDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSaleRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colHsnCode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBarcode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.semBulkRateKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.bulkQtyKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.bulkRateKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.taxIncCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.colorLookup1 = new Konto.Shared.Masters.Color.ColorLookup();
            this.categoryLookup1 = new Konto.Shared.Masters.Category.CategoryLookup();
            this.brandLookup1 = new Konto.Shared.Masters.Brand.BrandLookup();
            this.subGroupLookup1 = new Konto.Shared.Masters.SubGroup.SubGroupLookup();
            this.groupLookup1 = new Konto.Shared.Masters.ProductGroup.GroupLookup();
            this.profitKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.uniqueCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.saleDiscKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.mrpKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.saleRateKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.purDiscKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.purRateKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.purUnitlookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.unitLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.taxTypelookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.hsnTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.descTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.nameTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.codeTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.barcodeTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.styleNoTextEdit = new Konto.Core.Shared.Libs.KontoTextEdit(this.components);
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.styleNoLayoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.semiBulkLayoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bulkRateLayoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.bulkQtyLayoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.pcsKontoSpinEdit = new Konto.Core.Shared.Libs.KontoSpinEdit(this.components);
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wtKontoSpinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtyKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeCheckedComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.negativeCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posPurTransDtoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.semBulkRateKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkQtyKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkRateKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxIncCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profitKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uniqueCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleDiscKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mrpKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleRateKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purDiscKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purRateKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purUnitlookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxTypelookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hsnTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcodeTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleNoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleNoLayoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.semiBulkLayoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkRateLayoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkQtyLayoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcsKontoSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cancelSimpleButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.okSimpleButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 436);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(897, 37);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(803, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 31);
            this.cancelSimpleButton.TabIndex = 6;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(711, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.pcsKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.wtKontoSpinEdit1);
            this.layoutControl1.Controls.Add(this.qtyKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.genItemSimpleButton);
            this.layoutControl1.Controls.Add(this.sizeCheckedComboBoxEdit);
            this.layoutControl1.Controls.Add(this.negativeCheckEdit);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.semBulkRateKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.bulkQtyKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.bulkRateKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.taxIncCheckEdit);
            this.layoutControl1.Controls.Add(this.colorLookup1);
            this.layoutControl1.Controls.Add(this.categoryLookup1);
            this.layoutControl1.Controls.Add(this.brandLookup1);
            this.layoutControl1.Controls.Add(this.subGroupLookup1);
            this.layoutControl1.Controls.Add(this.groupLookup1);
            this.layoutControl1.Controls.Add(this.profitKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.uniqueCheckEdit);
            this.layoutControl1.Controls.Add(this.saleDiscKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.mrpKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.saleRateKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.purDiscKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.purRateKontoSpinEdit);
            this.layoutControl1.Controls.Add(this.purUnitlookUpEdit);
            this.layoutControl1.Controls.Add(this.unitLookUpEdit);
            this.layoutControl1.Controls.Add(this.taxTypelookUpEdit);
            this.layoutControl1.Controls.Add(this.hsnTextBoxExt);
            this.layoutControl1.Controls.Add(this.descTextBoxExt);
            this.layoutControl1.Controls.Add(this.nameTextBoxExt);
            this.layoutControl1.Controls.Add(this.codeTextBoxExt);
            this.layoutControl1.Controls.Add(this.barcodeTextBoxExt);
            this.layoutControl1.Controls.Add(this.styleNoTextEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(716, 168, 650, 400);
            this.layoutControl1.OptionsFocus.AllowFocusControlOnActivatedTabPage = true;
            this.layoutControl1.OptionsFocus.AllowFocusTabbedGroups = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(897, 436);
            this.layoutControl1.TabIndex = 9;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // wtKontoSpinEdit1
            // 
            this.wtKontoSpinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.wtKontoSpinEdit1.EnterMoveNextControl = true;
            this.wtKontoSpinEdit1.Location = new System.Drawing.Point(786, 169);
            this.wtKontoSpinEdit1.Name = "wtKontoSpinEdit1";
            this.wtKontoSpinEdit1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wtKontoSpinEdit1.Properties.Appearance.Options.UseFont = true;
            this.wtKontoSpinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wtKontoSpinEdit1.Size = new System.Drawing.Size(106, 24);
            this.wtKontoSpinEdit1.StyleController = this.layoutControl1;
            this.wtKontoSpinEdit1.TabIndex = 29;
            // 
            // qtyKontoSpinEdit
            // 
            this.qtyKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.qtyKontoSpinEdit.EnterMoveNextControl = true;
            this.qtyKontoSpinEdit.Location = new System.Drawing.Point(586, 169);
            this.qtyKontoSpinEdit.Name = "qtyKontoSpinEdit";
            this.qtyKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtyKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.qtyKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.qtyKontoSpinEdit.Size = new System.Drawing.Size(96, 24);
            this.qtyKontoSpinEdit.StyleController = this.layoutControl1;
            this.qtyKontoSpinEdit.TabIndex = 21;
            // 
            // genItemSimpleButton
            // 
            this.genItemSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genItemSimpleButton.Appearance.Options.UseFont = true;
            this.genItemSimpleButton.Location = new System.Drawing.Point(760, 225);
            this.genItemSimpleButton.Name = "genItemSimpleButton";
            this.genItemSimpleButton.Size = new System.Drawing.Size(132, 22);
            this.genItemSimpleButton.StyleController = this.layoutControl1;
            this.genItemSimpleButton.TabIndex = 28;
            this.genItemSimpleButton.Text = "Generate";
            // 
            // sizeCheckedComboBoxEdit
            // 
            this.sizeCheckedComboBoxEdit.EnterMoveNextControl = true;
            this.sizeCheckedComboBoxEdit.Location = new System.Drawing.Point(379, 85);
            this.sizeCheckedComboBoxEdit.Name = "sizeCheckedComboBoxEdit";
            this.sizeCheckedComboBoxEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sizeCheckedComboBoxEdit.Properties.Appearance.Options.UseFont = true;
            this.sizeCheckedComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sizeCheckedComboBoxEdit.Size = new System.Drawing.Size(184, 24);
            this.sizeCheckedComboBoxEdit.StyleController = this.layoutControl1;
            this.sizeCheckedComboBoxEdit.TabIndex = 9;
            // 
            // negativeCheckEdit
            // 
            this.negativeCheckEdit.EnterMoveNextControl = true;
            this.negativeCheckEdit.Location = new System.Drawing.Point(294, 225);
            this.negativeCheckEdit.Name = "negativeCheckEdit";
            this.negativeCheckEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.negativeCheckEdit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.negativeCheckEdit.Properties.Appearance.Options.UseFont = true;
            this.negativeCheckEdit.Properties.Appearance.Options.UseForeColor = true;
            this.negativeCheckEdit.Properties.Caption = "Check Negative";
            this.negativeCheckEdit.Size = new System.Drawing.Size(241, 21);
            this.negativeCheckEdit.StyleController = this.layoutControl1;
            this.negativeCheckEdit.TabIndex = 26;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.posPurTransDtoBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(5, 251);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(887, 180);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // posPurTransDtoBindingSource
            // 
            this.posPurTransDtoBindingSource.DataSource = typeof(Konto.Data.Models.Pos.Dtos.PosPurTransDto);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemCode,
            this.colStyleNo,
            this.colDescription,
            this.colGroupId,
            this.colBrand,
            this.colAvgWt,
            this.colCategory,
            this.colSellingPrice,
            this.colBulkQty,
            this.colBulkRate,
            this.colSubGroupId,
            this.colGroupName,
            this.colSize,
            this.colSubGroupName,
            this.colSizeId,
            this.colCategoryId,
            this.colBrandId,
            this.colPurUomId,
            this.colPurDisc,
            this.colSaleDisc,
            this.colMrp,
            this.colProfitPer,
            this.colSemiBulkRate,
            this.colCheckNegative,
            this.colSaleRateTaxInc,
            this.colTaxId,
            this.colProductName,
            this.colColorName,
            this.colDesignName,
            this.colGradeName,
            this.colLotNo,
            this.colQty,
            this.colCut,
            this.colPcs,
            this.colUomId,
            this.colRate,
            this.colTotal,
            this.colDisc,
            this.colDiscAmt,
            this.colFreightRate,
            this.colFreight,
            this.colOtherAdd,
            this.colOtherLess,
            this.colSgstPer,
            this.colSgst,
            this.colCgstPer,
            this.colCgst,
            this.colIgstPer,
            this.colIgst,
            this.colCessPer,
            this.colCess,
            this.colNetTotal,
            this.colRemark,
            this.colChallanNo,
            this.colChallanDate,
            this.colOrderNo,
            this.colOrderDate,
            this.colId,
            this.colBillId,
            this.colBatchId,
            this.colProductId,
            this.colColorId,
            this.colDesignId,
            this.colGradeId,
            this.colRefId,
            this.colRefTransId,
            this.colRefVoucherId,
            this.colOrdId,
            this.colOrdDate,
            this.colChDate,
            this.colSaleRate,
            this.colHsnCode,
            this.colBarcode});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.gridView1.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.gridView1.OptionsLayout.Columns.StoreAllOptions = true;
            this.gridView1.OptionsLayout.Columns.StoreAppearance = true;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAppearance = true;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colItemCode
            // 
            this.colItemCode.FieldName = "ItemCode";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.Visible = true;
            this.colItemCode.VisibleIndex = 1;
            // 
            // colStyleNo
            // 
            this.colStyleNo.FieldName = "StyleNo";
            this.colStyleNo.Name = "colStyleNo";
            this.colStyleNo.Visible = true;
            this.colStyleNo.VisibleIndex = 4;
            // 
            // colDescription
            // 
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Width = 119;
            // 
            // colGroupId
            // 
            this.colGroupId.FieldName = "GroupId";
            this.colGroupId.Name = "colGroupId";
            // 
            // colBrand
            // 
            this.colBrand.FieldName = "Brand";
            this.colBrand.Name = "colBrand";
            this.colBrand.Visible = true;
            this.colBrand.VisibleIndex = 14;
            // 
            // colAvgWt
            // 
            this.colAvgWt.Caption = "Weight";
            this.colAvgWt.FieldName = "AvgWt";
            this.colAvgWt.Name = "colAvgWt";
            this.colAvgWt.Visible = true;
            this.colAvgWt.VisibleIndex = 5;
            // 
            // colCategory
            // 
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.Visible = true;
            this.colCategory.VisibleIndex = 20;
            // 
            // colSellingPrice
            // 
            this.colSellingPrice.FieldName = "SellingPrice";
            this.colSellingPrice.Name = "colSellingPrice";
            this.colSellingPrice.Visible = true;
            this.colSellingPrice.VisibleIndex = 10;
            this.colSellingPrice.Width = 89;
            // 
            // colBulkQty
            // 
            this.colBulkQty.FieldName = "BulkQty";
            this.colBulkQty.Name = "colBulkQty";
            // 
            // colBulkRate
            // 
            this.colBulkRate.FieldName = "BulkRate";
            this.colBulkRate.Name = "colBulkRate";
            this.colBulkRate.Visible = true;
            this.colBulkRate.VisibleIndex = 15;
            // 
            // colSubGroupId
            // 
            this.colSubGroupId.FieldName = "SubGroupId";
            this.colSubGroupId.Name = "colSubGroupId";
            // 
            // colGroupName
            // 
            this.colGroupName.FieldName = "GroupName";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 18;
            this.colGroupName.Width = 97;
            // 
            // colSize
            // 
            this.colSize.FieldName = "Size";
            this.colSize.Name = "colSize";
            this.colSize.Visible = true;
            this.colSize.VisibleIndex = 16;
            // 
            // colSubGroupName
            // 
            this.colSubGroupName.FieldName = "SubGroupName";
            this.colSubGroupName.Name = "colSubGroupName";
            this.colSubGroupName.Visible = true;
            this.colSubGroupName.VisibleIndex = 17;
            this.colSubGroupName.Width = 118;
            // 
            // colSizeId
            // 
            this.colSizeId.FieldName = "SizeId";
            this.colSizeId.Name = "colSizeId";
            // 
            // colCategoryId
            // 
            this.colCategoryId.FieldName = "CategoryId";
            this.colCategoryId.Name = "colCategoryId";
            this.colCategoryId.Width = 109;
            // 
            // colBrandId
            // 
            this.colBrandId.FieldName = "BrandId";
            this.colBrandId.Name = "colBrandId";
            // 
            // colPurUomId
            // 
            this.colPurUomId.FieldName = "PurUomId";
            this.colPurUomId.Name = "colPurUomId";
            this.colPurUomId.Width = 106;
            // 
            // colPurDisc
            // 
            this.colPurDisc.FieldName = "PurDisc";
            this.colPurDisc.Name = "colPurDisc";
            this.colPurDisc.Visible = true;
            this.colPurDisc.VisibleIndex = 8;
            // 
            // colSaleDisc
            // 
            this.colSaleDisc.FieldName = "SaleDisc";
            this.colSaleDisc.Name = "colSaleDisc";
            this.colSaleDisc.Visible = true;
            this.colSaleDisc.VisibleIndex = 9;
            // 
            // colMrp
            // 
            this.colMrp.FieldName = "Mrp";
            this.colMrp.Name = "colMrp";
            this.colMrp.Visible = true;
            this.colMrp.VisibleIndex = 12;
            // 
            // colProfitPer
            // 
            this.colProfitPer.FieldName = "ProfitPer";
            this.colProfitPer.Name = "colProfitPer";
            this.colProfitPer.Visible = true;
            this.colProfitPer.VisibleIndex = 13;
            // 
            // colSemiBulkRate
            // 
            this.colSemiBulkRate.FieldName = "SemiBulkRate";
            this.colSemiBulkRate.Name = "colSemiBulkRate";
            this.colSemiBulkRate.Visible = true;
            this.colSemiBulkRate.VisibleIndex = 21;
            this.colSemiBulkRate.Width = 120;
            // 
            // colCheckNegative
            // 
            this.colCheckNegative.FieldName = "CheckNegative";
            this.colCheckNegative.Name = "colCheckNegative";
            this.colCheckNegative.Width = 90;
            // 
            // colSaleRateTaxInc
            // 
            this.colSaleRateTaxInc.FieldName = "SaleRateTaxInc";
            this.colSaleRateTaxInc.Name = "colSaleRateTaxInc";
            // 
            // colTaxId
            // 
            this.colTaxId.FieldName = "TaxId";
            this.colTaxId.Name = "colTaxId";
            // 
            // colProductName
            // 
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 2;
            this.colProductName.Width = 160;
            // 
            // colColorName
            // 
            this.colColorName.FieldName = "ColorName";
            this.colColorName.Name = "colColorName";
            this.colColorName.Visible = true;
            this.colColorName.VisibleIndex = 19;
            // 
            // colDesignName
            // 
            this.colDesignName.FieldName = "DesignName";
            this.colDesignName.Name = "colDesignName";
            // 
            // colGradeName
            // 
            this.colGradeName.FieldName = "GradeName";
            this.colGradeName.Name = "colGradeName";
            // 
            // colLotNo
            // 
            this.colLotNo.FieldName = "LotNo";
            this.colLotNo.Name = "colLotNo";
            // 
            // colQty
            // 
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 6;
            this.colQty.Width = 84;
            // 
            // colCut
            // 
            this.colCut.FieldName = "Cut";
            this.colCut.Name = "colCut";
            // 
            // colPcs
            // 
            this.colPcs.FieldName = "Pcs";
            this.colPcs.Name = "colPcs";
            // 
            // colUomId
            // 
            this.colUomId.FieldName = "UomId";
            this.colUomId.Name = "colUomId";
            this.colUomId.Visible = true;
            this.colUomId.VisibleIndex = 11;
            // 
            // colRate
            // 
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 7;
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 22;
            // 
            // colDisc
            // 
            this.colDisc.FieldName = "Disc";
            this.colDisc.Name = "colDisc";
            this.colDisc.Visible = true;
            this.colDisc.VisibleIndex = 23;
            // 
            // colDiscAmt
            // 
            this.colDiscAmt.FieldName = "DiscAmt";
            this.colDiscAmt.Name = "colDiscAmt";
            this.colDiscAmt.Visible = true;
            this.colDiscAmt.VisibleIndex = 24;
            // 
            // colFreightRate
            // 
            this.colFreightRate.FieldName = "FreightRate";
            this.colFreightRate.Name = "colFreightRate";
            this.colFreightRate.Visible = true;
            this.colFreightRate.VisibleIndex = 25;
            this.colFreightRate.Width = 100;
            // 
            // colFreight
            // 
            this.colFreight.FieldName = "Freight";
            this.colFreight.Name = "colFreight";
            this.colFreight.Visible = true;
            this.colFreight.VisibleIndex = 26;
            // 
            // colOtherAdd
            // 
            this.colOtherAdd.FieldName = "OtherAdd";
            this.colOtherAdd.Name = "colOtherAdd";
            this.colOtherAdd.Visible = true;
            this.colOtherAdd.VisibleIndex = 27;
            // 
            // colOtherLess
            // 
            this.colOtherLess.FieldName = "OtherLess";
            this.colOtherLess.Name = "colOtherLess";
            this.colOtherLess.Visible = true;
            this.colOtherLess.VisibleIndex = 28;
            // 
            // colSgstPer
            // 
            this.colSgstPer.FieldName = "SgstPer";
            this.colSgstPer.Name = "colSgstPer";
            this.colSgstPer.Visible = true;
            this.colSgstPer.VisibleIndex = 29;
            // 
            // colSgst
            // 
            this.colSgst.FieldName = "Sgst";
            this.colSgst.Name = "colSgst";
            this.colSgst.Visible = true;
            this.colSgst.VisibleIndex = 30;
            // 
            // colCgstPer
            // 
            this.colCgstPer.FieldName = "CgstPer";
            this.colCgstPer.Name = "colCgstPer";
            this.colCgstPer.Visible = true;
            this.colCgstPer.VisibleIndex = 31;
            // 
            // colCgst
            // 
            this.colCgst.FieldName = "Cgst";
            this.colCgst.Name = "colCgst";
            this.colCgst.Visible = true;
            this.colCgst.VisibleIndex = 32;
            // 
            // colIgstPer
            // 
            this.colIgstPer.FieldName = "IgstPer";
            this.colIgstPer.Name = "colIgstPer";
            this.colIgstPer.Visible = true;
            this.colIgstPer.VisibleIndex = 33;
            this.colIgstPer.Width = 68;
            // 
            // colIgst
            // 
            this.colIgst.FieldName = "Igst";
            this.colIgst.Name = "colIgst";
            this.colIgst.Visible = true;
            this.colIgst.VisibleIndex = 34;
            // 
            // colCessPer
            // 
            this.colCessPer.FieldName = "CessPer";
            this.colCessPer.Name = "colCessPer";
            // 
            // colCess
            // 
            this.colCess.FieldName = "Cess";
            this.colCess.Name = "colCess";
            // 
            // colNetTotal
            // 
            this.colNetTotal.FieldName = "NetTotal";
            this.colNetTotal.Name = "colNetTotal";
            this.colNetTotal.Visible = true;
            this.colNetTotal.VisibleIndex = 35;
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 36;
            // 
            // colChallanNo
            // 
            this.colChallanNo.FieldName = "ChallanNo";
            this.colChallanNo.Name = "colChallanNo";
            // 
            // colChallanDate
            // 
            this.colChallanDate.FieldName = "ChallanDate";
            this.colChallanDate.Name = "colChallanDate";
            // 
            // colOrderNo
            // 
            this.colOrderNo.FieldName = "OrderNo";
            this.colOrderNo.Name = "colOrderNo";
            // 
            // colOrderDate
            // 
            this.colOrderDate.FieldName = "OrderDate";
            this.colOrderDate.Name = "colOrderDate";
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colBillId
            // 
            this.colBillId.FieldName = "BillId";
            this.colBillId.Name = "colBillId";
            // 
            // colBatchId
            // 
            this.colBatchId.FieldName = "BatchId";
            this.colBatchId.Name = "colBatchId";
            // 
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            // 
            // colColorId
            // 
            this.colColorId.FieldName = "ColorId";
            this.colColorId.Name = "colColorId";
            // 
            // colDesignId
            // 
            this.colDesignId.FieldName = "DesignId";
            this.colDesignId.Name = "colDesignId";
            // 
            // colGradeId
            // 
            this.colGradeId.FieldName = "GradeId";
            this.colGradeId.Name = "colGradeId";
            // 
            // colRefId
            // 
            this.colRefId.FieldName = "RefId";
            this.colRefId.Name = "colRefId";
            // 
            // colRefTransId
            // 
            this.colRefTransId.FieldName = "RefTransId";
            this.colRefTransId.Name = "colRefTransId";
            // 
            // colRefVoucherId
            // 
            this.colRefVoucherId.FieldName = "RefVoucherId";
            this.colRefVoucherId.Name = "colRefVoucherId";
            // 
            // colOrdId
            // 
            this.colOrdId.FieldName = "OrdId";
            this.colOrdId.Name = "colOrdId";
            // 
            // colOrdDate
            // 
            this.colOrdDate.FieldName = "OrdDate";
            this.colOrdDate.Name = "colOrdDate";
            // 
            // colChDate
            // 
            this.colChDate.FieldName = "ChDate";
            this.colChDate.Name = "colChDate";
            // 
            // colSaleRate
            // 
            this.colSaleRate.FieldName = "SaleRate";
            this.colSaleRate.Name = "colSaleRate";
            // 
            // colHsnCode
            // 
            this.colHsnCode.FieldName = "HsnCode";
            this.colHsnCode.Name = "colHsnCode";
            this.colHsnCode.Visible = true;
            this.colHsnCode.VisibleIndex = 3;
            // 
            // colBarcode
            // 
            this.colBarcode.FieldName = "Barcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.Visible = true;
            this.colBarcode.VisibleIndex = 0;
            this.colBarcode.Width = 90;
            // 
            // semBulkRateKontoSpinEdit
            // 
            this.semBulkRateKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.semBulkRateKontoSpinEdit.EnterMoveNextControl = true;
            this.semBulkRateKontoSpinEdit.Location = new System.Drawing.Point(586, 197);
            this.semBulkRateKontoSpinEdit.Name = "semBulkRateKontoSpinEdit";
            this.semBulkRateKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.semBulkRateKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.semBulkRateKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.semBulkRateKontoSpinEdit.Size = new System.Drawing.Size(101, 24);
            this.semBulkRateKontoSpinEdit.StyleController = this.layoutControl1;
            this.semBulkRateKontoSpinEdit.TabIndex = 24;
            // 
            // bulkQtyKontoSpinEdit
            // 
            this.bulkQtyKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.bulkQtyKontoSpinEdit.EnterMoveNextControl = true;
            this.bulkQtyKontoSpinEdit.Location = new System.Drawing.Point(334, 197);
            this.bulkQtyKontoSpinEdit.Name = "bulkQtyKontoSpinEdit";
            this.bulkQtyKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bulkQtyKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.bulkQtyKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.bulkQtyKontoSpinEdit.Size = new System.Drawing.Size(148, 24);
            this.bulkQtyKontoSpinEdit.StyleController = this.layoutControl1;
            this.bulkQtyKontoSpinEdit.TabIndex = 23;
            // 
            // bulkRateKontoSpinEdit
            // 
            this.bulkRateKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.bulkRateKontoSpinEdit.EnterMoveNextControl = true;
            this.bulkRateKontoSpinEdit.Location = new System.Drawing.Point(105, 197);
            this.bulkRateKontoSpinEdit.Name = "bulkRateKontoSpinEdit";
            this.bulkRateKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bulkRateKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.bulkRateKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.bulkRateKontoSpinEdit.Size = new System.Drawing.Size(125, 24);
            this.bulkRateKontoSpinEdit.StyleController = this.layoutControl1;
            this.bulkRateKontoSpinEdit.TabIndex = 22;
            // 
            // taxIncCheckEdit
            // 
            this.taxIncCheckEdit.EnterMoveNextControl = true;
            this.taxIncCheckEdit.Location = new System.Drawing.Point(5, 225);
            this.taxIncCheckEdit.Name = "taxIncCheckEdit";
            this.taxIncCheckEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taxIncCheckEdit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.taxIncCheckEdit.Properties.Appearance.Options.UseFont = true;
            this.taxIncCheckEdit.Properties.Appearance.Options.UseForeColor = true;
            this.taxIncCheckEdit.Properties.Caption = "Sale Rate Tax Inclusive";
            this.taxIncCheckEdit.Size = new System.Drawing.Size(285, 21);
            this.taxIncCheckEdit.StyleController = this.layoutControl1;
            this.taxIncCheckEdit.TabIndex = 25;
            // 
            // colorLookup1
            // 
            this.colorLookup1.Location = new System.Drawing.Point(667, 85);
            this.colorLookup1.LookupTitle = null;
            this.colorLookup1.Name = "colorLookup1";
            this.colorLookup1.PrimaryKey = null;
            this.colorLookup1.RequiredField = true;
            this.colorLookup1.SelectedText = null;
            this.colorLookup1.SelectedValue = null;
            this.colorLookup1.Size = new System.Drawing.Size(225, 24);
            this.colorLookup1.TabIndex = 10;
            // 
            // categoryLookup1
            // 
            this.categoryLookup1.Location = new System.Drawing.Point(105, 85);
            this.categoryLookup1.LookupTitle = null;
            this.categoryLookup1.Name = "categoryLookup1";
            this.categoryLookup1.PrimaryKey = null;
            this.categoryLookup1.RequiredField = true;
            this.categoryLookup1.SelectedText = null;
            this.categoryLookup1.SelectedValue = null;
            this.categoryLookup1.Size = new System.Drawing.Size(170, 24);
            this.categoryLookup1.TabIndex = 8;
            // 
            // brandLookup1
            // 
            this.brandLookup1.Location = new System.Drawing.Point(667, 61);
            this.brandLookup1.LookupTitle = null;
            this.brandLookup1.Name = "brandLookup1";
            this.brandLookup1.PrimaryKey = null;
            this.brandLookup1.RequiredField = true;
            this.brandLookup1.SelectedText = null;
            this.brandLookup1.SelectedValue = null;
            this.brandLookup1.Size = new System.Drawing.Size(225, 20);
            this.brandLookup1.TabIndex = 7;
            // 
            // subGroupLookup1
            // 
            this.subGroupLookup1.Location = new System.Drawing.Point(379, 61);
            this.subGroupLookup1.LookupTitle = null;
            this.subGroupLookup1.Name = "subGroupLookup1";
            this.subGroupLookup1.PrimaryKey = null;
            this.subGroupLookup1.RequiredField = true;
            this.subGroupLookup1.SelectedText = null;
            this.subGroupLookup1.SelectedValue = null;
            this.subGroupLookup1.Size = new System.Drawing.Size(184, 20);
            this.subGroupLookup1.TabIndex = 6;
            // 
            // groupLookup1
            // 
            this.groupLookup1.Location = new System.Drawing.Point(105, 61);
            this.groupLookup1.LookupTitle = null;
            this.groupLookup1.Name = "groupLookup1";
            this.groupLookup1.PrimaryKey = null;
            this.groupLookup1.RequiredField = true;
            this.groupLookup1.SelectedText = null;
            this.groupLookup1.SelectedValue = null;
            this.groupLookup1.Size = new System.Drawing.Size(170, 20);
            this.groupLookup1.TabIndex = 5;
            // 
            // profitKontoSpinEdit
            // 
            this.profitKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.profitKontoSpinEdit.EnterMoveNextControl = true;
            this.profitKontoSpinEdit.Location = new System.Drawing.Point(786, 141);
            this.profitKontoSpinEdit.Name = "profitKontoSpinEdit";
            this.profitKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profitKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.profitKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.profitKontoSpinEdit.Size = new System.Drawing.Size(106, 24);
            this.profitKontoSpinEdit.StyleController = this.layoutControl1;
            this.profitKontoSpinEdit.TabIndex = 18;
            // 
            // uniqueCheckEdit
            // 
            this.uniqueCheckEdit.EnterMoveNextControl = true;
            this.uniqueCheckEdit.Location = new System.Drawing.Point(539, 225);
            this.uniqueCheckEdit.Name = "uniqueCheckEdit";
            this.uniqueCheckEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uniqueCheckEdit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.uniqueCheckEdit.Properties.Appearance.Options.UseFont = true;
            this.uniqueCheckEdit.Properties.Appearance.Options.UseForeColor = true;
            this.uniqueCheckEdit.Properties.Caption = "Unique Barcode ?";
            this.uniqueCheckEdit.Size = new System.Drawing.Size(217, 21);
            this.uniqueCheckEdit.StyleController = this.layoutControl1;
            this.uniqueCheckEdit.TabIndex = 27;
            // 
            // saleDiscKontoSpinEdit
            // 
            this.saleDiscKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.saleDiscKontoSpinEdit.EnterMoveNextControl = true;
            this.saleDiscKontoSpinEdit.Location = new System.Drawing.Point(586, 141);
            this.saleDiscKontoSpinEdit.Name = "saleDiscKontoSpinEdit";
            this.saleDiscKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saleDiscKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.saleDiscKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.saleDiscKontoSpinEdit.Size = new System.Drawing.Size(96, 24);
            this.saleDiscKontoSpinEdit.StyleController = this.layoutControl1;
            this.saleDiscKontoSpinEdit.TabIndex = 17;
            // 
            // mrpKontoSpinEdit
            // 
            this.mrpKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mrpKontoSpinEdit.EnterMoveNextControl = true;
            this.mrpKontoSpinEdit.Location = new System.Drawing.Point(334, 169);
            this.mrpKontoSpinEdit.Name = "mrpKontoSpinEdit";
            this.mrpKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrpKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.mrpKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mrpKontoSpinEdit.Size = new System.Drawing.Size(148, 24);
            this.mrpKontoSpinEdit.StyleController = this.layoutControl1;
            this.mrpKontoSpinEdit.TabIndex = 20;
            // 
            // saleRateKontoSpinEdit
            // 
            this.saleRateKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.saleRateKontoSpinEdit.EnterMoveNextControl = true;
            this.saleRateKontoSpinEdit.Location = new System.Drawing.Point(105, 169);
            this.saleRateKontoSpinEdit.Name = "saleRateKontoSpinEdit";
            this.saleRateKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saleRateKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.saleRateKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.saleRateKontoSpinEdit.Size = new System.Drawing.Size(125, 24);
            this.saleRateKontoSpinEdit.StyleController = this.layoutControl1;
            this.saleRateKontoSpinEdit.TabIndex = 19;
            // 
            // purDiscKontoSpinEdit
            // 
            this.purDiscKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.purDiscKontoSpinEdit.EnterMoveNextControl = true;
            this.purDiscKontoSpinEdit.Location = new System.Drawing.Point(334, 141);
            this.purDiscKontoSpinEdit.Name = "purDiscKontoSpinEdit";
            this.purDiscKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purDiscKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.purDiscKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.purDiscKontoSpinEdit.Size = new System.Drawing.Size(148, 24);
            this.purDiscKontoSpinEdit.StyleController = this.layoutControl1;
            this.purDiscKontoSpinEdit.TabIndex = 16;
            // 
            // purRateKontoSpinEdit
            // 
            this.purRateKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.purRateKontoSpinEdit.EnterMoveNextControl = true;
            this.purRateKontoSpinEdit.Location = new System.Drawing.Point(105, 141);
            this.purRateKontoSpinEdit.Name = "purRateKontoSpinEdit";
            this.purRateKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purRateKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.purRateKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.purRateKontoSpinEdit.Size = new System.Drawing.Size(125, 24);
            this.purRateKontoSpinEdit.StyleController = this.layoutControl1;
            this.purRateKontoSpinEdit.TabIndex = 15;
            // 
            // purUnitlookUpEdit
            // 
            this.purUnitlookUpEdit.EnterMoveNextControl = true;
            this.purUnitlookUpEdit.Location = new System.Drawing.Point(631, 113);
            this.purUnitlookUpEdit.Name = "purUnitlookUpEdit";
            this.purUnitlookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.purUnitlookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purUnitlookUpEdit.Properties.Appearance.Options.UseFont = true;
            this.purUnitlookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.purUnitlookUpEdit.Properties.ImmediatePopup = true;
            this.purUnitlookUpEdit.Properties.NullText = "";
            this.purUnitlookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.purUnitlookUpEdit.Size = new System.Drawing.Size(91, 24);
            this.purUnitlookUpEdit.StyleController = this.layoutControl1;
            this.purUnitlookUpEdit.TabIndex = 13;
            // 
            // unitLookUpEdit
            // 
            this.unitLookUpEdit.EnterMoveNextControl = true;
            this.unitLookUpEdit.Location = new System.Drawing.Point(793, 113);
            this.unitLookUpEdit.Name = "unitLookUpEdit";
            this.unitLookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.unitLookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unitLookUpEdit.Properties.Appearance.Options.UseFont = true;
            this.unitLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.unitLookUpEdit.Properties.ImmediatePopup = true;
            this.unitLookUpEdit.Properties.NullText = "";
            this.unitLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.unitLookUpEdit.Size = new System.Drawing.Size(99, 24);
            this.unitLookUpEdit.StyleController = this.layoutControl1;
            this.unitLookUpEdit.TabIndex = 14;
            // 
            // taxTypelookUpEdit
            // 
            this.taxTypelookUpEdit.EnterMoveNextControl = true;
            this.taxTypelookUpEdit.Location = new System.Drawing.Point(379, 113);
            this.taxTypelookUpEdit.Name = "taxTypelookUpEdit";
            this.taxTypelookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.taxTypelookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taxTypelookUpEdit.Properties.Appearance.Options.UseFont = true;
            this.taxTypelookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.taxTypelookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TaxName", "")});
            this.taxTypelookUpEdit.Properties.ImmediatePopup = true;
            this.taxTypelookUpEdit.Properties.NullText = "";
            this.taxTypelookUpEdit.Properties.ShowFooter = false;
            this.taxTypelookUpEdit.Properties.ShowHeader = false;
            this.taxTypelookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.taxTypelookUpEdit.Size = new System.Drawing.Size(184, 24);
            this.taxTypelookUpEdit.StyleController = this.layoutControl1;
            this.taxTypelookUpEdit.TabIndex = 12;
            // 
            // hsnTextBoxExt
            // 
            this.hsnTextBoxExt.BeforeTouchSize = new System.Drawing.Size(358, 24);
            this.hsnTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.hsnTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hsnTextBoxExt.EnterMoveNextControl = true;
            this.hsnTextBoxExt.Location = new System.Drawing.Point(105, 113);
            this.hsnTextBoxExt.MaxLength = 15;
            this.hsnTextBoxExt.Name = "hsnTextBoxExt";
            this.hsnTextBoxExt.Size = new System.Drawing.Size(170, 24);
            this.hsnTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.hsnTextBoxExt.TabIndex = 11;
            this.hsnTextBoxExt.ThemeName = "Metro";
            this.hsnTextBoxExt.UseBorderColorOnFocus = true;
            // 
            // descTextBoxExt
            // 
            this.descTextBoxExt.BeforeTouchSize = new System.Drawing.Size(358, 24);
            this.descTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.descTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.descTextBoxExt.EnterMoveNextControl = true;
            this.descTextBoxExt.Location = new System.Drawing.Point(534, 33);
            this.descTextBoxExt.MaxLength = 499;
            this.descTextBoxExt.Name = "descTextBoxExt";
            this.descTextBoxExt.Size = new System.Drawing.Size(358, 24);
            this.descTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.descTextBoxExt.TabIndex = 4;
            this.descTextBoxExt.ThemeName = "Metro";
            this.descTextBoxExt.UseBorderColorOnFocus = true;
            // 
            // nameTextBoxExt
            // 
            this.nameTextBoxExt.BeforeTouchSize = new System.Drawing.Size(358, 24);
            this.nameTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.nameTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.nameTextBoxExt.EnterMoveNextControl = true;
            this.nameTextBoxExt.Location = new System.Drawing.Point(105, 33);
            this.nameTextBoxExt.MaxLength = 100;
            this.nameTextBoxExt.Name = "nameTextBoxExt";
            this.nameTextBoxExt.Size = new System.Drawing.Size(325, 24);
            this.nameTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.nameTextBoxExt.TabIndex = 3;
            this.nameTextBoxExt.ThemeName = "Metro";
            this.nameTextBoxExt.UseBorderColorOnFocus = true;
            // 
            // codeTextBoxExt
            // 
            this.codeTextBoxExt.BeforeTouchSize = new System.Drawing.Size(358, 24);
            this.codeTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.codeTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeTextBoxExt.EnterMoveNextControl = true;
            this.codeTextBoxExt.Location = new System.Drawing.Point(385, 5);
            this.codeTextBoxExt.MaxLength = 25;
            this.codeTextBoxExt.Name = "codeTextBoxExt";
            this.codeTextBoxExt.Size = new System.Drawing.Size(156, 24);
            this.codeTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.codeTextBoxExt.TabIndex = 1;
            this.codeTextBoxExt.ThemeName = "Metro";
            this.codeTextBoxExt.UseBorderColorOnFocus = true;
            // 
            // barcodeTextBoxExt
            // 
            this.barcodeTextBoxExt.BeforeTouchSize = new System.Drawing.Size(358, 24);
            this.barcodeTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.barcodeTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barcodeTextBoxExt.EnterMoveNextControl = true;
            this.barcodeTextBoxExt.Location = new System.Drawing.Point(105, 5);
            this.barcodeTextBoxExt.MaxLength = 25;
            this.barcodeTextBoxExt.Name = "barcodeTextBoxExt";
            this.barcodeTextBoxExt.Size = new System.Drawing.Size(185, 24);
            this.barcodeTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.barcodeTextBoxExt.TabIndex = 0;
            this.barcodeTextBoxExt.ThemeName = "Metro";
            this.barcodeTextBoxExt.UseBorderColorOnFocus = true;
            // 
            // styleNoTextEdit
            // 
            this.styleNoTextEdit.EditValue = "";
            this.styleNoTextEdit.EnterMoveNextControl = true;
            this.styleNoTextEdit.Location = new System.Drawing.Point(645, 5);
            this.styleNoTextEdit.Name = "styleNoTextEdit";
            this.styleNoTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleNoTextEdit.Properties.Appearance.Options.UseFont = true;
            this.styleNoTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.styleNoTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.styleNoTextEdit.Properties.MaxLength = 25;
            this.styleNoTextEdit.Size = new System.Drawing.Size(247, 24);
            this.styleNoTextEdit.StyleController = this.layoutControl1;
            this.styleNoTextEdit.TabIndex = 2;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.styleNoLayoutControlItem,
            this.layoutControlItem5,
            this.layoutControlItem25,
            this.layoutControlItem31,
            this.layoutControlItem32,
            this.layoutControlItem33,
            this.layoutControlItem35,
            this.layoutControlItem11,
            this.layoutControlItem14,
            this.semiBulkLayoutControlItem,
            this.layoutControlItem15,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem21,
            this.bulkRateLayoutControlItem,
            this.bulkQtyLayoutControlItem,
            this.layoutControlItem16,
            this.layoutControlItem19,
            this.layoutControlItem38,
            this.layoutControlItem10,
            this.layoutControlItem17,
            this.layoutControlItem3,
            this.layoutControlItem18,
            this.layoutControlItem20,
            this.layoutControlItem22});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.Root.Size = new System.Drawing.Size(897, 436);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.barcodeTextBoxExt;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 28);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(129, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(289, 28);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Barcode:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.codeTextBoxExt;
            this.layoutControlItem2.Location = new System.Drawing.Point(289, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 28);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(115, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(251, 28);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Product Code:";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(86, 17);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AllowHtmlStringInCaption = true;
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.nameTextBoxExt;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 28);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(129, 28);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(429, 28);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "<color=255, 0, 0>*</color>Product Name:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AllowHtmlStringInCaption = true;
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.Control = this.hsnTextBoxExt;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(0, 28);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(129, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(274, 28);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "<color=255, 0, 0>*</color>Hsn Code:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AllowHtmlStringInCaption = true;
            this.layoutControlItem7.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem7.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem7.Control = this.taxTypelookUpEdit;
            this.layoutControlItem7.Location = new System.Drawing.Point(274, 108);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(288, 28);
            this.layoutControlItem7.Text = "<color=255, 0, 0>*</color>Gst Slab:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AllowHtmlStringInCaption = true;
            this.layoutControlItem8.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem8.Control = this.unitLookUpEdit;
            this.layoutControlItem8.Location = new System.Drawing.Point(721, 108);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(170, 28);
            this.layoutControlItem8.Text = "<color=255, 0, 0>*</color>Sale Unit:";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(62, 17);
            this.layoutControlItem8.TextToControlDistance = 5;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.AllowHtmlStringInCaption = true;
            this.layoutControlItem9.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem9.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem9.Control = this.purUnitlookUpEdit;
            this.layoutControlItem9.Location = new System.Drawing.Point(562, 108);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(159, 28);
            this.layoutControlItem9.Text = "<color=255, 0, 0>*</color>Pur Unit:";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(59, 17);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // styleNoLayoutControlItem
            // 
            this.styleNoLayoutControlItem.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleNoLayoutControlItem.AppearanceItemCaption.Options.UseFont = true;
            this.styleNoLayoutControlItem.Control = this.styleNoTextEdit;
            this.styleNoLayoutControlItem.Location = new System.Drawing.Point(540, 0);
            this.styleNoLayoutControlItem.Name = "styleNoLayoutControlItem";
            this.styleNoLayoutControlItem.Size = new System.Drawing.Size(351, 28);
            this.styleNoLayoutControlItem.Text = "Style No:";
            this.styleNoLayoutControlItem.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AllowHtmlStringInCaption = true;
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.descTextBoxExt;
            this.layoutControlItem5.Location = new System.Drawing.Point(429, 28);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 28);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(129, 28);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(462, 28);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "<color=255, 0, 0>*</color>Description:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem25.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem25.Control = this.groupLookup1;
            this.layoutControlItem25.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(274, 24);
            this.layoutControlItem25.Text = "Group:";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem31.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem31.Control = this.subGroupLookup1;
            this.layoutControlItem31.Location = new System.Drawing.Point(274, 56);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(288, 24);
            this.layoutControlItem31.Text = "Sub Group:";
            this.layoutControlItem31.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem32.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem32.Control = this.brandLookup1;
            this.layoutControlItem32.Location = new System.Drawing.Point(562, 56);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(329, 24);
            this.layoutControlItem32.Text = "Brand:";
            this.layoutControlItem32.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem33.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem33.Control = this.categoryLookup1;
            this.layoutControlItem33.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(274, 28);
            this.layoutControlItem33.Text = "Category:";
            this.layoutControlItem33.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem35.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem35.Control = this.colorLookup1;
            this.layoutControlItem35.Location = new System.Drawing.Point(562, 80);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Size = new System.Drawing.Size(329, 28);
            this.layoutControlItem35.Text = "Color:";
            this.layoutControlItem35.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem11.Control = this.purRateKontoSpinEdit;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 136);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(229, 28);
            this.layoutControlItem11.Text = "Pur. Rate:";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem14.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem14.Control = this.mrpKontoSpinEdit;
            this.layoutControlItem14.Location = new System.Drawing.Point(229, 164);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(252, 28);
            this.layoutControlItem14.Text = "MRP:";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(97, 17);
            // 
            // semiBulkLayoutControlItem
            // 
            this.semiBulkLayoutControlItem.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.semiBulkLayoutControlItem.AppearanceItemCaption.Options.UseFont = true;
            this.semiBulkLayoutControlItem.Control = this.semBulkRateKontoSpinEdit;
            this.semiBulkLayoutControlItem.Location = new System.Drawing.Point(481, 192);
            this.semiBulkLayoutControlItem.Name = "semiBulkLayoutControlItem";
            this.semiBulkLayoutControlItem.Size = new System.Drawing.Size(205, 28);
            this.semiBulkLayoutControlItem.Text = "Semi Bulk Rate:";
            this.semiBulkLayoutControlItem.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem15.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem15.Control = this.saleDiscKontoSpinEdit;
            this.layoutControlItem15.Location = new System.Drawing.Point(481, 136);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(200, 28);
            this.layoutControlItem15.Text = "Sale Disc %:";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem12.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem12.Control = this.purDiscKontoSpinEdit;
            this.layoutControlItem12.Location = new System.Drawing.Point(229, 136);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(252, 28);
            this.layoutControlItem12.Text = "Pur. Disc %:";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem13.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem13.Control = this.saleRateKontoSpinEdit;
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 164);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(229, 28);
            this.layoutControlItem13.Text = "Sale Rate:";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem21.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem21.Control = this.profitKontoSpinEdit;
            this.layoutControlItem21.Location = new System.Drawing.Point(681, 136);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(210, 28);
            this.layoutControlItem21.Text = "Profit %:";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(97, 17);
            // 
            // bulkRateLayoutControlItem
            // 
            this.bulkRateLayoutControlItem.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bulkRateLayoutControlItem.AppearanceItemCaption.Options.UseFont = true;
            this.bulkRateLayoutControlItem.Control = this.bulkRateKontoSpinEdit;
            this.bulkRateLayoutControlItem.Location = new System.Drawing.Point(0, 192);
            this.bulkRateLayoutControlItem.Name = "bulkRateLayoutControlItem";
            this.bulkRateLayoutControlItem.Size = new System.Drawing.Size(229, 28);
            this.bulkRateLayoutControlItem.Text = "Bulk Rate:";
            this.bulkRateLayoutControlItem.TextSize = new System.Drawing.Size(97, 17);
            // 
            // bulkQtyLayoutControlItem
            // 
            this.bulkQtyLayoutControlItem.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bulkQtyLayoutControlItem.AppearanceItemCaption.Options.UseFont = true;
            this.bulkQtyLayoutControlItem.Control = this.bulkQtyKontoSpinEdit;
            this.bulkQtyLayoutControlItem.Location = new System.Drawing.Point(229, 192);
            this.bulkQtyLayoutControlItem.Name = "bulkQtyLayoutControlItem";
            this.bulkQtyLayoutControlItem.Size = new System.Drawing.Size(252, 28);
            this.bulkQtyLayoutControlItem.Text = "Bulk Qty:";
            this.bulkQtyLayoutControlItem.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem16.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem16.Control = this.sizeCheckedComboBoxEdit;
            this.layoutControlItem16.Location = new System.Drawing.Point(274, 80);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(288, 28);
            this.layoutControlItem16.Text = "Size:";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem19.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem19.Control = this.qtyKontoSpinEdit;
            this.layoutControlItem19.Location = new System.Drawing.Point(481, 164);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(200, 28);
            this.layoutControlItem19.Text = "Purchase Qty:";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(97, 17);
            // 
            // layoutControlItem38
            // 
            this.layoutControlItem38.Control = this.taxIncCheckEdit;
            this.layoutControlItem38.Location = new System.Drawing.Point(0, 220);
            this.layoutControlItem38.Name = "layoutControlItem38";
            this.layoutControlItem38.Size = new System.Drawing.Size(289, 26);
            this.layoutControlItem38.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem38.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.negativeCheckEdit;
            this.layoutControlItem10.Location = new System.Drawing.Point(289, 220);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(245, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.uniqueCheckEdit;
            this.layoutControlItem17.Location = new System.Drawing.Point(534, 220);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 246);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(891, 184);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.genItemSimpleButton;
            this.layoutControlItem18.Location = new System.Drawing.Point(755, 220);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(136, 26);
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem20.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem20.Control = this.wtKontoSpinEdit1;
            this.layoutControlItem20.Location = new System.Drawing.Point(681, 164);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(210, 28);
            this.layoutControlItem20.Text = "Weight:";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(97, 17);
            // 
            // pcsKontoSpinEdit
            // 
            this.pcsKontoSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.pcsKontoSpinEdit.EnterMoveNextControl = true;
            this.pcsKontoSpinEdit.Location = new System.Drawing.Point(791, 197);
            this.pcsKontoSpinEdit.Name = "pcsKontoSpinEdit";
            this.pcsKontoSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pcsKontoSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.pcsKontoSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.pcsKontoSpinEdit.Size = new System.Drawing.Size(101, 24);
            this.pcsKontoSpinEdit.StyleController = this.layoutControl1;
            this.pcsKontoSpinEdit.TabIndex = 30;
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem22.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem22.Control = this.pcsKontoSpinEdit;
            this.layoutControlItem22.Location = new System.Drawing.Point(686, 192);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(205, 28);
            this.layoutControlItem22.Text = "Barcode Pcs:";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(97, 17);
            // 
            // PurchaseProductView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(897, 473);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.Name = "PurchaseProductView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Purchase Product & Barcode";
            this.Load += new System.EventHandler(this.PurchaseProductView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wtKontoSpinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtyKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeCheckedComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.negativeCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posPurTransDtoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.semBulkRateKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkQtyKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkRateKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxIncCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profitKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uniqueCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleDiscKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mrpKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleRateKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purDiscKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purRateKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purUnitlookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxTypelookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hsnTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcodeTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleNoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleNoLayoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.semiBulkLayoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkRateLayoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkQtyLayoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcsKontoSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private KontoSpinEdit semBulkRateKontoSpinEdit;
        private KontoSpinEdit bulkQtyKontoSpinEdit;
        private KontoSpinEdit bulkRateKontoSpinEdit;
        private DevExpress.XtraEditors.CheckEdit taxIncCheckEdit;
        private Shared.Masters.Color.ColorLookup colorLookup1;
        private Shared.Masters.Category.CategoryLookup categoryLookup1;
        private Shared.Masters.Brand.BrandLookup brandLookup1;
        private Shared.Masters.SubGroup.SubGroupLookup subGroupLookup1;
        private Shared.Masters.ProductGroup.GroupLookup groupLookup1;
        private KontoSpinEdit profitKontoSpinEdit;
        private DevExpress.XtraEditors.CheckEdit uniqueCheckEdit;
        private KontoSpinEdit saleDiscKontoSpinEdit;
        private KontoSpinEdit mrpKontoSpinEdit;
        private KontoSpinEdit saleRateKontoSpinEdit;
        private KontoSpinEdit purDiscKontoSpinEdit;
        private KontoSpinEdit purRateKontoSpinEdit;
        private DevExpress.XtraEditors.LookUpEdit purUnitlookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit unitLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit taxTypelookUpEdit;
        private Core.Shared.Libs.KontoTextBoxExt hsnTextBoxExt;
        private Core.Shared.Libs.KontoTextBoxExt descTextBoxExt;
        private Core.Shared.Libs.KontoTextBoxExt nameTextBoxExt;
        private Core.Shared.Libs.KontoTextBoxExt codeTextBoxExt;
        private Core.Shared.Libs.KontoTextBoxExt barcodeTextBoxExt;
        private KontoTextEdit styleNoTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem styleNoLayoutControlItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem semiBulkLayoutControlItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem bulkRateLayoutControlItem;
        private DevExpress.XtraLayout.LayoutControlItem bulkQtyLayoutControlItem;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit negativeCheckEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.CheckedComboBoxEdit sizeCheckedComboBoxEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraEditors.SimpleButton genItemSimpleButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private KontoSpinEdit qtyKontoSpinEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private System.Windows.Forms.BindingSource posPurTransDtoBindingSource;
        private Core.Shared.Libs.CustomGridColumn colItemCode;
        private Core.Shared.Libs.CustomGridColumn colStyleNo;
        private Core.Shared.Libs.CustomGridColumn colDescription;
        private Core.Shared.Libs.CustomGridColumn colGroupId;
        private Core.Shared.Libs.CustomGridColumn colSubGroupId;
        private Core.Shared.Libs.CustomGridColumn colSizeId;
        private Core.Shared.Libs.CustomGridColumn colCategoryId;
        private Core.Shared.Libs.CustomGridColumn colBrandId;
        private Core.Shared.Libs.CustomGridColumn colPurUomId;
        private Core.Shared.Libs.CustomGridColumn colPurDisc;
        private Core.Shared.Libs.CustomGridColumn colSaleDisc;
        private Core.Shared.Libs.CustomGridColumn colCheckNegative;
        private Core.Shared.Libs.CustomGridColumn colSaleRateTaxInc;
        private Core.Shared.Libs.CustomGridColumn colTaxId;
        private Core.Shared.Libs.CustomGridColumn colProductName;
        private Core.Shared.Libs.CustomGridColumn colColorName;
        private Core.Shared.Libs.CustomGridColumn colDesignName;
        private Core.Shared.Libs.CustomGridColumn colGradeName;
        private Core.Shared.Libs.CustomGridColumn colLotNo;
        private Core.Shared.Libs.CustomGridColumn colQty;
        private Core.Shared.Libs.CustomGridColumn colCut;
        private Core.Shared.Libs.CustomGridColumn colPcs;
        private Core.Shared.Libs.CustomGridColumn colUomId;
        private Core.Shared.Libs.CustomGridColumn colRate;
        private Core.Shared.Libs.CustomGridColumn colTotal;
        private Core.Shared.Libs.CustomGridColumn colDisc;
        private Core.Shared.Libs.CustomGridColumn colDiscAmt;
        private Core.Shared.Libs.CustomGridColumn colFreightRate;
        private Core.Shared.Libs.CustomGridColumn colFreight;
        private Core.Shared.Libs.CustomGridColumn colOtherAdd;
        private Core.Shared.Libs.CustomGridColumn colOtherLess;
        private Core.Shared.Libs.CustomGridColumn colSgstPer;
        private Core.Shared.Libs.CustomGridColumn colSgst;
        private Core.Shared.Libs.CustomGridColumn colCgstPer;
        private Core.Shared.Libs.CustomGridColumn colCgst;
        private Core.Shared.Libs.CustomGridColumn colIgstPer;
        private Core.Shared.Libs.CustomGridColumn colIgst;
        private Core.Shared.Libs.CustomGridColumn colCessPer;
        private Core.Shared.Libs.CustomGridColumn colCess;
        private Core.Shared.Libs.CustomGridColumn colNetTotal;
        private Core.Shared.Libs.CustomGridColumn colRemark;
        private Core.Shared.Libs.CustomGridColumn colChallanNo;
        private Core.Shared.Libs.CustomGridColumn colChallanDate;
        private Core.Shared.Libs.CustomGridColumn colOrderNo;
        private Core.Shared.Libs.CustomGridColumn colOrderDate;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colBillId;
        private Core.Shared.Libs.CustomGridColumn colBatchId;
        private Core.Shared.Libs.CustomGridColumn colProductId;
        private Core.Shared.Libs.CustomGridColumn colColorId;
        private Core.Shared.Libs.CustomGridColumn colDesignId;
        private Core.Shared.Libs.CustomGridColumn colGradeId;
        private Core.Shared.Libs.CustomGridColumn colRefId;
        private Core.Shared.Libs.CustomGridColumn colRefTransId;
        private Core.Shared.Libs.CustomGridColumn colRefVoucherId;
        private Core.Shared.Libs.CustomGridColumn colOrdId;
        private Core.Shared.Libs.CustomGridColumn colOrdDate;
        private Core.Shared.Libs.CustomGridColumn colChDate;
        private Core.Shared.Libs.CustomGridColumn colSaleRate;
        private Core.Shared.Libs.CustomGridColumn colHsnCode;
        private Core.Shared.Libs.CustomGridColumn colBarcode;
        private Core.Shared.Libs.CustomGridColumn colBrand;
        private Core.Shared.Libs.CustomGridColumn colCategory;
        private Core.Shared.Libs.CustomGridColumn colBulkQty;
        private Core.Shared.Libs.CustomGridColumn colBulkRate;
        private Core.Shared.Libs.CustomGridColumn colGroupName;
        private Core.Shared.Libs.CustomGridColumn colSize;
        private Core.Shared.Libs.CustomGridColumn colSubGroupName;
        private Core.Shared.Libs.CustomGridColumn colMrp;
        private Core.Shared.Libs.CustomGridColumn colProfitPer;
        private Core.Shared.Libs.CustomGridColumn colSemiBulkRate;
        private Core.Shared.Libs.CustomGridColumn colSellingPrice;
        private KontoSpinEdit wtKontoSpinEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private Core.Shared.Libs.CustomGridColumn colAvgWt;
        private KontoSpinEdit pcsKontoSpinEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
    }
}
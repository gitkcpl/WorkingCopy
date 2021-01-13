using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Shared.Masters.Color;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Konto.Shared.Masters.Item
{
    public partial class ItemFormulaView : KontoForm

    {
        public List<PFormulaDto> FormulaData { get; set; }
        public List<PFormulaDto> DelData { get; set; }

        public ItemFormulaView()
        {
            InitializeComponent();
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            this.Load += ItemFormulaView_Load;
            this.gridView1.KeyDown += GridView1_KeyDown;
            this.gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr == null) return;
            if (gridView1.FocusedColumn.FieldName == "ProductName")
            {
                if (Convert.ToInt32(dr.RefProductId) > 0) return;
                if (e.KeyCode == Keys.Return)
                {
                    if (dr.RefProductId == 0 )
                    {
                        OpenItemLookup(0, dr);
                        // e.Handled = true;
                    }
                    else
                    {
                        OpenItemLookup((int)dr.ProductId, dr);
                    }
                }
                else if (e.KeyCode == Keys.F1)
                {
                    OpenItemLookup((int)dr.ProductId, dr);
                    e.Handled = true;
                }
            }
            else if (gridView1.FocusedColumn.FieldName == "ColorName")
            {
                if (e.KeyCode == Keys.Return)
                {
                    if (dr.ColorId == 0 )
                    {
                        OpenColorLookup(0, dr);
                        // e.Handled = true;
                    }
                    else
                        OpenColorLookup((int)dr.ColorId,dr);

                }
                else if (e.KeyCode == Keys.F1)
                {
                    OpenColorLookup((int)dr.ColorId,dr);
                    e.Handled = true;
                }
            }
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as PFormulaDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelData.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
            }
        }

        private void ItemFormulaView_Load(object sender, System.EventArgs e)
        {
            pFormulaModelBindingSource.DataSource = this.FormulaData;
            this.ActiveControl = this.gridControl1;

            using (var db = new KontoContext())
            {

                var _uomlist = (from p in db.Uoms
                                where !p.IsDeleted & p.IsActive
                                orderby p.UnitName
                                select new UomLookupDto()
                                {
                                    DisplayText = p.UnitName,
                                    Id = p.Id,
                                    RateOn = p.RateOn
                                }).ToList();



                uomRepositoryItemLookUpEdit.DataSource = _uomlist;
            }
        }

        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup(dr.ProductId, dr);

        }
        private void ColorRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            var dr = PreOpenLookup();
            if (dr != null)
                OpenColorLookup(dr.ColorId, dr);
        }

        private void OpenItemLookup(int _selvalue, PFormulaDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;
          
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.RefProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
              
                er.Rate = model.DealerPrice;

                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }
           
        }
        private void OpenColorLookup(int _selvalue, PFormulaDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
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

        private PFormulaDto PreOpenLookup()
        {
            
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (PFormulaDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
    }
}

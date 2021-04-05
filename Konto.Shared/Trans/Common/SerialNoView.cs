using DevExpress.XtraGrid.Views.Grid;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Trans.Common
{
    public partial class SerialNoView : KontoForm
    {
        public List<SerialBatchDto> Serials { get; set; }

        public List<SerialBatchDto> AllSerials { get; set; }
        public List<SerialBatchDto> DelSerials { get; set; }
        public int RefTransId { get; set; }
        public int ProductId { get; set; }
        public bool IsStockSelection { get; set; }
        public SerialBatchDto SelectedSerial { get; set; }

        public List<BillTransDto> _BillTrans { get; set; }

        public SerialNoView()
        {
            InitializeComponent();
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.gridView1.KeyDown += GridView1_KeyDown;
            this.gridView1.ValidatingEditor += GridView1_ValidatingEditor;
            this.gridView1.InvalidValueException += GridView1_InvalidValueException;
            this.Load += SerialNoView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.deleteSimpleButton.Click += DeleteSimpleButton_Click;
            this.gridView1.InitNewRow += GridView1_InitNewRow;
            this.gridView1.ShowingEditor += GridView1_ShowingEditor;
            this.DelSerials = new List<SerialBatchDto>();
        }

        private void GridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            var rw =gridView1.GetRow(gridView1.FocusedRowHandle) as SerialBatchDto;
            if (rw != null && !rw.IsAcitve) // user serial can not be changed
            {
                e.Cancel = true;
            }
        }

        private void GridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as SerialBatchDto;
            rw.Id = -1 * gridView1.RowCount;
            rw.RefTransId = this.RefTransId;
            rw.IsAcitve = true;
            
        }

        private void DeleteSimpleButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                return;
            GridView view = gridView1 as GridView;
            var row = view.GetRow(view.FocusedRowHandle) as SerialBatchDto;

            if (row.Id > 0 && !row.IsAcitve) // check for used serial
            {
                MessageBox.Show("Serial in used. can not delete");
                return;
            }

            view.DeleteRow(view.FocusedRowHandle);
            DelSerials.Add(row);
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (this.IsStockSelection)
            {
                this.SelectedSerial = this.gridView1.GetRow(gridView1.FocusedRowHandle) as SerialBatchDto;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Value!=null && !string.IsNullOrEmpty(e.Value.ToString()))
            {
                var lsts = serialBatchDtoBindingSource.DataSource as List<SerialBatchDto>;
                var row = gridView1.GetRow(gridView1.FocusedRowHandle) as SerialBatchDto;


                if( AllSerials.Any(x=>x.SerialNo!=null && x.SerialNo.ToUpper() == e.Value.ToString().ToUpper()) ||
                   lsts.Any(x=>x.SerialNo!= null && x.SerialNo.ToUpper()== e.Value.ToString().ToUpper())){
                    e.Valid = false;
                    e.ErrorText = "Serial No Already Exists";
                }
                using(var db = new KontoContext())
                {
                    if(db.ItemSerials.Any(x=>x.SerialNo.ToUpper() == e.Value.ToString().ToUpper() && 
                    x.Id!= row.Id))
                    {
                        e.Valid = false;
                        e.ErrorText = "Serial No Already Exists";
                    }
                }
            }
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && this.IsStockSelection)
            {
                okSimpleButton.PerformClick();
            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //   gridView1.cure
            //    var rw = gridView1.GetRow(gridView1.FocusedRowHandle) as SerialBatchDto;
            //    if (rw == null || string.IsNullOrEmpty(rw.SerialNo))
            //    {
            //        gridView1.DeleteRow(gridView1.FocusedRowHandle);
            //        this.SelectNextControl(gridControl1, true, true, true, true);
            //    }

            //}
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as SerialBatchDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelSerials.Add(row);
            }
        }

        private void SerialNoView_Load(object sender, EventArgs e)
        {
            if (this.Serials == null)
                this.Serials = new List<SerialBatchDto>();

            
            if (this.IsStockSelection)
            {
                this.gridView1.OptionsBehavior.Editable = false;
                this.gridView1.OptionsBehavior.ReadOnly = true;
                this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;

                using (var db = new KontoContext())
                {
                    var srs = (from p in db.ItemSerials
                                    where p.ProductId == this.ProductId
                                    && p.IsActive && !p.IsDeleted
                                    select new SerialBatchDto
                                    {
                                        Id = p.Id,
                                        SerialNo = p.SerialNo
                                    }).ToList();
                    
                    this.Serials = (from p in srs
                                    where !_BillTrans.Any(x => (x.DesignId == p.Id) && x.Id != this.RefTransId)
                                    select p).ToList();
                }

            }

            serialBatchDtoBindingSource.DataSource = this.Serials;
        }

        private void GridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}

using DevExpress.XtraGrid.Views.Grid;
using Konto.Core.Shared;
using Konto.Data.Models.Transaction;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Trans.Common
{
    public partial class AttachmentView : KontoForm
    {
        public List<AttachmentModel> Trans { get; set; }

        public List<AttachmentModel> DelTrans { get; set; }
        public AttachmentView()
        {
            InitializeComponent();
            this.repositoryItemButtonEdit1.ButtonClick += RepositoryItemButtonEdit1_ButtonClick;
            this.Load += AttachmentView_Load;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.gridView1.KeyDown += GridView1_KeyDown;
            this.viewSimpleButton.Click += ViewSimpleButton_Click;
        }

        private void ViewSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0) return;
                var row = gridView1.GetRow(gridView1.FocusedRowHandle) as AttachmentModel;
                if (row == null) return;
                string windir = Environment.GetEnvironmentVariable("WINDIR");
                Process prc = new Process();
                prc.StartInfo.FileName = windir + @"\explorer.exe";
                if(row.Id!=0)
                    prc.StartInfo.Arguments = "attachment\\" + row.FilePath;
                else
                    prc.StartInfo.Arguments =  row.FilePath;
                prc.Start();
            }
            catch (Exception ex)
            {

                Log.Error(ex.ToString(), "file View");
                MessageBox.Show(ex.ToString());
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
                var row = view.GetRow(view.FocusedRowHandle) as AttachmentModel;
                view.DeleteRow(view.FocusedRowHandle);
                if(row.Id > 0)
                    DelTrans.Add(row);
            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AttachmentView_Load(object sender, EventArgs e)
        {
            if (this.Trans == null)
                this.Trans = new List<AttachmentModel>();
            if (this.DelTrans == null)
                this.DelTrans = new List<AttachmentModel>();
            this.attachmentModelBindingSource.DataSource = this.Trans;
        }

        private void RepositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            var res = dlg.ShowDialog();
            if(res == DialogResult.OK)
            {
                var er = gridView1.GetRow(gridView1.FocusedRowHandle) as AttachmentModel;
                if (er != null)
                    er.FilePath = dlg.FileName;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            var er = gridView1.GetRow(gridView1.FocusedRowHandle) as AttachmentModel;
            if (er == null || er.Id==0) return;
            if (gridView1.FocusedColumn.FieldName == "FilePath")
            {
                e.Cancel = true;
            }
        }
    }
}

using Aspose.Cells;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.ProductGroup
{
    public partial class ItemGroupImport : KontoForm
    {
        DataTable _dataTable;
        
        public ItemGroupImport()
        {
            InitializeComponent();
            excelSimpleButton.Click += ExcelSimpleButton_Click;
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dataTable == null || _dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No Data Found for Import");
                    return;
                }
                var impList = new List<PGroupModel>();
                using (var db = new KontoContext())
                {
                    foreach (DataRow item in _dataTable.Rows)
                    {
                        var _imp = new PGroupModel();
                        string groupname = item[0].ToString().ToUpper();
                        var exist = db.PGroups.Any(x => x.GroupName == groupname);
                        if (exist)
                            continue;
                        if (item[0].ToString().Length < 2)
                            continue;
                        

                        _imp.GroupName = item[0].ToString().ToUpper();
                        _imp.GroupCode = "00";
                        _imp.IsActive = true;
                        impList.Add(_imp);
                    }

                    if (impList.Count > 0)
                    {
                        db.PGroups.AddRange(impList);

                        db.SaveChanges();
                        MessageBox.Show("Imported Successfully");
                    }
                    else
                    {
                        MessageBox.Show("No Record Found to be import or already Exists");
                    }
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "ac group import");
                MessageBox.Show(ex.ToString());
            }
        }

        private void ExcelSimpleButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open Account Excel File";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.ShowDialog();
            string filePath = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(filePath)) return;

            Workbook workbook = new Workbook(filePath);
            Worksheet worksheet = workbook.Worksheets[0];
            var exp = new ExportTableOptions();
            exp.ExportAsString = true;

            exp.ExportColumnName = true;
            worksheet.Cells.DeleteBlankColumns();
            worksheet.Cells.DeleteBlankRows();
            _dataTable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, exp);
            this.gridControl1.DataSource = _dataTable;
        }
    }
}

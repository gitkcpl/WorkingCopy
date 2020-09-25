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

namespace Konto.Shared.Masters.PG
{
    public partial class PgImport : KontoForm
    {
        DataTable _dataTable;
        
        public PgImport()
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
                var impList = new List<PartyGroupModel>();
                using (var db = new KontoContext())
                {
                    foreach (DataRow item in _dataTable.Rows)
                    {
                        var _imp = new PartyGroupModel();
                        string groupname = item[0].ToString().ToUpper();
                        var exist = db.PartyGroups.Any(x => x.GroupName == groupname);
                        if (exist)
                            continue;
                        if (item[0].ToString().Length < 2)
                            continue;
                        string stname = item[1].ToString();
                        _imp.GroupName = groupname;

                        _imp.Address = stname;
                        
                        _imp.IsActive = true;
                        impList.Add(_imp);
                    }

                    if (impList.Count > 0)
                    {
                        db.PartyGroups.AddRange(impList);

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

                Log.Error(ex, "party group import");
                MessageBox.Show(ex.ToString());
            }
        }

        private void ExcelSimpleButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open Party Group Excel File";
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

using Aspose.Cells;
using DevExpress.Office.History;
using DevExpress.Pdf.Native;
using DevExpress.XtraExport.Helpers;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Acc
{
    public partial class AccImport : KontoForm
    {
        DataTable _dataTable;
        
        public AccImport()
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
                var impList = new List<AccModel>();
                var balList = new List<AccBalModel>();
                var addrssList = new List<AccAddressModel>();
                using (var db = new KontoContext())
                {
                    var acid = db.Accs.Max(k => k.Id);
                    int AccId = 0;
                    if (acid != 0)
                        AccId = (int)acid;


                    foreach (DataRow item in _dataTable.Rows)
                    {
                        var imp = new AccModel();
                        string _name = item[0].ToString().ToUpper();
                        var exist = db.Accs.Any(x => x.AccName == _name);
                        if (exist) // already exist name
                            continue;
                        if (item[0].ToString().Length < 2) // account name less than 2
                            continue;
                        
                        if (item[1] == null) // group not found
                            continue;

                        string _grpName = item[1].ToString().Trim().ToUpper();

                        var _grp = db.AcGroupModels.Where(x => x.GroupName == _grpName).FirstOrDefault();
                        if (_grp == null) // group not found in database
                        {
                            MessageBox.Show(_grpName + " Not Found");

                            return;
                        }

                        imp.AccName = _name;
                        imp.PrintName = _name;
                        imp.GroupId = _grp.Id;
                        string gstn = item[3].ToString();
                        if (gstn.Length == 15) {
                            imp.GstIn = gstn;
                            imp.PanNo = gstn.Substring(2, 10);
                            imp.VatTds = "REG";
                        }
                        else
                        {
                            imp.VatTds = "NA";
                        }

                        imp.GSTDate = DateTime.Now;
                        AccId = AccId + 1;
                        imp.Id = AccId;
                        imp.RowId = Guid.NewGuid();

                        var _agentname = item[6].ToString();
                        var _ag = db.Accs.FirstOrDefault(x => x.AccName == _agentname);
                        if (_ag != null)
                            imp.AgentId = _ag.Id;
                        

                        var _partygroup = item[8].ToString();
                        var _pg = db.PartyGroups.FirstOrDefault(x => x.GroupName == _partygroup);
                        if (_pg != null)
                            imp.PGroupId = _pg.Id;
                        else
                            imp.PGroupId = 1;

                        impList.Add(imp);
                        // add value in accbal 
                        var acBal = new AccBalModel();
                        acBal.AccId = imp.Id;
                        acBal.GroupId = _grp.Id;
                        string _ctname = item[4].ToString();

                        if (!string.IsNullOrEmpty(_ctname))
                        {
                            var _st = db.Cities.Where(x => x.CityName == _ctname).FirstOrDefault();
                            if (_st != null)
                            {
                                acBal.CityId = _st.Id;
                            }
                            else
                            {
                                var sttname = item["State"].ToString().Trim();
                                if (!string.IsNullOrEmpty(sttname))
                                {
                                    var stt =db.States.FirstOrDefault(x => x.StateName.ToUpper() == sttname.ToUpper());
                                    if (stt != null)
                                    {
                                        _st = new CityModel();
                                        _st.CityName = _ctname;
                                        _st.StateId = stt.Id;
                                        db.Cities.Add(_st);
                                        db.SaveChanges();
                                        acBal.CityId = _st.Id;
                                    }
                                }
                                else
                                    acBal.CityId = 1;
                            }
                        }
                        else
                        {
                            acBal.CityId = 1;
                        }
                        
                        string _pin = item[5].ToString();
                        if (!string.IsNullOrEmpty(_pin))
                            acBal.PinCode = _pin;

                        acBal.CompId = KontoGlobals.CompanyId;
                        acBal.YearId = KontoGlobals.YearId;
                        acBal.AccRowId =imp.RowId;
                        acBal.AreaId = 1;
                        string _addr = item[2].ToString();
                        if (_addr.Length > 100)
                        {
                            acBal.Address1 = _addr.Substring(0, 100);
                            acBal.Address2 = _addr.Substring(100, _addr.Length-1-100);
                        }
                        else
                        {
                            acBal.Address1 = _addr;
                        }
                        if (item[7].ToString().Length >=10)
                            acBal.MobileNo = item[7].ToString().Substring(0,10);

                        acBal.Email = item[8].ToString();
                        balList.Add(acBal);
                        
                        //address model
                        var addrss = new AccAddressModel();
                        addrss.AccId = imp.Id;
                        addrss.AddressType = "Mailing Address";
                        addrss.Address1 = acBal.Address1;
                        addrss.Address2 = acBal.Address2;
                        addrss.PinCode = acBal.PinCode;
                        addrss.CityId = acBal.CityId;
                        addrss.AreaId = acBal.AreaId;
                        addrss.MobileNo = acBal.MobileNo;
                        addrss.Email = acBal.Email;
                        addrssList.Add(addrss);
                    }

                    if (impList.Count > 0)
                    {
                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                db.Accs.AddRange(impList);
                                db.AccAddresses.AddRange(addrssList);
                                db.SaveChanges();
                                foreach (var item in addrssList)
                                {
                                    var _ac = balList.FirstOrDefault(x => x.AccId == item.AccId);
                                    _ac.AddressId = item.Id;
                                }
                                db.AccBals.AddRange(balList);
                                db.SaveChanges();
                                tran.Commit();
                                MessageBox.Show("Imported Successfully");
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                Log.Error(ex, "acc import");
                                MessageBox.Show(ex.ToString());
                            }
                            
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("No Record Found to be import or already Exists");
                    }
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "acc import");
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (_dataTable == null || _dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No Data Found for Import");
                return;
            }
            try
            {
                using (var db = new KontoContext())
                {
                    foreach (DataRow item in _dataTable.Rows)
                    {

                        string _name = item[0].ToString().ToUpper();
                        decimal _bal = Convert.ToDecimal(item[1].ToString());

                        var exist = db.Accs.FirstOrDefault(x => x.AccName == _name);
                        if (exist == null)
                            continue;
                        var acb = db.AccBals.FirstOrDefault(x => x.AccId == exist.Id && x.CompId == KontoGlobals.CompanyId
                        && x.YearId == KontoGlobals.YearId);
                        if (acb == null) continue;
                        
                        acb.OpBal = _bal;
                        acb.OpDebit = 0;
                        acb.OpCredit = 0;
                        if (_bal > 0)
                            acb.OpDebit = _bal;
                        else
                            acb.OpCredit = -1 * _bal;

                    }
                    db.SaveChanges();
                    MessageBox.Show("Opening Balance Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Op balance import");
                MessageBox.Show(ex.ToString());
            }
        
        }
    }
}

using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Admin.Dtos;
using Konto.Shared.Masters.Acc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Setup
{
    public partial class SettingWindow : KontoForm
    {
        
        public SettingWindow()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            customGridControl1.ProcessGridKey += CustomGridControl1_ProcessGridKey;
        }

        private void CustomGridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control )
            {
                var row = customGridView1.GetRow(customGridView1.FocusedRowHandle) as ParaDto;
                if (row == null) return;
                var frm = new AccLkpWindow();
                frm.Tag = MenuId.Account;
                frm.SelectedValue = Convert.ToInt32(row.ParaValue);
                frm.ShowDialog();
                
                row.ParaValue = frm.SelectedValue.ToString();
                row.Remark = frm.SelectedTex;


            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                var ModelList = this.paraDtoBindingSource.DataSource as List<ParaDto>;
                using (var db = new KontoContext())
                {
                    foreach (ParaDto para in ModelList)
                    {
                        if (this.SettingCategroy != "sys")
                        {
                            CompParaModel _compPara = new CompParaModel();
                            if (para.Id == 0)
                            {
                                _compPara.ParaValue = para.ParaValue;
                                _compPara.ParaId = para.ParaId;
                                _compPara.CompId = KontoGlobals.CompanyId;
                                _compPara.Remark = para.Remark;
                                _compPara.RowId = Guid.NewGuid();
                                db.CompParas.Add(_compPara);
                            }
                            else
                            {
                                _compPara = db.CompParas.Find(para.Id);
                                _compPara.ParaValue = para.ParaValue;

                            }
                        }
                        else
                        {
                            var _para = db.SysParas.Find(para.ParaId);
                            _para.DefaultValue = para.ParaValue;
                        }

                        //// add all parameter to dictionary
                        //if (ParaUtils.ParaDict.ContainsKey(para.Id))
                        //{
                        //    ParaUtils.ParaDict.Remove(para.Id);
                        //    ParaUtils.ParaDict.Add(para.Id, para.ParaValue);
                        //}
                    }
                    db.SaveChanges();

                }
                MessageBox.Show(KontoGlobals.SaveMessage, "Confirmation !!", MessageBoxButtons.OK);
                MessageBox.Show("Please Re-Open this Module in order to effect setting");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Setting ViewModel Under Save Transaction");
                
            }
        }

        private void SettingWindow_Load(object sender, EventArgs e)
        {
            using(var db = new KontoContext())
            {
                if (this.SettingCategroy != "sys")
                {
                    var ModelList = db.Database.SqlQuery<ParaDto>(
                    "dbo.Settingslist @CompanyId={0}, @Category={1}",
                    KontoGlobals.CompanyId, this.SettingCategroy).ToList();
                    this.paraDtoBindingSource.DataSource = ModelList;
                }
                else
                {
                    var modelist = (from p in db.SysParas
                                    where p.Category == "sys"
                                    select new ParaDto
                                    {
                                        Category = p.Category,
                                        DefaultValue = p.DefaultValue,
                                        Descr = p.Descr,
                                        ParaId = p.Id,
                                        ParaValue = p.DefaultValue,
                                        ValueDescr = p.ValueDescr
                                    }).ToList();
                    this.paraDtoBindingSource.DataSource = modelist;
                }
            }
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void customGridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}

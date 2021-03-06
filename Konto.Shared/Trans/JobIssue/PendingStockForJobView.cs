﻿using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Trans.JobIssue
{
    public partial class PendingStockForJobView : KontoForm
    {
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public List<DetailStockDto> list = new List<DetailStockDto>();
        public string IssueType = "MillIssue";
        public PendingStockForJobView()
        {
            InitializeComponent();
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.gridView1.MouseUp += GridView1_MouseUp;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.Load += PendingGreyForMillView_Load;
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.gridView1.CustomSummaryCalculate += GridView1_CustomSummaryCalculate;
        }
        decimal NetWeight;
        int BoxCount;
        private void GridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (!e.IsTotalSummary) return;
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                NetWeight = 0;
                BoxCount = 0;
            }
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                GridView gv = (GridView)sender;
                if ( Convert.ToBoolean(gv.GetRowCellValue(e.RowHandle, "IsSelected")) == true)
                {
                    NetWeight = NetWeight + Convert.ToDecimal(gv.GetRowCellValue(e.RowHandle, colNetWt));
                    BoxCount = BoxCount + 1;
                }
            }
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                this.Text = "Pending Taka [Selected Taka : " + BoxCount.ToString() + " Meters: " + NetWeight.ToString("F") + " ]";
            }
        }

        private void GridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void PendingGreyForMillView_Load(object sender, EventArgs e)
        {
            try
            {
                

                using (var db = new KontoContext())
                {
                    var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                                       (int)SpCollectionEnum.OutwardBeamProd);
                    db.Database.CommandTimeout = 0;
                    if (spcol == null)
                    {
                        list = db.Database.SqlQuery<DetailStockDto>(
                            "dbo.OutwardBeamProd @CompanyId={0} ,@ProductId={1},@IsOk={2},@vtype={3},@ptypeid={4}",
                            KontoGlobals.CompanyId,0, 1, this.IssueType, (int)ProductTypeEnum.GREY).ToList();
                    }
                    else
                    {
                        list = db.Database.SqlQuery<DetailStockDto>(
                            spcol.Name + " @CompanyId={0} ,@ProductId={1},@IsOk={2},@vtype={3},@ptypeid={4}",
                        KontoGlobals.CompanyId,0, 1, this.IssueType,(int)ProductTypeEnum.GREY).ToList();
                    }

                    gridControl1.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                
                Log.Error(ex, "Pending Taka Stock");

                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GridView1_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo ViewInfo = view.GetViewInfo() as GridViewInfo;
                GridState prevState = view.State;
                if ((e.Button & MouseButtons.Left) != 0)
                {
                    if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new Point(e.X, e.Y)))
                    {
                        ViewInfo.SelectionInfo.ClearPressedInfo();
                        args.Handled = true;
                    }
                }
            }
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
                Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
                bounds.Width -= 10;
                bounds.Height -= 3;
                bounds.Y += 3;
                headerEdit.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                headerEdit.EditValue = hi.Column.Caption;
                headerEdit.Show();
                headerEdit.Focus();
                activeCol = hi.Column;
            }
        }

        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }
    }
}

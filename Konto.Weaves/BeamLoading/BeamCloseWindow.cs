using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Konto.Weaves.BeamLoading
{
    public partial class BeamCloseWindow : KontoForm
    {
        public int BeamId { get; set; }
        public string BeamNo { get; set; }
        public BeamCloseWindow()
        {
            InitializeComponent();

            this.Load += TakaViewWindow_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
           
           
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(BeamNoTextBox.Text) || BeamNoTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Beam No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeamNoTextBox.Clear();
                BeamNoTextBox.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(CloseDateEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Date", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CloseDateEdit.Focus();
                return false;
            }

            using (var db = new KontoContext())
            {
                var find = db.Prods.FirstOrDefault(
                   x => x.Id== this.BeamId && x.IsClose && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Beam is Already Closed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BeamNoTextBox.Focus();
                    return false;
                }
            }

            return true;
        }
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData()) return;
                if (System.Windows.MessageBox.Show("Do You want to Close the beam?", "Close Beam!!", MessageBoxButton.YesNo) ==
               MessageBoxResult.Yes)
                {
                    var _db = new KontoContext();
                    //var vtype = (int)VoucherTypeEnum.BeamProd;
                    var beamLoaded = //_db.Prods.FirstOrDefault(k => k.VoucherNo == Model.VoucherNo);
                    (from pd in _db.Prods
                     join v in _db.Vouchers on pd.VoucherId equals v.Id into vou_join
                     from vou in vou_join.DefaultIfEmpty()
                     where pd.Id== this.BeamId
                     && pd.ProdStatus == "LOADED"
                     && pd.IsActive == true && pd.IsDeleted == false
                     select pd);

                    if (beamLoaded != null)
                    {
                        if (beamLoaded.FirstOrDefault() != null)
                        {
                            int beamId = beamLoaded.FirstOrDefault().Id;
                             
                            ProdModel ModelProd = _db.Prods.Find(beamId);

                            if (ModelProd.MacId == null && ModelProd.MacId == 0)
                            {
                                MessageBox.Show("Beam Not Loaded Yet..U can not Open or close beam", KontoGlobals.ConfirmationTitle);
                            }
                            else if (!ModelProd.IsClose)
                            {
                                using (var transaction = _db.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        DateTime closedate = CloseDateEdit.DateTime;

                                        ModelProd.CloseDate = closedate;
                                        ModelProd.ModifyDate = DateTime.Now;
                                        ModelProd.ModifyUser = KontoGlobals.UserName;
                                        ModelProd.IsClose = true;
                                         
                                        var loadtrans = _db.loadingTranModels.FirstOrDefault(
                                  k => k.ProdId == ModelProd.Id
                                    && k.ProdStatus.ToUpper() == "LOADED");

                                        if (loadtrans != null)
                                        {
                                            loadtrans.ProdStatus = "CLOSE";
                                            loadtrans.ModifyDate = DateTime.Now;
                                            loadtrans.ModifyUser = KontoGlobals.UserName;
                                             
                                        }
                                        _db.SaveChanges();
                                        transaction.Commit();
                                        System.Windows.MessageBox.Show(KontoGlobals.SaveMessage, "Confirmation !!", System.Windows.MessageBoxButton.OK);
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        Log.Error(ex, "Beam Loading (BBLWindowviewmodel) Save Under Transaction");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(KontoGlobals.FilterNotFound, KontoGlobals.ConfirmationTitle);
                        }
                    }
                    else
                    {
                        MessageBox.Show(KontoGlobals.FilterNotFound, KontoGlobals.ConfirmationTitle);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Beam Loading List view on Beam Close (BeamLoadingListViewWindow) On Ok Click command");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TakaViewWindow_Load(object sender, EventArgs e)
        {
            try
            {
                this.BeamNoTextBox.Text = this.BeamNo;
                this.ActiveControl = CloseDateEdit;
            }

            catch (Exception ex)
            {
                Log.Error(ex, "Beam Close View");
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
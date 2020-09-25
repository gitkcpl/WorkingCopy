using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using System;
using System.Windows.Forms;

namespace Konto.Shared.Masters.LedgerGroup
{
    public partial class GroupSettings : KontoForm
    {
        public int GroupId { get; set; }
        AcGroupModel model;
        KontoContext db;
        public GroupSettings()
        {
            InitializeComponent();
            db = new KontoContext();
        }

        private void GroupSettings_Load(object sender, EventArgs e)
        {
           
                 model = db.AcGroupModels.Find(this.GroupId);
                if (model == null) return;
                addresscheckEdit.Checked = model.AddressReq;
                agentcheckEdit.Checked = model.AgentReq;
                transportCheckEdit.Checked = model.TransportReq;
                salesmancheckEdit.Checked = model.SalesmanReq;
                partyGroupCheckEdit.Checked = model.PartyGroupReq;
                taxdetailscheckEdit.Checked = model.TaxationReq;
                priceLevelcheckEdit.Checked = model.PriceLevelReq;
                bankDetailscheckEdit.Checked = model.BankDetailReq;
                collectionDaysCheckEdit.Checked = model.CollDaysReq;
                interestcheckEdit.Checked = model.IntAccountReq;
                deprCheckEdit1.Checked = model.DeprAccountReq;
                taxTypecheckEdit.Checked = model.TaxTypeReq;
                tdsCheckEdit.Checked = model.TdsReq;
                tcsCheckEdit1.Checked = model.TcsReq;
                opBalCheckEdit.Checked = model.OpBalanceReq;
                creditLimitCheckEdit1.Checked = model.CrLimitReq;
                gradeCheckEdit.Checked = Convert.ToBoolean(model.GradeReq);
            
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            model.AddressReq = addresscheckEdit.Checked;
            model.AgentReq = agentcheckEdit.Checked;
            model.TransportReq = transportCheckEdit.Checked;
            model.SalesmanReq = salesmancheckEdit.Checked;
            model.PartyGroupReq = partyGroupCheckEdit.Checked;
            model.TaxationReq = taxdetailscheckEdit.Checked;
            model.PriceLevelReq = priceLevelcheckEdit.Checked;
            model.BankDetailReq = bankDetailscheckEdit.Checked;
            model.CollDaysReq = collectionDaysCheckEdit.Checked;
            model.IntAccountReq = interestcheckEdit.Checked;
            model.DeprAccountReq = deprCheckEdit1.Checked;
            model.TaxTypeReq = taxTypecheckEdit.Checked;
            model.TdsReq = tdsCheckEdit.Checked;
            model.TcsReq = tcsCheckEdit1.Checked;
            model.OpBalanceReq = opBalCheckEdit.Checked;
            model.CrLimitReq = creditLimitCheckEdit1.Checked;
            model.GradeReq = gradeCheckEdit.Checked;
            db.SaveChanges();
            MessageBox.Show("Settings Updates Successfully");
            cancelSimpleButton.PerformClick();
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}

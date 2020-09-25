using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class OutsDto
    {
        public string DrCr { get; set; }

        public string SrNo { get; set; }
        public string BillNo { get; set; }
        public DateTime Date { get; set; }
        public string VoucherName { get; set; }
        public string BookName { get; set; }
        public string Account { get; set; }
        public string GstIn { get; set; }
        public string State { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Agent { get; set; }
        public string CompanyName { get; set; }
        public string MobileNo { get; set; }
        public string GroupName { get; set; }
        public string PartyEmail { get; set; }
        public DateTime DueDate { get; set; }
        public int? DueDays { get; set; }

        public decimal TotalQty { get; set; }
        public decimal TotalPcs { get; set; }

        public decimal GrossAmt { get; set; }
        public decimal BillAmt { get; set; }
        public decimal ReturnAmt { get; set; }
        public decimal AdjustAmt { get; set; }
        public decimal TdsAmt { get; set; }
        public decimal TcsAmt { get; set; }
        public decimal PendingAmt { get; set; }
        //   public string Pending { get; set; }
        public decimal Cess { get; set; }
        public decimal NetRate { get; set; }
        public string Pending
        {
            get
            {
                if (PendingAmt < 0)
                { return ((-1 * PendingAmt).ToString() + "Cr"); }
                else return (PendingAmt.ToString() + "Dr");
            }
        }
        public int? AccountId { get; set; }
        public string Bal { get; set; }
        public int Days { get; set; }
        public int Id { get; set; }
        public int? VTypeId { get; set; }
    }
}

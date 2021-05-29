using System;
using System.Globalization;

namespace Konto.Data.Models.Transaction.TradingDto
{
    public class GreyPurchaseAgainstGoDto
    {
        public string VoucherNo { get; set; }
        public string ChallanNo { get; set; }

        public int ChlnDate { get; set; }
        public DateTime VoucherDate { 
            get
            {

                {
                    return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture));
                }

            }
        }

        public string Party { get; set; }
        public string LotNo { get; set; }
        public int Pcs { get; set; }
        public decimal Mtrs { get; set; }
        public decimal Rate { get; set; }
        public int Id { get; set; }

        
    }
    
    public class JobReceiptAgainstIssue
    {
        public string VoucherNo { get; set; }
        public string ChallanNo { get; set; }

        public int ChlnDate { get; set; }
        public DateTime VoucherDate
        {
            get
            {

                {
                    return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture));
                }

            }
        }
        public string Party { get; set; }
        public string LotNo { get; set; }
        public int IssuePcs { get; set; }
        public decimal IssueMtrs { get; set; }
        public int FinPcs { get; set; }
        public decimal FinMtrs { get; set; }
        public decimal ShPer
        {
            get
            {
                if (this.IssueMtrs > 0)
                    return decimal.Round(Convert.ToInt32(this.ShMtrs) * 100 / Convert.ToDecimal(this.IssueMtrs), 2, System.MidpointRounding.AwayFromZero);
                else return 0;
            }
        }
        public decimal ShMtrs
        {
            get
            {
                return IssueMtrs - FinMtrs;
            }
        }

        public decimal Rate { get; set; }
        public int Id { get; set; }
    }
    public class MillReceiptAgainstOrder
    {
        public string VoucherNo { get; set; }
        public string ChallanNo { get; set; }

        public int ChlnDate { get; set; }
        public DateTime VoucherDate
        {
            get
            {

                {
                    return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture));
                }

            }
        }

        public string Party { get; set; }
        public  string Quality { get; set; }
        public string LotNo { get; set; }
        public int GreyPcs { get; set; }
        public decimal GreyMtrs { get; set; }
        public int FinPcs { get; set; }
        public decimal FinMtrs { get; set; }
        public decimal ShPer { get
            {
                if (this.GreyMtrs > 0)
                    return decimal.Round(Convert.ToInt32(this.ShMtrs) * 100 / Convert.ToDecimal(this.GreyMtrs), 2, System.MidpointRounding.AwayFromZero);
                else return 0;
            }
            }
        public decimal ShMtrs { get
            {
                return GreyMtrs - FinMtrs;
            } }

        public decimal Rate { get; set; }
        public  string JobType { get; set; }
        public int Id { get; set; }
    }
}

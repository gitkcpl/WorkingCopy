using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.TradingDto
{
    public class PendingForCuttingDto
    {
        public  string LotNo { get; set; }

        public  string ChallanNo { get; set; }

        public  int? ProductId { get; set; }

        public  string Product { get; set; }

        public  int? Id { get; set; }

        public  int TransId { get; set; }

        public  DateTime ChallanDate { get; set; }

        public  string TakaNo { get; set; }

        public  decimal? GrayMeter { get; set; }

        public  decimal? FinMeter { get; set; }

        public  int? TakaId { get; set; }

        public  int? ColorId { get; set; }

        public  int? DesignId { get; set; }

        public  string DesignNo { get; set; }

        public  string ColorName { get; set; }

        public  int? GradeId { get; set; }

        public  string GradeName { get; set; }
        public int VoucherId { get; set; }
    }

}

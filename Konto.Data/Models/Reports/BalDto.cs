using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class BalSumDto
    {
        public int? LGroupId { get; set; }
        public string LGroup { get; set; }
        public string LNature { get; set; }
        public decimal? LDiff { get; set; }
        public decimal? LBal { get; set; }
        public int? RGroupId { get; set; }
        public string RGroup { get; set; }
        public string RNature { get; set; }
        public decimal? RDiff { get; set; }
        public decimal? RBal { get; set; }
        public string BalType { get; set; }
        public int BlSort { get; set; }
        public int TlSort { get; set; }



    }
    public class BalDto
    {
        public int? AcgroupId { get; set; }
        public string Acgroup { get; set; }
        public string Nature { get; set; }
        public string AccountName { get; set; }
        public decimal Diff { get; set; }
        public decimal Bal { get; set; }
        public int TransType { get; set; }
        public bool Audit { get; set; }
        public int AcId { get; set; }
        public decimal PreDiff { get; set; }
        public decimal preBal { get; set; }
        public int? BlSort { get; set; }
        public int? TlSort { get; set; }
    }
}

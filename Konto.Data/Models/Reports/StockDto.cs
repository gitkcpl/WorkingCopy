using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class StockDto 
    {
        

      
        public int ItemId { get; set; }
        public string Product { get; set; }
       
      

        public decimal Qty { get; set; }
        public decimal OpQty { get; set; }
        public decimal PurQty { get; set; }
        public decimal InForJobQty { get; set; }
        public decimal TransInQty { get; set; }
        public decimal MillRec { get; set; }
        public decimal JobRec { get; set; }
        public decimal ProdQty { get; set; }
        public decimal SRetQty { get; set; }
        public decimal StoreIssRetQty { get; set; }
        public decimal InFromJobQty { get; set; }

        public decimal SaleQty { get; set; }
        public decimal MillIsQty { get; set; }
        public decimal IsForJobQty { get; set; }
        public decimal SaleJobQty { get; set; }
        public decimal TransOutQty { get; set; }
        public decimal RefIssQty { get; set; }
        public decimal StoreIssQty { get; set; }
        public decimal PRetQty { get; set; }

        public decimal StockQty { get; set; }

        public decimal InwQty { get; set; }
        public decimal OutQty { get; set; }
        public int? InwPcs { get; set; }
        public int? OutPcs { get; set; }

        public int? OpPcs { get; set; }
        public int? PurPcs { get; set; }
        public int? InForJobPcs { get; set; }
        public int? TransInPcs { get; set; }
        public int? MillPcs { get; set; }
        public int? JobPcs { get; set; }
        public int? ProdPcs { get; set; }
        public int? SRetPcs { get; set; }
        public int? StoreIssRetPcs { get; set; }
        public int? InFromJobPcs { get; set; }

        public int? SalePcs { get; set; }
        public int? MillIsPcs { get; set; }
        public int? IsForJobPcs { get; set; }
        public int? SaleJobPcs { get; set; }
        public int? TransOutPcs { get; set; }
        public int? RefIssPcs { get; set; }
        public int? StoreIssPcs { get; set; }
        public int? PRetPcs { get; set; }

        public int? StockPcs { get; set; }

    }
}

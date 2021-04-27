using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class StockDto
    {
        public virtual string Product { get; set; }

        public virtual int ItemId { get; set; }

        public virtual decimal? OpQty { get; set; }

        public virtual decimal? StockQty { get; set; }

        public virtual int? OpPcs { get; set; }

        public virtual int? StockPcs { get; set; }

        public virtual int BranchId { get; set; }

        public virtual decimal PurQty { get; set; }

        public virtual decimal InForJobQty { get; set; }

        public virtual decimal TransInQty { get; set; }

        public virtual decimal MillRec { get; set; }

        public virtual decimal JobRec { get; set; }

        public virtual decimal ProdQty { get; set; }

        public virtual decimal SRetQty { get; set; }

        public virtual decimal StoreIssRetQty { get; set; }

        public virtual decimal InFromJobQty { get; set; }

        public virtual decimal SaleQty { get; set; }

        public virtual decimal MillIsQty { get; set; }

        public virtual decimal IsForJobQty { get; set; }

        public virtual decimal SaleJobQty { get; set; }

        public virtual decimal TransOutQty { get; set; }

        public virtual decimal RefIssQty { get; set; }

        public virtual decimal StoreIssQty { get; set; }

        public virtual decimal InwQty { get; set; }

        public virtual int InwPcs { get; set; }

        public virtual decimal OutQty { get; set; }

        public virtual int OutPcs { get; set; }

        public virtual int? PurPcs { get; set; }

        public virtual int? InForJobPcs { get; set; }

        public virtual int? TransInPcs { get; set; }

        public virtual int? MillPcs { get; set; }

        public virtual int? JobPcs { get; set; }

        public virtual int? ProdPcs { get; set; }

        public virtual int? SRetPcs { get; set; }

        public virtual int? StoreIssRetPcs { get; set; }

        public virtual int? InFromJobPcs { get; set; }

        public virtual int? MillIsPcs { get; set; }

        public virtual int? SalePcs { get; set; }

        public virtual int? StoreIssPcs { get; set; }

        public virtual int? PRetPcs { get; set; }

        public virtual int? IsForJobPcs { get; set; }

        public virtual int? SaleJobPcs { get; set; }

        public virtual int? TransOutPcs { get; set; }

        public virtual int? RefIssPcs { get; set; }

        public virtual decimal SaleRate { get; set; }

        public virtual decimal DealerPrice { get; set; }

        public virtual decimal Mrp { get; set; }

        public virtual decimal? StockValue { get; set; }
    }


}

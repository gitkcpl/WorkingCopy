using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
   public class StoreIssuePara
    {
        public static bool Taka_From_Stock = true;
        public static bool Issue_By_Barcode = false;
        public static bool Store_Issue_Against_Order = false;
        public static bool Editable_Qty = false;
    }


    public class StoreReturnPara
    {
        public static bool Color_Required = false;
        public static bool Batch_Required = false;
        public static bool LotNo_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool Cut_Required = false;
        public static bool Freight_Required = false;

        public static bool Taka_Detail_Required = true;
        public static bool OtherAdd_Required = true;

        public static bool OtherLess_Required = true;
        public static bool Cess_Required = false;
        public static bool Generate_Barcode = false;
    }
}

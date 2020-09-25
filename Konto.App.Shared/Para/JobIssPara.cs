using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class JobIssPara
    {
        public static bool Color_Required = false;
        public static bool Batch_Required = false;
        public static bool LotNo_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool Cut_Required = false;
        public static bool Freight_Required = false;
        public static int Auto_Book_Id = 0;
        public static int Auto_Voucher_Id = 0;
        public static bool Taka_Detail_Required = true;
        public static bool Disable_TakaQty_In_Issue = true;
        public static bool Taka_From_Stock = true;
        public static bool Finish_Product_Requred=true;
        public static bool Issue_By_Barcode = false;
        public static bool Job_Issue_Against_Order = false;

    }
}

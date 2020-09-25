using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class SCPara
    {
        public static bool Color_Required = false;
        public static bool Batch_Required = false;
        public static bool LotNo_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool NewProduct_Required = false;
        public static bool Cut_Required = false;
        public static bool Freight_Required = false;
        public static int Auto_Book_Id = 0;
        public static int Auto_Voucher_Id = 0;
        public static bool Taka_Detail_Required = true;
        public static bool Auto_Bill_Generate_Required = false;
        public static bool Direct_Issue_From_Purchase = true;
        public static bool Direct_Sale_From_Purchase = false;
        public static bool Ask_for_issue_in_job_receipt_in_purchase = true;
        public static bool Disable_TakaQty_In_Issue = true;
        public static bool Taka_From_Stock = true;
        public static bool Jobcard_Required = true;
        public static string Default_Challan_Print = "SalesChallan.rdlx";
    }
}

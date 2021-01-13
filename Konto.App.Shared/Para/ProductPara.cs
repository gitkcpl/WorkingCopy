using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class ProductPara
    {

        public static bool Dual_Stock_Required = false;
        public static bool Product_Search_On_Product_Code = false;
        public static string Product_Popup_Details_Top_List = "ProductName|ProductCode";

        public static string Product_Popup_Details_Bottom = "DealerPrice|SaleRate|Mrp|ClosingStock|HsnCode|Gst%";

        public static bool Group_Required = false;
        public static bool Sub_Group_Required = false;
        public static bool Color_Required = false;
        public static bool Brand_Required = false;
        public static bool Size_Required = false;
        public static bool Category_Required = false;
        public static bool Style_Reqired = false;
        public static bool Vendor_Required = false;
        public static bool Stock_Account_Required = false;
        public static bool Batch_Required = false;
        public static bool Serial_Required = true;
        public static bool Weaving_Detail_Required = false;
        public static bool RS_In_Inch = false;

        public static int Decimal_Point_Unit_1 = 0;
        public static int Decimal_Point_Unit_2 = 0;
        public static int Decimal_Point_Rate = 2;
        public static bool Color_Detail_Required = false;
        public static bool Cost_Rate_Inc_Gst = false;
    }
     
} 
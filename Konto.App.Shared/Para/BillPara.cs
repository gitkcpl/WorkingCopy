using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class BillPara
    {
        public static bool Challan_Required = false;
        public static bool Color_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool Cut_Required = true;
        public static bool Freight_Required = true;
        public static bool OtherAdd_Required = true;
        public static bool OtherLess_Required = true;
        public static bool Cess_Required = false;
        public static bool OceanFreight_Required = false;
        public static bool CustomDuty_Required = false;
        public static bool Tds_Required = false;
        public static bool Tcs_Required = false;
        public static int Auto_Book_Id = 0;
        public static int Auto_Voucher_Id = 0;
        public static bool Tax_RoundOff = false;
        public static string Default_Invoice_Print = "BillPrintTextilesF1.rdlx";
        public static bool Allow_Duplicate_Order_Ecommerce = false;
        public static bool Ask_For_Voucher_Selection = false;
        public static int Rate_Decimal = 2;
        public static int Qty_Decimal = 2;
        public static bool Allow_Gst_Editable = false;
        public static bool Tcs_Round_Off = true;
        public static bool Order_Required= false;
        public static bool HsnCode_Required = false;

    }

    public class SaleRetPara
    {
        public static bool Color_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool Cut_Required = true;
        public static bool Freight_Required = false;
        public static bool OtherAdd_Required = false;
        public static bool OtherLess_Required = false;
        public static bool Cess_Required = false;
        public static bool OceanFreight_Required = false;
        public static bool CustomDuty_Required = false;
        public static bool Tds_Required = false;
        public static bool Tcs_Required = false;
        public static int Auto_Book_Id = 0;
        public static int Auto_Voucher_Id = 0;
        public static bool Tax_RoundOff = false;
        public static bool Ask_For_Voucher_Selection = false;
        public static int Rate_Decimal = 2;
        public static int Qty_Decimal = 2;
        public static bool Allow_Gst_Editable = false;
        public static bool HsnCode_Required = false;

    }
}

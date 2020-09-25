using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class MillRecPara
    {
        public static bool Challan_Required = false;
        public static bool Color_Required = false;
        public static bool Batch_Required = false;
        public static bool LotNo_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool Cut_Required = true;
        public static bool Freight_Required = true;
        public static bool OtherAdd_Required = true;
        public static bool OtherLess_Required = true;
        public static bool Cess_Required = true;
        public static bool OceanFreight_Required = true;
        public static bool CustomDuty_Required = true;
        public static bool Tds_RoundOff = true;
        public static int Auto_Book_Id = 0;
        public static int Auto_Voucher_Id = 0;
        public static bool Taka_Detail_Required = true;
        public static bool FinishMeter_more_than_GreyMeter = false;
        public static bool Generate_Barcode = false;
    }
}

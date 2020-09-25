using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class JobRecPara
    {
        public static bool Challan_Required = true;
        public static bool Color_Required = false;
        public static bool Batch_Required = false;
        public static bool LotNo_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool ShortPcs_Required = false;
        public static bool ShortMtr_Required = false;
        public static bool Cut_Required = true;
        public static bool Freight_Required = false;
        public static bool OtherAdd_Required = false;
        public static bool OtherLess_Required = false;
        public static bool Cess_Required = false;
        public static bool Tds_RoundOff = true;
        public static bool Taka_Detail_Required = false;
        public static bool JobReceipt_Against_Po = false;
        public static bool Generate_Barcode = false;
    }


    public class TakaJobRecPara
    {
        public static bool Color_Required = false;
        public static bool Batch_Required = false;
        public static bool LotNo_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool Cut_Required = true;
        public static bool Freight_Required = true;
        public static bool OtherAdd_Required = true;
        public static bool OtherLess_Required = true;
        public static bool Tds_RoundOff = true;
        public static bool Taka_Detail_Required = true;
        public static bool Challan_Required = true;
    }
}

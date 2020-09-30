using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class GenExpPara
    {
        public static bool Freight_Required = false;
        public static bool OtherAdd_Required = true;
        public static bool OtherLess_Required = true;
        public static bool Cess_Required = false;
        public static bool Tds_Required = false;
        public static bool Tcs_Required = false;
        public static bool TDS_RoundOff = false;
        public static bool Tax_RoundOff = false;
        public static int Rate_Decimal = 2;
        public static int Qty_Decimal = 2;
        public static bool Tds_On_Line_Level = false;
    }
}

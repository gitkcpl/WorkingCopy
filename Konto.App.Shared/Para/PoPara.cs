using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class PoPara
    {
        public static bool Color_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;

        public static bool Grey_Details_Textiles_Required = false;
        public static bool Checker_In_Required = false;
        public static bool Checker_Out_Required = false;
        public static string Default_Order_Status = "APPROVED";
        public static int Min_Odr_Qty = 5;
        public static bool Currency_Required = false;
        public static bool Cess_Required = false;

        public static bool Agent_Reguired = false;
        public static bool Transport_Required = false;
        public static bool Division_Required = false;
        public static bool PartyGroup_Required = false;
        public static bool Order_AssignBy_Required = false;
        public static bool Pay_Terms_Requied = false;

    }

    public class IndentPara
    {
        public static bool Color_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;

        public static bool Grey_Details_Textiles_Required = false;
        public static bool Checker_In_Required = false;
        public static bool Checker_Out_Required = false;
        public static string Default_Order_Status = "PENDING";
        public static int Min_Odr_Qty = 1;
        public static bool Currency_Required = false;
        public static bool Cess_Required = false;
    }
}

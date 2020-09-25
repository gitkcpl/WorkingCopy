﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared.Para
{
    public class SoPara
    {
        public static bool Color_Required = false;
        public static bool Design_Required = false;
        public static bool Grade_Required = false;
        public static bool Division_Required = false;
        public static bool WarpItem_Required = false;
        public static bool Grey_Details_Textiles_Required = false;
        public static bool Checker_In_Required = false;
        public static bool Checker_Out_Required = false;

        public static string Default_Order_Status = "APPROVED";
        public static int Min_Odr_Qty = 5;
        public static bool Generate_SO_to_PO = false;
        public static bool Currency_Required = false;
        public static bool Cut_Required = false;
        public static bool Pcs_Required = false;
    }
}

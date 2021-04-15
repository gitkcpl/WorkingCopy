using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared
{
   
    public enum PurchaseOrderStaus
        {
            Draft = 0, Job_Order = 4, Ordered = 1, Partial_Shipment = 2, Received = 3,
        }

        public enum SaleOrderStatus
        {
            Orders = 0, In_Manufacturing = 5, Partial_Shipped_Invoiced = 6,
            Partial_Shipped_Not_Invoiced = 7, Closed_Shipped_Invoiced = 8
        }
        public enum SaleChallanType
        {
            Sales = 0, Job = 1, Transfer = 2, Purchase_Return = 3, Other = 4
        }
        public enum InwardType
        {
            Purchase = 0, Inward_for_Job = 1, Inward_from_Job = 2,
            Sales_Return = 3, Transfer = 4,
            Others = 5
        }



    public static class KontoGlobals
    {
        

        public static int Edition = 0;
        public static bool IsDevelopment = false;
        public static string DbGroup = "";
            public static string DbName = "";
            public static int UserId = 1;
            public static int EmpId = 1;
            public static int UserRoleId = 1;
            public static bool isSysAdm = false;
            public static int PackageId = 1;
            public static int CompanyId = 1;
            public static string CompanyName = "NA";
            public static string GstIn = "NA";
            public static int YearId = 1;
            public static int MenuId = 1;
            public static int BranchId = 1;
            public static int GodownId = 1;
            public static int ModuleId = 1;
            public static int FromDate = 20190401;
            public static int ToDate = 20200331;
            public static string Pass = "";
            public static DateTime DFromDate = DateTime.ParseExact(FromDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture);
            public static DateTime DToDate = DateTime.ParseExact(ToDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture);
            public static bool LoadLayout = false;
            public static string UserName = "keysoft";
            public static string Conn = ConfigurationManager.ConnectionStrings["KontoContext"].ToString();

        public static SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder(Conn);

            public static string ComputerName = "NA"; //Dns.GetHostEntry(Environment.MachineName).AddressList[2].ToString()+"-" + Dns.GetHostName() ;

            public static string SaveMessage = "Record Saved Successfully";

            public static string DeleteBeforeMsg =
                "Are you sure want to delete selected record..? " + "\n" + "Record will deleted permanently";
            public static string CancelBeforeMsg =
                "Are you sure want to cancel selected record..? " + "\n" + "Record will cancelled";
            public static string RevisedBeforeMsg =
             "Are you sure want to Revised selected record..? " + "\n" + "Record will Revised";

            public static string DeleteAfterMsg = "Selected Record Deleted !!";

            public static string CancelAfterMsg = "Selected Record Cancelled !!";

            public static string DeleteCancelMsg = "Delete Cancelled !!";
            public static string CancelCancelMsg = "Action Cancelled !!";

            public static string ConfirmationTitle = "Confirmation !!";

            public const string DuplicateKeyMsg = "Duplicate Key Can Not Be Accepted";

            public const string DuplicateOrderMsg = "Duplicate Order No. Can Not Be Accepted";
            public const string DuplicateVoucherMsg = "Duplicate Voucher No. Can Not Be Accepted";
            public const string DuplicateInvoiceMsg = "Duplicate Invoice No. Can Not Be Accepted";
            public const string DuplicateTakaMsg = "Duplicate Taka No. Can Not Be Accepted";
            public const string DuplicateProductColor = "This Product and Color already exist";
            public const string DateComparision = "Please Enter valid Date !!!";

            public const string FilterNotFound = "No Record Found..Please Try Again";
            public const string NotViewPermission = "You does not have Viewing Right,Please Contact your System Adminstrator";
            public const string DeleteWarning = "Can not Delete. Ref Used in ";
            public const string EditWarning = "Edit Not Allowd! Entry Used in Challan.";
            public const string BeamCloseMsg = "Beam Close Succesfilly.";
            public const string DeleteFreezeWarning = "Delete/Cancel Not Allowed! Voucher has been freezed.";
            public const string EditFreezeWarning = "Edit Not Allowed! Voucher has been freezed.";
            public const string SaveFreezeWarning = "Save Not Allowed! Voucher has been freezed.";
            //public static int gethello()
            //{
            //    return FromDate;
            //}

            #region "layout file Name"
           // public const string Account_List_Layout = "accList.xml";
            public const string Month_list_layout = "MonthWiseView.xml";

            public const string Normal_list_layout = "NormalView.xml";
            public const string Party_list_layout = "PartyWiseView.xml";
            public const string Detail_list_layout = "DetailWiseView.xml";
            public const string Summary_list_layout = "SummaryView.xml";
            public const string Ageing_Summary_list_layout = "AgeingSummaryView.xml";
            public const string Ageing_Details_list_layout = "AgeingDetailsView.xml";
            public const string FIFO_Ageing_list_layout = "FIFO_AgeingView.xml";
            public const string Ageing_list_layout = "AgeingListView.xml";

            public const string PartySummary_list_layout = "PartySummaryView.xml";
            public const string ProductSummary_list_layout = "ProductSummaryView.xml";
            public const string PartyProductSummary_list_layout = "PartyProductSummaryView.xml";

            public const string TrialBalance_layout = "TrialBalance.xml";
            public const string TrialBalancet1_layout = "TrialBalancet1.xml";
            public const string TrialBalancet2_layout = "TrialBalancet2.xml";
            public const string BalanceSheet1_layout = "BalanceSheet1.xml";
            public const string BalanceSheet2_layout = "BalanceSheet2.xml";
            public const string SalesChallan_Layout = "SalesChallan.xml";
            public const string PurchaseOrder_Layout = "POrder.xml";
            public const string SaleOrder_Layout = "SOrder.xml";
            public const string SaleInvoice_Layout = "SInvoice.xml";


            #endregion

            

    }
        public enum VoucherTypeEnum
        {
            None = 0,
            OpeningAccountBalance = 1,
            Indent = 2,
            PurchaseOrder = 3,
            SalesOrder = 4,
            Inward = 5,
            SalesChallan = 6,
            MillReceipt = 7,
            JobReceipt = 8,
            StoreIssue = 9,
            DesignMapping = 10,
            BeamProd = 11,
            SaleInvoice = 12,
            PurchaseInvoice = 13,
            JournalVoucher = 14,
            ReceiptVoucher = 15,
            PaymentVoucher = 16,
            TakaProd = 17,
            PurchaseReturn = 18,
            SaleReturn = 19,
            Batch = 20,
            YarnProd = 21,
            Cutting = 22,
            GenExpense = 23,
            DebitCreditNote = 24,
            SalePurchaseOpBill = 26,
            ColorRecipe = 27,
            ChemicalRecipe = 28,
            OpTaka = 29,
            JobProcessInvoice = 30,
          //  MillProcessInvoice = 31,
            GrayPurchaseChallan = 32,
            JobCard = 33,
            BOM = 34,
            StoreIssueReturn = 35,
            GrayPurchaseInvoice = 36,
            MillIssue = 37,
            JobIssue = 38,
            GreyOrder = 39,
            MillReceiptVoucher = 40,
            JobReceiptVoucher = 41,
            TakaConv = 42,
            TakaCutting = 43,
            OpMillIssue = 44,
            OpJobIssue = 45,
            OutJobChallan = 46,
            JobAgainstPo = 47,
            GenExpRet = 48,
            MillReturn = 49,
            BrokerageVoucher = 50,
            TakaWiseJobReceipt = 51,
            StockJournal = 52,
            GateInward = 53,
            BomIssue=54,
            Pos_Purchase=55,
            Pos_Invoice=56,
            Stock_Transfer=57

    }
        public enum TypeEnum
        {
            Inward = 1,
            Outward = 2,
            MillReceipt = 3,
            JobReceipt = 4,
            DesignMapping = 5,
            StoreIssue = 6,
            Cutting = 7,
            SO = 8,
            PO = 9,
            PInvoice = 10,
            SInvoice = 11,
            PReturn = 12,
            SReturn = 13,
            MillIssue = 14,
            JobIssue = 15,
            GrayPurchase = 16,
            StoreIssueReturn = 17,
            GreyOrder = 18,
            GrayBill = 19,
            OpMillIssue = 20,
            OpJobIssue = 21,
            JobAgainstOp = 22,
            GenExpRet = 23,
            MillReturn = 24,
            StockJournal = 25
        }
        public enum ChallanTypeEnum
        {
            PURCHASE = 1,
            INWARD_FOR_JOB = 2,
            SALES_RETURN = 3,
            TRANSFER_IN = 4,
            OTHERS = 5,
            SALES_CHALLAN = 6,
            MILL_ISSUE = 7,
            ISSUE_FOR_JOB = 8,
            SALES_JOB = 9,
            TRANSFER_OUT = 10,
            REFINISH_ISSUE = 11,
            PURCHASE_RETURN = 12,
            INWARD_FROM_JOB = 13,
            ALL=14,
            RETURNABLE_ITEM=15,
            NON_RETURNABLE_ITEM=16
            
        }
        public enum ProductTypeEnum
        {
            NA=0,
            GREY = 2,
            FINISH = 3,
            BEAM = 5,
            YARN = 6,
            POY = 7,
            COLOR=12,
            CHEMICAL=13
        }
        public enum EntryViewType
        {
            Main = 0,
            List = 1,
            Dashboard = 2,
            Report = 3,
            Others = 4
        }

        public enum ListViewType
        {
            NormalView = 0,
            PartyWiseView = 1,
            DetailView = 2,
            PartySummaryView = 3,
            ProductSummaryView = 4,
            PartyProductSummaryView = 5
        }


        public enum SpCollectionEnum
        {
            BeamprodList = 2,
            DeliveryAddressList = 7,
            GetOrderApproveList = 10,
            ListofBill = 18,
            PendingBill = 26,
            MachineWiseTakaProdList = 19,
            OutwardBeamProd = 21,
            OutwardprodList = 22,
            PendingBeamLoading = 25,
            PendingChallanOnInvoice = 27,
            PendingChallanOnInvoiceDet = 28,
            PendingForCutting = 29,
            PendingForCuttingDet = 30,
            PendingJOBonChallan = 31,
            PendingJRProd = 33,
            PendingMillReceipt = 34,
            PendingMRProd = 35,
            PendingOrderonChallan = 36,
            PendingOrderonJobCard = 37,
            PendingTransferOut = 39,
            TakaprodList = 43,
            PendingGreyOrderonChallan = 45,
            PendingOJC = 46,
            PendingOJCProd = 47,
            ColorMatchList = 48,
            PendingJobCard = 49,
            JobCardList = 50,
            GreyPurchaseList = 51,
            GetWeftById = 52,
            PendingMillIssue = 53,
            PendingBatchLot = 54,
            PendingOrderonIssue=55,
            PendingReturnableForGrn=56,
           
    }

    public enum Permission : int
    {
        None = 0,
        Create = 1,
        Modify = 2,
        Delete = 3,
        View = 4,
        Export = 5,
        Print = 6,
        Settings = 7
    }

    public enum LedgerGroupEnum
    {
        NONE=0,
        CAPITAL_ACCOUNT=1,
        LOAN_LIABILITY=2,
        SALES_ACCOUNTS=9,
        PURCHASE_ACCOUNTS=10,
        DUTIES_AND_TAXES=19,
        SUNDRY_CREDITORS=21,
        SUNDRY_DEBTORS=25,
        CASH_ACCOUNTS=26,
        BANK_ACCOUNTS=27,
        CREDITORS_FOR_BROKERAGE=31,
        CREDITORS_FOR_TRANSPORTATION=32,
        DIRECT_EXPENSES=12,
        TDS=39,
        TRADING_EXPENSE=29
    }

    public enum PackageType
    {
        TEXTILE_TRADING=1,
        ACCOUNTS=2,
        WEAVING=3,
        YARN=4,
        POS=6,
        CUSTOMIZED=7,
        PROCESS_HOUSE=10,
        INVENTORY=9
    }

}

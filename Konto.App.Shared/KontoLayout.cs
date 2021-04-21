using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared
{
    public class KontoFileLayout
    {
        #region "Master Layout File"
        public const string StateMasterList_Layout = "masters\\statelist.xml";
        public const string CityMasterList_Layout = "masters\\citylist.xml";
        public const string CityLookup_Layout = "masters\\citylookup.xml";
        public const string Catalog_Lookup = "masters\\catalog_lookup.xml";
        public const string Grade_Lookup = "masters\\grade_lookup.xml";
        public const string AreaMasterList_Layout = "masters\\arealist.xml";
        public const string AreaLookup_Layout = "masters\\arealookup.xml";
        public const string CountryMasterList_Layout = "masters\\countrylist.xml";
        public const string Template_Master_List_Layout = "masters\\templates_master_list.xml";
        public const string CompanyMaster_List_Layout = "masters\\companyList.xml";
        public const string BranchMaster_List_Layout = "masters\\branchlist.xml";
        public const string RpSet_List_Layout = "masters\\rpsetlist.xml";
        public const string Product_List_Layout = "masters\\product_list.xml";
        public const string Design_List = "masters\\design_list.xml";
        public const string DivMaster_List_Layout = "masters\\divlist.xml";
        public const string RefBank_List_Layout = "masters\\refbanklist.xml";
        public const string Process_Master_List_Layout = "masters\\processlist.xml";
        public const string StoreMaster_List_Layout = "masters\\storelist.xml";
        public const string ProductType_Master_List_Layout = "masters\\product_Type_list.xml";
        public const string ProductType_lookup_Layout = "masters\\product_Type_lookup.xml";
        public const string Uom_Master_List_Layout = "masters\\uom_list.xml";
        public const string Brand_Master_List_Layout = "masters\\brand_list.xml";
        public const string Grade_list = "masters\\grade_list.xml";
        public const string Group_Master_List_Layout = "masters\\group_list.xml";
        public const string Group_Lookup_Layout = "masters\\group_lookup.xml";
        public const string Process_Lookup_Layout = "masters\\process_lookup.xml";
        public const string RefBank_Lookup_Layout = "masters\\refbank_lookup.xml";
        public const string Brand_Lookup_Layout = "masters\\brand_lookup.xml";
        public const string Category_Lookup_Layout = "masters\\category_lookup.xml";
        public const string Color_Lookup_Layout = "masters\\color_lookup.xml";
        public const string Size_Lookup_Layout = "masters\\size_lookup.xml";
        public const string Product_Lookup_Layout = "masters\\product_lookup.xml";
        public const string Design_Lookup = "masters\\design_lookup.xml";
        public const string Ledger_Group_Lookup_Layout = "masters\\ledger_group_lookup.xml";
        public const string Voucher_Lookup = "masters\\voucher_lookup.xml";
        public const string Sub_Group_Master_List_Layout = "masters\\sub_group_list.xml";
        public const string Sub_Group_Lookup_Layout = "masters\\sub_group_lookup.xml";
        public const string Size_Master_List_Layout = "masters\\size_list.xml";
        public const string Color_Master_List_Layout = "masters\\color_list.xml";
        public const string Voucher_List_Layout = "masters\\voucher_list.xml";
        public const string Voucher_Type_List = "masters\\vtype_list.xml";
        public const string Category_Master_List_Layout = "masters\\category_list.xml";
        public const string Tax_Master_List_Layout = "masters\\tax_list.xml";
        public const string Catalogue_Master_List_Layout = "masters\\Catalogue_list.xml";
        public const string AcGroup_Master_List_Layout = "masters\\acgroup_list.xml";
        public const string Party_Group_Master_List_Layout = "masters\\party_group_list.xml";
        public const string Party_Group_Lookup_Layout = "masters\\party_group_lookup.xml";
        public const string Emp_Master_List_Layout = "masters\\emp_list.xml";
        public const string Haste_Master_List_Layout = "masters\\haste_list.xml";
        public const string Emp_Lookup_List_Layout = "masters\\emp_lookup.xml";
        public const string Haste_Lookup_List_Layout = "masters\\haste_lookup.xml";

        public const string AccountMaster_Layout = "masters\\accIndex.xml";

        public const string Voucher_Type = "masters\\VTypeIndex.xml";
        
        public const string Voucher_Master = "masters\\VoucherIndex.xml";

        public const string Product_Master_Layout = "masters\\productIndex.xml";
        public const string Design_Master = "masters\\DesignIndex.xml";
        public const string Account_Master_List_Layout = "masters\\acc_list.xml";
        public const string Acc_Lookup_List_Layout = "masters\\acc_lookup_list.xml";
        public const string Address_Lookup_List_Layout = "masters\\address_lookup_list.xml";
        public const string Acc_Opbal_List_Layout = "masters\\acc_opbal_list.xml";
        public const string Item_Opbal_List_Layout = "masters\\item_opbal_list.xml";
        public const string Acc_Address_List_Layout = "masters\\acc_address_list.xml";
        public const string Role_List_Layout = "masters\\role_list.xml";
        public const string User_List_Layout = "masters\\User_list.xml";

        public const string PackingType_List_Layout = "masters\\pack_type_list.xml";
        public const string CostHead_List_Layout = "masters\\cost_head_list.xml";

        public const string Customer_Master_List_Layout= "masters\\customer_master_list.xml";

        #endregion

        #region opening
        public const string Op_Bill_List = "op\\op_bill_list.xml";
        public const string Op_Bill_Main = "op\\op_bill.xml";
        #endregion
        #region Transaction

        public const string Gate_Inward_Index = "po\\gate_inward_index.xml";


        public const string Indent_Index = "po\\indent_index.xml";

        public const string Indent_Trans = "po\\indent_trans.xml";
        public const string So_Index = "so\\so_index.xml";
        public const string So_Trans = "so\\so_trans.xml";
        public const string Po_Index = "po\\po_index.xml";
        public const string Po_Trans = "po\\po_trans.xml";

        public const string GreyOrder_Index = "go\\grey_order_index.xml";
        public const string GreyOrder_Trans = "go\\grey_order_trans.xml";
        public const string Grey_Order_Purchase_Layout = "go\\go_purchase.xml";
        public const string Grey_Order_Issue_Layout = "go\\go_issue.xml";
        public const string Grey_Order_Mill_Receipt_Layout = "go\\go_mill_rcpt.xml";

        public const string GRN_Yarn_Item_Details = "grn\\grn_yarn_item_details.xml";
        public const string GRN_Beam_Item_Details = "grn\\grn_beam_item_details.xml";
        public const string GRN_Grey_Item_Details = "grn\\grn_Grey_item_details.xml";
        public const string GRN_Finish_Item_Details = "grn\\grn_finish_item_details.xml";


        public const string Sc_Yarn_Item_Details = "sc\\sc_yarn_item_details.xml";
        public const string Sc_Beam_Item_Details = "sc\\sc_beam_item_details.xml";
        public const string Sc_Grey_Item_Details = "sc\\sc_Grey_item_details.xml";
        public const string Sc_Finish_Item_Details = "sc\\sc_finish_item_details.xml";


        public const string Stock_Yarn_Item_Details = "sc\\stock_yarn_item_details.xml";
        public const string Stock_Beam_Item_Details = "sc\\stock_beam_item_details.xml";
        public const string Stock_Grey_Item_Details = "sc\\Stock_Grey_item_details.xml";
        public const string Stock_Finish_Item_Details = "sc\\stock_finish_item_details.xml";


        public const string Op_Mill_Stock_Index = "op\\op_mill_stock_index.xml";
        public const string Op_Mill_Stock_Trans = "op\\op_mill_stock_trans.xml";

        public const string Op_Job_Issue_Index = "op\\op_job_issue_index.xml";
        public const string Op_Job_Issue_Trans = "op\\op_job_issue_trans.xml";


        public const string Gp_Index = "trans\\gp_index.xml";
        public const string Gp_Trans = "trans\\gp_trans.xml";

        public const string Mi_Index = "mi\\mi_index.xml";
        public const string Mi_Trans = "mi\\mi_trans.xml";
        public const string Mill_Issue_Grey_Item_Details = "mi\\mill_issue_grey_item_details.xml";


        public const string Job_Issue_Index = "ji\\job_index.xml";
        public const string Job_Issue_Trans = "ji\\job_trans.xml";
        public const string Job_Issue_Grey_Item_Details = "ji\\job_issue_grey_item_details.xml";
        public const string Job_Issue_Yarn_Item_Details = "ji\\job_issue_yarn_item_details.xml";
        public const string Job_Issue_Beam_Item_Details = "ji\\job_issue_beam_item_details.xml";
        public const string Job_Issue_Finish_Item_Details = "jis\\job_issue_finish_item_details.xml";

        public const string Mrv_Index = "mrv\\mrv_index.xml";
        public const string Mrv_Trans = "mrv\\mrv_trans.xml";
        
        public const string Ojc_Index = "mrv\\ojc_index.xml";
        public const string Ojc_Trans = "mrv\\ojc_trans.xml";


        public const string Mrv_Taka_Detail = "mrv\\mrv_taka.xml";
        public const string Mrv_Pending_Detail = "mrv\\mrv_pending_trans.xml";
        
        public const string OJC_Pending_Detail = "mrv\\ojc_pending_trans.xml";

        public const string Mr_Index = "mr\\mr_index.xml";
        public const string Mr_Trans = "mr\\mr_trans.xml";
        public const string Mr_Taka_Detail = "mr\\mr_taka.xml";
        public const string Mr_Stock_Taka_Detail = "mr\\mr_stock_taka.xml";

        public const string Gen_Expense_Index = "exp\\gen_exp_index.xml";
        public const string Gen_Expense_Trans = "exp\\gen_exp_trans.xml";

        public const string DrCr_Index = "drcr\\drcr_index.xml";
        public const string DrCr_Trans = "drcr\\drcr_trans.xml";

        public const string Grn_Index = "grn\\grn_index.xml";
        public const string Grn_Trans = "grn\\grn_trans.xml";

        public const string Sc_Index = "sc\\sc_index.xml";
        public const string Sc_Trans = "sc\\sc_trans.xml";

        public const string Stock_Transfer_Index = "sc\\st_index.xml";
        public const string Stock_Transfer_Trans = "sc\\st_trans.xml";

        public const string Purchase_Index = "PI\\purchase_index.xml";
        public const string Purchase_Trans = "PI\\purchase_trans.xml";

        public const string Purchase_Return_Index = "pr\\purchase_return_index.xml";
        public const string Purchase_Return_Trans = "pr\\purchase_return_trans.xml";

        public const string Sales_Index = "SI\\sales_index.xml";
        public const string Sales_Trans = "SI\\sales_trans.xml";

       

        public const string Sales_Return_Index = "SR\\sales_return_index.xml";
        public const string Sales_Return_Trans = "SR\\sales_return_trans.xml";

        public const string Receipt_Index = "trans\\receipt_index.xml";
        public const string Receipt_Trans = "trans\\receipt_trans.xml";

        public const string Payment_Index = "trans\\payment_index.xml";
        public const string Payment_Trans = "trans\\payment_trans.xml";


        public const string Journal_Index = "trans\\journal_index.xml";
        public const string Journal_Trans = "trans\\journal_trans.xml";

        public const string Pending_Bill = "trans\\pending_bill.xml";

        public const string Jrv_Index = "jrv\\jrv_index.xml";
        public const string Jrv_Trans = "jrv\\jrv_trans.xml";
        public const string Jrv_Pending_Detail = "jrv\\mrv_pending_trans.xml";

        public const string Job_Receipt_Against_Issue_Layout = "jrv\\job_rcpt_against_issue.xml";

        public const string StockJournal_Index = "trans\\StockJournal_Index.xml";
        public const string StockJournal_Trans = "trans\\StockJournal_Trans.xml";

        public const string StoreIssue_Index = "trans\\StoreIssue_Index.xml";
        public const string StoreIssue_Trans = "trans\\StoreIssue_Trans.xml";

        public const string SIReturn_Index = "trans\\SIReturn_Index.xml";
        public const string SIReturn_Trans = "trans\\SIReturn_Trans.xml";


        public const string HSN_Detail_List = "trans\\HSN_Detail_List.xml";

        public const string Cutting_Detail_Window = "trans\\cutting_detail.xml";
        public const string Cutting_Index = "trans\\cutting_index.xml";
        public const string Cutting_Trans= "trans\\cutting_trans.xml";
        public const string Cutting_Pending_Detail= "trans\\cutting_pending_detail.xml";

        public const string TakaJR_Index = "takajr\taka_jr_index.xml";
        public const string TakaJR_Trans = "takajr\taka_jr_trans.xml";
        #endregion

        #region report
        public const string Ledger_Details_View = "trans\\ledger_details_view.xml";
        public const string Outs_Details_View = "trans\\outs_details_view.xml";
        public const string Outs_Summary_View = "trans\\outs_summary_view.xml";
        public const string Outs_Ageing_Summary_View = "trans\\outs_ageing_summary.xml";
        public const string Outs_Ageing_Details_View = "trans\\outs_ageing_details.xml";
        #endregion

        #region Weaving

        public const string BeamProd_Index = "Weaving\\BeamProd_Index.xml";
        public const string BeamProd_Trans = "Weaving\\BeamProd_Trans.xml";
        public const string BeamProd_List = "Weaving\\BeamProd_List.xml";
        public const string BeamLoading_Index = "Weaving\\beam_loading_Index.xml";
        public const string BeamLoading_Trans = "Weaving\\beam_loading_Trans.xml";
        public const string BeamLoading_List = "Weaving\\beam_loading_List.xml";

        public const string TakaProd_Index = "Weaving\\TakaProd_Index.xml";
        public const string TakaProd_Trans = "Weaving\\TakaProd_Trans.xml";
        public const string TakaProd_List = "Weaving\\TakaProd_List.xml";

        public const string TakaConv_Index = "Weaving\\TakaConv_Index.xml";
        public const string TakaConv_Trans = "Weaving\\TakaConv_Trans.xml";
        public const string TakaConv_List = "Weaving\\TakaConv_List.xml";

        public const string TakaCutting_Index = "Weaving\\TakaCutting_Index.xml";
        public const string TakaCutting_List = "Weaving\\TakaCutting_List.xml";
        public const string TakaCutting_Trans = "Weaving\\TakaCutting_Trans.xml";

        public const string JobCard_Index = "Weaving\\JobCard_Index.xml";
        public const string JobCard_List = "Weaving\\JobCard_List.xml";
        public const string JobCard_Trans = "Weaving\\JobCard_Trans.xml";

        public const string ColorMatching_Index = "Weaving\\ColrMatching_Index.xml";
        public const string ColorMaching_List = "Weaving\\ColorMaching_List.xml";
        public const string ColorMatching_Trans = "Weaving\\ColorMatching_Trans.xml";

        public const string TakaOp_Index = "Weaving\\TakaOp_Index.xml";
        public const string TakaOp_List = "Weaving\\TakaOp_List.xml";
        public const string TakaOp_Trans = "Weaving\\TakaOp_Trans.xml";

        #endregion

        #region Yarm
        public const string Batch_Index = "yarn\\batch_index.xml";
        public const string Batch_List = "yarn\\batch_list.xml";
        public const string Batch_Trans = "yarn\\batch_trans.xml";

        public const string Yarn_Packing_Index = "yarn\\packing_index.xml";
        public const string Yarn_Packing_List = "yarn\\packing_list.xml";
        public const string Yarn_Packing_Trans = "yarn\\packing_trans.xml";

        public const string ColorFormula_Index= "yarn\\color_formula.xml";
        public const string ColorFormula_Trans = "yarn\\color_formula_trans.xml";

        public  const string YarnJobCard_Index = "yarn\\jobcard_index.xml";
        public const string YarnJobCard_Trans = "yarn\\jobcard_trans.xml";
        public const string ColorFormula_List= "yarn\\color_formula_list.xml";
        public const string YarnJobCard_List= "yarn\\jobcard_list.xml";
        #endregion

        // apparel
        public const string BOM_Layout = "apparel\\BOM_Layout.xml";
        public const string BOM_List_Layout = "apparel\\BOM_List_Layout.xml";
        public const string Inw_List_Layout = "apparel\\inw_list_layout.xml";
        public const string QC_List_Layout = "apparel\\qc_list_layout.xml";
        public const string Barcode_List_Layout = "apparel\\barcode_list_layout.xml";


        // POS

        #region POS

        public const string Pos_Purchase_Index = "pos\\purchas_index.xml";
        public const string Pos_Purchase_Trans = "pos\\purchas_trans.xml";

        public const string Pos_Sales_Index = "pos\\sales_index.xml";
        public const string Pos_Sales_Trans = "pos\\sales_trans.xml";

        public const string Pos_Sales_Return_Index = "pos\\sales_return_index.xml";
        public const string Pos_Sales_Return_Trans = "pos\\sales_return_trans.xml";

        public const string Pos_Purchase_Return_Index = "pos\\purchas_return_index.xml";
        public const string Pos_Purchase_Return_Trans = "pos\\purchas_return_trans.xml";

        public const string Pay_Mode_Master_List = "masters\\pay_model_list.xml";

        #endregion
    }
}

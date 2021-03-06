﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class OpPurchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Oppurchase ID is required")]
        [Display(Name = "Oppurchase ID")]
        public int OppurchaseID { get; set; }

        [Display(Name = "Voucher ID")]
        public long? VoucherID { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Display(Name = "Account ID")]
        public long? AccountID { get; set; }

        [Display(Name = "Agent ID")]
        public long? AgentID { get; set; }

        [Display(Name = "Voucher Date")]
        public int? VoucherDate { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Challan No")]
        public string ChallanNo { get; set; }

        [Display(Name = "Bill Amount")]
        public decimal? BillAmount { get; set; }

        [Display(Name = "Pre Paid")]
        public decimal? PrePaid { get; set; }

        [Display(Name = "Balance")]
        public decimal? Balance { get; set; }

        [Required(ErrorMessage = "Bill Close is required")]
        [Display(Name = "Bill Close")]
        public bool BillClose { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Company ID")]
        public long? CompanyID { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Division ID")]
        public int? DivisionID { get; set; }

        [Display(Name = "Gross Amount")]
        public decimal? GrossAmount { get; set; }

        [Display(Name = "Purchase Ac ID")]
        public long? PurchaseAcID { get; set; }

        [Display(Name = "Disc Per")]
        public decimal? DiscPer { get; set; }

        [Display(Name = "Disc Amount")]
        public decimal? DiscAmount { get; set; }

        [Display(Name = "Wt Less Qty")]
        public decimal? WtLessQty { get; set; }

        [Display(Name = "Other Less")]
        public decimal? OtherLess { get; set; }

        [Display(Name = "Add Amount")]
        public decimal? AddAmount { get; set; }

        [Display(Name = "RD Amount")]
        public decimal? RDAmount { get; set; }

        [Display(Name = "Wt Less Rate")]
        public decimal? WtLessRate { get; set; }

        [Display(Name = "Wt Less Amount")]
        public decimal? WtLessAmount { get; set; }

        [Display(Name = "Sweet Amount")]
        public decimal? SweetAmount { get; set; }

        [Display(Name = "RG Amount")]
        public decimal? RGAmount { get; set; }

        [Display(Name = "Add Per")]
        public decimal? AddPer { get; set; }

        [Display(Name = "Add Amount2")]
        public decimal? AddAmount2 { get; set; }

        [Display(Name = "DD Com Amount")]
        public decimal? DDComAmount { get; set; }

        [Display(Name = "Dispute Amount")]
        public decimal? DisputeAmount { get; set; }

        [Display(Name = "Fold Amount")]
        public decimal? FoldAmount { get; set; }

        [Display(Name = "Total Add Less")]
        public decimal? TotalAddLess { get; set; }

        [Display(Name = "Tds Per")]
        public decimal? TdsPer { get; set; }

        [Display(Name = "Tds Amount")]
        public decimal? TdsAmount { get; set; }

        [Display(Name = "Tds Account ID")]
        public long? TdsAccountID { get; set; }

        [Display(Name = "Tds Date")]
        public int? TdsDate { get; set; }

        [Display(Name = "Interest")]
        public decimal? Interest { get; set; }

        [Display(Name = "Total Amount")]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "Pay Date")]
        public int? PayDate { get; set; }

        [Required(ErrorMessage = "Op Code is required")]
        [Display(Name = "Op Code")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OpCode { get; set; }

        //[ForeignKey("voucher_id")]
        //public voucher Voucher { get; set; }

        //[ForeignKey("AccountID")]
        //public account Account { get; set; }

        //[ForeignKey("AgentID")]
        //public account Account1 { get; set; }

        //[ForeignKey("UnitID")]
        //public Unit_M UnitM { get; set; }

        //[ForeignKey("DivisionID")]
        //public DivisionM DivisionM { get; set; }

        //public List<OpPurchaseTrans> OpPurchaseTrans { get; set; }
    }
}


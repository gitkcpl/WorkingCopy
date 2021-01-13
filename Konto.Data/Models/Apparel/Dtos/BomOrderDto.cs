using System;

using System.Globalization;

namespace Konto.Data.Models.Apparel.Dtos
{
    public class BomOrderDto
    {
        public int Id
        {
            get; set;
        } // int

        public int BomId { get; set; }

        public int OrderTransId
        {
            get; set;
        } // int
        public int ProductId
        {
            get; set;
        } // int

      
        public string RefNo
        {
            get; set;
        } // varchar(max)

 
        public int OrderDate
        {
            get; set;
        } // int

        public DateTime? ODate
        {
            get
            {
                if (OrderDate != 0)
                { return (DateTime.ParseExact(OrderDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        } // int

        
        public string OrderNo
        {
            get; set;
        } // varchar(max) // bigint

        public decimal Qty
        {
            get; set;
        } // numeric(18,2)

        
        
        public string Remark1
        {
            get; set;
        } // varchar(max)
        public string Remark2
        {
            get; set;
        } // varchar(max)

        public string Status
        {
            get; set;
        } // varchar(500)

        
        
        public string AccName
        {
            get; set;
        } //  
        public int AccId
        {
            get; set;
        } //

        public string ProductName
        {
            get; set;
        }

       
        public decimal WipStock { get; set; }
        public decimal StockQty { get; set; }
        public decimal WareHouseStock { get; set; }
        public decimal Balance { get {
                return Qty -StockQty;
            } set { }
        }
        public int BarcodeQty { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
    }
}

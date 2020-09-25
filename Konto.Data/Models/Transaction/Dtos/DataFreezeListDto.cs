using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{

    public class DataFreezeListDto
    {
   public int Id { get; set; }
    public int? FromDate { get; set; }
    public int? ToDate { get; set; }
    public long? VoucherTypeID { get; set; }
    public bool? Freeze { get; set; }
    public string Pass { get; set; }
    public string Voucher { get; set; }
    public int? CompanyID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class Invoices: BaseEntity
    {
        public int? ExpensesId { get; set; }
        public string? Folio { get; set; }
        public string? FilePath1 { get; set; }
        public string? FileURL1 { get; set; }
        public string? FilePath2 { get; set; }
        public string? FileURL2 { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public virtual Expenses Expenses { get; set; }
    }
}

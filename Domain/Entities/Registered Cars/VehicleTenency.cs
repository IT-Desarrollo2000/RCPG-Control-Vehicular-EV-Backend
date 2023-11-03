using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleTenency : BaseEntity
    {
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; } 
        public DateTime TenencyPaymentDate { get; set; }
        public int TenencyYear { get; set; }
        public decimal TenencyCost { get; set; }
        public int? ExpenseId { get; set; }
        public virtual Expenses? Expense { get; set; }
    }
}

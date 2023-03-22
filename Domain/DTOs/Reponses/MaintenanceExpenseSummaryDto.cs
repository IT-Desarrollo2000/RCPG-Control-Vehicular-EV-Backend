using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class MaintenanceExpenseSummaryDto
    {
        public MaintenanceExpenseSummaryDto()
        {
            ExpensesSummary = new List<ExpenseSummary>();
        }
        public int MaintenanceId { get; set; }
        public decimal ExpenseTotal { get; set; }
        public List<ExpenseSummary> ExpensesSummary { get; set; }
        
    }

    public class ExpenseSummary
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public bool Invoiced { get; set; }
        public string ERPFolio { get; set; }
        public string? Comment { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}

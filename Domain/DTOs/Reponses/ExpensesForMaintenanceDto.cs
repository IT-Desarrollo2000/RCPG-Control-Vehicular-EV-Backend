using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class ExpensesForMaintenanceDto
    {
        public ExpensesForMaintenanceDto()
        {
            Vehicles = new List<VehiclesDto>();
            PhotosOfSpending = new List<PhotosOfSpendingDto>();
        }
        public int Id { get; set; }
        public TypesOfExpensesDto TypesOfExpenses { get; set; }
        public decimal Cost { get; set; }
        public bool Invoiced { get; set; }
        public DateTime ExpenseDate { get; set; }
        public List<VehiclesDto> Vehicles { get; set; }
        public string MechanicalWorkshop { get; set; }
        public string ERPFolio { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public MaintenanceWorkshopForMaintenanceDto? VehicleMaintenanceWorkshop { get; set; }
        public List<PhotosOfSpendingDto> PhotosOfSpending { get; set; }
        public VehicleReportDto VehicleReport { get; set; }
        public int? VehicleMaintenanceId { get; set; }
        public int? VehicleServiceId { get; set; }
        public string? Comment { get; set; }
        public int? DepartmentId { get; set; }
        public DepartamentDto Department { get; set; }
    }
}

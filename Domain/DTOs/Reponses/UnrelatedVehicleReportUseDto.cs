using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class UnrelatedVehicleReportUseDto
    {
        public int Id { get; set; }
        public double? InitialMileage { get; set; }
        public double? FinalMileage { get; set; }
        public ReportUseType StatusReportUse { get; set; }
        public string? Observations { get; set; }
        public int? ChecklistId { get; set; }
        public DateTime UseDate { get; set; }
        public int? UserProfileId { get; set; }
        public int? AppUserId { get; set; }
        public CurrentFuel? CurrentFuelLoad { get; set; }
        public bool Verification { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public int DriverUserId { get; set; }
        public string DriverName { get; set; }
        public int ApprovedByAdminUserId { get; set; }
        public string ApprovedByAdminName { get; set; }
    }
}

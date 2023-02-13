using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class VehicleReportUseDto
    {
        public VehicleReportUseDto() 
        {
            VehicleReport = new List<VehicleReportSlimDto>();
            Destinations = new List<DestinationOfReportUseDto>();
        }

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
        public ChecklistDto Checklist { get; set; }
        public List<VehicleReportSlimDto> VehicleReport { get; set; }
        public int DriverUserId { get; set; }
        public string DriverName { get; set; }
        public int ApprovedByAdminUserId { get; set; }
        public string ApprovedByAdminName { get; set; }
        public List<DestinationOfReportUseDto> Destinations { get; set; }
    }
}

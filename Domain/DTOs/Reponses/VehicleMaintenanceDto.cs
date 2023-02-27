﻿using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Domain.Enums;

namespace Domain.DTOs.Reponses
{
    public class VehicleMaintenanceDto
    {
        public int Id { get; set; }
        public string ReasonForMaintenance { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public VehicleServiceStatus Status { get; set; }

        public int? InitialMileage { get; set; }
        public CurrentFuel? InitialFuel { get; set; }
        public int? FinalMileage { get; set; }
        public CurrentFuel? FinalFuel { get; set; }

        public int VehicleId { get; set; }
        public UnrelatedVehiclesDto Vehicle { get; set; }

        public int? WorkShopId { get; set; }
        public virtual MaintenanceWorkShopSlimDto? WorkShop { get; set; }

        public int? ApprovedByUserId { get; set; }
        public string? ApprovedByAdminName { get; set; }

        public int? ReportId { get; set; }
        public VehicleReportSlimDto Report { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

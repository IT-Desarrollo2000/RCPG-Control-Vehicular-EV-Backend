﻿using Domain.Enums;

namespace Domain.DTOs.Reponses
{
    public class VehicleReportDto
    {
        public VehicleReportDto()
        {
            VehicleReportImages = new List<VehicleReportImageDto>();
            Expenses = new List<UnrelatedExpensesDto>();
            Maintenances = new List<VehicleMaintenanceDto>();
        }

        public int Id { get; set; }
        public ReportType ReportType { get; set; }
        public int VehicleId { get; set; }
        public UnrelatedVehiclesDto Vehicle { get; set; }
        public string? Commentary { get; set; }
        public int? MobileUserId { get; set; }
        public string? MobileUserName { get; set; }
        public int? AdminUserId { get; set; }
        public string? AdminUserName { get; set; }
        public DateTime ReportDate { get; set; }
        public List<UnrelatedExpensesDto> Expenses { get; set; }
        public List<VehicleReportImageDto> VehicleReportImages { get; set; }
        public bool IsResolved { get; set; }
        public double? GasolineLoadAmount { get; set; }
        public int? GasolineCurrentKM { get; set; }
        public string? ReportSolutionComment { get; set; }
        public ReportStatusType ReportStatus { get; set; }
        public int? VehicleReportUseId { get; set; }
        public UnrelatedVehicleReportUseDto? VehicleReportUses { get; set; }
        public int? SolvedByAdminUserId { get; set; }
        public string? SolvedByAdminUserName { get; set; }
        public double? AmountGasoline { get; set; }
        public List<VehicleMaintenanceDto> Maintenances { get; set; }
    }
}

﻿using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Enums;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleReportUse : BaseEntity
    {
        public VehicleReportUse()
        {
            this.VehicleReport = new HashSet<VehicleReport>();
            this.Destinations = new HashSet<DestinationOfReportUse>();
        }

        public int VehicleId { get; set; }
        public double? InitialMileage { get; set; }
        public double? FinalMileage { get; set; }
        public ReportUseType StatusReportUse { get; set; }
        public string? Observations { get; set; }
        public int? ChecklistId { get; set; }
        public DateTime? UseDate { get; set; }
        public int? UserProfileId { get; set; }
        public int? AppUserId { get; set; }
        public int? CurrentChargeLoad { get; set; }
        public int? LastChargeLoad { get; set; }
        public bool? Verification { get; set; }
        public int? InitialCheckListId { get; set; }
        public int? FinishedByDriverId { get; set; }
        public double? InitialLatitude { get; set; }
        public double? InitialLongitude { get; set; }

        public virtual UserProfile? FinishedByDriver { get; set; }
        public int? FinishedByAdminId { get; set; }
        public virtual AppUser? FinishedByAdmin { get; set; }
        public virtual Checklist? InitialCheckList { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Checklist? Checklist { get; set; }
        public virtual UserProfile? UserProfile { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<DestinationOfReportUse> Destinations { get; set; }
        public virtual ICollection<VehicleReport> VehicleReport { get; set; }
    }
}

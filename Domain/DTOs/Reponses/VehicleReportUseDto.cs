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
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public double? FinalMileage { get; set; }
        public ReportUseType StatusReportUse { get; set; }
        public string Observations { get; set; }
        public int? ChecklistId { get; set; }
        public DateTime UseDate { get; set; }
        public int? UserProfileId { get; set; }
        public int? AppUserId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Checklist Checklist { get; set; }
        public ICollection<VehicleReport?> VehicleReport { get; set; }
        public virtual UserProfile? UserProfile { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public ICollection<DestinationOfReportUse> Destinations { get; set; }
    }
}

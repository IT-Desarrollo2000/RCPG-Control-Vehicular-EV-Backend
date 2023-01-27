﻿using Domain.Entities.Identity;
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
    public class VehicleReportDto
    {
        public int Id { get; set; }
        public ReportType ReportType { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public string Commentary { get; set; }
        public int? UserProfileId { get; set; }
        public virtual UserProfile? UserProfile { get; set; }
        public int? AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public DateTime ReportDate { get; set; }
        public virtual ICollection<Expenses?> Expenses { get; set; }
        public virtual ICollection<VehicleReportImage?> VehicleReportImages { get; set; }
        public bool IsResolved { get; set; }
        //Aquí va el Id del reporte de uso que aun no esta implementado
        public GasolineLoadType? GasolineLoad { get; set; }
        public string ReportSolutionComment { get; set; }
        public ReportStatusType ReportStatus { get; set; }
    }
}

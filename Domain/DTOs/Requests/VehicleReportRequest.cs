using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleReportRequest
    {
        public ReportType ReportType { get; set; }
        public int VehicleId { get; set; }
        public string Commentary { get; set; }
        public int? UserProfileId { get; set; }
        public int? AppUserId { get; set; }
        public DateTime ReportDate { get; set; }
        public bool IsResolved { get; set; }
        //Aquí va el Id del reporte de uso que aun no esta implementado
        public GasolineLoadType? GasolineLoad { get; set; }
        public string ReportSolutionComment { get; set; }
        public ReportStatusType ReportStatus { get; set; }
        public int? VehicleReportUseId { get; set; }
    }
}

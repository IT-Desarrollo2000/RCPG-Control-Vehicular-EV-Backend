using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleReportRequest
    {
        public VehicleReportRequest()
        {
            ReportImages = new List<IFormFile>();
            Expenses = new List<int>();
        }
        [Required]
        public ReportType ReportType { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public string Commentary { get; set; }
        public int? AdminUserId { get; set; }
        public int? MobileUserId { get; set; }
        [Required]
        public DateTime ReportDate { get; set; }
        public int? GasolineLoadAmount { get; set; }
        public int? GasolineCurrentKM { get; set; }
        public int? VehicleReportUseId { get; set; }
        public List<int> Expenses { get; set; }
        public List<IFormFile> ReportImages { get; set; }
    }
}

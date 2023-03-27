using Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class MaintenanceSpotlightDto
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public VehicleServiceType Type { get; set; }
        public int? CurrentKM { get; set; }
        public int? LastServiceId { get; set; }
        public int ServicePeriodMonths { get; set; }
        public int ServicePeriodKM { get; set; }
        public string StatusMessage { get; set; }
        public string StatusColor { get; set; }
        public string StatusName { get; set; }
        public StopLightAlert AlertType { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public int? NextServiceKM { get; set; }
    }
}

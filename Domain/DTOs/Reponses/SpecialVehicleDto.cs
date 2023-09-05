using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class SpecialVehicleDto
    {
        public SpecialVehicleDto() 
        {
            VehicleImages = new List<VehicleImageDto>();
            VehicleServices = new List<VehicleServiceShortDto>();
            VehicleMaintenances = new List<VehicleMaintenanceShortDto>();
            AssignedDepartments = new List<ShortDepartmentDto>();
        }

        public int Id { get; set; }
        public string Serial { get; set; }
        public string? VehicleResponsibleName { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public int? ModelYear { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public int? PropietaryId { get; set; }
        public PropietaryDto? Propietary { get; set; }
        public ShortPolicyDto? Policy { get; set; }
        public List<VehicleImageDto> VehicleImages { get; set; }
        public List<VehicleServiceShortDto> VehicleServices { get; set; }
        public List<VehicleMaintenanceShortDto> VehicleMaintenances { get; set; }
        public List<ShortDepartmentDto> AssignedDepartments { get; set; }
    }
}

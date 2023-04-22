using Domain.Enums;

namespace Domain.DTOs.Reponses
{
    public class GetVehicleActiveDto
    {
        public GetVehicleActiveDto()
        {
            VehicleDepartments = new List<UnrelatedDepartamentDto>();
        }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public int DriverUserId { get; set; }
        public string DriverName { get; set; }
        public double? InitialLatitude { get; set; }
        public double? InitialLongitude { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public List<UnrelatedDepartamentDto> VehicleDepartments { get; set; }
        public ICollection<UnrelatedDestinationOfReportUseDto> Destinations { get; set; } //ojo
    }
}

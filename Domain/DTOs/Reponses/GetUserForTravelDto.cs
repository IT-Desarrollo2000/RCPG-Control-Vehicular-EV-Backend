using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class GetUserForTravelDto
    {
        public GetUserForTravelDto() 
        {
            AssignedDepartments = new List<DepartamentDto>();
        }
        
        public List<string> VehicleName { get; set; }
        public int UserDriverId { get; set; } = 0;
        public string UserName { get; set; }
        public int TripNumber { get; set; }
        public string? ProfileImageURL { get; set; }
        public List<DepartamentDto> AssignedDepartments { get; set; }
        public string? error { get; set; }
        
    }

}

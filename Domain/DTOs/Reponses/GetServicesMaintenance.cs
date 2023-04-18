using Domain.Entities.Departament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class GetServicesMaintenance
    {
        public GetServicesMaintenance() 
        {
            AssignedDepartments = new List<DepartamentDto>();
        }

        public int VehicleId { get; set; }
        public string NameVehicle { get; set; }
        public int TotalService { get; set; }
        public int TotalMaintenance { get; set; }
        public List<DepartamentDto> AssignedDepartments { get; set; }
        public string Error { get; set; }
    }
}

using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;

namespace Domain.DTOs.Reponses
{
    public class UnrelatedDepartamentDto
    {
        public UnrelatedDepartamentDto()
        {
            AssignedVehicles = new List<DepartmentVehicleDtoo>();
            Supervisors = new List<AdminUserDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public List<DepartmentVehicleDtoo> AssignedVehicles { get; set; }
        public List<AdminUserDto> Supervisors { get; set; }
    }

    public class DepartmentVehicleDtoo
    {
        public int Id { get; set; }
    }


}

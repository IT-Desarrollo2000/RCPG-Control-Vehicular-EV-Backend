using Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Reponses
{
    public class DepartamentDto
    {
        public DepartamentDto()
        {
            AssignedVehicles = new List<DepartmentVehicleDto>();
            Supervisors = new List<AdminUserDto>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual UnrelatedCompanyDto Company { get; set; }
        public List<DepartmentVehicleDto> AssignedVehicles { get; set; }
        public List<AdminUserDto> Supervisors { get; set; }
    }

    public class DepartmentVehicleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public bool IsUtilitary { get; set; }
    }
}

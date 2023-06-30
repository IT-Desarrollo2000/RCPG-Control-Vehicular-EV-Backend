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
            Expenses = new List<ExpensesDto>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public UnrelatedCompanyDto Company { get; set; }
        public List<DepartmentVehicleDto> AssignedVehicles { get; set; }
        public List<AdminUserDto> Supervisors { get; set; }
        public List<ExpensesDto> Expenses { get; set; }
    }

    public class DepartmentVehicleDto
    {
        public int Id { get; set; }
    }

    public class ShortDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}

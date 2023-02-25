using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Reponses
{
    public class DepartamentDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual UnrelatedCompanyDto Company { get; set; }
    }
}

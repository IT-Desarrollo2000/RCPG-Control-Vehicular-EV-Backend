using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Reponses
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string ReasonSocial { get; set; }
        public virtual ICollection<UnrelatedDepartamentDto> Departaments { get; set; }
    }
}

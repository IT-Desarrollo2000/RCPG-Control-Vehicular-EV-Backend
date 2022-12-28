using Domain.Entities.Company;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Reponses
{
    public class DepartamentDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual Companies Company { get; set; }
    }
}

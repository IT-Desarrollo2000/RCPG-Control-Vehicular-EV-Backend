using Domain.Entities.Departament;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string ReasonSocial { get; set; }
        public virtual ICollection<Departaments> Departaments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class UnrelatedDepartamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
    }
}

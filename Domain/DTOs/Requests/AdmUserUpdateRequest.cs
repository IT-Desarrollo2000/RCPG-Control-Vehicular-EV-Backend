using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class AdmUserUpdateRequest
    {
        [Required]
        public int AppUserId { get; set; }
        public string? Name { get; set; }
        public string? LastNameP { get; set; }
        public string? LastNameM { get; set; }
    }
}

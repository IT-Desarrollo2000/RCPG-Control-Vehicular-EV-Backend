using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class WebAdmRolesRequest
    {
        [Required]
        public int UserId { get; set; }
        public IList<AdminRoleType> Roles { get; set; }
    }
}

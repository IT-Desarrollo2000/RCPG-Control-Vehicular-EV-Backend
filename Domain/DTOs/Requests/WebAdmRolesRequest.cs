using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class WebAdmRolesRequest
    {
        [Required]
        public int UserId { get; set; }
        public IList<AdminRoleType> Roles { get; set; }
    }
}

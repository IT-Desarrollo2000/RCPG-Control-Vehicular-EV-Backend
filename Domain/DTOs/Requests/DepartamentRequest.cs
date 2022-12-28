using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class DepartamentRequest
    {
        [Required]
        public string name { get; set; }
        public int CompanyId { get; set; }
    }
}

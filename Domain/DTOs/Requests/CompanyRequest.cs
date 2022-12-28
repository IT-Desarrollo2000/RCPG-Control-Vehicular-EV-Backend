using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class CompanyRequest
    {

        public string Name { get; set; }
        [Required]
        public string ReasonSocial { get; set; }
    }
}

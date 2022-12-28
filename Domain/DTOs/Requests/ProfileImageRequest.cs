using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class ProfileImageRequest
    {
        [Required]
        public int UserProfileId { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }
    }
}

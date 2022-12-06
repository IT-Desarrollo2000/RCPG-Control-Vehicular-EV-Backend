using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

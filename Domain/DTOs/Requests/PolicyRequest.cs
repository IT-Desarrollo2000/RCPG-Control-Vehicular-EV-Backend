using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class PolicyRequest
    {
        public PolicyRequest() 
        {
            Images = new List<IFormFile?>();
        }

        public string? PolicyNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public int? VehicleId { get; set; }
        public List<IFormFile?> Images { get; set; }
    }


    public class PolicyImagesRequest
    {
        public PolicyImagesRequest()
        {
            Images =  new List<IFormFile>();
        }


        [Required]
        public List<IFormFile> Images { get; set; }
    }


}

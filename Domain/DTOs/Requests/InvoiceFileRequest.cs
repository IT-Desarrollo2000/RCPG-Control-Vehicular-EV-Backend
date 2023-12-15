using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class InvoiceFileRequest
    {
        [Required]
        public IFormFile InvoiceFile { get; set; }
        [Required]
        public int VehicleId { get; set; }
    }
}

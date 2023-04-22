using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class PolicyExpenseRequest
    {
        public PolicyExpenseRequest()
        {
            ExpenseAttachments = new List<IFormFile>();
            PolicyAttachments = new List<IFormFile?>();
        }

        [Required]
        public decimal Cost { get; set; }
        [Required]
        public bool Invoiced { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public string PolicyNumber { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }
        [Required]
        public DateTime PolicyExpirationDate { get; set; }
        [Required]
        public string PolicyCompanyName { get; set; }
        public string? Comment { get; set; }
        public int? DepartmentId { get; set; }

        public List<IFormFile> ExpenseAttachments { get; set; }
        public List<IFormFile> PolicyAttachments { get; set; }
    }
}

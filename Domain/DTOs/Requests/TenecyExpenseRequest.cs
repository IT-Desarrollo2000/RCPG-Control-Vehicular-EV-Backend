using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class TenecyExpenseRequest
    {
        public TenecyExpenseRequest() 
        {
            ExpenseAttachments = new List<IFormFile>();
        }

        [Required]
        public decimal Cost { get; set; }
        [Required]
        public bool Invoiced { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }
        public string? Comment { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        public int TenencyYear { get; set; }
        public List<IFormFile> ExpenseAttachments { get; set; }
    }
}

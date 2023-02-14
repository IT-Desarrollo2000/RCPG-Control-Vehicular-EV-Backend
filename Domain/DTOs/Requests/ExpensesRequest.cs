using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class ExpensesRequest
    {
        public ExpensesRequest()
        {
            VehicleIds = new List<int>();
            Attachments = new List<IFormFile>();
        }

        public decimal Cost { get; set; }
        public bool Invoiced { get; set; }
        public List<int> VehicleIds { get; set; }
        public int TypesOfExpensesId { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public int? VehicleReportId { get; set; }

    }

    public class ExpensePhotoRequest
    {
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
        public string? Comment { get; set; }

    }

    public class ExpensePhotoRequest
    {
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}

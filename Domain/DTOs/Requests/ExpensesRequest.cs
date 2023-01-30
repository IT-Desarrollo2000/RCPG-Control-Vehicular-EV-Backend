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

        public decimal Cost { get; set; }
        public int VehicleId { get; set; }
        public int TypesOfExpensesId { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public DateTime ExpenseDate { get; set; }      
        public string ERPFolio { get; set; }
        public List<IFormFile> Attachments { get; set; }

    }

    public class ExpensePhotoRequest
    {
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}

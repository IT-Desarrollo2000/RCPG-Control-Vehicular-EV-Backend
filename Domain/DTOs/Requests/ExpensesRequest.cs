using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
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
        public DateTime ExpenseDate { get; set; }      
        public string MechanicalWorkshop { get; set; }
        public string ERPFolio { get; set; }
       

    }
}

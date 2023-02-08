﻿using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class GetExpensesDto
    {
        public int Id { get; set; }
        public  GetTypesOfExpensesDto TypesOfExpenses { get; set; }
        public decimal Cost { get; set; }
        public DateTime ExpenseDate { get; set; }
        public GetVehicleMaintenanceWorkshopDto VehicleMaintenanceWorkshop { get; set; }
        public string ERPFolio { get; set; }
        //public List<PhotosOfSpending> PhotosOfSpending { get; set; }
    }
}

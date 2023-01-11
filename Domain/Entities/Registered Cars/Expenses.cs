﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class Expenses : BaseEntity
    {
        public int TypesOfExpensesId { get; set; }
        public virtual TypesOfExpenses TypesOfExpenses { get; set; }
        public decimal Cost { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public string MechanicalWorkshop { get; set; }
        public string ERPFolio { get; set; }
        public virtual ICollection<PhotosOfSpending> PhotosOfSpending { get; set; }


    }
}
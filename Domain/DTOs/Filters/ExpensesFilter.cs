﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class ExpensesFilter
    {
        public DateTime? CreatedAfterDate { get; set; }
        public DateTime? CreatedBeforeDate { get; set; }
        public int? TypesOfExpensesId { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int? VehicleId { get; set; }
        public string? MechanicalWorkshop { get; set; }
        public string? ERPFolio { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}